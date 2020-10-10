using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgeNoise: NoiseInterface{
    SimplexNoise noise = new SimplexNoise();
    public NoiseSettings settings;
    
    public RidgeNoise(NoiseSettings settings){
        this.settings = settings;
    }
    public float Evaluate(Vector3 point){
        float freq = settings.freq;
        float amplitude = settings.amplitude;
        float noiseValue = 0;

        for(int i = 0; i < settings.numLayers; i++){ // add noise of increasing frequencies 
            float v = 1 - Mathf.Abs(noise.Evaluate((point+this.settings.noiseCenter)*freq));
            //v *= v;
            noiseValue += v*amplitude;
            
            freq *= settings.freqPower;
            amplitude *= settings.fallof; 
        }
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue); // create a "floor" for the planet
        return noiseValue;
    }
}
