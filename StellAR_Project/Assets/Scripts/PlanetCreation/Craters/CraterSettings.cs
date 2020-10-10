using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraterSettings : ScriptableObject
{
	public float radius = 0.2f;
	public int numCraters = 1;
	public float rimSteepness = 0.5f;
	public float rimWidth = 0.5f;
	public float smoothness = 0.0f;
	public float floorHeight = -1.0f;
	public float impact = 1.5f;
}
