using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ArPlacement : MonoBehaviour
{
    public GameObject gameObjectToInstantiate; //this is a reference to the prefab we are placing
    public Camera ARCamera; //the camera
    public float distanceFromCamera; //as of now a set distance
    private GameObject objectToPlace;//this is the object that is placed
    private bool placed = false;//of the object is placed or not


    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //a list of raycasthits

    // Start is called before the first frame update
    void Awake()
    {
        objectToPlace = Instantiate(gameObjectToInstantiate, ARCamera.transform.position + ARCamera.transform.forward*distanceFromCamera, ARCamera.transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
        if (placed != true) //while it is not placed it will follow the cameras position
        {
            objectToPlace.transform.position = ARCamera.transform.position + ARCamera.transform.forward * distanceFromCamera;
            objectToPlace.transform.rotation = new Quaternion(0.0f, ARCamera.transform.rotation.y, 0.0f, ARCamera.transform.rotation.w);
        }
        if (Input.touchCount > 0)
        {
           placed = true; //this places the object and it is locked to the latest position it had. Physics should take it from here.
        }
    }
}
