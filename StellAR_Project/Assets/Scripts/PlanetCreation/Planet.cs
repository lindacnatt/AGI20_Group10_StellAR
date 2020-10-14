using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour{   
    
    [Range(2, 2)]
    // Resolution of every terrainFace
    public int resolution = 10;

    [Range(0, 3)]
    public int isoDetail = 1;

    public Slider cSlider;

    // update on buttonpress or on change
    public bool autoUpdate = true; 

    // data on the planet
    public ColorSettings colorSettings; 
    public ShapeSettings shapeSettings;
    
    // create mouseInteractions
    public MouseInteraction interaction;
    ShapeGenerator shapeGenerator;
    
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    IsoSphere isoSphere;
    MeshFilter meshFilter;


    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    void Initialize(){
        if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }

        if(interaction == null){
            interaction = this.GetComponent<MouseInteraction>();
        }

        shapeGenerator = new ShapeGenerator(shapeSettings, interaction);

        if(meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }      

        terrainFaces = new TerrainFace[6]; 
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        
        for(int i = 0; i < 6; i++){
            if(meshFilters[i] == null){
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }    
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]); 
        } 
    }

    void InitializeIsoSphere(){        
        if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }

        if(interaction == null){
            interaction = this.GetComponent<MouseInteraction>();
        }

        shapeGenerator = new ShapeGenerator(shapeSettings, interaction);

        if(meshFilter == null){
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        }
        
        
        isoSphere = new IsoSphere(shapeGenerator, shapeSettings.radius, isoDetail, meshFilter.sharedMesh);
        
    }

    void Awake(){
         if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }
        if(interaction == null){
            interaction = this.GetComponent<MouseInteraction>();
        }
    }
    public void GeneratePlanet(){
        //Initialize();
        InitializeIsoSphere();
        //GenerateMesh();
        GenerateMeshIso();
        //GenerateColors();
    }
    void GenerateMesh(){
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
    }

    void GenerateMeshIso(){
        isoSphere.ConstructMesh();
    }
 
    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            //Initialize();
            //GenerateMesh();
            InitializeIsoSphere();
            GenerateMeshIso();
        }    
    }

    void GenerateColors(){ // update color for every mesh given from the colorsettings
        foreach(MeshFilter m in meshFilters){
            Color newPlanetColor = colorSettings.planetColor;
            //covert the colors to HSV and only change the hue
            newPlanetColor = Color.HSVToRGB(cSlider.value, 1, 1);
            m.GetComponent<MeshRenderer>().sharedMaterial.color = newPlanetColor;
        }
    }

    public void OnColorSettingsUpdated(){ //Rebuild planet when color is updated
        if(autoUpdate){
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMeshIcosphere(){

    }
}
