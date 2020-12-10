using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedChildRotation : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;
    

    void Start()
    {
        rotation = transform.localRotation;

    }


    void LateUpdate()
    {
            transform.rotation = rotation;
    }
}
