using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadScenes : MonoBehaviour
{

    public int sceneIndex;
    public bool save = false;
    public bool load = false;
    public bool delete = false;
    public GameObject prefab;
    public GameObject prefab2;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 6)
        {
            load = true;
        }
    }

    void Update()
    {
        if(save){
            if(sceneIndex == 5){
                SaveLoadStarSystem.SaveStarSystem();
                save=false;
            }
            else if (sceneIndex == 5)
                //Maybe use for going from planet creation to solar system when there's just one planet in the scene.
            {
                GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
                if (planets.Length > 0)
                {
                    MotherPlanet mp = planets[0].GetComponent<MotherPlanet>();
                    SaveLoadStarSystem.SavePlanet(mp);
                }
                save = false;
            }
        }
        if(load){
            if(sceneIndex == 6){
                SystemSimulationData data = SaveLoadStarSystem.LoadStarSystem();
                if( data != null){

                    CelestialObject.DestroyAll();
                    for(int i=0; i<data.planetCount; i++){
                        GameObject obj = Instantiate(prefab);
                        obj.GetComponent<CelestialObject>().enabled = true;
                        CelestialObject co = obj.GetComponent<CelestialObject>();
                        co.SetState(data.physicsData[i]);

                        MotherPlanet mo = obj.GetComponentInChildren<MotherPlanet>();
                        mo.GeneratePlanet();
                        mo.SetShape(data.planetList[i]);
                        mo.UpdateMesh();
                        
                    }
                    load =false;
                }
            }
            else if (sceneIndex == 1){
                PlanetData data = SaveLoadStarSystem.LoadPlanets();
                if (data != null)
                {
                    CelestialObject.DestroyAll();
                    GameObject obj = Instantiate(prefab);
                    obj.GetComponent<MotherPlanet>().enabled = true;
                    MotherPlanet mo = obj.GetComponent<MotherPlanet>();
                    mo.GeneratePlanet();
                    mo.SetShape(data.planetData);
                    mo.UpdateMesh();
                    load = false;
                }
            }
            else{
                load=false;
                Debug.Log("failed to load");
            }
                
        }
        if(delete){
            if(sceneIndex == 0){
                SaveLoadStarSystem.DeleteStarSystem();
                delete=false;
            }
        }

        
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
}
