using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseFilter[] noiseFilters;
    //CraterSettings craterSettings;

    public ShapeGenerator(ShapeSettings settings){
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere, float craterHeight){
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        //Vector3 testvec = new Vector3(0.0f, 0.0f, 0.1f);
        //Debug.Log(pointOnUnitSphere);

        float mask;
        float elevation = 0;
        elevation += firstLayerValue;
        for (int i = 1; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
                mask = settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
                //double y = pointOnUnitSphere.y;

                elevation += (noiseFilters[i].Evaluate(pointOnUnitSphere) * mask);
            }  
        }

        elevation += craterHeight;
        return pointOnUnitSphere * settings.radius  +  (pointOnUnitSphere * elevation);
    }

    /*
    T[] InitializeArray<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (int i = 0; i < length; ++i)
        {
            array[i] = new T();
        }

        return array;
    }
    */

    public static float toFloat(double value)
    {
        return (float)value;
    }
}
