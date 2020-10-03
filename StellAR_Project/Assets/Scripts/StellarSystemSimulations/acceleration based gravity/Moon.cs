using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public const float gravityConstant = 667.408f;

    public Rigidbody rigidBody;
    
    [HideInInspector]
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    
    [HideInInspector]
    public Vector3 velocity = new Vector3(0f,0f,0f);


    [HideInInspector]
    public Vector3 pausedVelocity;
    private bool hasBeenPaused;

    public GameObject parentObject;
    [HideInInspector]
    public CelestialObject parent;
    

     public static List<Moon> Objects;

    // Update call for the attractor. Runs through the static attractors list.
    void Start(){
        parent = (CelestialObject) parentObject.GetComponent<CelestialObject>();
    }

    void FixedUpdate(){
        if(!SimulationPauseControl.gameIsPaused){
            SimulateAcceleration();
            SetPositions();
        }
    }

     void SimulateAcceleration(){
        
            if(parent!=null){
            AttractSource(ref parent);}
            
            else{
                int length = CelestialObject.Objects.Count;
                for(int j=0; j<length; j++){
                    CelestialObject current = CelestialObject.Objects[j];
                    if (current.staticBody){
                            AttractSource(ref current);
                    }
                }

            }
        
        }

    void SetPositions(){
            
        UpdateVelocity(Time.fixedDeltaTime);
        Vector3 new_pos = UpdatePosition(Time.fixedDeltaTime);
    
        
        //UpdateVelocity(Time.fixedDeltaTime,new_pos, ref current);
            
        //current.rigidBody.MovePosition(new_pos);
        this.rigidBody.position=new_pos;
        this.acceleration = new Vector3(0f,0f,0f);
    }

    void UpdateVelocity(float timestep){
            //current.rigidBody.velocity+=current.acceleration*timestep;
            this.velocity+=(this.acceleration*timestep)*Mathf.Sqrt(2.0f/this.rigidBody.mass);
            //https://physics.stackexchange.com/questions/29190/how-exactly-does-mass-affect-speed
    }

    Vector3 UpdatePosition (float timeStep) {
            //Vector3 new_pos= current.rigidBody.position+current.rigidBody.velocity*timeStep;  
            Vector3 new_pos= this.rigidBody.position+this.velocity*timeStep; 
            return new_pos;
    }
    
    void AttractSource(ref CelestialObject otherObject){
                float distance = (otherObject.rigidBody.position-this.rigidBody.position).magnitude;
                Vector3 direction = (otherObject.rigidBody.position-this.rigidBody.position).normalized;
                this.acceleration += direction * gravityConstant *otherObject.rigidBody.mass / Mathf.Pow(distance,2);
    }
    

    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Objects == null){
            Objects = new List<Moon>();
        }
        Objects.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Objects.Remove(this);
    }

     void OnCollisionEnter(){
            Destroy(this.gameObject);

    }


}
