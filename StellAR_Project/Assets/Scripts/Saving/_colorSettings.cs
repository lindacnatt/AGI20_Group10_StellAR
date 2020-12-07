using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class _colorSettings
{
    public Material planetMaterial;
    public ColorSettings.BiomeColorSettings biomeColorSettings;

    public _colorSettings(Material planetMaterial, ColorSettings.BiomeColorSettings biomeColorSettings)
    {
        this.planetMaterial = planetMaterial;
        this.biomeColorSettings = biomeColorSettings;
    }
}
