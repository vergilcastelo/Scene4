using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Origin Shifter class
- references both prefab componets 
- spawns 2nd aircraft on spacebar press
- 2nd aircraft spawns at the same speed as the 1st
- shift between the 2 aircrafts xrOrigin on enter
*/
public class OriginShifter : MonoBehaviour
{
    //reference both aircraft prefabs in editor
    public GameObject planeOnePrefab;
    public GameObject planeTwoPrefab;

    //for instantiation of 2nd aircraft when spawning
    private GameObject planeTwoInstance;

    //for individual rigs 
    private GameObject xrRigOnPlaneOne;
    private GameObject xrRigOnPlaneTwo;
    

    // Start is called before the first frame update
    void Start()
    {
        //get aircraftOne's rig
          xrRigOnPlaneOne = GetXRRig(planeOnePrefab);

    }

    // Update is called once per frame
    void Update()
    {
        //add inputs spacebar and enter
          if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlaneTwo();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ToggleXRRigs();
        }

        //maintain speed on spawn
        if (planeTwoInstance != null)
        {
            // Ensure planeTwo travels at the same speed as planeOne
            planeTwoInstance.GetComponent<AircraftMovement>().speed = 
                planeOnePrefab.GetComponent<AircraftMovement>().speed;
        }
    }

    //space bar spawn method
     void SpawnPlaneTwo()
    {
        //check for plane two
        if (planeTwoInstance == null)
        {
            //get current plane one location and add 10m
            Vector3 spawnPosition = planeOnePrefab.transform.position + planeOnePrefab.transform.forward * 10f;
            //instantiate our prefab
            planeTwoInstance = Instantiate(planeTwoPrefab, spawnPosition, planeOnePrefab.transform.rotation);

            xrRigOnPlaneTwo = GetXRRig(planeTwoInstance);

            if (xrRigOnPlaneTwo == null)
            {
                Debug.LogError("XR Rig on Plane Two is not found!");
            }
            else
            {
                // Initially disable the XR Rig on planeTwo
                xrRigOnPlaneTwo.SetActive(false);
            }
        }
    }

    //origin shift toggle method
    void  ToggleXRRigs()
    {
        //make sure we got two planes
          if (xrRigOnPlaneOne != null && xrRigOnPlaneTwo != null)
        {
            if (xrRigOnPlaneOne.activeSelf)
        {
            // Deactivate XR Rig on Plane One and activate XR Rig on Plane Two
            xrRigOnPlaneOne.SetActive(false);
            xrRigOnPlaneTwo.SetActive(true);
          
        }
        else
        {
            // Activate XR Rig on Plane One and deactivate XR Rig on Plane Two
            xrRigOnPlaneOne.SetActive(true);
            xrRigOnPlaneTwo.SetActive(false);
   
        }
        }
    }

    //get origin rig return it (change return type)
    GameObject GetXRRig(GameObject plane)
    {
        foreach (Transform child in plane.transform)
        {
        // Check for XR Rig by name or a unique component it might have
        // For example, if the XR Rig is named "XRRig", you can check the name
            if (child.name == "XR Origin (XR Rig)")
            {
                return child.gameObject;
            }
        }
        return null;
        
    }

}
