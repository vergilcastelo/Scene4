using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for Editor tools
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
Player Object
generates it own public data Name and Color
data based upon lists in GlobalDataManager
renames itself to lowercase Name data
*/
public class PlayerObject : MonoBehaviour
{
    //Name var proper case, unique string from AdjectivesList and ItemNamesList
    public string Name;

    //Color var chosen from ColorsList 
    public Color Color;

    //Generate method used to populate our player data in editor only    
     public void Generate()
    {
        //make sure list are populated
        if(GlobalDataManager.AdjectivesList.Count < 1)
        {
            GlobalDataManager.PopulateLists();
        }
        
        //local vars taken from AdjectivesList and ItemNamesList randomly
        Debug.Log(GlobalDataManager.AdjectivesList.Count);
        string adjective = GlobalDataManager.AdjectivesList[Random.Range(0, GlobalDataManager.AdjectivesList.Count)];
        string itemName = GlobalDataManager.ItemNamesList[Random.Range(0, GlobalDataManager.ItemNamesList.Count)];
        
        //Set Name to combination of local vars
        Name = adjective + " " + itemName;

        //log it to check 
        Debug.Log(Name);

        //Set Color from ColorsList randomly
        string colorName = GlobalDataManager.ColorsList[Random.Range(0, GlobalDataManager.ColorsList.Count)];

        //use utility to convert string into Color type
        ColorUtility.TryParseHtmlString(colorName, out Color);

        //log Color to check
        Debug.Log(Color);

        //Rename the GO to Name using lower camel case
        gameObject.name = adjective.ToLower() + itemName;

        //log gameObject.name
        Debug.Log(gameObject.name);
    }

}
//condition: in Unity IDE
#if UNITY_EDITOR
//custom inwpector window for this class
[CustomEditor(typeof(PlayerObject))]
//define class
public class PlayerObjectEditor : Editor
{
    //override default functionality of base Editor class
    public override void OnInspectorGUI()
    {
        //function auto draws a default inspector window 
        //used to add button not have to redo entire inspector
        DrawDefaultInspector();
        
        //target this script
        PlayerObject script = (PlayerObject)target;

        //automatic gui layout 
        //add button
        if(GUILayout.Button("Generate"))
        {
            //call generate on press
            script.Generate();
        }
    }

}
#endif

