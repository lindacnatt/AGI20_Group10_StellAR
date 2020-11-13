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
                SystemSimulationData systemData = SaveLoadStarSystem.LoadStarSystem();
                if(systemData == null){
                    return;
                }
                
                CelestialObject.Objects.Clear(); //clearing the old planets
                CelestialObject.Objects.TrimExcess();

                TrajectoryVelocity.startSlingshot = false;
                TrajectoryVelocity.start = new Vector3(0f,0f,0f);
                SimulationPauseControl.gameIsPaused = false;
                TrajectorySimulation.drawLine = false;
                TrajectorySimulation.destroyLine = false;
                TrajectorySimulation.freeze = false;
                TrajectorySimulation.shoot = false;

                for(int i=0; i<systemData.planetCount; i++){
                    GameObject obj = Instantiate(prefab);
                    obj.GetComponent<CelestialObject>().enabled = true;
                }

                systemData.DeployData();
                load=false;
            }
        if(delete){
            if(sceneIndex == 0){
                SaveLoadStarSystem.DeleteStarSystem();
                delete=false;
            }
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
