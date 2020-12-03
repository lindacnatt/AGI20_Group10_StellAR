using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {
    public enum FilterType {Simple, Rigid, LandMass, OceanBed};
    public FilterType filterType;

    [Range(1, 8)]
    public int numLayers = 4;
    public float fallof = 0.5f;
    public float freqPower = 2f;
    public float freq = 1f;
    public float minValue = .5f;
    public int clampSteps = 3;
    [Range(0, 3)]
    public float amplitude = 1.0f;
    public Vector3 noiseCenter = Vector3.zero;
}
