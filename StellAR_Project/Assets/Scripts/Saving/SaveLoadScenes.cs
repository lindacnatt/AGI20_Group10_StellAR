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
            if(sceneIndex == 0){
                SaveLoadStarSystem.SaveStarSystem();
                save=false;
            }
            if (sceneIndex == 5)
            {
                GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
                foreach (GameObject planet in planets)
                {
                    MotherPlanet mp = planet.GetComponent<MotherPlanet>();
                    SaveLoadStarSystem.SavePlanet(mp);
                }
                save = false;
            }
        }
        if(load){
            if(sceneIndex == 0){
                SystemSimulationData data = SaveLoadStarSystem.LoadStarSystem();
                if( data != null){

                    CelestialObject.DestroyAll();
                    
                    for(int i=0; i<data.planetCount; i++){
                        GameObject obj = Instantiate(prefab);
                        obj.GetComponent<CelestialObject>().enabled = true;
                        CelestialObject co = obj.GetComponent<CelestialObject>();
                        co.SetState(data.physicsData[i]);
                    }
                    load=false;
                }   
            }
            else if (sceneIndex == 6){
                PlanetData data = SaveLoadStarSystem.LoadPlanets();
                if (data != null)
                {
                    CelestialObject.DestroyAll();
                    GameObject obj = Instantiate(prefab);
                    obj.GetComponent<MotherPlanet>().enabled = true;
                    MotherPlanet mo = obj.GetComponent<MotherPlanet>();
                    mo.GeneratePlanet();
                    mo.SetShape(data);
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
