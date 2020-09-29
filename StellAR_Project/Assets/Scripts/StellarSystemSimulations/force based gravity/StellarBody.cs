using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarBody : MonoBehaviour
{
    public Rigidbody rigidBody;
    public bool staticBody = false;
    
    public static List<StellarBody> StellarBodies;
    
    void Awake(){
        if (staticBody)
           rigidBody.isKinematic = true;
            
    }

    // For adding StellarBodys to the StellarBodys list
    void OnEnable(){  
        if(StellarBodies == null){
            StellarBodies = new List<StellarBody>();
        }
        StellarBodies.Add(this);
    }

    // For removing StellarBodys from the StellarBodys list
    void OnDisable(){
        StellarBodies.Remove(this);
    }

}
