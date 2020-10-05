using UnityEngine;

[CreateAssetMenu()]
public class MoonShape : CelestialBodyShape
{

	public CraterSettings craterSettings;

	public Vector4 testParams;

	protected override void SetShapeData()
	{
		var prng = new PRNG(seed);

		//SetCraterSettings(prng, seed, randomize);

		heightMapCompute.SetVector("testParams", testParams);
	}
	/*
	void SetCraterSettings(PRNG prng, int seed, bool randomizeValues)
	{
		if (randomizeValues)
		{
			var chance = new Chance(prng);
			if (chance.Percent(70))
			{ // Medium amount of mostly small to medium craters
				craterSettings.SetComputeValues(heightMapCompute, seed, prng.Range(100, 700), new Vector2(0.01f, 0.1f), 0.57f);
			}
			else if (chance.Percent(15))
			{ // Many small craters
				craterSettings.SetComputeValues(heightMapCompute, seed, prng.Range(800, 1800), new Vector2(0.01f, 0.08f), 0.74f);
			}
			else if (chance.Percent(15))
			{ // A few large craters
				craterSettings.SetComputeValues(heightMapCompute, seed, prng.Range(50, 150), new Vector2(0.01f, 0.2f), 0.4f);
			}
		}
		else
		{
			craterSettings.SetComputeValues(heightMapCompute, seed);
		}
	}
	*/

}