using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _shapeSettings
{
    //This contains all crater data serialized for one crater
    public float radius;
    public bool zeroLvlIsOcean;
    public ShapeSettings.NoiseLayer[] noiseLayers;
    public List<string> noisePoints;
    public List<float> noiseValues;

    public _shapeSettings(float radius, bool zeroLvlIsOcean, ShapeSettings.NoiseLayer[] noiseLayers,
            List<string> noisePoints, List<float> noiseValues)
    {
        this.radius = radius;
        this.zeroLvlIsOcean = zeroLvlIsOcean;
        this.noiseLayers = noiseLayers;
        this.noisePoints = noisePoints;
        this.noiseValues = noiseValues;
    }
}
