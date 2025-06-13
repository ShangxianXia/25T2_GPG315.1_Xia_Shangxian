using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class HelloWorldWindow : EditorWindow
{
    [MenuItem("Window/Hello World")]
    public static void ShowWindow()
    {
        GetWindow<HelloWorldWindow>("Hello World");
    }

    void OnGUI()
    {
        // GUILayout.Label("Hello World!", EditorStyles.boldLabel);
        GUILayout.Label("Reset Stuff", EditorStyles.boldLabel);
        if (GUILayout.Button("Close"))
        {
            Close();
        }
        
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
        
        GUILayout.Space(10);
        GUILayout.Label("Rotation Stuff", EditorStyles.boldLabel);
        if (GUILayout.RepeatButton("Randomise Rotation X Axis"))
        {
            // (1, 0, 0)
            Selection.activeTransform.Rotate(Vector3.right, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("Randomise Rotation Y Axis"))
        {
            // (0, 1, 0)
            Selection.activeTransform.Rotate(Vector3.up, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("Randomise Rotation Z Axis"))
        {
            // (0, 0, 1)
            Selection.activeTransform.Rotate(Vector3.forward, Random.Range(0f, 360f));
        }
        
        if (GUILayout.RepeatButton("Randomise Rotation All Axis"))
        {
            // (1, 1, 1)
            Selection.activeTransform.Rotate(Vector3.one, Random.Range(0f, 360f));
        }
        
        GUILayout.Space(10);
        GUILayout.Label("Scale Stuff", EditorStyles.boldLabel);
        if (GUILayout.RepeatButton("Randomise X Scale by 1-10"))
        {
            Selection.activeTransform.localScale = new Vector3(Random.Range(0, 10), 0, 0);
        }
        
        if (GUILayout.RepeatButton("Randomise Y Scale 1-10"))
        {
            Selection.activeTransform.localScale = new Vector3(0, Random.Range(0, 10), 0);
        }
        
        if (GUILayout.RepeatButton("Randomise Z Scale 1-10"))
        {
            Selection.activeTransform.localScale = new Vector3(0, 0, Random.Range(0, 10));
        }
        
        if (GUILayout.RepeatButton("Randomise All Size/Scale 1-10"))
        {
            Selection.activeTransform.localScale = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
        }
        
        GUILayout.Space(10);
        GUILayout.Label("Colour Stuff", EditorStyles.boldLabel);
        if (GUILayout.RepeatButton("Randomise Colour"))
        {
            Selection.activeGameObject.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 10f), Random.Range(0, 10f), Random.Range(0, 10f));
        }
    }
}