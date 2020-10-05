using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestSettings
{
    public float testval = 0.5f;
	public int craterSeed;
	public int numCraters = 10;
	public Vector2 craterSizeMinMax = new Vector2(0.01f, 0.1f);
	public float rimSteepness = 0.13f;
	public float rimWidth = 1.6f;
	public Vector2 smoothMinMax = new Vector2(0.4f, 1.5f);
	[Range(0, 1)]
	public float sizeDistribution = 0.6f;
}
