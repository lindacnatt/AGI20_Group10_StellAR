using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour{   

    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = false;

    public ColorSettings colorSettings;
    public Slider cSlider;

    public ShapeSettings shapeSettings;
    

    // create mouseInteractions
    public MouseInteraction interaction;
    
    ShapeGenerator shapeGenerator;
    
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;
  
    void Initialize(){
        if(interaction == null){
            //MouseInteraction interaction = GameObject.GetComponent<MouseInteraction>();
        }
       
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction);

        if(meshFilters == null || meshFilters.Length == 0){
            Debug.Log("meshFilters null");
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

    public void GeneratePlanet(){
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    void GenerateMesh(){
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
    }
 
    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            Initialize();
            GenerateMesh();
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
}
