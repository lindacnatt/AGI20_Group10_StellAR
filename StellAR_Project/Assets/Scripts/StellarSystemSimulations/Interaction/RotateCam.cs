using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject camContainer;
    private Quaternion rotation;

    void Start(){
        camContainer = new GameObject("Camera container");
        camContainer.transform.position = transform.position;
        transform.SetParent(camContainer.transform);

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro(){
        if(SystemInfo.supportsGyroscope){
            gyro=Input.gyro;
            gyro.enabled = true;

            camContainer.transform.rotation = Quaternion.Euler(90f,90f,0f);
            rotation = new Quaternion(0,0,1,0);
            return true;
        }
        return false;
    }


    
 
    void Update () {
        if(gyroEnabled){
            transform.localRotation = gyro.attitude*rotation;
        }
    }
}
