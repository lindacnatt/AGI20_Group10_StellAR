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

    public void ChangeTintCol(Vector3 weights){ 
        planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.r = weights[0];
        planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.g = weights[1];
        planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.b = weights[2];
        //planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.r = value;
        planet.GenerateColors(); 

    }

}
