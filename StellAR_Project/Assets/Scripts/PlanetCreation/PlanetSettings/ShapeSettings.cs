using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject{
    public float radius = 0.5f;
    public bool zeroLvlIsOcean = true;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer{
        public bool enabled = true;
        public bool useFirstLayerAsMask = false;
        public bool useMouseAsMask = true;
        public NoiseSettings noiseSettings;
    }

}
