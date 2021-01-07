using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationSim : MonoBehaviour
{
    private float rotSpeed = 30f;
    private bool rotationIsSet = false;
    private bool startRot = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;
    private float rotX;
    private float rotY;

    void Update(){
        if(!rotationIsSet){
            
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            else if(Input.GetMouseButton(0)){
                endPos = Input.mousePosition;
                direction = endPos - startPos;
                if(direction.magnitude>0.05f){
                    rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
                    rotY = Input.GetAxis("Mouse Y")*rotSpeed*Mathf.Deg2Rad;
                }

                else{
                    rotX = 0f;
                    rotY = 0f;
                }
                startRot = true;
                
            }
            else if(Input.GetMouseButtonUp(0)){
                endPos = Input.mousePosition;
                direction = endPos - startPos;
                if(direction.magnitude>0.1f){
                    rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
                    rotY = Input.GetAxis("Mouse Y")*rotSpeed*Mathf.Deg2Rad;
                }

                else{
                    rotX = 0f;
                    rotY = 0f;
                }
                
            }
           
        }
    }

    void FixedUpdate(){
        if (startRot){
            this.gameObject.transform.RotateAround(Vector3.up, -rotX);
            this.gameObject.transform.RotateAround(Vector3.right, rotY);
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

    public void SetRotSpeed(float speed){
        rotSpeed = speed;
    }

    public bool Rotating(){
        return rotationIsSet;
    }

    public void SetState(bool state){
        rotationIsSet = state;
    }
}

