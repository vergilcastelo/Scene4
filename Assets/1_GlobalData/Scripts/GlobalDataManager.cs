using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Global Data Manager
Singlegton for one instace at any time.
Manager for modification of data lists.
Serialized lists for access in inspector.
Corresponding public lists for runtime access. 
*/ 
public class GlobalDataManager : MonoBehaviour
{
    //Singleton instance, access lists via instance
    public static GlobalDataManager Instance;
    //internal private lists
    [SerializeField, Tooltip("List of adjectives.")]
    private List<string> adjectives = new List<string>();

    [SerializeField, Tooltip("List of item names.")]
    private List<string> itemNames = new List<string>();
    
    [SerializeField, Tooltip("List of colors.")]
    private List<string> colors = new List<string>();

    //external public lists with getters and setters
    public static List<string> AdjectivesList { get; private set; } = new List<string>();
    public static List<string> ItemNamesList { get; private set; } = new List<string>();
    public static List<string> ColorsList { get; private set; } = new List<string>();


    //Awake called to init variables before application starts 
    private void Awake()
    {
        Debug.Log("on awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PopulateLists();
        }else
        {
            Destroy(gameObject);
        }
    }

    //Generic method to initialize any list
    private static void InitializeList(List<string> targetList, List<string> sourceList)
    {
        targetList.Clear();
        foreach (string item in sourceList)
        {
            targetList.Add(item);
        }
    }

    //PopulateLists method initialize list if empty
    public static void PopulateLists()
    {
        //Set all lists
        if(Instance != null){
            InitializeList(AdjectivesList, Instance.adjectives);
            Debug.Log("Adj Length " + AdjectivesList.Count);
            InitializeList(ItemNamesList, Instance.itemNames);
            InitializeList(ColorsList, Instance.colors);
        }

        Debug.Log("List are populated");
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
