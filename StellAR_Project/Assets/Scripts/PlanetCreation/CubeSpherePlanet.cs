using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpherePlanet : MotherPlanet{
    [Range(2, 100)]
    // Resolution of every terrainFace
    public int resolution = 10;

    //public Slider cSlider;
    
    // create mouseInteractions
    public MouseInteraction interaction;
    ShapeGenerator shapeGenerator;
    
    //[SerializeField, HideInInspector]
    public MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    public override void Initialize(){
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
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Unlit/Color"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }    
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]); 
        } 
    }

    public override void GenerateMesh(){
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
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
}
