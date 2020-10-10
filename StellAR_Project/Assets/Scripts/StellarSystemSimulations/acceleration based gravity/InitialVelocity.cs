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
        Moon m = this.GetComponent<Moon>();
        if (m) {
        //Component is valid AND stored for later use, preventing any further GetComponent calls
            m.velocity = initialvelocity*Mathf.Sqrt(2.0f/rb.mass);;
        }
        else{
            CelestialObject ob = (CelestialObject) gameObject.GetComponent<CelestialObject>();
            ob.velocity+=initialvelocity*Mathf.Sqrt(2.0f/rb.mass);
        }
        
        //rb.velocity += initialvelocity;
        
        
    }
}
