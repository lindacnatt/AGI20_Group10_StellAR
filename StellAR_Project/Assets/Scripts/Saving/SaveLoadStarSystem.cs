using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadStarSystem 
{
    public static void SaveStarSystem(){
        SystemSimulationData simData = new SystemSimulationData(CelestialObject.Objects.Count);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath +"/system.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, simData);
        stream.Close();

    }

    public static SystemSimulationData LoadStarSystem(){
        string path = Application.persistentDataPath + "/system.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SystemSimulationData simData = formatter.Deserialize(stream) as SystemSimulationData;
            stream.Close();
            return simData;
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
