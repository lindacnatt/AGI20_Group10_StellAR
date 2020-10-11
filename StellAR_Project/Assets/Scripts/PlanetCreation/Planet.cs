using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour{   
    
    [Range(2, 100)]
    // Resolution of every terrainFace
    public int resolution = 10;

    public Slider cSlider;

    // update on buttonpress or on change
    public bool autoUpdate = true; 

    // data on the planet
    public ColorSettings colorSettings; 
    public ShapeSettings shapeSettings;
    

    // create mouseInteractions
    public MouseInteraction interaction;
    ShapeGenerator shapeGenerator;
    ColorGenerator colorGenerator = new ColorGenerator();

    
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

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
        colorGenerator.UpdateSettings(colorSettings);

        if(meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }    

        terrainFaces = new TerrainFace[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        
        for(int i = 0; i < 6; i++){
            if(meshFilters[i] == null){
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>(); //.sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]); 
        } 
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
        Initialize();
        GenerateMesh();
        GenerateColors();
    }
    void GenerateMesh(){
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }
 
    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateMesh();
        }    
    }

    void GenerateColors(){ // update color for every mesh given from the colorsettings

        colorGenerator.UpdateColors();
       /*  foreach(MeshFilter m in meshFilters){
            Color newPlanetColor = colorSettings.planetColor;
            //covert the colors to HSV and only change the hue
            newPlanetColor = Color.HSVToRGB(cSlider.value, 1, 1);
            m.GetComponent<MeshRenderer>().sharedMaterial.color = newPlanetColor;
        } */
    }

    public void OnColorSettingsUpdated(){ //Rebuild planet when color is updated
        if(autoUpdate){
            Initialize();
            GenerateColors();
        }
      }
}
