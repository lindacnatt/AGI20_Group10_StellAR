using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSim : MonoBehaviour
{
    private float rotSpeed = 0.5f;
    private bool rotationIsSet = false;
    private bool startRot = false;
    private bool deployed = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;
    private float rotX;
    private float rotY;

    
    void Update(){
        if(!rotationIsSet){
            
            if(Input.touchCount == 1){
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    if(touch.deltaPosition.magnitude > 0.03f){
                    rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                    rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

                    startRot = true;
                    }
                }
            }
        }
    }


    void FixedUpdate(){
        if (startRot){
            if(!rotationIsSet || !SimulationPauseControl.gameIsPaused || !deployed){
                this.gameObject.transform.RotateAround(Vector3.up, -rotX);
                this.gameObject.transform.RotateAround(Vector3.right, rotY);
            }
        }
    }

    public float[] GetRotation(){
        float[] rot = new float[] {rotX, rotY};
        return rot;
    }

    public void SetRotation( float x, float y){
        rotX = x;
        rotY = y;
    }

    public float GetRotSpeed(){
        return rotSpeed;
    }

    /*public void SetRotSpeed(float speed){
        rotSpeed = speed;
    }*/

    public bool Rotating(){
        return rotationIsSet;
    }

    public void SetState(bool state){
        rotationIsSet = state;
    }

    public void StartRotation(bool state){
        startRot = state;
    }

    public void Deploy(){
        deployed = true;
    }
}
