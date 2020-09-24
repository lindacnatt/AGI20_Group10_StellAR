using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter {
    SimplexNoise noise = new SimplexNoise();
    public NoiseSettings settings;
    
    public NoiseFilter(NoiseSettings settings){
        this.settings = settings;
    }
    public float Evaluate(Vector3 point){
        float freq = settings.freq;
        float amplitude = settings.amplitude;
        float noiseValue = 0;

        for(int i = 0; i < settings.numLayers; i++){ // add noise of increasing frequencies 
            float v = (noise.Evaluate((point+this.settings.noiseCenter)*freq)+1)*0.5f;
            noiseValue += v*amplitude;
            
            freq *= settings.freqPower;
            amplitude *= settings.fallof; 
        }
        noiseValue = Mathf.Max(settings.minValue, noiseValue); // create a "floor" for the planet
        return noiseValue;
    }
}
