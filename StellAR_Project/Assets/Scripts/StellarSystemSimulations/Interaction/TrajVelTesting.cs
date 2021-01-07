using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajVelTesting : MonoBehaviour
{
    public GameObject mainObject;
    //private Rigidbody rb;
    [HideInInspector]
    public static Vector3 direction = new Vector3(0f,0f,0f);
    public SpriteRenderer arrow;
    [HideInInspector]
    public static float magnitude = 4f;


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
        arrow = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        arrow.enabled = false;
        //rb=this.GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {   
        if(mainObject != null){
            if(((SimulationPauseControl.gameIsPaused) && (!TrajectorySimulation.drawLine))){
                
                if (start.magnitude <= 0.001f){
                    start = mainObject.transform.position;
                }
                if(startSlingshot & !TrajectorySimulation.freeze ){
                    if(Input.GetMouseButton(0)){
                    MouseSlingShot();
                    mainObject.transform.position = end;
                    }
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
            //viewDir.SetPosition(i,start-1f*direction*s*i);
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
        arrow.size = new Vector2(0.26f+scaling*0.05f, 0.13f);
        //Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        //Quaternion rotation = Quaternion.LookRotation(direction);
        
        
        //float angleXY = Vector3.Angle(new Vector3(direction.x, 0f,0f),new Vector3(0f, direction.y,0f));
        //float angleZ= Vector3.Angle(new Vector3(direction.x, direction.y,0f),direction);
        //Quaternion rotation = Quaternion.Euler(0f,angleXY,angleZ);

        //float angleYZ = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        //Quaternion rotationX = Quaternion.AngleAxis(angle, transform.right);

        //arrow.gameObject.transform.Rotate(new Vector3(0f,angleXY,angleZ));
        //arrow.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f*Time.deltaTime);
        arrow.gameObject.transform.LookAt(start, Vector3.up);
        

        
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

    void MouseSlingShot(){
        viewDir.enabled = true;
        viewDir.positionCount = vertices;

        Vector2 mousePos = Input.mousePosition;
        end = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5.364094f));
        CheckPositionChange();
        direction = (start-end);
        //direction = (end-start);
        
        
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
