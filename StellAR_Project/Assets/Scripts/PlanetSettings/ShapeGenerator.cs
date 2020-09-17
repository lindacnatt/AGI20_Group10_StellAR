using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator{
    ShapeSettings planetSettings;
    public ShapeGenerator(ShapeSettings planetSettings){
        this.planetSettings = planetSettings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        return pointOnUnitSphere * planetSettings.radius;
    }
}
