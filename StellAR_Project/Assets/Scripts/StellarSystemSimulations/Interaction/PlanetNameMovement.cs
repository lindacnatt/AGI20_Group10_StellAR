using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNameMovement : MonoBehaviour
{

    GameObject planet;

    void FixedUpdate(){
        if(planet != null){
            transform.position = planet.transform.position + new Vector3(0f,0.2f, 0f);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void SetPlanet(GameObject p){
        planet = p;
    }
}
