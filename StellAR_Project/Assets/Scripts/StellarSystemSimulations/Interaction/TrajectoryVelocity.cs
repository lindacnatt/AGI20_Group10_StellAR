using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVelocity : MonoBehaviour
{

    public GameObject mainObject;
    //private Rigidbody rb;
    [HideInInspector]
    public static Vector3 direction = new Vector3(0f,0f,0f);
    public SpriteRenderer arrow;
    [HideInInspector]
    public static float magnitude = 4f*0.5f; //*0.1 for slow, *0.5 for medium and *1.0 for fast


    public LineRenderer viewDir;
    
    public float scaling;

    [HideInInspector]
    public int vertices;

    [HideInInspector]
    public static Vector3 start = new Vector3(0f,0f,0f);

    [HideInInspector]
    public Vector3 end = new Vector3(0f,0f,0f);
    [HideInInspector]
    public Vector3 oldEnd = new Vector3(1f,1f,1f);


    [HideInInspector]
    public GameObject cam;
    private float offset;

    public static bool startSlingshot;


    // Start is called before the first frame update
    void Start()
    {   
        vertices = 20;
        this.GetComponent<LineRenderer>().enabled = false;
        if (this.transform.childCount > 0)
        {
            arrow = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
            arrow.enabled = false;
        }
        //rb=this.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {   
        if(mainObject != null){
            if(((SimulationPauseControl.gameIsPaused) && (!TrajectorySimulation.drawLine))){
                //ViewDirection();
                
                if (start.magnitude <= 0.001f){
                    start = mainObject.transform.position;
                }
                if(startSlingshot & !TrajectorySimulation.freeze ){
                    SlingShot();
                    mainObject.transform.position = end;
                }
                else if (!startSlingshot & !TrajectorySimulation.freeze){
                    mainObject.transform.position = start;
                }

                else{
                    viewDir.enabled = false;
                }
                
            }
            else{
                viewDir.positionCount = 0;
                viewDir.enabled = false;
                start = new Vector3(0f,0f,0f);
                startSlingshot = false;
                arrow.enabled=false;
                arrow.size=new Vector2(0f,0f);

            }
        }
        
        if(TrajectorySimulation.destroyLine){
            mainObject = null;
            viewDir.positionCount = 0;
            viewDir.enabled = false;
            start = new Vector3(0f,0f,0f);
            arrow.enabled=false;
            arrow.size=new Vector2(0f,0f);
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

    void DrawDirectionSprite(){
        float scaling = direction.magnitude-0.26f;
        scaling = scaling < 0.0f ? 0.0f : scaling;
        arrow.size = new Vector2(0.26f+scaling, 0.13f);
        //Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
        arrow.gameObject.transform.rotation = rotation;
        //arrow.gameObject.transform.LookAt(start, new Vector3(0, 0, -1));
    }

    void ViewDirection(){
        viewDir.enabled = true;
        viewDir.positionCount = vertices;
        direction = Camera.main.transform.forward;
        DrawDirection(scaling, mainObject.transform.position);
        direction = direction*magnitude;
    }

    void SlingShot(){
            viewDir.enabled = true;
            viewDir.positionCount = vertices;
            end = Camera.main.transform.position + 2.0f*Camera.main.transform.forward;//cam.transform.position+2.0f*cam.transform.forward;
            CheckPositionChange();
            direction = (start-end);
            
            
            //arrow.transform.position=end;
            //arrow.enabled = true;
            
            
            DrawDirection(end);
            //DrawDirectionSprite();
            direction *= magnitude;
    }

    public void ToggleSlingShot(){
        startSlingshot = !startSlingshot;
    }

    public void CheckPositionChange(){
        float change = (end - oldEnd).magnitude;
        if(change > 0.03){  
            oldEnd = end;
        }
        else{
           end = oldEnd;
        }
    }

}
