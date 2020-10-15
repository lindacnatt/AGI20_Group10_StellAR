using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodyPhysics : MonoBehaviour
{

    //public const float gravityConstant = 667.408f; //StellarSystemSim scene
    public const float gravityConstant = 0.6667408f; //ARScene

    // Start is called before the first frame update
    void FixedUpdate(){
        if(!SimulationPauseControl.gameIsPaused){
            
       
                SimulateAcceleration();
                SetPositions();
        
           
        }
       
    }

    void SimulateAcceleration(){
        int length = CelestialObject.Objects.Count;
        for(int j=0; j<length; j++){
                CelestialObject current = CelestialObject.Objects[j];
                if (!current.staticBody){
                    if(ToggleGravityMode.nBodyGravity){
                        AttractNBody(ref current);
                        }
                    else
                    {   
                        AttractSource(ref current);
                    }
                }
            }
        
    }

    void SetPositions(){
        int length = CelestialObject.Objects.Count;
        for(int j=0; j<length; j++){
            if(CelestialObject.Objects[j].staticBody)
                continue;
            CelestialObject current  = CelestialObject.Objects[j];
            
            UpdateVelocity(Time.fixedDeltaTime, ref current);
            Vector3 new_pos = UpdatePosition(Time.fixedDeltaTime, ref current);
        
            
            //UpdateVelocity(Time.fixedDeltaTime,new_pos, ref current);
                
            //current.rigidBody.MovePosition(new_pos);
            current.rigidBody.position=new_pos;
            current.acceleration = new Vector3(0f,0f,0f);}
    }


    void UpdateVelocity(float timestep, Vector3 new_pos, ref CelestialObject current){
            current.velocity=((new_pos-current.rigidBody.position)/timestep);
        }
    
     void UpdateVelocity(float timestep, ref CelestialObject current){
            //current.rigidBody.velocity+=current.acceleration*timestep;
            current.velocity+=(current.acceleration*timestep)*Mathf.Sqrt(2.0f/current.rigidBody.mass);
            //https://physics.stackexchange.com/questions/29190/how-exactly-does-mass-affect-speed
        }
    

    /*Vector3 UpdatePosition (float timeStep, ref CelestialObject current) {
            Vector3 new_pos= current.rigidBody.position+current.halfVelocity*timeStep+current.acceleration*Mathf.Pow(timeStep,2)/2.0f;  
            return new_pos;
        }*/
    
    Vector3 UpdatePosition (float timeStep, ref CelestialObject current) {
            //Vector3 new_pos= current.rigidBody.position+current.rigidBody.velocity*timeStep;  
            Vector3 new_pos= current.rigidBody.position+current.velocity*timeStep; 
            return new_pos;
        }

    void AttractNBody(ref CelestialObject current){
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

      void AttractSource(ref CelestialObject current){
        int length = CelestialObject.Objects.Count;
        for(int j=0; j<length; j++){
            CelestialObject otherObject = CelestialObject.Objects[j];
            if((otherObject != current)&&otherObject.staticBody){
                
                float distance = (otherObject.rigidBody.position-current.rigidBody.position).magnitude;
                Vector3 direction = (otherObject.rigidBody.position-current.rigidBody.position).normalized;
                current.acceleration += direction * gravityConstant *otherObject.rigidBody.mass / Mathf.Pow(distance,2);
            }

        }

        
    }
}
