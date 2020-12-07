using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class SizeChanger : MonoBehaviour
{
    public GameObject Planet;
    public Slider SizeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (Planet == null)
        {
            Planet = GameObject.FindGameObjectWithTag("Planet");
        }
        if (Planet.GetComponent<IsTypeOfPlanet>().IsGassy == true)
        {

            SizeSlider.value = 11.20f; //Defaults to Jupiter
            SizeSlider.minValue = 2.50f;
            SizeSlider.maxValue = 14.00f; //Jupiter is 11.2, 95% of all exo planets nasa has confirmed has a radius lower than 13.25
            SizeUpdate(11.20f);
        }
        if (Planet.GetComponent<IsTypeOfPlanet>().IsRocky == true) {
            
            SizeSlider.value = 1.00f; //Defaults to Earth
            SizeSlider.minValue = 0.30f; //Slightly smaller than Mercury
            SizeSlider.maxValue = 2.50f; //Super-terrans
            SizeUpdate(1.00f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SizeUpdate(float value) {
        value = Mathf.Log(value*100)/5;
        if (Planet.GetComponent<IsTypeOfPlanet>().IsGassy == true)
        {
            Planet.transform.localScale = new Vector3(value * 2, value * 2, value * 2); //localscale adjusts diameter, to keep consistency with rocky icospheres we halve it to get a radius

        }
        if (Planet.GetComponent<IsTypeOfPlanet>().IsRocky == true)
        {
            Planet.GetComponent<IcoPlanet>().shapeSettings.radius = value;
            Planet.GetComponent<IcoPlanet>().UpdateMesh();
            //Debug.Log(Planet.GetComponent<IcoPlanet>().shapeSettings.radius);

            
        }
    }
    public void RandomSize(){
        float randomSize = Random.Range(0, 1);
        SizeUpdate(randomSize);
    }
       

}
