using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MotherPlanet: CelestialObject {
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public CraterSettings craterSettings;

    public ShapeGenerator shapeGenerator;
    public CraterGenerator craterGenerator;
    public ColorGenerator colorGenerator;

    public bool autoUpdate = true;
    public bool clearCraters = false;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;
    [HideInInspector]
    public bool craterSettingsFoldout;

    Button oceanBtn;
    Text oceanBtnTxt;

    // these need to be overriden in subClass of planet
    public abstract void Initialize();  
    public abstract void GenerateMesh();
    public abstract void GenerateColors();
    public abstract void UpdateMesh();
    public abstract void OnCraterSettingsUpdated();
    //public abstract void OnCollisionEnter(Collision collision);
    
    // these can be used directly in subClass 
    void Awake(){
        GeneratePlanet();
    }
    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        UpdateMesh();
        GenerateColors();
        UpdateCollider(); 
    }

    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            UpdateMesh();
            UpdateCollider();
        }    
    }
     public void OnColorSettingsUpdated(){ //Rebuild planet when color is updated
        if(autoUpdate){
            GenerateColors();
        }
    }

    public void UpdateCollider(){
        SphereCollider collider = this.GetComponent<SphereCollider>();
        collider.radius = this.shapeSettings.radius;
    }

    public void toggleOcean()
    {
        shapeSettings.zeroLvlIsOcean ^= true;
        Color toggledTxtColor = new Color(1.0f, 1.0f, 1.0f);
        Color untoggledTxtColor = new Color(0.5f, 0.5f, 0.5f);
        Color toggledColor = new Color(0.1f, 0.0f, 0.7f);
        Color untoggledColor = new Color(0.2f, 0.2f, 0.5f);
        oceanBtn = GameObject.Find("ToggleWater").GetComponent<Button>();
        oceanBtnTxt = oceanBtn.GetComponentInChildren<Text>();
        if (shapeSettings.zeroLvlIsOcean)
        {
            oceanBtn.GetComponent<Image>().color = toggledColor;
            oceanBtnTxt.color = toggledTxtColor;
        }
        else
        {
            oceanBtn.GetComponent<Image>().color = untoggledColor;
            oceanBtnTxt.color = untoggledTxtColor;
        }
        UpdateMesh();
        GenerateColors();
    }

}
