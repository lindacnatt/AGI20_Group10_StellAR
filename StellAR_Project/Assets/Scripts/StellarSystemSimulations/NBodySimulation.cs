using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour
{
    public const float gravityConstant = 667.408f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Parallel_trajectory.mainPhysics){
            foreach(StellarBody sb in StellarBody.StellarBodies){
                
                if(!(sb.gameObject.scene.name == Parallel_trajectory.mainScene.name))
                    continue;
                
                SimulateStellarSystem(Parallel_trajectory.mainScene.name,sb);
            }
        }
    }

    public static void SimulateStellarSystem(string scene, StellarBody obj){
      if(StellarBody.StellarBodies != null){
        foreach(StellarBody stellarBody in StellarBody.StellarBodies){
                if ((stellarBody != obj)&&(stellarBody.gameObject.scene.name==scene) &&(!stellarBody.staticBody)){
                    Attract(stellarBody,obj);
                    
                }
            }
    }
  }

  public static void Attract(StellarBody attractedObj, StellarBody obj){
        
        Rigidbody rigidBodyToAttract = attractedObj.rigidBody;
        Vector3 distanceDirection = obj.rigidBody.position - rigidBodyToAttract.position;
        float distance = distanceDirection.magnitude;
        //If two StellarBodys are at the exact same place we just return out of it
        if(distance == 0f){
            return;
        }
        float forceMag = (gravityConstant * obj.rigidBody.mass * rigidBodyToAttract.mass) / Mathf.Pow(distance,2);
        Vector3 forceDirection = distanceDirection.normalized * forceMag;

        rigidBodyToAttract.AddForce(forceDirection);
  }
}

