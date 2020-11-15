using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSimulationData
{
    
    public _celestialObject[] physicsData;
    public int planetCount;

    public SystemSimulationData(int objectCount){
        physicsData = new _celestialObject[objectCount];
        planetCount = objectCount;
        CollectData();
    }

    public void CollectData(){
        for(int i=0; i<planetCount; i++){
            CelestialObject co = CelestialObject.Objects[i];
            physicsData[i] = new _celestialObject(co);
        }
    }
}
