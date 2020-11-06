using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoPlanet : MotherPlanet{
    [Range(0, 5)]
    public int LOD;

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

        if(craterSettings == null){
            craterSettings = SettingSpawner.loadDefaultCraters();
        }

        if(this.transform.Find("mesh") == null){ // no meshObj initialized yet
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.AddComponent<MeshRenderer>(); //.material = (Material) Resources.Load("defaultMat");
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();     
            meshObj.GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
        }
        else{
            GameObject meshObj = this.transform.Find("mesh").gameObject;
            meshFilter = meshObj.GetComponent<MeshFilter>();
            meshObj.GetComponent<MeshRenderer>(); //.material = (Material) Resources.Load("defaultMat");
            meshFilter.sharedMesh = new Mesh();
            meshObj.GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

        }

        craterGenerator = new CraterGenerator(craterSettings);
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction, craterGenerator);
        colorGenerator = new ColorGenerator();

        colorGenerator.UpdateSettings(colorSettings);

        icoSphere = new IcoSphere(shapeGenerator, shapeSettings.radius, LOD, meshFilter.sharedMesh); 
        icoSphere.UpdateUVs();   
    }

    public override void GenerateMesh(){
        icoSphere.ConstructMesh();
    }

    public override void UpdateMesh(){
        if(icoSphere == null){
            Debug.Log("initialized");
            Initialize();
            icoSphere.ConstructMesh();
            icoSphere.UpdateMesh();
        }
        else{
            icoSphere.UpdateMesh();
        }           
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    public override void GenerateColors(){   
        //Debug.Log(colorGenerator);
        //icoSphere.UpdateUVs();
        colorGenerator.UpdateColors();
        if(meshFilter.gameObject.activeSelf){
            //icoSphere.UpdateUVs();
        }
    }
    
    public override void OnCraterSettingsUpdated(){ //Rebuild planet when color is updated
        if (autoUpdate)
        {
            clearCraters = false;
            Initialize();
            GenerateMesh();
        }
    }
    
    public override void OnCollisionEnter(Collision collision){
        clearCraters = false;
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point.normalized;
        Vector3 planetRotEuler  = gameObject.transform.localRotation.eulerAngles;
        Quaternion rotation = Quaternion.AngleAxis(-planetRotEuler[2], Vector3.forward)
            * Quaternion.AngleAxis(-planetRotEuler[0], Vector3.right)
            * Quaternion.AngleAxis(-planetRotEuler[1], Vector3.up);
        
        position = rotation * position;
        float velocity = collision.relativeVelocity.magnitude;
        craterSettings.impact = 0.2f + velocity/2;
        craterSettings.floorHeight = -2f/velocity;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        
        if (asteroids.Length > 0){
            GameObject asteroid = asteroids[0];
            SphereCollider asteroidCollider = asteroid.GetComponent<SphereCollider>();
            float radius = asteroid.transform.localScale.x * asteroidCollider.radius;
            craterSettings.radius = radius;
        }
        shapeGenerator.craterGenerator.CreateCrater(position);
        UpdateMesh();  
    }
}
