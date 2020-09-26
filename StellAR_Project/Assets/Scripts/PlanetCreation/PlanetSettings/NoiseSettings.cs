using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {
    [Range(1, 8)]
    public int numLayers;
    public float fallof = 0.5f;
    public float freqPower;
    public float freq;
    public float minValue = .5f;
    public float amplitude = 1.0f;
    public Vector3 noiseCenter;
}
