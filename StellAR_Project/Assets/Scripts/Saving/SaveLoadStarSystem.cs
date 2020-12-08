using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveLoadStarSystem
{
    
    public static bool SaveStarSystem(bool addNew, string fileName){
        SystemSimulationData simData = new SystemSimulationData(CelestialObject.Objects.Count, addNew);
        string path = Application.persistentDataPath + fileName;
        string content = JsonUtility.ToJson(simData);

        return SaveToFile(path, content);

    }

    public static bool SaveSpecificStarSystem(bool addNew, string fileName)
    {
        SystemSimulationData simData = new SystemSimulationData(CelestialObject.Objects.Count, addNew);
        string path = Application.persistentDataPath + "/savedSystems/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += fileName;
        string content = JsonUtility.ToJson(simData);

        return SaveToFile(path, content);

    }

    public static bool SaveToFile(string path, string content)
    {
        try
        {
            File.WriteAllText(path, content);
            Debug.Log(path);
            Debug.Log(content);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to write to path {path} with execption {e}");
        }
        return false;
    }

    public static SystemSimulationData LoadStarSystem(bool newPlanet){
        string path = Application.persistentDataPath + "/system.data";
        if (newPlanet)
        {
            path = Application.persistentDataPath + "/newPlanet.data";
        }
        if(File.Exists(path)){
            string result = File.ReadAllText(path);
            Debug.Log("loadResult: " + result);
            SystemSimulationData data = JsonUtility.FromJson<SystemSimulationData>(result);
            //Debug.Log(data.physicsData[1].position.ToString("F3"));
            
            TrajectoryVelocity.startSlingshot = false;
            TrajectoryVelocity.start = new Vector3(0f,0f,0f);
            SimulationPauseControl.gameIsPaused = false;
            TrajectorySimulation.drawLine = false;
            TrajectorySimulation.destroyLine = false;
            TrajectorySimulation.freeze = false;
            TrajectorySimulation.shoot = false;
            ToggleGravityMode.nBodyGravity = data.gravityState;

            return data;
        }
        else{
            return null;
        }
    }

    public static SystemSimulationData LoadSavedStarSystem(string name)
    {
        string path = Application.persistentDataPath + "/savedSystems/" + name + ".data";
        if (File.Exists(path))
        {
            string result = File.ReadAllText(path);
            Debug.Log("loadResult: " + result);
            SystemSimulationData data = JsonUtility.FromJson<SystemSimulationData>(result);
            //Debug.Log(data.physicsData[1].position.ToString("F3"));

            TrajectoryVelocity.startSlingshot = false;
            TrajectoryVelocity.start = new Vector3(0f, 0f, 0f);
            SimulationPauseControl.gameIsPaused = false;
            TrajectorySimulation.drawLine = false;
            TrajectorySimulation.destroyLine = false;
            TrajectorySimulation.freeze = false;
            TrajectorySimulation.shoot = false;
            ToggleGravityMode.nBodyGravity = data.gravityState;

            return data;
        }
        else
        {
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
