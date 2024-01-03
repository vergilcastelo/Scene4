using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Game Manager
creates multiple player objects
initializes player objects by calling generate method on each
creates editor tool to manage player objects
*/
public class GameManager : MonoBehaviour
{
     //access our PlayerObject prefab
     public GameObject playerObjectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //create 19 more player objects
         for (int i = 0; i < 20; i++)
        {
            //create GO to init player prefab
            GameObject newObj = Instantiate(playerObjectPrefab);
            //set prefab to new PlayerObject
            PlayerObject player = newObj.GetComponent<PlayerObject>();
            //call generate on new player
            player.Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
