using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct _crater
{
    //This contains all crater data serialized for one crater
    public Vector3 center;
    public float radius;
    public float floor;
    public float smoothness;
    public float impact;
    public float rimSteepness;
    public float rimWidth;
    public _crater(Vector3 center, float radius, float floor,
            float smoothness, float impact, float rimSteepness, float rimWidth)
    {
        this.center = center;
        this.radius = radius;
        this.floor = floor;
        this.smoothness = smoothness;
        this.impact = impact;
        this.rimSteepness = rimSteepness;
        this.rimWidth = rimWidth;
    }
}
