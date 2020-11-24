﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSimulationData
{
    
    public _celestialObject[] physicsData;
    //public _planet planetData;
    public _planet[] planetList;
    public int planetCount;

    public SystemSimulationData(int objectCount){
        physicsData = new _celestialObject[objectCount];
        planetList = new _planet[objectCount];
        planetCount = objectCount;
        CollectData();
    }

    public void CollectData(){
        for(int i=0; i<planetCount; i++){
            CelestialObject co = CelestialObject.Objects[i];
            physicsData[i] = new _celestialObject(co);
            MotherPlanet mp = co.GetComponentInChildren<MotherPlanet>();
            planetList[i] = new _planet(mp);
        }
    }
}
