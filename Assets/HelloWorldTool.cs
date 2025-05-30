using UnityEngine;
using UnityEditor;

public class HelloWorldWindow : EditorWindow
{
    [MenuItem("Window/Hello World")]
    public static void ShowWindow()
    {
        GetWindow<HelloWorldWindow>("Hello World");
    }

    void OnGUI()
    {
        GUILayout.Label("Hello World!", EditorStyles.boldLabel);

        if (GUILayout.Button("Close"))
        {
            Close();
        }
    }
}