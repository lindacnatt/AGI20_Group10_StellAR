using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject{
    [Range(0, 10)]
    public float radius;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer{
        public bool enabled = true;
        public bool useFirstLayerAsMask = false;
        public bool useMouseAsMask = false;
        public NoiseSettings  noiseSettings;
    }
}
