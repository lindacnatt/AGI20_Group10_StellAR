using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : CelestialObject
{
    TrailRenderer trail;
    // Start is called before the first frame update
    /*
    void Start()
    {
        trail = this.gameObject.GetComponent<TrailRenderer>();
        if (trail == null)
        {
            trail = this.gameObject.AddComponent<TrailRenderer>();
        }
        //trail.colorGradient
        trail.material = Resources.Load<Material>("unity_builtin_extra/Default-Particle.mat");
    }*/
}
