using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rigidBody;
    const float gravityConstant = 667.408f;

    public static List<Attractor> Attractors;

    // Update call for the attractor. Runs through the static attractors list.
    void FixedUpdate(){
        
        foreach(Attractor attractor in Attractors){
            if (attractor != this){
                Attract(attractor);
            }
        }

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
}
