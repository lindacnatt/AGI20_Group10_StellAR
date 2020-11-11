using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFromSliderValue : MonoBehaviour
{
    Text valueText;
    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponent<Text>(); //Gets the text part of the textobject this script is applied to
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateText (float value) //Called from the eventhandler of the slider-object
    {
        valueText.text = (Mathf.Round(value * 100) / 100).ToString("f2");
    }
}
