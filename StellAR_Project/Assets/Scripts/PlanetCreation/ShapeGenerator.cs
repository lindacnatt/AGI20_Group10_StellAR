using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseFilter[] noiseFilters;

    MouseInteraction interaction;
    List<Vector3> touchedPoints;


    public ShapeGenerator(ShapeSettings settings, MouseInteraction interaction){
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        this.interaction = interaction;
        touchedPoints = interaction.GetPaintedVertices();
        foreach (Vector3 point in touchedPoints){
            Debug.Log(point);
        }
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float mask;
        float elevation = 0;
        if(settings.noiseLayers[0].enabled){
            elevation += firstLayerValue;
        }
        for(int i = 1; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
                if(checkIfmarked(touchedPoints, pointOnUnitSphere, interaction.brushSize)){
                    mask = settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
                    elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;    
                }
            }  
        }
        return pointOnUnitSphere * settings.radius  +  (pointOnUnitSphere * elevation);
    }

    private bool checkIfmarked(List<Vector3> touchedPoints, Vector3 pointOnSphere, float radius){
        foreach (Vector3 point in touchedPoints){
            if((point-pointOnSphere).magnitude < radius){
                return true;
            }
        }
        return false;
    }


}
