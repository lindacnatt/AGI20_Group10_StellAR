using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraterGenerator 
{
    //ShapeSettings setings;
    CraterSettings craterSettings;

    public CraterGenerator(CraterSettings craterSettings)
    {
        if (craterSettings != null)
        {
            this.craterSettings = craterSettings;
        }
        else
        {
            //this.craterSettings = SettingSpawner.loadDefaultCraters();
        }
        //this.setings = setings;
    }

    public class Crater
    {
        public Vector3 center;
        public float radius;
        public float floor;
        public float smoothness;
        public float impact;
        public Crater(Vector3 center, float radius, float floor, float smoothness, float impact)
        {
            this.center = center;
            this.radius = radius;
            this.floor = floor;
            this.smoothness = smoothness;
            this.impact = impact;
    }
    };

    public float CalculateCraterDepth(Vector3 vertexPos)
    {
        float craterHeight = 0;
        //Will need some adjustments for more than one craters
        List<Crater> craterList = craterSettings.craterList;
        for (int i = 0; i < craterList.Count; i++)
        {
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
                craterHeight += craterShape * craterList[i].radius * craterSettings.impact;
            }
            /*
            if (craterHeight >= -0.2 && craterHeight < 0.2)
            {
                craterHeight = 0;
            }
            if (craterHeight >= 0.2 && craterHeight < 0.4)
            {
                craterHeight = 0.2f;
            }
            if (craterHeight >= 0.4 && craterHeight < 0.6)
            {
                craterHeight = 0.4f;
            }
            if (craterHeight >= 0.6)
            {
                craterHeight = 0.6f;
            }
            if (craterHeight <= -0.2 && craterHeight > -0.4)
            {
                craterHeight = -0.2f;
            }
            if (craterHeight <= -0.4 && craterHeight > -0.6)
            {
                craterHeight = -0.4f;
            }
            if (craterHeight <= -0.6)
            {
                craterHeight = -0.6f;
            }
            */
        }
        return craterHeight;
    }

    public void CreateCrater(Vector3 pos)
    {
        if (craterSettings.craterList != null)
        {
            craterSettings.craterList.Add(new Crater(pos,
                craterSettings.radius, craterSettings.floorHeight,
                craterSettings.smoothness, craterSettings.impact));
        }
        else
        {
            Debug.Log("craterlist null");
        }
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

}
