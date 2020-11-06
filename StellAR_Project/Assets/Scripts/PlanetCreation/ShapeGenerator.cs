using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeGenerator {
    ShapeSettings settings;
    NoiseInterface[] noiseFilters;
    MouseInteraction interaction;
    List<Vector3> touchedPoints;
    public MinMax elevationMinMax;
    public CraterGenerator craterGenerator;
    Dictionary<String, float> masks;

    public ShapeGenerator(ShapeSettings settings, MouseInteraction interaction, CraterGenerator craterGenerator){
        Debug.Log("new shape");
        this.settings = settings;
        noiseFilters = new NoiseInterface[settings.noiseLayers.Length];
        
        this.interaction = interaction;
        this.masks = new Dictionary<string, float>();
        //touchedPoints = interaction.GetPaintedVertices();
        
        for (int i = 0; i < noiseFilters.Length; i++){
            noiseFilters[i] = NoiseFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
        this.craterGenerator = craterGenerator; 
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere){
        float craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        //Debug.Log(craterHeight);
        float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float elevation = 0;
        float noiseelevation = 0;
        float mask = 1.0f; //should be changed depending on maskType
        float dist;
        String pointStr = pointOnUnitSphere.ToString();

        for(int i = 0; i < noiseFilters.Length; i++){
            if(settings.noiseLayers[i].enabled){
                if(settings.noiseLayers[i].useMouseAsMask){
                    // check if the point is in radius of the painted vertices 
                    //dist = checkIfmarked(touchedPoints, pointOnUnitSphere*settings.radius, interaction.brushSize*settings.radius);
                    dist = (pointOnUnitSphere*settings.radius - interaction.interactionPoint).magnitude;
                    if(dist < interaction.brushSize){
                        mask = (interaction.brushSize-dist)/interaction.brushSize;
                        if(masks.ContainsKey(pointStr)){
                            //Debug.Log("Point Updated");
                            masks[pointStr] = Sigmoid(masks[pointStr] + mask);    
                        }
                        else{
                            //Debug.Log("point Added");
                            masks.Add(pointStr, mask);
                        }
                    }
                    else{
                        // if dictionary contains value set to value otherwise 0
                        mask = (masks.ContainsKey(pointStr) ? masks[pointStr]: 0f);
                    }
                }
                noiseelevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        elevation += noiseelevation;
        if (craterHeight < 0)
        {
            //Remove any mountain and add the height of the crater at this position
            elevation += craterHeight - noiseelevation;
        }
        if (craterHeight > 0)
        {
            //Make rims of craters look better with noise
            elevation += (craterHeight + noiseelevation/2)/2;
        }
        if (elevation < -1.2f)
        {
            elevation = -1.2f;
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

    private float Sigmoid(float value){
        return 1.0f / (1.0f + (float) Math.Exp(-value));
    }      
}
