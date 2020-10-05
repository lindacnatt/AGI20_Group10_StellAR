using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseFilter[] noiseFilters;
    CraterSettings craterSettings;

    public ShapeGenerator(ShapeSettings settings){
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float test = settings.testSettings.testval;
        float mask;
        float elevation = 0;
        elevation += firstLayerValue;
        for(int i = 1; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
                mask = settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
                //double y = pointOnUnitSphere.y;
                elevation += (noiseFilters[i].Evaluate(pointOnUnitSphere) * mask + Mathf.Sin(pointOnUnitSphere.y * test));
                //elevation += test;
            }  
        }
        return pointOnUnitSphere * settings.radius  +  (pointOnUnitSphere * elevation);
    }

    public static float toFloat(double value)
    {
        return (float)value;
    }
}
