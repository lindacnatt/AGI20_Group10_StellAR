using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rigidBody;
    public const float gravityConstant = 667.408f;

    public bool staticBody = false;
    
    public static List<Attractor> Attractors;
    
    

    void FixedUpdate(){
        SimulateStellarSystem();
    }

    
    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Attractors == null){
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Attractors.Remove(this);
    }

    void Attract(Attractor attractedObj){
        
        Rigidbody rigidBodyToAttract = attractedObj.rigidBody;
        Vector3 distanceDirection = rigidBody.position - rigidBodyToAttract.position;
        float distance = distanceDirection.magnitude;
        //If two attractors are at the exact same place we just return out of it
        if(distance == 0f){
            return;
        }
        float forceMag = (gravityConstant * rigidBody.mass * rigidBodyToAttract.mass) / Mathf.Pow(distance,2);
        Vector3 forceDirection = distanceDirection.normalized * forceMag;

        rigidBodyToAttract.AddForce(forceDirection);
  }

  public void SimulateStellarSystem(){
      if(Attractors != null){
        foreach(Attractor attractor in Attractors){
                if ((attractor != this) && (attractor.gameObject.scene == this.gameObject.scene)&&(!attractor.staticBody)){
                    Attract(attractor);
                }
            }
    }
  }

  public static void SimulateStellarSystem(string scene, Attractor obj){
      if(Attractors != null){
        foreach(Attractor attractor in Attractors){
                if ((attractor != obj) &&(attractor.gameObject.scene.name==scene)&&(!attractor.staticBody)){
                    Attract(attractor, obj);
                }
            }
    }
  }

  public static void Attract(Attractor attractedObj, Attractor obj){
        
        Rigidbody rigidBodyToAttract = attractedObj.rigidBody;
        Vector3 distanceDirection = obj.rigidBody.position - rigidBodyToAttract.position;
        float distance = distanceDirection.magnitude;
        //If two attractors are at the exact same place we just return out of it
        if(distance == 0f){
            return;
        }
        float forceMag = (gravityConstant * obj.rigidBody.mass * rigidBodyToAttract.mass) / Mathf.Pow(distance,2);
        Vector3 forceDirection = distanceDirection.normalized * forceMag;

        rigidBodyToAttract.AddForce(forceDirection);
  }

}
