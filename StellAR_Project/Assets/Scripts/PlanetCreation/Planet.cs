using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : CelestialObject{

    [Range(2, 100)]
    // Resolution of every terrainFace
    public int resolution = 10;

    [Range(0, 5)]
    public int icoDetail = 1;
    public bool isIcoSphere = true;

    public Slider cSlider;

    // update on buttonpress or on change
    public bool autoUpdate = true;

    // data on the planet
    public ColorSettings colorSettings;
    public ShapeSettings shapeSettings;
    public CraterSettings craterSettings;
    //public NoiseSettings noiseSettings;

    // create mouseInteractions
    public MouseInteraction interaction;
    ShapeGenerator shapeGenerator;
    CraterGenerator craterGenerator;
    ColorGenerator colorGenerator = new ColorGenerator();


    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
    IcoSphere icoSphere;
    MeshFilter meshFilter;


    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;
    [HideInInspector]
    public bool craterSettingsFoldout;

    public List<CraterGenerator.Crater> craterList = new List<CraterGenerator.Crater>();

    void Initialize(){
        if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }
        if(craterSettings == null)
        {
            craterSettings = SettingSpawner.loadDefaultCraters();
        }
        craterList = new List<CraterGenerator.Crater>();

        interaction = gameObject.GetComponent<MouseInteraction>();

        craterGenerator = new CraterGenerator(craterSettings, craterList);
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction, craterGenerator);

        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0){
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
        for (int i = 0; i < 6; i++){
            if(meshFilters[i] == null){
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>(); //.sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i], craterGenerator);
        }
    }

    void InitializeIcoSphere(){
        if(shapeSettings == null || colorSettings == null){
            shapeSettings = SettingSpawner.loadDefaultShape();
            colorSettings = SettingSpawner.loadDefaultColor();
        }
        if (craterSettings == null)
        {
            craterSettings = SettingSpawner.loadDefaultCraters();
        }

        craterList = new List<CraterGenerator.Crater>();

        if (interaction == null){
            interaction = this.GetComponent<MouseInteraction>();
        }

        craterGenerator = new CraterGenerator(craterSettings, craterList);
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction, craterGenerator);

        colorGenerator.UpdateSettings(colorSettings);

        if (this.transform.Find("mesh") != null){
            meshFilter = this.transform.Find("mesh").GetComponent<MeshFilter>();
        }
        if(meshFilter == null){
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        }
        icoSphere = new IcoSphere(shapeGenerator, shapeSettings.radius, icoDetail, meshFilter.sharedMesh, craterGenerator);
    }

    void UpdateIcoSphere()
    {
        if (craterSettings == null)
        {
            craterSettings = SettingSpawner.loadDefaultCraters();
        }

        craterGenerator = new CraterGenerator(craterSettings, craterList);
        shapeGenerator = new ShapeGenerator(shapeSettings, interaction, craterGenerator);

        colorGenerator.UpdateSettings(colorSettings);

        if (this.transform.Find("mesh") != null)
        {
            meshFilter = this.transform.Find("mesh").GetComponent<MeshFilter>();
        }
        if (meshFilter == null)
        {
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;
            meshObj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        }
        icoSphere = new IcoSphere(shapeGenerator, shapeSettings.radius, icoDetail, meshFilter.sharedMesh, craterGenerator);
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
        UpdateCollider();
        if(isIcoSphere){
            InitializeIcoSphere();
            GenerateMeshIco();
        }
        else{
            Initialize();
            GenerateMesh();
            GenerateColors();
        }
    }
    void GenerateMesh(){
        /* for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf) {
                terrainFaces[i].ConstructMesh();
            }
        } */
        foreach(TerrainFace face in terrainFaces){
            face.ConstructMesh();
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateMeshIco(){
        icoSphere.ConstructMesh();
    }

    public void OnShapeSettingsUpdated(){
        if(autoUpdate){
            UpdateCollider();
            if(isIcoSphere){
                InitializeIcoSphere();
                GenerateMeshIco();
            }
            else{
                Initialize();
                GenerateMesh();
            }
        }
    }

    void GenerateColors(){ // update color for every mesh given from the colorsettings

        colorGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf) {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
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
            InitializeIcoSphere();
            CreateCrater(new Vector3(0,0,-1), 1);
            GenerateMeshIco();
        }
    }

    public void MakeCrater(Collision collision, float otherRadius)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point.normalized;
        Vector3 planetRotEuler = gameObject.transform.localRotation.eulerAngles;
        Quaternion rotation = Quaternion.AngleAxis(-planetRotEuler[2], Vector3.forward)
            * Quaternion.AngleAxis(-planetRotEuler[0], Vector3.right)
            * Quaternion.AngleAxis(-planetRotEuler[1], Vector3.up);
        position = rotation * position;
        float velocity = collision.relativeVelocity.magnitude;
        craterSettings.impact = 0.2f + velocity / 2;
        craterSettings.floorHeight = -2f / velocity;
        craterSettings.radius = otherRadius;

        if (isIcoSphere)
        {
            CreateCrater(position, 1);
            UpdateIcoSphere();
            GenerateMeshIco();
        }
        else
        {
            CreateCrater(position, 1);
            Initialize();
            GenerateMesh();
        }
    }


    public void PlaceCrater(Vector3 position)
    {
        if (isIcoSphere){
            CreateCrater(position.normalized, 0.001f);
            UpdateIcoSphere();
            GenerateMeshIco();
        }
        else{
            CreateCrater(position.normalized, 0.001f);
            Initialize();
            GenerateMesh();
        }
    }

    public void CreateCrater(Vector3 pos, float multilplier)
    {
        craterList.Add(new CraterGenerator.Crater(pos,
            craterSettings.radius, craterSettings.floorHeight,
            craterSettings.smoothness, craterSettings.impact * multilplier,
            craterSettings.rimSteepness * multilplier, craterSettings.rimWidth * multilplier));
    }

    private void UpdateCollider(){
        SphereCollider collider = this.GetComponent<SphereCollider>();
        collider.radius = shapeSettings.radius;
    }
}
