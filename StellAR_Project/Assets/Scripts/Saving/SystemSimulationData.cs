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
    public bool gravityState;

    public SystemSimulationData(int objectCount, bool addNew){
        
        int rockyplanetcount = 0;
        int gasyplanetcount = 0;
        int count = 0;
        Debug.Log("objectcount");
        Debug.Log(objectCount);
        for (int i = 0; i < objectCount; i++)
        {
            CelestialObject co = CelestialObject.Objects[i];
            if(!co.staticBody && co.tag != "Asteroid"){

                if (co.tag == "Planet")
                {
                    rockyplanetcount += 1;
                    count +=1;
                }
                else if (co.tag == "GasPlanet")
                {
                    gasyplanetcount += 1;
                    count +=1;
                }
            }
        }
        physicsData = new _celestialObject[count];
        planetList = new _planet[rockyplanetcount];
        gasPlanetList = new _gasPlanet[gasyplanetcount];
        planetCount = count;
        this.addNew = addNew;
        gravityState = ToggleGravityMode.nBodyGravity;
        CollectData();
    }

    public void CollectData(){
        //increase starting i if adding to existing system?
        int rockyAdded = 0;
        int gasyAdded = 0;
        int count=0;
        for(int i=0; i<CelestialObject.Objects.Count; i++){
            CelestialObject co = CelestialObject.Objects[i];
            if(!co.staticBody){
                
                physicsData[count] = new _celestialObject(co, count);
                IcoPlanet ip = co.GetComponentInChildren<IcoPlanet>();
                if (ip != null)
                {
                    planetList[rockyAdded] = new _planet(ip, count);
                    rockyAdded += 1;
                }
                GasPlanetShaderMAterialPropertyBlock gp = co.GetComponentInChildren<GasPlanetShaderMAterialPropertyBlock>();
                if (gp != null)
                {
                    gasPlanetList[gasyAdded] = new _gasPlanet(gp, count);
                    gasyAdded += 1;
                }
                count +=1;
            }
        }
    }
}
