using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodyPhysics : MonoBehaviour
{

    public const float gravityConstant = 0.667408f; /*tinier planets needs a bigger gravity constant, for planets in the scale of around 1 m and 10-20 kg
                                                     they need a 10^10 times bigger gravity constant than the real one.
                                                     We should ajdust this later so that 1 unit of mass in unity = one earth-mass*/

    // Start is called before the first frame update
   void FixedUpdate(){
        if(!SimulationPauseControl.gameIsPaused){
            
       
            SimulateAcceleration();
            
            int length = CelestialObject.Objects.Count;
            for(int j=0; j<length; j++){
                if(CelestialObject.Objects[j].staticBody)
                    continue;
                CelestialObject current  = CelestialObject.Objects[j];
                SetPositions(ref current);
            }
           
        }
       
    }

    void SimulateAcceleration(){
        int length = CelestialObject.Objects.Count;
        for(int j=0; j<length; j++){
                CelestialObject current = CelestialObject.Objects[j];
                if (!current.staticBody){
                    Attract(ref current);
                }
            }
        
    }

    void SetPositions(ref CelestialObject current){
        if(current.staticBody){
            return;
        }
        UpdateVelocity(Time.fixedDeltaTime, ref current);
        Vector3 new_pos = UpdatePosition(Time.fixedDeltaTime, ref current);
        
            
        //UpdateVelocity(Time.fixedDeltaTime,new_pos, ref current);
            
        current.rigidBody.MovePosition(new_pos);
        current.acceleration = new Vector3(0f,0f,0f);
    }


    void UpdateVelocity(float timestep, Vector3 new_pos, ref CelestialObject current){
            current.rigidBody.velocity=((new_pos-current.rigidBody.position)/timestep);
        }
    
     void UpdateVelocity(float timestep, ref CelestialObject current){
            current.rigidBody.velocity+=current.acceleration*timestep;
        }
    

    /*Vector3 UpdatePosition (float timeStep, ref CelestialObject current) {
            Vector3 new_pos= current.rigidBody.position+current.rigidBody.velocity*timeStep+current.acceleration*Mathf.Pow(timeStep,2)/2.0f;  
            return new_pos;
        }*/

    Vector3 UpdatePosition (float timeStep, ref CelestialObject current) {
            Vector3 new_pos= current.rigidBody.position+current.rigidBody.velocity*timeStep;  
            return new_pos;
        }

    void Attract(ref CelestialObject current){
        int length = CelestialObject.Objects.Count;
        for(int j=0; j<length; j++){
            CelestialObject otherObject = CelestialObject.Objects[j];
            if((otherObject != current)){
                
                float distance = (otherObject.rigidBody.position-current.rigidBody.position).magnitude;
                Vector3 direction = (otherObject.rigidBody.position-current.rigidBody.position).normalized;
                current.acceleration += direction * gravityConstant *otherObject.rigidBody.mass / Mathf.Pow(distance,2);
            }

        }

        
    }
}
