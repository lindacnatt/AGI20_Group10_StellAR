using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOcean : MonoBehaviour
{

    Button oceanBtn;
    Text oceanBtnTxt;
    MotherPlanet planet;
    Color toggledTxtColor;
    Color untoggledTxtColor;
    Color toggledColor;
    Color untoggledColor;
    // Start is called before the first frame update
    void Start()
    {
        planet = gameObject.GetComponent<MotherPlanet>();
        toggledTxtColor = new Color(1.0f, 1.0f, 1.0f);
        untoggledTxtColor = new Color(0.5f, 0.5f, 0.5f);
        toggledColor = new Color(0.1f, 0.0f, 0.7f);
        untoggledColor = new Color(0.2f, 0.2f, 0.5f);
    }

    // Update is called once per frame
    public void toggleOcean()
    {
        planet.shapeSettings.zeroLvlIsOcean ^= true;
        oceanBtn = GameObject.Find("ToggleWater").GetComponent<Button>();
        oceanBtnTxt = oceanBtn.GetComponentInChildren<Text>();
        if (planet.shapeSettings.zeroLvlIsOcean)
        {
            oceanBtn.GetComponent<Image>().color = toggledColor;
            oceanBtnTxt.color = toggledTxtColor;
        }
        else
        {
            oceanBtn.GetComponent<Image>().color = untoggledColor;
            oceanBtnTxt.color = untoggledTxtColor;
        }
        planet.shapeGenerator.elevationMinMax = new MinMax();
        Debug.Log("Min: " + planet.shapeGenerator.elevationMinMax.Min);
        Debug.Log("Max: " + planet.shapeGenerator.elevationMinMax.Max);
        planet.UpdateMesh();
        planet.GenerateColors();
    }

}
