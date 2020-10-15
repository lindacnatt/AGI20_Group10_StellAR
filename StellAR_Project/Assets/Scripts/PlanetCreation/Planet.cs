using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour{

    [Range(2, 100)]
    // Resolution of every terrainFace
    public int resolution = 10;

    public Slider cSlider;
    Vector3 craterCenter = new Vector3(0, 0, 0);

    // update on buttonpress or on change
    public bool autoUpdate = true;

    // data on the planet
    public ColorSettings colorSettings;
    public ShapeSettings shapeSettings;
    public CraterSettings craterSettings;
    public Explosion explosion;
    //public NoiseSettings noiseSettings;
    SphereCollider planetCollider;

    // create mouseInteractions
    public MouseInteraction interaction;
    ShapeGenerator shapeGenerator;
    CraterGenerator craterGenerator;


    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;
    [HideInInspector]
    public bool craterSettingsFoldout;

    void Initialize(){
        if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }
        if(craterSettings == null)
        {
            craterSettings = SettingSpawner.loadDefaultCraters();
        }

        if(interaction == null){
            interaction = this.GetComponent<MouseInteraction>();
        }

        shapeGenerator = new ShapeGenerator(shapeSettings, interaction);
        craterGenerator = new CraterGenerator(craterSettings);

        planetCollider = GetComponent<SphereCollider>();
        craterGenerator.CreateCrater(craterCenter);

        if (meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        for (int i = 0; i < 6; i++){
            if(meshFilters[i] == null){
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i], craterGenerator);
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
            //newPlanetColor = Color.HSVToRGB(cSlider.value, 1, 1);
            //m.GetComponent<MeshRenderer>().sharedMaterial.color = newPlanetColor;
        }
    }

    public void OnColorSettingsUpdated(){ //Rebuild planet when color is updated
        if(autoUpdate){
            Initialize();
            GenerateColors();
        }
      }

    public void OnCraterSettingsUpdated()
    { //Rebuild planet when color is updated
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point.normalized;
        Vector3 planetRotEuler  = gameObject.transform.localRotation.eulerAngles;
        Quaternion rotation = Quaternion.AngleAxis(-planetRotEuler[2], Vector3.forward)
            * Quaternion.AngleAxis(-planetRotEuler[0], Vector3.right)
            * Quaternion.AngleAxis(-planetRotEuler[1], Vector3.up);
        position = rotation * position;
        craterCenter = position;
        float velocity = collision.relativeVelocity.magnitude;
        craterSettings.impact = 0.2f + velocity/2;
        Debug.Log("impact: " + craterSettings.impact);
        craterSettings.floorHeight = -2f/velocity;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if (asteroids.Length > 0)
        {
            GameObject asteroid = asteroids[0];
            SphereCollider asteroidCollider = asteroid.GetComponent<SphereCollider>();
            float radius = asteroid.transform.localScale.x * asteroidCollider.radius;
            craterSettings.radius = radius;

        }
        Initialize();
        GenerateMesh();
    }
}
