using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTint : MonoBehaviour
{

     Color newTint;
     MotherPlanet planet;
     
    // Start is called before the first frame update
    void Start()
    {
        planet = gameObject.GetComponent<MotherPlanet>();

    }


    public void ChangeTintCol(float value){ 

        planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.r = value;
        planet.GenerateColors(); 

    }

}
