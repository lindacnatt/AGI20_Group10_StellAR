using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _gasSettings
{
    public Color BandColorStorm;
    public float StormSize;
    public float StormSpeed;
    public float StormStrength;
    public float StormPlacement;

    public float BandScale1;
    public float BandXSeed1;
    public float BandYSeed1;
    public Color BandColor1;

    public float BandScale2;
    public float BandXSeed2;
    public float BandYSeed2;
    public Color BandColor2;

    public Color BandColor3;

    public _gasSettings(GasPlanetShaderMAterialPropertyBlock GasPlanetLook)
    {
        BandColorStorm = GasPlanetLook.BandColorStorm;
        StormSize = GasPlanetLook.StormSize;
        StormSpeed = GasPlanetLook.StormSpeed;
        StormStrength = GasPlanetLook.StormStrength;
        StormPlacement = GasPlanetLook.StormPlacement;

        BandScale1 = GasPlanetLook.BandScale1;
        BandXSeed1 = GasPlanetLook.BandXSeed1;
        BandYSeed1 = GasPlanetLook.BandYSeed1;
        BandColor1 = GasPlanetLook.BandColor1;

        BandScale2 = GasPlanetLook.BandScale2;
        BandXSeed2 = GasPlanetLook.BandXSeed2;
        BandYSeed2 = GasPlanetLook.BandYSeed2;
        BandColor2 = GasPlanetLook.BandColor2;

        BandColor3 = GasPlanetLook.BandColor3;



    }
}

