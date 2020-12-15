using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeGenerator {
    public ShapeSettings settings;
    NoiseInterface[] noiseFilters;
    //MouseInteraction interaction;
    Interactor interaction;
    List<Vector3> touchedPoints;
    public MinMax elevationMinMax;
    public CraterGenerator craterGenerator;
    public List<Dictionary<String, float>> masks;

    public List<string> maskKeys;
    public List<float> maskValues;

    public ShapeGenerator(ShapeSettings settings, Interactor interaction, CraterGenerator craterGenerator){
        this.settings = settings;
        noiseFilters = new NoiseInterface[settings.noiseLayers.Length];
        
        this.interaction = interaction;
        this.masks = new List<Dictionary<string, float>>(); 
        //this.masks.Add(DataChanger.arraysToDict(maskKeys, maskValues));
        
        this.maskKeys = new List<string>();
        this.maskValues = new List<float>();

        for (int i = 0; i < noiseFilters.Length; i++){
            masks.Add(new Dictionary<string, float>());
            noiseFilters[i] = NoiseFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }

        elevationMinMax = new MinMax();
        this.craterGenerator = craterGenerator;
        settings.zeroLvlIsOcean = true;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere) {
        float craterHeight = craterGenerator.CalculateCraterDepth(pointOnUnitSphere);
        float elevation = 0;
        float noiseelevation = 0;
        float mask; 
        float dist;
        String pointStr = pointOnUnitSphere.ToString();

        for (int i = 0; i < noiseFilters.Length; i++) {
            mask = 0f;
            if (settings.noiseLayers[i].enabled) {
                if (settings.noiseLayers[i].useMouseAsMask) {
                    if (interaction.noiseType == i) {
                        // check if the point is in radius of the painted vertices 
                        dist = (pointOnUnitSphere * settings.radius - interaction.interactionPoint).magnitude;
                        if (dist <= interaction.brushSize) {
                            // the mask is the distance from point to brush
                            mask = (interaction.brushSize - dist) / interaction.brushSize;
                            mask *= 0.05f;
                            if (masks[i].ContainsKey(pointStr)) {
                                masks[i][pointStr] += mask;
                                if (masks[i][pointStr] >= 1f) {
                                    masks[i][pointStr] = 1f;
                                }
                                mask = masks[i][pointStr];
                            }
                            else{
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
        if(interaction.noiseType == -1){
            if (masks[0].ContainsKey(pointStr)) {
                if(masks[0][pointStr] > 0.01){
                    dist = (pointOnUnitSphere * settings.radius - interaction.interactionPoint).magnitude;
                    if (dist <= interaction.brushSize) {
                        mask = (interaction.brushSize - dist) / interaction.brushSize;
                        masks[0][pointStr] *= 1 - mask;
                    }
                }
            }
        }

        elevation += noiseelevation;
        elevation += craterHeight;
        if (craterHeight < 0)
        {
            elevation += -noiseelevation*0.65f;
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
            elevationMinMax.AddValue(Mathf.Max(elevation, settings.radius -1f));
        }
        return pointOnUnitSphere * elevation;
    }
    
    public string[] getMaskKeys(){
        return DataChanger.getKeysFromDict(masks[0]);
    }

    public float[] getMaskValues(){
        return DataChanger.getValuesFromDict(masks[0]);
    }

    public void checkIfCrater(Vector3 point)
    {

    }
}
