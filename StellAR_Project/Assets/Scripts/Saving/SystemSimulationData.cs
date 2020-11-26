using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemSimulationData
{
    
    public _celestialObject[] physicsData;
    //public _planet planetData;
    public _planet[] planetList;
    public _gasPlanet[] gasPlanetList;
    public int planetCount;
    public bool addNew = false;

    public SystemSimulationData(int objectCount, bool addNew){
        physicsData = new _celestialObject[objectCount];
        int rockyplanetcount = 0;
        int gasyplanetcount = 0;
        for (int i = 0; i < objectCount; i++)
        {
            CelestialObject co = CelestialObject.Objects[i];
            if (co.tag == "Planet")
            {
                rockyplanetcount += 1;
            }
            else if (co.tag == "GasPlanet")
            {
                gasyplanetcount += 1;
            }
        }
        planetList = new _planet[rockyplanetcount];
        gasPlanetList = new _gasPlanet[gasyplanetcount];
        planetCount = objectCount;
        this.addNew = addNew;
        CollectData();
    }

    public void CollectData(){
        //increase starting i if adding to existing system?
        int rockyAdded = 0;
        int gasyAdded = 0;
        for(int i=0; i<planetCount; i++){
            CelestialObject co = CelestialObject.Objects[i];
            physicsData[i] = new _celestialObject(co, i);
            MotherPlanet mp = co.GetComponentInChildren<MotherPlanet>();
            if (mp != null)
            {
                planetList[rockyAdded] = new _planet(mp, i);
                rockyAdded += 1;
            }
            GasPlanetShaderMAterialPropertyBlock gp = co.GetComponentInChildren<GasPlanetShaderMAterialPropertyBlock>();
            if (gp != null)
            {
                gasPlanetList[gasyAdded] = new _gasPlanet(gp, i);
                gasyAdded += 1;
            }
        }
    }
}
