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
        float amplitude = settings.amplitude;
        return 1f*amplitude;
    }
}
