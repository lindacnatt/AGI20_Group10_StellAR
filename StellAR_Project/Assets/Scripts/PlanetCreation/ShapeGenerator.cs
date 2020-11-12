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
    List<Dictionary<String, float>> masks;

    public ShapeGenerator(ShapeSettings settings, MouseInteraction interaction, CraterGenerator craterGenerator){
        this.settings = settings;
        noiseFilters = new NoiseInterface[settings.noiseLayers.Length];
        
        this.interaction = interaction;
        this.masks = new List<Dictionary<string, float>>();

        for (int i = 0; i < noiseFilters.Length; i++){
            masks.Add(new Dictionary<string, float>());
            noiseFilters[i] = NoiseFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }

        elevationMinMax = new MinMax();
        this.craterGenerator = craterGenerator;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere) {
        float craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        //Debug.Log(craterHeight);
        //float firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
        float elevation = 0;
        float noiseelevation = 0;
        float mask;//should be changed depending on maskType
        float dist;
        String pointStr = pointOnUnitSphere.ToString();

        for (int i = 0; i < noiseFilters.Length; i++) {
            mask = 1.0f;
            if (settings.noiseLayers[i].enabled) {
                if (settings.noiseLayers[i].useMouseAsMask) {
                    if (interaction.noiseType == i) {
                        // check if the point is in radius of the painted vertices 
                        dist = (pointOnUnitSphere * settings.radius - interaction.interactionPoint).magnitude;
                        if (dist < interaction.brushSize) {
                            // the mask is the distance from point to brush
                            mask = (interaction.brushSize - dist) / interaction.brushSize;
                            mask *= 0.2f;
                            if (masks[i].ContainsKey(pointStr)) {
                                if (masks[i][pointStr] >= 1f) {
                                    masks[i][pointStr] = 1f;
                                }
                                else {
                                    masks[i][pointStr] += mask;
                                }
                            }
                            else {
                                masks[i].Add(pointStr, mask);
                            }
                        }
                        else {
                            // if dictionary contains value set to value otherwise 0
                            mask = (masks[i].ContainsKey(pointStr) ? masks[i][pointStr] : 0f);
                        }
                    }
                    else {
                        mask = (masks[i].ContainsKey(pointStr) ? masks[i][pointStr] : 0f);
                    }
                }
                noiseelevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        elevation += noiseelevation;
        elevation += craterHeight;
        if (craterHeight < 0)
        {
            elevation += -noiseelevation*0.8f;
        }
        elevation = settings.radius * (1 + elevation);
        if (elevation < settings.radius && settings.zeroLvlIsOcean)
        {
            elevation = settings.radius;
        }
        if (settings.zeroLvlIsOcean)
        {
            elevationMinMax.AddValue(Mathf.Max(elevation, settings.radius));
        }
        else
        {
            elevationMinMax.AddValue(Mathf.Max(elevation, settings.radius -1.5f));
        }
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
