using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoPlanet : MotherPlanet{
    [Range(0, 5)]
    public int LOD;
    
    ShapeGenerator shapeGenerator;
    
    // for planetGeneration
    IcoSphere icoSphere;
    [SerializeField, HideInInspector]
    MeshFilter meshFilter;
    MouseInteraction interaction;
    
    public override void Initialize(){
        if(shapeSettings == null || colorSettings == null){
        shapeSettings = SettingSpawner.loadDefaultShape();
        colorSettings = SettingSpawner.loadDefaultColor();
        }

        if(interaction == null){
            interaction = GetComponent<MouseInteraction>();
        }

        if(this.GetComponent<SphereCollider>() == null){
            this.gameObject.AddComponent<SphereCollider>();
        }

        if(this.transform.Find("mesh") == null){ // no meshObj initialized yet
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();     
        }
        else{
            GameObject meshObj = this.transform.Find("mesh").gameObject;
            meshFilter = meshObj.GetComponent<MeshFilter>();
            meshObj.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
            meshFilter.sharedMesh = new Mesh();
        }

        shapeGenerator = new ShapeGenerator(shapeSettings, interaction);
        icoSphere = new IcoSphere(shapeGenerator, shapeSettings.radius, LOD, meshFilter.sharedMesh);     
    }

    public override void GenerateMesh(){
        icoSphere.ConstructMesh();
    }

    public override void GenerateColors(){   
    }

}
