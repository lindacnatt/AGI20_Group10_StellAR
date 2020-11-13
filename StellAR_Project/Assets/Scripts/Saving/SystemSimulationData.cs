using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSimulationData : MonoBehaviour
{
    public bool[] staticBodies;
    public SerializeVector3[][] physicsData;
    public int planetCount;

    public SystemSimulationData(int objectCount){
        staticBodies = new bool[objectCount];
        physicsData = new SerializeVector3[objectCount][];
        planetCount = objectCount;
        CollectData();
    }

    public void CollectData(){
        for(int i=0; i<planetCount; i++){
            CelestialObject co = CelestialObject.Objects[i];
            staticBodies[i] = co.staticBody;
            physicsData[i] = SaveObjectVecData(co);
        }
    }

    public void DeployData(){
        for(int i=0; i<planetCount; i++){
            CelestialObject co = CelestialObject.Objects[i];
            LoadObjectData(co, physicsData[i], staticBodies[i]);
        }
    }

    public SerializeVector3[] SaveObjectVecData(CelestialObject co){
        SerializeVector3[] savedObjVecData = new SerializeVector3[3];
        savedObjVecData[0] = new SerializeVector3(co.transform.position);
        savedObjVecData[1] = new SerializeVector3(co.velocity);
        savedObjVecData[2] = new SerializeVector3(co.acceleration);
        return savedObjVecData;
    }

    public void LoadObjectData(CelestialObject co, SerializeVector3[] data, bool state){
        co.transform.position = new Vector3(data[0].x, data[0].y,data[0].z);
        co.velocity = new Vector3(data[1].x, data[1].y,data[1].z);
        co.acceleration = new Vector3(data[2].x, data[2].y,data[2].z);
        co.staticBody = state;
        co.SetState();
    }
}
