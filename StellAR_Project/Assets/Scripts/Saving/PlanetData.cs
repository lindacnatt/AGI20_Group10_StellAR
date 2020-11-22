using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData
{
    //This should contain all shape data serialized for all planets
    public _planet planetData;

    public PlanetData(MotherPlanet planet)
    {
        planetData = new _planet(planet);
        //Debug.Log(craterList);
    }
}