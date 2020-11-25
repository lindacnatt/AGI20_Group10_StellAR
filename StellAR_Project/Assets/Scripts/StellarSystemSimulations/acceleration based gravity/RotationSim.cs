using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSim : MonoBehaviour
{
    private Quaternion rotation;
    private float rotSpeed = 30f;
    private bool rotationIsSet = false;
    private bool startRot = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    // Start is called before the first frame update

    void Update(){
        if(!rotationIsSet){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            if(touch.phase == TouchPhase.Moved){
                endPos = touch.position;
                
            }
            if(touch.phase == TouchPhase.Ended){
                direction = endPos - startPos;
                Quaternion rotationY = Quaternion.Euler(0f, -direction.y * direction.magnitude, 0f);
                Quaternion rotationX = Quaternion.Euler(-direction.x * direction.magnitude, 0f, 0f);
                rotation = rotationY * rotationX;
                startRot = true;
            }
        }
    }

    void FixedUpdate(){
        if (startRot){
            this.gameObject.transform.Rotate(rotation.eulerAngles*Time.fixedDeltaTime);
        }
    }

    public Quaternion GetRotation(){
        return rotation;
    }

    public void SetRotation( Quaternion rot){
        rotation = rot;
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
