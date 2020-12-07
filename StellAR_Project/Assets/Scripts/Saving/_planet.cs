using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _planet
{
    //This contains all shape data serialized for one planet
    public int id;
    public List<_crater> craterList;
    public _shapeSettings shape;
    public _colorSettings color;

    public _planet(MotherPlanet planet, int id)
    {
        this.id = id;
        ShapeSettings shapeSettings;
        ColorSettings colorSettings;
        craterList = new List<_crater>();
        string[] noisePoints = planet.shapeGenerator.getMaskKeys();
        float[] noiseValues = planet.shapeGenerator.getMaskValues();
        shapeSettings = planet.shapeGenerator.settings;
        shape = new _shapeSettings(shapeSettings.radius, shapeSettings.zeroLvlIsOcean,
            shapeSettings.noiseLayers, noisePoints, noiseValues);
        colorSettings = planet.colorGenerator.settings;
        color = new _colorSettings(colorSettings.planetMaterial, colorSettings.biomeColorSettings);
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
