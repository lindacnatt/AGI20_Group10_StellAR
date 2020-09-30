using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySimulation : MonoBehaviour
{
    
    public GameObject mainObject;
    //private Vector3 mainPos;
    //private float mainMass;

    public Vector3 initialVelocity;

    public int lineVertices;

    public LineRenderer traj;

    private Vector3[] velosities;
    private Vector3[] positions;
    private float[] massess;

    private float[] radius;

    private bool[] dead;

    private bool[] isStatic;

    private int length;

    private float timer;

    public static bool isDone = false;

    Vector3[] prel_acc;

    private bool startSimulation;

    float timeDelta;

  void Awake(){
      Time.fixedDeltaTime = 0.01f;
  } 

  void Start()
    {
        traj = GetComponent<LineRenderer>();
        
    }

    void CopyObjects(){
        length=CelestialObject.Objects.Count;
        velosities=new Vector3[length];
        positions=new Vector3[length];
        massess=new float[length];
        radius=new float[length];
        dead=new bool[length];
        isStatic=new bool[length];

        Transform T = (Transform) mainObject.GetComponent(typeof(Transform));
        Rigidbody rb = (Rigidbody) mainObject.GetComponent(typeof(Rigidbody));
        positions[0] = rb.position;
        massess[0] = rb.mass;
        //velosities[0] = rb.velocity;
        radius[0] = T.localScale.x;

        CelestialObject a = (CelestialObject) mainObject.GetComponent(typeof(CelestialObject));
        if(a.staticBody)
            isStatic[0]=true;
        else{
            velosities[0] = a.pausedVelocity;
        }

        int count = 1;
        for(int i=0; i<length; i++){
            GameObject go = CelestialObject.Objects[i].gameObject;
            if(go == mainObject){
                continue; }
            else {
            Transform Ti = (Transform) go.GetComponent(typeof(Transform));
            Rigidbody rbi = (Rigidbody) go.GetComponent(typeof(Rigidbody));
            positions[count] = rbi.position;
            massess[count] = rbi.mass;
            //velosities[count] = rbi.velocity;
            radius[count] = Ti.localScale.x;
            CelestialObject ai = (CelestialObject) go.GetComponent(typeof(CelestialObject));
            if(ai.staticBody)
                isStatic[count]=true;
            else
                velosities[count] = ai.pausedVelocity;
            count++;
            }
        }
    }

    void CalcNextVelo(int index, Vector3 new_pos, float timeStep){
        velosities[index]=(new_pos-positions[index])/timeStep;
    }

    void CalcNextVeloWithAcc(int index, Vector3 acc, float timeStep){
        velosities[index]+=acc*timeStep;
    }

     void CalcVeloStart(){
        velosities[0] = velosities[0] + initialVelocity;
    }

    Vector3 CalcAcc(int index){
        

        Vector3 dirResultant = new Vector3(0f,0f,0f);
        
        for (int i=0; i<length; i++){
            if(i!=index && !dead[i]){
                float distance = (positions[i]-positions[index]).magnitude;
                Vector3 direction = (positions[i]-positions[index]).normalized;
                dirResultant += NBodyPhysics.gravityConstant*direction*massess[i]/Mathf.Pow(distance,2.0f);
            }    
        }
        return dirResultant;
    }

    Vector3 CalcPos(int index, float timeStep, Vector3 acc){
        return positions[index] +velosities[index]*timeStep + acc*Mathf.Pow(timeStep,2.0f)/2.0f; 
    }

     Vector3 CalcPos(int index, float timeStep){
        return positions[index]+velosities[index]*timeStep; 
    }

    void CheckBoundary(int index){
        Vector3 firstPos = positions[index];
        bool firstDead = false;
        for(int i=0; i<length; i++){
            if((i!=index)&&(!dead[i])){
                Vector3 secondPos = positions[i]; 
    
                float distance = (secondPos-firstPos).magnitude;
                float radiusDistance= radius[index]+radius[i];
                
                if(distance <= radiusDistance*0.5f){ 
                    if(!isStatic[i]){
                        dead[i] = true;
                        firstDead = true;}
                    else{
                        firstDead = true;
                    }
                }
            }
        }
        if(firstDead)
            dead[index]=true;
    }
        

    // Update is called once per frame
    void Update()
    {
        if (mainObject != null) //wont run if there is nothing to place, i.e. nothing to show trajectory of.
        {
            if (SimulationPauseControl.gameIsPaused)
            {
                // if (Input.GetKeyDown(KeyCode.T) ){ 
                gameObject.GetComponent<LineRenderer>().enabled = true;
                CalcTrajectory(Time.fixedDeltaTime);


                //}
                //if(Input.GetKeyDown(KeyCode.Return)){ 

                //    SimulationPauseControl.gameIsPaused = false;

                //}
            }
            if (isDone) //when the trajectory is mapped out and beautiful...
            {
                SetInitialVel(); //...the object being placed gets its initial velocity (right now the amount of velocity is set in ArPlacement) and...
                isDone = !isDone;
                mainObject = null; /*...we placed it so now we are ready to do something other than placing, 
                                   also makes sure that user does not add additional velocities when pausing the game after placing planet*/

            }
        }
    }


    void CalcTrajectory(float time){
        CopyObjects();
        CalcVeloStart();
        traj.positionCount = lineVertices;
        int count=0;
        for(int j=0; j<traj.positionCount; j++){
            prel_acc = new Vector3[length];
            
            traj.SetPosition(count, positions[0]);
            count++;
                
            for(int i=0; i<length; i++){
                if((!isStatic[i]) && (!dead[i])){
                        prel_acc[i]=CalcAcc(i);
                
                }
            }
             for(int i=0; i<length; i++){ 
                if((!isStatic[i]) && (!dead[i])){
                    
                    
                    //Vector3 new_pos = CalcPos(i,time);
                    //CalcNextVeloWithAcc(i,prel_acc[i],time);
                    

                    Vector3 new_pos = CalcPos(i,time,prel_acc[i]); 
                    CalcNextVelo(i,new_pos,time);
                    
                    positions[i] = new_pos;
                }
            }
            for(int i=0; i<length; i++){
                if((!isStatic[i]) && (!dead[i])){
                        CheckBoundary(i);
                    }
            }

        }
    }

    void SetInitialVel(){
        //CelestialObject a = (CelestialObject) mainObject.GetComponent(typeof(CelestialObject));
        //a.pausedVelocity += initialVelocity;
        mainObject.GetComponent<Rigidbody>().velocity += initialVelocity; //the above previous code did not work for some reason, changed it to this simpler thing not sure if good tho
    }
    }
