using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSpawner {
    // Start is called before the first frame update
    public static ShapeSettings loadDefaultShape(){
        var shapeSettings = Resources.Load<ShapeSettings>("Settings/DefaultShape");
        return shapeSettings;
    }

    public static ColorSettings loadDefaultColor(){
        var colorSettings = Resources.Load<ColorSettings>("Settings/DefaultColor");
        return colorSettings;
    }

    public static CraterSettings loadDefaultCraters()
    {
        var craterSettings = Resources.Load<CraterSettings>("Settings/DefaultCraters");
        return craterSettings;
    }

    public static ShapeSettings CopyShapeSettings(){
        ShapeSettings newSettings = (ShapeSettings) ScriptableObject.CreateInstance("ShapeSettings");
        newSettings.noiseLayers = new ShapeSettings.NoiseLayer[2];
        for(int i = 0; i < newSettings.noiseLayers.Length; i++){
            newSettings.noiseLayers[i] = new ShapeSettings.NoiseLayer();
            newSettings.noiseLayers[i].noiseSettings = new NoiseSettings();
        }
        newSettings.noiseLayers[0].noiseSettings.filterType = NoiseSettings.FilterType.Rigid;

        // set stadard values for landmass noise
        newSettings.noiseLayers[1].noiseSettings.filterType = NoiseSettings.FilterType.LandMass;
        newSettings.noiseLayers[1].noiseSettings.amplitude = 0.1f;

        return newSettings;
    }

    public static ColorSettings CopyColorSettings(){
        ColorSettings defaultColor = loadDefaultColor();
        ColorSettings newSettings = (ColorSettings) ScriptableObject.CreateInstance("ColorSettings");

        // Create deep copy of material 
        newSettings.planetMaterial = new Material(Resources.Load<Material>("Graphics/Planet Material"));
    
        // init some stuff
        newSettings.biomeColorSettings = new ColorSettings.BiomeColorSettings();
        newSettings.biomeColorSettings.noise = new NoiseSettings();
        newSettings.biomeColorSettings.biomes = new ColorSettings.BiomeColorSettings.Biome[3];

        // set biomeColorSettingw
        newSettings.biomeColorSettings.noiseOffset = defaultColor.biomeColorSettings.noiseOffset;
        newSettings.biomeColorSettings.noiseStrength = defaultColor.biomeColorSettings.noiseStrength;
        newSettings.biomeColorSettings.blendAmount = defaultColor.biomeColorSettings.blendAmount;

        // init first biome
        for(int i = 0; i < newSettings.biomeColorSettings.biomes.Length; i++){
            newSettings.biomeColorSettings.biomes[i] = new ColorSettings.BiomeColorSettings.Biome();
            newSettings.biomeColorSettings.biomes[i].gradient = defaultColor.biomeColorSettings.biomes[i].gradient;
            newSettings.biomeColorSettings.biomes[i].tint = defaultColor.biomeColorSettings.biomes[i].tint;
            //newSettings.biomeColorSettings.biomes[i].tintPercent = defaultColor.biomeColorSettings.biomes[i].tintPercent; 
            newSettings.biomeColorSettings.biomes[i].tintPercent = 0.4f; 
            newSettings.biomeColorSettings.biomes[i].startHeight = defaultColor.biomeColorSettings.biomes[i].startHeight; 
        }
        return newSettings;
    }

    public static CraterSettings CopyCraterSettings(){
        CraterSettings newSettings = (CraterSettings) ScriptableObject.CreateInstance("CraterSettings");
        return newSettings;
    }
}
