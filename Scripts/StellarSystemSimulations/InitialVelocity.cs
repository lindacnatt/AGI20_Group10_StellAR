using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 initialvelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody>();
        rb.velocity += initialvelocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
