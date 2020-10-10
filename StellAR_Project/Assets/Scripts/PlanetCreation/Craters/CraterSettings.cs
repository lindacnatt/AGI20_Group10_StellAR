using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraterSettings : ScriptableObject
{
	public float radius = 0.01f;
	public int numCraters = 1;
	public float rimSteepness = 0.13f;
	public float rimWidth = 1.6f;
	public float smoothness = 0.0f;
	public float floorHeight = -1.0f;
	public float impact = 1.0f;
}
