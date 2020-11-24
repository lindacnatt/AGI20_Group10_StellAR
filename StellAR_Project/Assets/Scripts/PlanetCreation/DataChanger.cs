using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChanger{
    public static string[] getKeysFromDict(Dictionary<string, float> dict){
        string[] keys = new string[dict.Count];
        int i = 0;
        foreach (KeyValuePair<string, float> entry in dict){
            keys[i] = entry.Key;
            i++;
        }
        return keys;
    }

    public static float[] getValuesFromDict(Dictionary<string, float> dict){
        float[] values = new float[dict.Count];
        int i = 0;
        foreach (KeyValuePair<string, float> entry in dict){
            values[i] = entry.Value;
            i++;
        }
        return values;
    }

    public static Dictionary<string, float> arraysToDict(string[] keys, float[] values){
        Dictionary<string, float> dict = new Dictionary<string, float>();
        for(int i = 0; i < keys.Length; i++){
            dict[keys[i]] = values[i];
        }

        return dict;
    }

}
