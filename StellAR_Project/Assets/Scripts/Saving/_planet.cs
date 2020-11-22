using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _planet
{
    //This contains all shape data serialized for one planet
    public List<_crater> craterList;

    public _planet(MotherPlanet planet)
    {
        craterList = new List<_crater>();

        CollectCraters(planet);
    }

    public void CollectCraters(MotherPlanet planet)
    {
        List<CraterGenerator.Crater> oldCL = planet.shapeGenerator.craterGenerator.craterList;
        for (int i = 0; i < oldCL.Count; i++)
        {
            craterList.Add(new _crater(oldCL[i].center, oldCL[i].radius,
    oldCL[i].floor, oldCL[i].smoothness, oldCL[i].impact, oldCL[i].rimSteepness,
    oldCL[i].rimWidth));
        }
    }

}
