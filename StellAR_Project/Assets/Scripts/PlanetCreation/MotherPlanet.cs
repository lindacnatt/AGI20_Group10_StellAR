using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotherPlanet: MonoBehaviour {
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    public bool autoUpdate = true;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    // these need to be overriden in subClass of planet
    public abstract void Initialize();  
    public abstract void GenerateMesh();
    public abstract void GenerateColors();
    
    // these can be used directly in subClass 

    void Awake(){
        GeneratePlanet();
    }
    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        GenerateColors();
        UpdateCollider(); 
    }

    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            GenerateMesh();  
            UpdateCollider();
        }    
    }
     public void OnColorSettingsUpdated(){ //Rebuild planet when color is updated
        if(autoUpdate){
            Initialize();
            GenerateColors();
        }
    }

    public void UpdateCollider(){
        SphereCollider collider = this.GetComponent<SphereCollider>();
        collider.radius = this.shapeSettings.radius;
    }
}
