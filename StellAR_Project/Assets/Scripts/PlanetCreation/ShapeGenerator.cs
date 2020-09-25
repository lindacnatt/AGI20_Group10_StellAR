using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseFilter[] noiseFilters;

    public ShapeGenerator(ShapeSettings settings){
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float mask;

        float elevation = 0;
        elevation += firstLayerValue;
        for(int i = 1; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
               mask = settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
               elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }  
        }
        return pointOnUnitSphere * settings.radius  +  (pointOnUnitSphere * elevation);
    }
}
