using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadScenes : MonoBehaviour
{
   
    public int sceneIndex = 0;
    public bool save = false;
    public bool load = false;
    public bool delete = false;
    public GameObject prefab;


    void Update()
    {
        if(save){
            if(sceneIndex == 0){
                SaveLoadStarSystem.SaveStarSystem();
                save=false;
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
