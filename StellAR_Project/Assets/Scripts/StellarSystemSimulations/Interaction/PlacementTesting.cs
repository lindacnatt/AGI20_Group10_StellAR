using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTesting : MonoBehaviour
{
    public GameObject simulationRunner;
    public GameObject objectToPlace;//this is the object that is placed
    
    // Start is called before the first frame update
    void Start()
    {
        objectToPlace.GetComponent<CelestialObject>().enabled = true;
        simulationRunner.GetComponent<TrajectoryLineAnimation>().main = objectToPlace;
        simulationRunner.GetComponent<TrajectorySimulation>().mainObject = objectToPlace;
        SimulationPauseControl.gameIsPaused = true;
        TrajVelTesting.startSlingshot = true;
        TrajVelTesting.start = new Vector3(0f, 0f, 0f);
        TrajectorySimulation.destroyLine = false;
        objectToPlace.GetComponent<SphereCollider>().enabled = true;
        
    }

}
