using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVelocity : MonoBehaviour
{

    public GameObject mainObject;
    [HideInInspector]
    public static Vector3 direction = new Vector3(0f,0f,0f);
    [HideInInspector]
    public static float magnitude = 5f;

    public LineRenderer viewDir;
    
    public float scaling;
    public GameObject simulation;

    [HideInInspector]
    public TrajectorySimulation ts;

    [HideInInspector]
    public int vertices;


    // Start is called before the first frame update
    void Start()
    {   
        vertices = 20;
        this.GetComponent<LineRenderer>().enabled = false;
        ts=(TrajectorySimulation) simulation.GetComponent<TrajectorySimulation>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(((SimulationPauseControl.gameIsPaused) && (!TrajectorySimulation.drawLine))){
            viewDir.enabled = true;
            viewDir.positionCount = vertices;
            direction = Camera.main.transform.forward;
            DrawDirection();
            ts.initialVelocity = direction*magnitude;
            print(!TrajectorySimulation.drawLine);
        }
        else{
            viewDir.positionCount = 0;
            viewDir.enabled = false;
        }
        
    }

    void DrawDirection(){
        Vector3 start=mainObject.transform.position;
        float s = scaling / (float) viewDir.positionCount;
        for (int i=0; i<viewDir.positionCount; i++){
            viewDir.SetPosition(i,start+direction*s*i);
        }
    }

}
