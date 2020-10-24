using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseInterface[] noiseFilters;
    MouseInteraction interaction;
    List<Vector3> touchedPoints;
    public MinMax elevationMinMax;

    public ShapeGenerator(ShapeSettings settings, MouseInteraction interaction){
        this.settings = settings;
        noiseFilters = new NoiseInterface[settings.noiseLayers.Length];
        this.interaction = interaction;
        touchedPoints = interaction.GetPaintedVertices();
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = NoiseFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        //float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float elevation = 0.0f;
        float mask = 1.0f; //should be changed depending on maskType
        float dist;
        float noiseValue;

        /*
        if(settings.noiseLayers[0].enabled){ //TODO: fix for mouse interaction s
            elevation += firstLayerValue;
        }
        */

        for(int i = 0; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
                if(settings.noiseLayers[i].useMouseAsMask){
                    dist = checkIfmarked(touchedPoints, pointOnUnitSphere, interaction.brushSize);  
                    if(dist < interaction.brushSize){
                        //mask = settings.noiseLayers[i].useFirstLayerAsMask ? firstLayerValue : 1;
                        mask = (interaction.brushSize-dist)/interaction.brushSize; 
                    }
                    else{
                        mask = .0f;
                    }
                }
                noiseValue =  noiseFilters[i].Evaluate(pointOnUnitSphere);
                //noiseValue = StepFunction(settings.noiseLayers[i].noiseSettings.clampSteps, noiseValue);
                elevation += noiseValue * mask;
            }  
        }
        elevation = settings.radius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere *  elevation;
    }

    private float checkIfmarked(List<Vector3> touchedPoints, Vector3 pointOnSphere, float radius){
        float minDist = radius;
        float tempDist;
        foreach (Vector3 point in touchedPoints){
            tempDist = (point-pointOnSphere).magnitude;
            if(tempDist < minDist){ // if point is within a certain area 
                minDist = tempDist; 
            }
        }
        return minDist;
    }

    private float StepFunction(int numSteps, float value){ // clamps the value, assumes value [0, 1]
        value = Mathf.Round(value*numSteps);
        value /= numSteps;
        return value;
    }

}
