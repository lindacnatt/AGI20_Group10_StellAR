using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodyPhysics : MonoBehaviour
{

    //public const float gravityConstant = 667.408f; //StellarSystemSim scene
    //public const float gravityConstant = 0.6667408f; //ARScene - fast setting
    public static float gravityConstant = 0.06667408f; //medium setting
    //public const float gravityConstant = 0.006667408f; // slow setting

    public static bool slow = false;
    public static bool medium = true;
    public static bool fast = false;


    // Start is called before the first frame update
    void FixedUpdate(){
        if(!SimulationPauseControl.gameIsPaused){
            if (CelestialObject.Objects != null)
            {
                SimulateAcceleration();
                SetPositions();
            }
        
        }
       
    }

    void SimulateAcceleration(){
        if (CelestialObject.Objects != null)
        {
            int length = CelestialObject.Objects.Count;
            if (length > 0)
            {
                for (int j = 0; j < length; j++)
                {
                    CelestialObject current = CelestialObject.Objects[j];
                    if (!current.staticBody)
                    {
                        if (ToggleGravityMode.nBodyGravity)
                        {
                            AttractNBody(ref current);
                        }
                        else
                        {
                            AttractSource(ref current);
                        }
                    }
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

    public void ToggleMedium(){
        medium = true;
        slow=false;
        fast=false;
        TrajectoryVelocity.magnitude = 4f*0.5f;
        //TrajectorySimulation.lineVertices=2000;

    }

    public void ToggleSlow(){
        medium =false;
        slow=true;
        fast=false;
        TrajectoryVelocity.magnitude = 4f*0.1f;
        //TrajectorySimulation.lineVertices=3000;
    }

    public void ToggleFast(){
        medium =false;
        slow=false;
        fast=true;
        TrajectoryVelocity.magnitude = 4f;
        //TrajectorySimulation.lineVertices=1000;
    }

    public void ChangeSpeed(){
        switch (gravityConstant)
            {
                case 0.06667408f: // medium 
                    if(fast){
                        gravityConstant =0.6667408f;}
                    else if(slow){
                        gravityConstant =0.006667408f;}
                    break;
                case 0.6667408f: // fast
                    if(medium){
                        gravityConstant =0.06667408f;}
                    else if(slow){
                        gravityConstant =0.006667408f;}
                    break;
                case 0.006667408f: //slow
                    if(fast){
                        gravityConstant =0.6667408f;}
                    else if(medium){
                        gravityConstant =0.06667408f;}
                    break;
                default:
                    Debug.Log("The function is broken");
                    break;
            }
        print(gravityConstant);

    }
}
