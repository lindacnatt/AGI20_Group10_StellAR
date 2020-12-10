using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNameMovement : MonoBehaviour
{

    GameObject planet;
    float translation;

    void FixedUpdate(){
        if(planet != null){
            this.transform.position = planet.transform.position + new Vector3(0f,translation, 0f);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void SetPlanet(GameObject p, float t){
        planet = p;
        translation = t;
    }
}
