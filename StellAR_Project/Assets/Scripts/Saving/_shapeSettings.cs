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
    List<float> masks;

    public _shapeSettings(float radius, bool zeroLvlIsOcean, ShapeSettings.NoiseLayer[] noiseLayers,
            List<float> masks)
    {
        this.radius = radius;
        this.zeroLvlIsOcean = zeroLvlIsOcean;
        this.noiseLayers = noiseLayers;
        this.masks = masks;
    }
}
