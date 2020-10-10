using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseInterface[] noiseFilters;
    MouseInteraction interaction;
    List<Vector3> touchedPoints;

    public ShapeGenerator(ShapeSettings settings, MouseInteraction interaction){
        this.settings = settings;
        noiseFilters = new NoiseInterface[settings.noiseLayers.Length];
        this.interaction = interaction;
        touchedPoints = interaction.GetPaintedVertices();
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = NoiseFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere, float craterHeight){
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float elevation = 0;
        float mask = 1.0f; //should be changed depending on maskType
        float dist;

        if(settings.noiseLayers[0].enabled){ //TODO: fix for mouse interaction s
            elevation += firstLayerValue;
        }
        for(int i = 1; i < noiseFilters.Length; i++){
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
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        if (craterHeight < 0)
        {
            //Remove any mountain and add the height of the crater at this position
            elevation += craterHeight * (1 + elevation * Math.Abs(1 / craterHeight));
        }
        //elevation += craterHeight;
        return pointOnUnitSphere * settings.radius  +  (pointOnUnitSphere * elevation);
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


}
