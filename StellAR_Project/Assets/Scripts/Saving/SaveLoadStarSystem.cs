using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveLoadStarSystem
{
    
    public static bool SaveStarSystem(){
        SystemSimulationData simData = new SystemSimulationData(CelestialObject.Objects.Count);

        string path = Application.persistentDataPath +"/system.data";
        string content = JsonUtility.ToJson(simData);

        try{
            File.WriteAllText(path,content);
            Debug.Log(content);
            return true;
        }
        catch(System.Exception e){
            Debug.LogError($"Failed to write to path {path} with execption {e}");
        }
        return false;

    }

    public static SystemSimulationData LoadStarSystem(){
        string path = Application.persistentDataPath + "/system.data";
        if(File.Exists(path)){
            string result = File.ReadAllText(path);
            SystemSimulationData data = JsonUtility.FromJson<SystemSimulationData>(result);
            //Debug.Log(data.physicsData[1].position.ToString("F3"));
            
            TrajectoryVelocity.startSlingshot = false;
            TrajectoryVelocity.start = new Vector3(0f,0f,0f);
            SimulationPauseControl.gameIsPaused = false;
            TrajectorySimulation.drawLine = false;
            TrajectorySimulation.destroyLine = false;
            TrajectorySimulation.freeze = false;
            TrajectorySimulation.shoot = false;

            return data;
        }
        else{
            return null;
        }
    }

    public static void DeleteStarSystem(){
        string path = Application.persistentDataPath + "/system.data";
        if(File.Exists(path)){
            File.Delete(path);
        }
    }
    
}
