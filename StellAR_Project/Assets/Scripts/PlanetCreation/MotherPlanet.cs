using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetShape(_planet data)
    {
        List<CraterGenerator.Crater> cList = craterGenerator.craterList;
        for (int i = 0; i < data.craterList.Count; i++)
        {
            _crater crater = data.craterList[i];
            cList.Add(new CraterGenerator.Crater(crater.center, crater.radius,
                crater.floor, crater.smoothness, crater.impact, crater.rimSteepness,
                crater.rimWidth));
        }
    }

}
