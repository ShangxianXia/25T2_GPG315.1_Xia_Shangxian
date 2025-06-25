using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    private void OnSceneGUI()
    {
        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        
        Path pathRef = target as Path;
        Handles.color = Color.green;
        for (int i = 0; i < pathRef.points.Count - 1; i++)
        {
            Handles.DrawDottedLine(pathRef.points[i], pathRef.points[i + 1], 4f);
            GUI.color = Color.cyan;
            Handles.Label((pathRef.points[i] + pathRef.points[i + 1]) / 2f , Vector3.Distance(pathRef.points[i], pathRef.points[i + 1]).ToString());
        }
        Undo.RecordObject(pathRef, "Path waypoints");
        for (int i = 0; i < pathRef.points.Count; i++)
        {
            pathRef.points[i] = Handles.PositionHandle(pathRef.points[i], Quaternion.identity);
        }

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Vector2 mousePos = Event.current.mousePosition;
            Vector3 mouseWaypointPosition;
            Vector3 mouseWaypointNormal;
            if(HandleUtility.PlaceObject(mousePos, out mouseWaypointPosition, out mouseWaypointNormal))
            {
                pathRef.points.Add(mouseWaypointPosition);
            }
        }
    }
}
