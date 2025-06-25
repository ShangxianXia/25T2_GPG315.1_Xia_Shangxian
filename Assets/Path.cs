using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();
    public float progress;
    public int wayPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progress = 0;
        wayPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints < points.Count - 1)
        {
            transform.position = Vector3.Lerp(points[wayPoints], points[wayPoints + 1], progress);
            progress += Time.deltaTime;
            if (progress > 1f)
            {
                progress = 0;
                wayPoints++;
                transform.position = points[wayPoints];
            }
        }
    }
}
