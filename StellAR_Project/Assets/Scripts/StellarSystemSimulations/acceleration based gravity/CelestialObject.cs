using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{

    [HideInInspector]
    public Rigidbody rigidBody;
    
    [HideInInspector]
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    
    [HideInInspector]
    public Vector3 velocity = new Vector3(0f,0f,0f);

    public bool staticBody;

     public static List<CelestialObject> Objects;

    // Update call for the attractor. Runs through the static attractors list.
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        if (staticBody)
            rigidBody.isKinematic = true;

    }

    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Objects == null){
            Objects = new List<CelestialObject>();
        }
        Objects.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Objects.Remove(this);
    }

     void OnCollisionEnter(){
        if (!staticBody && this.enabled)
        {            
            Destroy(this.gameObject);
        }
    }

}
