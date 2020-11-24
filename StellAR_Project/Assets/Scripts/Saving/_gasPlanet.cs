using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _gasPlanet
{
    public Color BandColorStorm;
    public float StormSize;
    public float StormSpeed;
    public float StormStrength;
    public float StormPlacement;
    public Vector2 SpiralDefinition;

    public float BandScale1;
    public float BandXSeed1;
    public float BandYSeed1;
    public Color BandColor1;

    public float BandScale2;
    public float BandXSeed2;
    public float BandYSeed2;
    public Color BandColor2;

    public _gasPlanet(GasPlanetShaderMAterialPropertyBlock gasPlanet)
    {
        BandColorStorm = gasPlanet.BandColorStorm;
        StormSize = gasPlanet.StormSize;
        StormSpeed = gasPlanet.StormSpeed;
        StormStrength = gasPlanet.StormStrength;
        StormPlacement = gasPlanet.StormPlacement;
        SpiralDefinition = gasPlanet.SpiralDefinition;

        BandScale1 = gasPlanet.BandScale1;
        BandXSeed1 = gasPlanet.BandXSeed1;
        BandYSeed1 = gasPlanet.BandYSeed1;
        BandColor1 = gasPlanet.BandColor1;

        BandScale2 = gasPlanet.BandScale2;
        BandXSeed2 = gasPlanet.BandXSeed2;
        BandYSeed2 = gasPlanet.BandYSeed2;
        BandColor2 = gasPlanet.BandColor2;
}

}
