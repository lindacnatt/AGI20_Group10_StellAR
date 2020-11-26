using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSim : MonoBehaviour
{
    private Quaternion rotation;
    private Vector3 rotationVec;
    private float startZ;
    private float rotSpeed = 30f;
    private bool rotationIsSet = false;
    private bool startRot = false;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start(){
        startZ = this.gameObject.transform.rotation.eulerAngles.z;
    }

    void Update(){
        if(!rotationIsSet){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            if(touch.phase == TouchPhase.Moved){
                endPos = touch.position;
                direction = endPos - startPos;
                if(direction.magnitude>0.05f){
                    float xDegree= Mathf.Rad2Deg*(Mathf.Acos(direction.x/direction.magnitude));
                    float yDegree= Mathf.Rad2Deg*(Mathf.Asin(direction.y/direction.magnitude));
                    rotationVec = new Vector3(xDegree, yDegree, 0f);
                }

                else{
                    rotationVec = new Vector3(0f, 0f, 0f);
                }

                //rotation = Quaternion.Euler(xDegree, yDegree, 0f);
                //Quaternion rotationX = Quaternion.Euler(, 0f, 0f);
                //rotation = rotationY * rotationX;
                startRot = true;
                
            }
            if(touch.phase == TouchPhase.Ended){
                endPos = touch.position;
                direction = endPos - startPos;
                if(direction.magnitude>0.1f){
                    float xDegree= Mathf.Rad2Deg*(Mathf.Acos(direction.x/direction.magnitude));
                    float yDegree= Mathf.Rad2Deg*(Mathf.Asin(direction.y/direction.magnitude));
                    rotationVec = new Vector3(xDegree, yDegree, 0f);
                }

                else{
                    rotationVec = new Vector3(0f, 0f, 0f);
                }
                
            }
            /*if (touch.phase == TouchPhase.Moved)
            {
                
                Quaternion rotationY = Quaternion.Euler(0f, touch.deltaPosition.y*touch.deltaPosition.magnitude , 0f);
                Quaternion rotationX = Quaternion.Euler(touch.deltaPosition.x*touch.deltaPosition.magnitude, 0f, 0f);
                rotation = rotationY * rotationX;
                startRot = true;
            }*/
        }
    }

    void FixedUpdate(){
        if (startRot){
            //this.gameObject.transform.Rotate(rotation.eulerAngles*Time.fixedDeltaTime);
            this.gameObject.transform.Rotate(rotationVec*Time.fixedDeltaTime);
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
