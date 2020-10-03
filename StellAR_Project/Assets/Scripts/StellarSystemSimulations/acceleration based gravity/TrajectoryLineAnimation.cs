using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLineAnimation : MonoBehaviour
{
    public LineRenderer traj;
    private Rigidbody rb;
    private int count = 0;
    public GameObject main;
    private int length; 

    // Start is called before the first frame update

  
    void Start()
    {
        traj = GetComponent<LineRenderer>();
        rb = (Rigidbody) main.GetComponent(typeof(Rigidbody));
        length=TrajectorySimulation.linePositions.Length;
        
    }

    // Update is called once per frame

    public void DrawLine(){
        count=0;
        traj.positionCount = length;
        int index=0;
        foreach(Vector3 pos in TrajectorySimulation.linePositions){
            traj.SetPosition(index,pos);
            index++;
        }
        TrajectorySimulation.drawLine=false;


    }

    void Update(){
        if(TrajectorySimulation.drawLine){
            DrawLine();
        }
    }

    void FixedUpdate()
    { 
        if(!SimulationPauseControl.gameIsPaused){
            if(TrajectorySimulation.destroyLine){
                
                Vector3 linePos =TrajectorySimulation.linePositions[length-1-count];
                float distance = (rb.position-linePos).magnitude;
            
                if(count < length ){
                    if(distance <= 1.0f){
                            traj.positionCount -=1;
                            count++;
                        }
                }
                else
                    {
                        TrajectorySimulation.destroyLine = false;
                        //traj.positionCount=0;
                        count = 0;
                    }

            }
            else{
                traj.positionCount=0;
            }
            
        }

    }
}
