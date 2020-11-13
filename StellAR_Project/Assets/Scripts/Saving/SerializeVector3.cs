using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializeVector3{
    public float x;
    public float y;
    public float z;

    public SerializeVector3(Vector3 vector){
        x=vector.x;
        y=vector.y;
        z=vector.z;
    }

    public SerializeVector3(float pX, float pY, float pZ){
        x=pX;
        y=pY;
        z=pZ;
    }
}

