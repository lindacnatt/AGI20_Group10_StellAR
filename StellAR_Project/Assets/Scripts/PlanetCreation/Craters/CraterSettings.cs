using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraterSettings : ScriptableObject
{
	public float radius = 0.2f;
	public int numCraters = 1;
	public float rimSteepness = 0.4f;
	public float rimWidth = 0.4f;
	public float smoothness = 0.2f;
	public float floorHeight = -0.6f;
	public float impact = 1.5f;
}
