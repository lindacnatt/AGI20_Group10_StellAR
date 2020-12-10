using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxQuadFixater : MonoBehaviour
{
    public Transform Quad;
    // Start is called before the first frame update

 
    // Update is called once per frame
    void Update()
    {

        Quad.rotation = transform.rotation;
        Quad.position = transform.position+transform.forward*29;
        
        
    }
}
