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
        newSettings.biomeColorSettings.biomes = new ColorSettings.BiomeColorSettings.Biome[1];

        // init first biome
        newSettings.biomeColorSettings.biomes[0] = new ColorSettings.BiomeColorSettings.Biome();
        newSettings.biomeColorSettings.biomes[0].gradient = defaultColor.biomeColorSettings.biomes[0].gradient;
        newSettings.biomeColorSettings.biomes[0].tint = defaultColor.biomeColorSettings.biomes[0].tint; 
        
        return newSettings;
    }

    public static CraterSettings CopyCraterSettings(){
        CraterSettings newSettings = (CraterSettings) ScriptableObject.CreateInstance("CraterSettings");
        return newSettings;
    }
}
