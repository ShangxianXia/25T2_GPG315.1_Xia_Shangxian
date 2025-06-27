using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class RandomStuffWindow : EditorWindow
{
    // this is for random stuff
    private float scaleValue = 1;
    private float rotationValue = 360;
    
    // this is for object finder script part
    private enum SearchMode { Script, Tag, Name }
    private SearchMode searchMode = SearchMode.Script;
    private string scriptName = "Path";
    private string tagName = "Untagged";
    private string objectName = "";
    
    // this is for the tabs up top to switch between each feature
    private enum WindowTab { Main, Randomiser, ObjectFinder }
    private WindowTab currentTab = WindowTab.Main;
    
    [MenuItem("Window/RandomStuff")]
    public static void ShowWindow()
    {
        GetWindow<RandomStuffWindow>("Random stuff");
    }
    
    void OnGUI()
    {
        currentTab = (WindowTab)GUILayout.Toolbar((int)currentTab, System.Enum.GetNames(typeof(WindowTab)));
        
        switch (currentTab)
        {
            case WindowTab.Main:
                MainTabStuff();
                break;
            case WindowTab.ObjectFinder:
                ObjectFinderStuff();
                break;
            case WindowTab.Randomiser:
                RandomiserStuff();
                break;
        }
    }
    void MainTabStuff()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        // Add any settings you want here
        EditorGUILayout.HelpBox("This is where you would put settings controls.", MessageType.Info);
    }
    
    void RandomiserStuff()
    {
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
        
        // GUILayout.Space(10);
        //
        // GUILayout.Label("Find script stuff");
        // if (GUILayout.Button("Find all objects that have path script"))
        // {
        //     Path[] pathComponents = FindObjectsByType<Path>(FindObjectsSortMode.None);
        //     
        //     List<GameObject> pathObjects = new List<GameObject>();
        //     
        //     foreach (Path path in pathComponents)
        //     {
        //         pathObjects.Add(path.gameObject);
        //     }
        //     
        //     Selection.objects = pathObjects.ToArray();
        // }
    }

    void ObjectFinderStuff()
    {
        if (GUILayout.Button("Close"))
        {
            Close();
        }
        GUILayout.Space(10f);
        
        GUILayout.Label("Object Finder", EditorStyles.boldLabel);
        
        searchMode = (SearchMode)EditorGUILayout.EnumPopup("Search by:", searchMode);
        
        switch (searchMode)
        {
            case SearchMode.Script:
                scriptName = EditorGUILayout.TextField("Script Name:", scriptName);
                break;
                
            case SearchMode.Tag:
                tagName = EditorGUILayout.TagField("Tag:", tagName);
                break;
                
            case SearchMode.Name:
                objectName = EditorGUILayout.TextField("Name Contains:", objectName);
                break;
        }
        
        if (GUILayout.Button("Find Objects"))
        {
            List<GameObject> foundObjects = new List<GameObject>();
            
            switch (searchMode)
            {
                case SearchMode.Script:
                    System.Type type = System.Type.GetType(scriptName + ", Assembly-CSharp");
                    if (type != null)
                    {
                        UnityEngine.Object[] components = Resources.FindObjectsOfTypeAll(type);
                        foreach (UnityEngine.Object component in components)
                        {
                            if (component is MonoBehaviour)
                            {
                                foundObjects.Add(((MonoBehaviour)component).gameObject);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Script not found: " + scriptName);
                    }
                    break;
                    
                case SearchMode.Tag:
                    GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagName);
                    foundObjects.AddRange(taggedObjects);
                    break;
                    
                case SearchMode.Name:
                    GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
                    foreach (GameObject obj in allObjects)
                    {
                        if (obj.name.Contains(objectName))
                        {
                            foundObjects.Add(obj);
                        }
                    }
                    break;
            }
            
            if (foundObjects.Count > 0)
            {
                Selection.objects = foundObjects.ToArray();
                EditorGUIUtility.PingObject(foundObjects[0]);
            }
            else
            {
                Debug.Log("No objects found matching the search criteria.");
            }
        }
    }
}