using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraterSettings : ScriptableObject
{

	// Exposed settings
	public bool enabled = true;
	public float test = 0.0f;
	public int craterSeed;
	public int numCraters = 10;
	public Vector2 craterSizeMinMax = new Vector2(0.01f, 0.1f);
	public float rimSteepness = 0.13f;
	public float rimWidth = 1.6f;
	public Vector2 smoothMinMax = new Vector2(0.4f, 1.5f);
	[Range(0, 1)]
	public float sizeDistribution = 0.6f;

}