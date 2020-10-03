using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractorv2 : MonoBehaviour
{

    
    public Rigidbody rigidBody;
    public const float gravityConstant = 667.408f;
    private Vector3 acceleration = new Vector3(0f,0f,0f);
    public bool staticBody;
    public Vector3 pausedVelocity;
    private bool hasBeenPaused;
    private float timeDelta;
    
    public static List<Attractorv2> Attractors;

    // Update call for the attractor. Runs through the static attractors list.
    void Awake(){
        if (staticBody)
            rigidBody.isKinematic = true;

    }

    void FixedUpdate(){
        if(!PauseControl.gameIsPaused){
            
            if(hasBeenPaused){
                rigidBody.velocity=pausedVelocity;
                hasBeenPaused=false;
            }

            //float timeStart = Time.fixedTime;
            SimulateStellarSystem();
            //timeDelta = timeStart+Time.fixedDeltaTime;
            pausedVelocity = rigidBody.velocity;
        }
        else{
            rigidBody.velocity=new Vector3(0f,0f,0f);
            hasBeenPaused = true;
        }
    }

    void Update(){
        //print("attractor time");
        //print(timeDelta);
        //print("--------");
    }

    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Attractors == null){
            Attractors = new List<Attractorv2>();
        }
        Attractors.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Attractors.Remove(this);
    }

     void Attract(Attractorv2 attractedObj){
        
        float distance = (attractedObj.rigidBody.position-rigidBody.position).magnitude;
        Vector3 direction = (attractedObj.rigidBody.position-rigidBody.position).normalized;
        acceleration += direction * gravityConstant * attractedObj.rigidBody.mass / Mathf.Pow(distance,2);
    }

    public void UpdateVelocityWithAcc(Vector3 acc, float timeStep){
        rigidBody.velocity=acc*Mathf.Pow(timeStep,2);
    }
    
    Vector3 UpdatePosition (float timeStep) {
        Vector3 new_pos= rigidBody.position+rigidBody.velocity*timeStep+acceleration*Mathf.Pow(timeStep,2)/2.0f; //rigidBody.velocity*timeStep 
        //rigidBody.MovePosition(new_pos);
        return new_pos;
    }

    void UpdateVelocity(float timestep, Vector3 new_pos){
        rigidBody.velocity=((new_pos-rigidBody.position)/timestep);
    }

    public void SimulateStellarSystem(){
      if(Attractors != null){
          if(this.staticBody)
            return;
        foreach(Attractorv2 attractor in Attractors){
                if ((attractor != this) && (attractor.gameObject.scene == this.gameObject.scene)){
                    Attract(attractor);
                }
            }
        }
        if(!this.staticBody){
            Vector3 new_pos = UpdatePosition(Time.fixedDeltaTime);
            
            UpdateVelocity(Time.fixedDeltaTime,new_pos);
            //UpdateVelocityWithAcc(acceleration,Time.fixedDeltaTime);
            
            rigidBody.MovePosition(new_pos);
            acceleration = new Vector3(0f,0f,0f);
            //rigidBody.position=new_pos;

            //rigidBody.AddForce(acceleration);
        }
            
    }

    void OnCollisionEnter(){
        if(!staticBody)
            Destroy(this.gameObject);

    }
}
