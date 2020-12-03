using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeChanger : MonoBehaviour
{
    private GameObject Planet = null;
    private bool Gas = false;
    public Slider SizeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
        if (Planet == null)
        {
            Planet = GameObject.FindGameObjectWithTag("Planet");
            if(Planet == null)
            {
                Planet = GameObject.FindGameObjectWithTag("GasPlanet");
                Gas = true;
            }
        }
        if (Gas)
        {

            SizeSlider.value = 11.20f; //Defaults to Jupiter
            SizeSlider.minValue = 2.50f;
            SizeSlider.maxValue = 14.00f; //Jupiter is 11.2, 95% of all exo planets nasa has confirmed has a radius lower than 13.25
            SizeUpdate(11.20f);
        }
        else {
            
            SizeSlider.value = 1.00f; //Defaults to Earth
            SizeSlider.minValue = 0.30f; //Slightly smaller than Mercury
            SizeSlider.maxValue = 2.50f; //Super-terrans
            SizeUpdate(1.00f);
        }
    }

    public void SizeUpdate(float value) {
        value = Mathf.Log(value*100)/5;
        if (Gas)
        {
            Planet.transform.localScale = new Vector3(value * 2, value * 2, value * 2); //localscale adjusts diameter, to keep consistency with rocky icospheres we halve it to get a radius

        }
        else
        {
            Planet.GetComponent<IcoPlanet>().shapeSettings.radius = value;
            Planet.GetComponent<IcoPlanet>().UpdateMesh();

            
        }
    }
}
