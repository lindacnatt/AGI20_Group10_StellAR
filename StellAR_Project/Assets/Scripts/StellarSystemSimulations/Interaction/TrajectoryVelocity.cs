using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVelocity : MonoBehaviour
{

    public GameObject mainObject;
    [HideInInspector]
    public static Vector3 direction = new Vector3(0f,0f,0f);
    [HideInInspector]
    public static float magnitude = 4f;

    public LineRenderer viewDir;
    
    public float scaling;
    public GameObject simulation;

    [HideInInspector]
    public int vertices;

    [HideInInspector]
    public Vector3 start = new Vector3(0f,0f,0f);

    [HideInInspector]
    public Vector3 end = new Vector3(0f,0f,0f);


    [HideInInspector]
    public GameObject cam;
    private float offset;

    public static bool startSlingshot;


    // Start is called before the first frame update
    void Start()
    {   
        vertices = 20;
        this.GetComponent<LineRenderer>().enabled = false;


        
    }

    // Update is called once per frame
    void Update()
    {   
        if(mainObject != null){
            if(((SimulationPauseControl.gameIsPaused) && (!TrajectorySimulation.drawLine))){
                //ViewDirection();
                
                if (start.magnitude <= 0.01f){
                    start = mainObject.transform.position;
                }
                if(startSlingshot){
                    SlingShot();
                    mainObject.transform.position = end;
                }
                else{
                    mainObject.transform.position = start;
                }
                
            }
            else{
                viewDir.positionCount = 0;
                viewDir.enabled = false;
                start = new Vector3(0f,0f,0f);
                startSlingshot = false;

            }
        }
        else{
            viewDir.positionCount = 0;
            viewDir.enabled = false;
            start = new Vector3(0f,0f,0f);
        }
        
        if(TrajectorySimulation.destroyLine){
            mainObject = null;
        }
        
    }

    void DrawDirection(float scaling, Vector3 start){
        float s = scaling / (float) viewDir.positionCount;
        for (int i=0; i<viewDir.positionCount; i++){
            viewDir.SetPosition(i,start+direction*s*i);
        }
    }

    void DrawDirection(Vector3 end){
        float s = direction.magnitude / (float) viewDir.positionCount;
        for (int i=0; i<viewDir.positionCount; i++){
            viewDir.SetPosition(i,end+direction*s*i);
        }
    }

    void ViewDirection(){
        viewDir.enabled = true;
        viewDir.positionCount = vertices;
        direction = Camera.main.transform.forward;
        DrawDirection(scaling, mainObject.transform.position);
        direction = direction*magnitude;
    }

    void SlingShot(){
            //Touch touch = Input.GetTouch(0);
            //Vector3 end = touch.position;
            //Vector3 start = mainObject.transform.position;
            viewDir.enabled = true;
            viewDir.positionCount = vertices;
            end = Camera.main.transform.position + 2.0f*Camera.main.transform.forward;//cam.transform.position+2.0f*cam.transform.forward;
            direction = (start-end);
            DrawDirection(end);
            direction *= magnitude;
    }

    public void ToggleSlingShot(){
        startSlingshot = !startSlingshot;
    }

}
