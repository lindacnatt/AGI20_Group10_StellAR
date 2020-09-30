using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{

    public Rigidbody rigidBody;
    
    [HideInInspector]
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    public bool staticBody;

    [HideInInspector]
    public Vector3 pausedVelocity;
    private bool hasBeenPaused;
    

     public static List<CelestialObject> Objects;

    // Update call for the attractor. Runs through the static attractors list.
    void Start(){
        if (staticBody)
            rigidBody.isKinematic = true;

    }

    void FixedUpdate(){
        if(!SimulationPauseControl.gameIsPaused){
            
            if(hasBeenPaused){
                rigidBody.velocity=pausedVelocity;
                hasBeenPaused=false;
            }
        }
        else{
            if(!hasBeenPaused)
                pausedVelocity = rigidBody.velocity;
            
            rigidBody.velocity=new Vector3(0f,0f,0f);
            hasBeenPaused = true;
        }
    }

    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Objects == null){
            Objects = new List<CelestialObject>();
        }
        Objects.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Objects.Remove(this);
    }

    /* void OnCollisionEnter(){
        if(!staticBody)
            Destroy(this.gameObject);

    }*/


}
