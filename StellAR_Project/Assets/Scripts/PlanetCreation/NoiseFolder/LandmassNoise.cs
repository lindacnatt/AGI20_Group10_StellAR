using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmassNoise : NoiseInterface{
    SimplexNoise noise = new SimplexNoise();
    public NoiseSettings settings;
    
    public LandmassNoise(NoiseSettings settings){
        this.settings = settings;
    }
    public float Evaluate(Vector3 point){
        float freq = settings.freq;
        float amplitude = settings.amplitude;
        
        /*
        float noiseValue = 0;

        for(int i = 0; i < settings.numLayers; i++){ // add noise of increasing frequencies 
            float v = (noise.Evaluate((point+this.settings.noiseCenter)*freq)+1)*0.5f;
            v = 1/(1 + Mathf.Exp(-v)); // sigmooid function to push down edges
            noiseValue += v*amplitude;
            
            freq *= settings.freqPower;
            amplitude *= settings.fallof; 
        }
        
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue); // create a "floor" for the planet
        */
        return 0.1f * amplitude; // noiseValue;
    }
}
