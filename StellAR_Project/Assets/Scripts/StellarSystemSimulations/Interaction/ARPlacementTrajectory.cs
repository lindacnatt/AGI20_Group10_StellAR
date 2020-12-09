using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlacementTrajectory : MonoBehaviour
{
    public GameObject gameObjectToInstantiate; //this is a reference to the prefab we are placing
    public GameObject simulationRunner;
    public Camera ARCamera; //the camera
    public float distanceFromCamera; //as of now a set distance
    private GameObject objectToPlace;//this is the object that is placed
    private bool placed = false;//of the object is placed or not

    // Start is called before the first frame update
    void Awake()
    {
        //objectToPlace = Instantiate(gameObjectToInstantiate, ARCamera.transform.position + ARCamera.transform.forward*distanceFromCamera, ARCamera.transform.rotation);
        //objectToPlace.AddComponent(typeof(RotationSim));
    }

    void Update(){
        if (objectToPlace != null)
        {
            if (Input.touchCount == 2 && (placed != true))
            {
                placed = true;
                objectToPlace.GetComponent<CelestialObject>().enabled = true;
                simulationRunner.GetComponent<TrajectoryLineAnimation>().main = objectToPlace;
                simulationRunner.GetComponent<TrajectorySimulation>().mainObject = objectToPlace;
                simulationRunner.transform.GetChild(0).gameObject.GetComponent<TrajectoryVelocity>().mainObject = objectToPlace;
                SimulationPauseControl.gameIsPaused = true;
                TrajectoryVelocity.startSlingshot = true;
                TrajectoryVelocity.start = new Vector3(0f, 0f, 0f);
                TrajectorySimulation.destroyLine = false;
                objectToPlace.GetComponent<SphereCollider>().enabled = true;
                objectToPlace.GetComponent<RotationSim>().SetState(true);
                //objectToPlace = null;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectToPlace != null)
        {
            if (placed != true) //while it is not placed it will follow the cameras position
            {
                objectToPlace.transform.position = ARCamera.transform.position + ARCamera.transform.forward * distanceFromCamera;
                //objectToPlace.transform.rotation = new Quaternion(0.0f, ARCamera.transform.rotation.y, 0.0f, ARCamera.transform.rotation.w);
            }
        }
    }

    public void PlaceNextObject(){
        objectToPlace = Instantiate(gameObjectToInstantiate, ARCamera.transform.position + ARCamera.transform.forward*distanceFromCamera, ARCamera.transform.rotation);
        placed = false;
        objectToPlace.AddComponent(typeof(RotationSim));
    }

    public void setGOtoInstantiate(GameObject go)
    {
        gameObjectToInstantiate = go;
        //Debug.Log(go.name);
        SphereCollider collider = gameObjectToInstantiate.GetComponent<SphereCollider>();
        collider.enabled = false;
        objectToPlace = gameObjectToInstantiate;
        objectToPlace.AddComponent(typeof(RotationSim));
        GameObject parent = GameObject.Find("SceneObjects");
        objectToPlace.transform.SetParent(parent.transform);
        
        placed=false;
        //objectToPlace = Instantiate(gameObjectToInstantiate, ARCamera.transform.position + ARCamera.transform.forward * distanceFromCamera, ARCamera.transform.rotation);
        //PlaceNextObject();
    }

}
