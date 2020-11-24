using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _planet
{
    //This contains all shape data serialized for one planet
    public List<_crater> craterList;
    //public NoiseSettings noiseSettings;
    public _shapeSettings shape;

    public _planet(MotherPlanet planet)
    {
        ShapeSettings shapeSettings;
        craterList = new List<_crater>();
        List<string> noisePoints = new List<string>();
        List<float> noiseValues = new List<float>();
        shapeSettings = planet.shapeGenerator.settings;
        //noiseSettings = planet.shapeGenerator.settings.noiseLayers[0].noiseSettings;
        shape = new _shapeSettings(shapeSettings.radius, shapeSettings.zeroLvlIsOcean,
            shapeSettings.noiseLayers, noisePoints, noiseValues);
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
