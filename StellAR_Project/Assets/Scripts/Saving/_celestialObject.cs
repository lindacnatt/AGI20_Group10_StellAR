using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _celestialObject 
{
    public int id;
    public Vector3 position;
    public Vector3 velocity;
    public bool staticBody;
    public float mass;
    public float[] rotation;
    public string name;

    public _celestialObject(CelestialObject planet, int id){
        this.id = id;
        position = planet.GetPosition();
        velocity=planet.velocity;
        staticBody=planet.staticBody;
        mass =planet.mass;
        
        RotationSim rot = planet.gameObject.GetComponent<RotationSim>();
        if(rot != null){
            rotation = rot.GetRotation();
        }
        else{
            rotation = new float[] {0f, 0f};
        }

        name=planet.GetName();

    }
}
