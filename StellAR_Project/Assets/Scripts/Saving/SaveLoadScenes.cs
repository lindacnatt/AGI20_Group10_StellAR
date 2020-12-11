using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadScenes : MonoBehaviour
{

    public int sceneIndex;
    public bool save = false;
    public bool load = false;
    public bool loadNewPlanet = false;
    public bool delete = false;
    public GameObject rockPrefab;
    public GameObject gasPrefab;
    bool gasy = false;
    bool rocky = false;
    int startingIndex = 0;
    bool saveSpecific = false;
    bool loadSpecific = false;
    string systemName;
    string delSystemName;

    private static bool firstGenSystemSaved = false;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 1)
        {
            load = true;
            loadNewPlanet = true;
        }
    }

    void Update()
    {
        if (save){
            if(sceneIndex == 2){
                //Save one planet to newPlanet.data
                SaveLoadStarSystem.SaveStarSystem(true, "/newPlanet.data");
                save=false;
            }
            else if (sceneIndex == 1)
            {
                //Save the entire system to system.data
                if (saveSpecific)
                {
                    SaveLoadStarSystem.SaveSpecificStarSystem(false, systemName + ".data");
                    saveSpecific = false;
                }
                else
                {
                    SaveLoadStarSystem.SaveStarSystem(false, "/system.data");
                    if (!firstGenSystemSaved)
                    {
                        firstGenSystemSaved = true;
                    }
                }
                save = false;
            }
        }
        if(load){
            if(sceneIndex == 1){
                //Load the entire system from system.data
                SystemSimulationData data;
                if (loadSpecific)
                {
                    data = SaveLoadStarSystem.LoadSavedStarSystem(systemName);
                    loadSpecific = false;
                }
                else
                {
                    if (firstGenSystemSaved)
                    {
                        data = SaveLoadStarSystem.LoadStarSystem(false);

                    }
                    else
                    {
                        data = null;
                    }
                    load = false;
                }
                if ( data != null){

                    CelestialObject.DestroyAll();
                    int rocky_i = 0;
                    int gasy_i = 0;
                    for (int i=0; i<data.planetCount; i++){
                        GameObject obj = getPrefab(data, i);
                        obj.GetComponent<CelestialObject>().enabled = true;
                        CelestialObject co = obj.GetComponent<CelestialObject>();
                        co.SetState(data.physicsData[i]);

                        MotherPlanet mp = obj.GetComponentInChildren<MotherPlanet>();
                        if (mp != null)
                        {
                            mp.GeneratePlanet();
                            mp.SetShape(data.planetList[rocky_i]);
                            mp.UpdateMesh();
                            rocky_i += 1;
                        }
                        GasPlanetShaderMAterialPropertyBlock gp = co.GetComponentInChildren<GasPlanetShaderMAterialPropertyBlock>();
                        if (gp != null)
                        {
                            gp.SetMaterial(data.gasPlanetList[gasy_i]);
                            gasy_i += 1;
                        }

                    }
                    load =false;
                }
                else
                {
                    load = false;
                    Debug.Log("failed to load entire system");
                }
            } 
        }
        if (loadNewPlanet)
        {
            if (sceneIndex == 1)
            {
                loadNewPlanet = false;
                //Load one planet from newPlanet data and p
                SystemSimulationData data = SaveLoadStarSystem.LoadStarSystem(true);
                if (data != null)
                {

                    //CelestialObject.DestroyAll();
                    int rocky_i = 0;
                    int gasy_i = 0;
                    for (int i = 0; i < data.planetCount; i++)
                    {
                        GameObject obj = getPrefab(data, i);
                        CelestialObject co = GetComponent<CelestialObject>();
                        if(co != null){
                            obj.AddComponent(typeof(CelestialObject));
                        }
                        //obj.GetComponent<CelestialObject>().enabled = true;

                        MotherPlanet mp = obj.GetComponentInChildren<MotherPlanet>();
                        if (mp != null)
                        {
                            mp.GeneratePlanet();
                            mp.SetShape(data.planetList[rocky_i]);
                            mp.UpdateMesh();
                            mp.GenerateColors();
                            mp.GetComponent<IcoPlanet>().staticBody=false;
                            mp.GetComponent<IcoPlanet>().enabled=false;

                            rocky_i += 1;
                        }
                        GasPlanetShaderMAterialPropertyBlock gp = obj.GetComponentInChildren<GasPlanetShaderMAterialPropertyBlock>();
                        if (gp != null)
                        {
                            gp.SetMaterial(data.gasPlanetList[gasy_i]);
                            gp.GetComponent<GasPlanetShaderMAterialPropertyBlock>().enabled = true;
                            gasy_i += 1;
                        }
                        string newPlanetName = PlayerPrefs.GetString("NewPlanetName", "Unknown Planet");
                        obj.GetComponent<CelestialObject>().SetName(newPlanetName);
                        PlayerPrefs.SetString("NewPlanetName", "Unknown Planet");
                        
                        obj.GetComponent<CelestialObject>().SetMass();
                        GameObject ARSessOrig = GameObject.Find("AR Session Origin");
                        ARPlacementTrajectory placement = ARSessOrig.GetComponent<ARPlacementTrajectory>();
                        placement.setGOtoInstantiate(obj);

                    }
                }
            }
        }
        if(delete){
            if(sceneIndex == 1){
                SaveLoadStarSystem.DeleteStarSystem(delSystemName);
                delete=false;
            }
        }

        
    }

    GameObject getPrefab(SystemSimulationData data, int i)
    {
        for (int j = 0; j < data.planetList.Length; j++)
        {
            if (data.planetList[j].id == data.physicsData[i].id)
            {
                return Instantiate(rockPrefab);
            }
        }
        for (int j = 0; j < data.gasPlanetList.Length; j++)
        {
            if (data.gasPlanetList[j].id == data.physicsData[i].id)
            {
                return Instantiate(gasPrefab);
            }
        }
        return Instantiate(gasPrefab);
    }

    public void ToggleSave(){
        save = !save;
    }

    public void ToggleLoad(){
        load = !load;

    }

    public void ToggleDelete(){
        delete = !delete;
    }

    public void saveSpecificSystem(string name)
    {
        save = true;
        saveSpecific = true;
        systemName = name;
    }

    public void loadSpecificSystem(string name)
    {
        load = true;
        loadSpecific = true;
        systemName = name;
    }

    public void delSpecificSystem(string name)
    {
        delete = true;
        delSystemName = name;
    }

    public void NextScene()
    {
        SaveLoadStarSystem.SaveStarSystem(true, "/newPlanet.data");
        save = false;
        SceneManager.LoadScene(1);
    }
}
