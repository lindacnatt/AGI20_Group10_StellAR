using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    public GameObject Planet;
    // Start is called before the first frame update
    void Start()
    {
        if (Planet == null)
            Planet = GameObject.FindGameObjectWithTag("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SizeUpdate(float value) { 
        if (Planet.GetComponent<IsTypeOfPlanet>().IsGassy == true)
        {
            Debug.Log(value);
            Planet.transform.localScale = new Vector3(value/2, value/2, value/2); //localscale adjusts diameter, to keep consistency with rocky icospheres we halve it to get a radius
        }
        if (Planet.GetComponent<IsTypeOfPlanet>().IsRocky == true)
        {
            Planet.GetComponent<IcoPlanet>().shapeSettings.radius = value;
            Planet.GetComponent<IcoPlanet>().UpdateMesh();

            Debug.Log(Planet.GetComponent<IcoPlanet>().shapeSettings.radius);

            
        }
    }
}
