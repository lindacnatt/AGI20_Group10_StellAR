using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferPlanetName: MonoBehaviour
{
    public InputField field;

    void Awake(){
        field = this.gameObject.GetComponent<InputField>();

    }

    public void SetName(){
        PlayerPrefs.SetString("NewPlanetName", field.text);
    }
}
