using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraterGenerator 
{
    //ShapeSettings setings;
    public CraterSettings craterSettings;
    public List<Crater> craterList;

    public CraterGenerator(CraterSettings craterSettings)
    {
        if (craterSettings != null)
        {
            this.craterSettings = craterSettings;
        }
        else
        {
            this.craterSettings = SettingSpawner.loadDefaultCraters();
        }
        this.craterList = new List<Crater>();
}

    public class Crater
    {
        public Vector3 center;
        public float radius;
        public float floor;
        public float smoothness;
        public float impact;
        public float rimSteepness;
        public float rimWidth;
        public Crater(Vector3 center, float radius, float floor,
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
    };

    public float CalculateCraterDepth(Vector3 vertexPos)
    {
        float craterHeight = 0;
        //Will need some adjustments for more than one craters
        //List<Crater> craterList = craterSettings.craterList;
        for (int i = 0; i < craterList.Count; i++)
        {
            //Debug.Log(craterList[i].center);
            Vector3 diff = vertexPos - craterList[i].center;
            float distFromCentre = diff.magnitude;
            if (distFromCentre <= craterList[i].radius + craterSettings.rimWidth)
            {
                float x = diff.magnitude / Mathf.Max(craterList[i].radius, 0.0001f);
                float cavity = x * x - 1;
                float rimX = Math.Min(Math.Abs(x) - 1 - craterSettings.rimWidth, 0);
                float rim = craterSettings.rimSteepness * rimX * rimX;

                float craterShape = smoothMax(cavity, craterList[i].floor, craterList[i].smoothness);
                craterShape = smoothMin(craterShape, rim, craterList[i].smoothness);
                craterHeight += craterShape * craterList[i].radius * craterList[i].impact;
            }
        }
        return craterHeight;
    }

    public void PlaceCrater(Vector3 position)
    {
        Vector3 pos = position.normalized;
        bool sameCrater = false;
        int index = 0;
        for (int i = 0; i < craterList.Count; i++)
        {   
                
            if (craterList[i].center == pos)
            {
                sameCrater = true;
                index = i;
            }
                
        }
        if (sameCrater)
        {
            if (craterList[index].impact < 1.5f)
            {
                craterList[index].impact += 0.03f;
            }
        }
        else
        {
            CreateCrater(pos, 0.01f);
        }

    }

    public void CreateCrater(Vector3 pos, float multiplier){   
        //Debug.Log("crater created at pos: " + pos);
        craterList.Add(new Crater(pos, craterSettings.radius, 
            craterSettings.floorHeight, craterSettings.smoothness, 
            craterSettings.impact * multiplier, craterSettings.rimSteepness,
            craterSettings.rimWidth));

    }

    public void CreateDynaCrater(Vector3 pos, float impact, float radius)
    {
        //Debug.Log("crater created at pos: " + pos);
        craterList.Add(new Crater(pos, radius,
            craterSettings.floorHeight, craterSettings.smoothness,
            impact, craterSettings.rimSteepness,
            craterSettings.rimWidth));

    }

    float smoothMin(float a, float b, float k)
    {
        float h = Mathf.Clamp01((b - a + k) / (2 * k));
        return a * h + b * (1 - h) - k * h * (1 - h);
    }

    float smoothMax(float a, float b, float k)
    {
        k = k * (-1);
        float h = Mathf.Clamp01((b - a + k) / (2 * k));
        return a * h + b * (1 - h) - k * h * (1 - h);
    }

    public void checkIfCrater(Vector3 point)
    {
        foreach (Crater crater in craterList.ToArray())
        {
            if ((point - crater.center).magnitude < crater.radius)
            {
                craterList.Remove(crater);
            }
        }
    }

}
