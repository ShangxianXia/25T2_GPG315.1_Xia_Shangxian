using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class RandomStuffTool : EditorWindow
{
    // this is for random stuff
    private float scaleValue = 1;
    private float rotationValue = 360;
    
    // this is for object finder script part
    private enum SearchMode { Script, Tag, Name }
    private SearchMode searchMode = SearchMode.Script;
    private string scriptName = "Enter a script name";
    private string tagName = "Untagged";
    private string objectName = "Enter a object name";
    
    // this is for the tabs up top to switch between each feature
    private enum WindowTab { MainInfo, Randomiser, ReferenceFinder }
    private WindowTab currentTab = WindowTab.MainInfo;
    
    [MenuItem("Window/RandomStuffTool")]
    public static void ShowWindow()
    {
        GetWindow<RandomStuffTool>("Random stuff tool");
    }
    
    void OnGUI()
    {
        currentTab = (WindowTab)GUILayout.Toolbar((int)currentTab, System.Enum.GetNames(typeof(WindowTab)));
        
        switch (currentTab)
        {
            case WindowTab.MainInfo:
                MainInfoTabStuff();
                break;
            case WindowTab.ReferenceFinder:
                ObjectFinderStuff();
                break;
            case WindowTab.Randomiser:
                RandomiserStuff();
                break;
        }
    }
    void MainInfoTabStuff()
    {
        // custom style
        GUIStyle myOwnStyleForAHeader = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            border = new RectOffset(10, 10, 10, 10)
        };
        
        GUILayout.Label("This is the Random Stuff Tool,\n to begin select the tabs up top that correspond to the feature!,\n Orrrrr press the buttons on the bottom :D, either works!", myOwnStyleForAHeader);

        if (GUILayout.Button("Randomiser?", GUILayout.Height(30f)))
        {
            currentTab = WindowTab.Randomiser;
        }
        if (GUILayout.Button("Reference finder?", GUILayout.Height(30f)))
        {
            currentTab = WindowTab.ReferenceFinder;
        }
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
            Undo.RecordObjects(Selection.transforms, "Reset all");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
                var renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null) renderer.material.color = Color.white;
                obj.transform.localScale = Vector3.one;
            }
        }

        if (GUILayout.Button("Reset position"))
        {
            Undo.RecordObjects(Selection.transforms, "Reset position");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.position = Vector3.zero;
            }
        }
        
        if (GUILayout.Button("Reset rotation"))
        {
            Undo.RecordObjects(Selection.transforms, "Reset rotation");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.rotation = Quaternion.identity;
            }
        }
    
        if (GUILayout.Button("Reset scale"))
        {
            Undo.RecordObjects(Selection.transforms, "Reset scale");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.localScale = Vector3.one;
            }
        }

        if (GUILayout.Button("Reset colour"))
        {
            Undo.RecordObjects(Selection.gameObjects.SelectMany(o => o.GetComponents<MeshRenderer>()).ToArray(), "Reset color");
            foreach (GameObject obj in Selection.gameObjects)
            {
                var renderer = obj.GetComponent<MeshRenderer>();
                if (renderer != null) renderer.material.color = Color.white;
            }
        }
        GUILayout.EndHorizontal();
    
    
        GUILayout.Space(10);
    
    
        GUILayout.Label($"Randomise Rotation Stuff by > {rotationValue} < degrees", EditorStyles.boldLabel);
        rotationValue = GUILayout.HorizontalSlider(rotationValue, 0, 360f);
        GUILayout.Space(15);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("X Axis"))        
        {
            Undo.RecordObjects(Selection.transforms, "Random X Rotation");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.Rotate(Vector3.right, Random.Range(0f, rotationValue));
            }
        }
    
        if (GUILayout.RepeatButton("Y Axis"))
        {
            Undo.RecordObjects(Selection.transforms, "Random Y Rotation");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.Rotate(Vector3.up, Random.Range(0f, rotationValue));
            }
        }
    
        if (GUILayout.RepeatButton("Z Axis"))
        {
            Undo.RecordObjects(Selection.transforms, "Random Z Rotation");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.Rotate(Vector3.forward, Random.Range(0f, rotationValue));
            }
        }
    
        if (GUILayout.RepeatButton("All Axis"))
        {
            Undo.RecordObjects(Selection.transforms, "Random All Rotation");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.Rotate(new Vector3(Random.Range(0f, rotationValue), Random.Range(0f, rotationValue), Random.Range(0f, rotationValue)));
            }
        }
        GUILayout.EndHorizontal();
    
        GUILayout.Space(10);
    
    
        GUILayout.Label($"Randomise Scale Stuff by > {scaleValue} <", EditorStyles.boldLabel);
        scaleValue = GUILayout.HorizontalSlider(scaleValue, 1, 100);
        GUILayout.Space(15f);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("X Scale"))
        {
            Undo.RecordObjects(Selection.transforms, "Random X Scale");
            foreach (GameObject obj in Selection.gameObjects)
            {
                Vector3 currentScale = obj.transform.localScale;
                obj.transform.localScale = new Vector3(Random.Range(0, scaleValue), currentScale.y, currentScale.z);
            }
        }
    
        if (GUILayout.RepeatButton("Y Scale"))
        {
            Undo.RecordObjects(Selection.transforms, "Random Y Scale");
            foreach (GameObject obj in Selection.gameObjects)
            {
                Vector3 currentScale = obj.transform.localScale;
                obj.transform.localScale = new Vector3(currentScale.x, Random.Range(0, scaleValue), currentScale.z);
            }
        }
    
        if (GUILayout.RepeatButton("Z Scale"))
        {
            Undo.RecordObjects(Selection.transforms, "Random Z Scale");
            foreach (GameObject obj in Selection.gameObjects)
            {
                Vector3 currentScale = obj.transform.localScale;
                obj.transform.localScale = new Vector3(currentScale.x, currentScale.y, Random.Range(0, scaleValue));
            }
        }
    
        if (GUILayout.RepeatButton("All Size/Scale"))
        {
            Undo.RecordObjects(Selection.transforms, "Random All Scale");
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.transform.localScale = new Vector3(Random.Range(0, scaleValue), Random.Range(0, scaleValue), Random.Range(0, scaleValue));
            }
        }
        GUILayout.EndHorizontal();
    
        GUILayout.Space(10);
    
    
        GUILayout.Label("Colour Stuff", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.RepeatButton("Randomise All Colour"))
        {
            var renderers = Selection.gameObjects.SelectMany(o => o.GetComponents<MeshRenderer>()).ToArray();
        
            Undo.RecordObjects(renderers, "Randomized all Color");
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material.color = new Color(Random.Range(0f, 5f),Random.Range(0f, 5f),Random.Range(0f, 5f));
            }
        }
        
        if (GUILayout.Button("Randomise Red Colour"))
        {
            var renderers = Selection.gameObjects.SelectMany(o => o.GetComponents<MeshRenderer>()).ToArray();
        
            Undo.RecordObjects(renderers, "Randomized red Color");
            foreach (MeshRenderer renderer in renderers)
            {
                Color original = renderer.material.color;
                renderer.material.color = new Color(Random.Range(0f, 5f), original.g ,original.b);
            }
        }   
        
        if (GUILayout.Button("Randomise Green Colour"))
        {
            var renderers = Selection.gameObjects.SelectMany(o => o.GetComponents<MeshRenderer>()).ToArray();
        
            Undo.RecordObjects(renderers, "Randomized green Color");
            foreach (MeshRenderer renderer in renderers)
            {
                Color original = renderer.material.color;
                renderer.material.color = new Color(original.r, Random.Range(0f, 5f) ,original.b);
            }
        }
        
        if (GUILayout.Button("Randomise Blue Colour"))
        {
            var renderers = Selection.gameObjects.SelectMany(o => o.GetComponents<MeshRenderer>()).ToArray();
        
            Undo.RecordObjects(renderers, "Randomized blue Color");
            foreach (MeshRenderer renderer in renderers)
            {
                Color original = renderer.material.color;
                renderer.material.color = new Color(original.r, original.g ,Random.Range(0f, 5f));
            }
        }
        GUILayout.EndHorizontal();
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
                        Object[] components = Resources.FindObjectsOfTypeAll(type);
                        foreach (Object component in components)
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
                    GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
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