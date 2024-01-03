using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PlayerUtilityTool : EditorWindow
{
    //track scroll position in scrolling window
    private Vector2 scrollPosition;

    //search query string
    private string searchQuery = "";

    //list for filtering player objects
    private List<PlayerObject> playerObjects = new List<PlayerObject>();

    private Dictionary<string, List<PlayerObject>> objectsByColor = new Dictionary<string, List<PlayerObject>>();
    private bool showColorFilterOptions = false;

    //create tool accessible from the top menu under window
    [MenuItem("Window/Player Utility Tool")]

    //show sindow method triggered by menuitem above
    public static void ShowWindow()
    {
        //create editor window of this class 
        GetWindow<PlayerUtilityTool>("Player Utility Tool");
    }

    //start with populated list 
    void OnEnable()
    {
        PopulateInitialList();
        GenerateColorFilterOptions();
        Debug.Log("Util list populated");
    }

    //on inpector gui creation
    void OnGUI()
    {
        //add label
        GUILayout.Label("Player Objects in Scene", EditorStyles.boldLabel);

        //search field
        GUILayout.BeginHorizontal();
        GUILayout.Label("Search:", GUILayout.Width(50));
        //set query to text
        searchQuery = EditorGUILayout.TextField(searchQuery);
        //make search button
        if (GUILayout.Button("Search", GUILayout.Width(100)))
        {
            //Filter on press
            FilterPlayerObjects();
        }
        GUILayout.EndHorizontal();
        
        //filter by color button
        if (GUILayout.Button("Filter by Color"))
        {
            //switch to color search option
            showColorFilterOptions = !showColorFilterOptions;
        }

        if (showColorFilterOptions)
        {
            //display colors available and Filter on press 
            DisplayColorOptions();
        }
        else
        {
            //get scroll pos at start
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            //debug var to count
            int objCount = 0;
            //loop to create player objects
            foreach (PlayerObject obj in playerObjects)
            {
                //create button
                Rect rect = EditorGUILayout.BeginHorizontal();
                //EditorGUI.DrawRect(rect, new Color(obj.Color.r, obj.Color.g, obj.Color.b, 0.5f));
                GUILayout.Label(obj.Name, new GUIStyle(GUI.skin.label) { normal = new GUIStyleState { textColor = obj.Color } });
                EditorGUILayout.EndHorizontal();

                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    Selection.activeGameObject = obj.gameObject;
                    Event.current.Use();
                }
                //debug 
                objCount++;
                Debug.Log("player added to list " + objCount);
            }

            EditorGUILayout.EndScrollView();
        }
    }

    //displays list of available color option to choose from
    //when a color option is selected then displays list of corresponding players 
    void DisplayColorOptions()
    {
        //loop for list of color options
        foreach (var colorEntry in objectsByColor)
        {
            //grab color
            Color color;
            ColorUtility.TryParseHtmlString("#" + colorEntry.Key, out color);
            //create colored dolor options
            Rect colorRect = GUILayoutUtility.GetRect(50, 20);
            EditorGUI.DrawRect(colorRect, color);
            if (GUI.Button(colorRect, GUIContent.none, GUIStyle.none))
            {
                //onPress filter players according to option selected
                playerObjects = colorEntry.Value;
                showColorFilterOptions = false;
            }
            EditorGUI.LabelField(colorRect, colorEntry.Key, new GUIStyle(GUI.skin.label) { normal = new GUIStyleState { textColor = Color.white }, alignment = TextAnchor.MiddleCenter });
        }
    }
    //populate player list
    void PopulateInitialList()
    {
        playerObjects.Clear();
        playerObjects.AddRange(FindObjectsOfType<PlayerObject>());
    }

    //filterd name search
  void FilterPlayerObjects()
    {
        playerObjects.Clear();
        string lowerSearchQuery = searchQuery.ToLower();
        
        //loop for list containing name
        foreach (var obj in FindObjectsOfType<PlayerObject>())
        {
            if (obj.Name.ToLower().Contains(lowerSearchQuery))
            {
                playerObjects.Add(obj);
            }
        }
    }

    void GenerateColorFilterOptions()
    {
        objectsByColor.Clear();
        //loop for list containing color
        foreach (PlayerObject obj in FindObjectsOfType<PlayerObject>())
        {
            string colorKey = ColorUtility.ToHtmlStringRGB(obj.Color);

            if (!objectsByColor.ContainsKey(colorKey))
            {
                objectsByColor[colorKey] = new List<PlayerObject>();
            }
            objectsByColor[colorKey].Add(obj);
        }
    }

    // Update the list in real time
    void OnInspectorUpdate()
    {
        Repaint();
    }
   

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
