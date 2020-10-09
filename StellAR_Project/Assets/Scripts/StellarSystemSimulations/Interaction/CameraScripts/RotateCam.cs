using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject camContainer;
    private Quaternion rotation;

    public Transform target;
    private Vector3 offset;
    public float TranslateSpeed;

    void Start(){
        offset = new Vector3(0f,10f,10f);
        camContainer = new GameObject("Camera container");
        transform.position += offset;
        transform.LookAt(target);
        camContainer.transform.position = target.position;//transform.position;
        transform.SetParent(camContainer.transform);

        gyroEnabled = EnableGyro();
        this.enabled = false;
    }

    private bool EnableGyro(){
        if(SystemInfo.supportsGyroscope){
            gyro=Input.gyro;
            gyro.enabled = true;
            //camContainer.transform.rotation = Quaternion.Euler(90f,90f,0f);
            rotation = new Quaternion(0,0,1,0);
            return true;
        }
        return false;
    }


    
 
    void Update () {
        if(gyroEnabled && (target !=null)){
            //transform.localRotation = gyro.attitude*rotation;
            camContainer.transform.rotation = gyro.attitude*rotation;
            transform.LookAt(target);
            //ChangePosition();
        }
        else{
            this.enabled = false;
        }
    }

    void ChangePosition(){

        Vector3 direction = target.position - transform.position;
        float t = Vector3.Angle(target.up,direction);

        Vector2 dir2D = new Vector2(direction.x,direction.y);
        Vector2 tar2D = new Vector2(target.position.x,0f);
        float s = Vector2.Angle(tar2D,dir2D);

        float x = target.position.x + offset.magnitude*Mathf.Cos(s)*Mathf.Cos(t);
        float y = target.position.y + offset.magnitude*Mathf.Sin(s)*Mathf.Sin(t);
        float z = target.position.z + offset.magnitude*Mathf.Cos(t);

        Vector3 new_pos = new Vector3(x,y,z);

        transform.position = Vector3.Slerp(transform.position, new_pos, TranslateSpeed * Time.deltaTime);
        transform.LookAt(target);
        /*Quaternion rot = gyro.attitude*rotation;
        camContainer.transform.RotateAround(target.position, target.forward,rot.eulerAngles.z);
        camContainer.transform.RotateAround(target.position, target.right,rot.eulerAngles.x);
        camContainer.transform.RotateAround(target.position, target.up,rot.eulerAngles.y);*/

    }
}
