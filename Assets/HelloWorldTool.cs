using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class RandomStuffWindow : EditorWindow
{
    private float scaleValue = 1;
    
    private float rotationValue = 360;
    
    [MenuItem("Window/RandomStuff")]
    public static void ShowWindow()
    {
        GetWindow<RandomStuffWindow>("Random stuff");
    }
    
    void OnGUI()
    {
        // GUILayout.Label("Hello World!", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Close"))
        {
            Close();
        }
        
        GUILayout.Space(10f);
        GUILayout.Label("Reset Stuff", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset all"))
        {
            Selection.activeTransform.transform.position = Vector3.zero;
            Selection.activeTransform.transform.rotation = Quaternion.identity;
            Selection.activeGameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            Selection.activeTransform.transform.localScale = Vector3.one;
        }

        if (GUILayout.Button("Reset position"))
        {
            Selection.activeTransform.transform.position = Vector3.zero;
            Selection.activeTransform.transform.rotation = Quaternion.identity;
        }
        
        if (GUILayout.Button("Reset scale"))
        {
            Selection.activeTransform.transform.localScale = Vector3.one;
        }

        if (GUILayout.Button("Reset colour"))
        {
            Selection.activeGameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        GUILayout.EndHorizontal();
        
        
        GUILayout.Space(10);
        GUILayout.Label($"Randomise Rotation Stuff by > {rotationValue} < degrees", EditorStyles.boldLabel);
        rotationValue = GUILayout.HorizontalSlider(rotationValue, 0, 360f);
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("X Axis"))        
        {
            // (1, 0, 0)
            Selection.activeTransform.Rotate(Vector3.right, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("Y Axis"))
        {
            // (0, 1, 0)
            Selection.activeTransform.Rotate(Vector3.up, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("Z Axis"))
        {
            // (0, 0, 1)
            Selection.activeTransform.Rotate(Vector3.forward, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("All Axis"))
        {
            // (1, 1, 1)
            Selection.activeTransform.Rotate(Vector3.one, Random.Range(0f, 360f));
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        GUILayout.Label($"Randomise Scale Stuff by > {scaleValue} <", EditorStyles.boldLabel);
        scaleValue = GUILayout.HorizontalSlider(scaleValue, 1, 100);
        GUILayout.Space(15f);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("X Scale"))
        {
            Selection.activeTransform.localScale = new Vector3(Random.Range(0, scaleValue), 0, 0);
        }
        
        if (GUILayout.RepeatButton("Y Scale"))
        {
            Selection.activeTransform.localScale = new Vector3(0, Random.Range(0, scaleValue), 0);
        }
        
        if (GUILayout.RepeatButton("Z Scale"))
        {
            Selection.activeTransform.localScale = new Vector3(0, 0, Random.Range(0, scaleValue));
        }
        
        if (GUILayout.RepeatButton("All Size/Scale"))
        {
            Selection.activeTransform.localScale = new Vector3(Random.Range(0, scaleValue), Random.Range(0, scaleValue), Random.Range(0, scaleValue));
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        GUILayout.Label("Colour Stuff", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("Randomise Colour"))
        {
            Selection.activeGameObject.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 10f), Random.Range(0, 10f), Random.Range(0, 10f));
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);

        
    }
}