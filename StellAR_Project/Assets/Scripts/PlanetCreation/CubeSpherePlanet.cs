using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpherePlanet : MotherPlanet{
    
    // Resolution of every terrainFace
    [Range(2, 100)]
    public int resolution = 10;

    //public Slider cSlider;
    
    // create mouseInteractions
    MouseInteraction interaction;
    
    [SerializeField, HideInInspector]
    public MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

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
        
        if(meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }

        colorGenerator = new ColorGenerator();
        craterGenerator = new CraterGenerator(craterSettings);
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction, craterGenerator);

        terrainFaces = new TerrainFace[6]; 
        colorGenerator.UpdateSettings(colorSettings);
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        
        if(this.transform.Find("mesh") == null){
            for(int i = 0; i < 6; i++){
                if(meshFilters[i] == null){
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;
                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = (Material) Resources.Load("defaultMat");
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }    
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]); 
            }  
        }
        else{
            for(int i = 0; i < 6; i++){
                if(meshFilters[i] == null){
                    GameObject meshObj = this.transform.Find("mesh").gameObject;
                    meshObj.transform.parent = transform;
                    meshObj.GetComponent<MeshRenderer>().sharedMaterial = (Material) Resources.Load("defaultMat");
                    meshFilters[i] = meshObj.GetComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }    
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]); 
            }    
        }  
    }
    public override void GenerateMesh(){
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
        foreach(TerrainFace face in terrainFaces){
            face.UpdateMesh();
        }
    }

    public override void UpdateMesh(){
        if(terrainFaces == null){
            this.Initialize();
        }
        foreach(TerrainFace face in terrainFaces){
            face.UpdateMesh();
            //face.UpdateUVs();
        }
    }

    public override void GenerateColors(){ // update color for every mesh given from the colorsettings
        foreach(MeshFilter m in meshFilters){
            //Color newPlanetColor = colorSettings.planetColor;
            //covert the colors to HSV and only change the hue
            //newPlanetColor = Color.HSVToRGB(cSlider.value, 1, 1);
            //m.GetComponent<MeshRenderer>().sharedMaterial.color = newPlanetColor;
        }
    }
    
    public override void OnCraterSettingsUpdated(){
        if (autoUpdate)
        {
            clearCraters = false;
            Initialize();
            GenerateMesh();
        }
    }
}
