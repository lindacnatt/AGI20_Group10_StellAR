
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class Triangle : MonoBehaviour {
Mesh mesh;
MeshRenderer meshRenderer;
Vector3[] vertices;
int[] triangles;
Ray ray;
RaycastHit2D hit;
Transform selection;
Renderer selectionRenderer;
BaryCentric bary;
GameObject handle;
float handleInitZ;
Vector3 handleInitPos;
PolygonCollider2D col;
Vector2[] colPoints;
Vector2 worldPos;
public GasPlanetShaderMAterialPropertyBlock gasPlanet;
List<Button> bandButtons = new List<Button>();
List<Button> biomeButtons = new List<Button>();
int currBand = 0;
int biomeIndex = 0;
Vector3[] bandWeights = new Vector3[4];
Vector3[] bandPos = new Vector3[4];
float[] bandIntensity = new float[4];
public MotherPlanet planet;
public GameObject buttonGroup;
public GameObject buttonGroupGAS;

float intensityLevel = 0;
Vector3 colorWeights;


// triangle visuals
public Material material;
public Texture2D tex;
[Range(0,10)]
public float size;
RaycastHit hit2;

void OnEnable(){
    
    if(gameObject.GetComponent<MeshFilter>() == null){
        gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        col = gameObject.AddComponent<PolygonCollider2D>();
    }
    else{
            //gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        col = gameObject.GetComponent<PolygonCollider2D>();
    }
        
    if(GameObject.FindGameObjectWithTag("Planet"))
    {
        planet = FindObjectsOfType<MotherPlanet>()[0];
        
        buttonGroup.SetActive(true);
        buttonGroupGAS.SetActive(false);
        foreach (Button child in buttonGroup.GetComponentsInChildren<Button>(true))
        {
            child.gameObject.SetActive(true);
            biomeButtons.Add(child);
            child.onClick.AddListener(() => biomeButtonClick(biomeButtons.IndexOf(child)));
        }
    }
    else if(GameObject.FindGameObjectWithTag("GasPlanet"))
    {
        gasPlanet = FindObjectsOfType<GasPlanetShaderMAterialPropertyBlock>()[0];
        buttonGroup.SetActive(false);
        buttonGroupGAS.SetActive(true);
        foreach (Button child in buttonGroupGAS.GetComponentsInChildren<Button>(true))
        {
            child.gameObject.SetActive(true);
            bandButtons.Add(child);
            child.onClick.AddListener(() => bandButtonClick(bandButtons.IndexOf(child)));
            
        }
        
    }


    GetComponentInChildren<Slider>().onValueChanged.AddListener(intensityOnChange);
    //GetComponent<MotherPlanet>();

    meshRenderer.material = material;
    mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = mesh;

    vertices = new Vector3[] {
        new Vector3(0,0,0),
        new Vector3(0.5f,0.866025404f,0)*size,
        new Vector3(1,0,0)*size
    };

    triangles = new[]{0,1,2};
    
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
    Vector2[] uvs = new []{new Vector2(0,0), new Vector2(1, 0.5f), new Vector2(0, 1)};
    mesh.uv = uvs;
    
    // set handle sprite
    handle = this.transform.GetChild(0).gameObject;
    
    handle.transform.localPosition = new Vector3(2, 1, handle.transform.localPosition.z);
    handleInitZ = handle.transform.localPosition.z;
    for (int i = 0; i < 4; i++)
    {
        bandPos[i] = handle.transform.localPosition;
    }
    
    // create shape of collider 
    colPoints = new Vector2[3];
    for(int i = 0; i < 3; i++){
        colPoints[i] = new Vector2(vertices[i].x, vertices[i].y);
    }
    col.pathCount = 1;
    col.SetPath(0, colPoints);
    for (int i = 0; i < 3; i++)
    {
        vertices[i] = mesh.vertices[i] / size;
    }
    //get initialweights
    Vector3 initWeights = BaryCentric.getWeights(handle.transform.localPosition / size, vertices);
    for (int i = 0; i < 4; i++)
    {
        bandWeights[i] = initWeights;
            bandIntensity[i] = intensityLevel; 
    }
}
  

    void Update(){
    

    if (gasPlanet)
    {
        //makes sure that each band keeps the color that was selected before switching to other band, and that the handle reflects that color
        
        handle.transform.localPosition = new Vector3(bandPos[currBand].x, bandPos[currBand].y, handleInitZ);
        GetComponentInChildren<Slider>().value = bandIntensity[currBand];
        UpdateColor(bandWeights[currBand], bandIntensity[currBand]);
    }
    else
    {
        handle.transform.localPosition = new Vector3(bandPos[biomeIndex].x, bandPos[biomeIndex].y, handleInitZ);
        GetComponentInChildren<Slider>().value = bandIntensity[biomeIndex];
        UpdateColor(bandWeights[biomeIndex], bandIntensity[biomeIndex]);
    }
    if(Input.GetMouseButton(0)){


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) * -1f;
        hit = Physics2D.GetRayIntersection(ray);
        //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.blue, 5f);
        if(hit){
            //Debug.Log("Den funkar");
            selection = hit.transform;
            //selectionRenderer = selection.GetComponent<Renderer>();
            handle.transform.position = hit.point;
            
            //the following line makes sure that the handle does not fuck off into oblivion on the z axis.
            handle.transform.localPosition = new Vector3(handle.transform.localPosition.x, handle.transform.localPosition.y, handleInitZ);
            
            colorWeights = BaryCentric.getWeights(handle.transform.localPosition/size, vertices);
            
            UpdateTintColor(colorWeights, intensityLevel);
            
            
           UpdateColor(colorWeights, intensityLevel);
        }
    }  
}
void UpdateColor(Vector3 weights, float power){

    float intensity = (weights.x + weights.y + weights.z) / 3f;
    float factor = Mathf.Pow(intensity, power);

        //Debug.Log(weights);
    Color newColor = Color.white;
    newColor.r *= factor;
    newColor.g *= factor;
    newColor.b *= factor;
    //this way was weirdly different than just multiplying the weights first, and it is more stable across color assignments.
    //Debug.Log(meshRenderer);

    meshRenderer.material.color = newColor;

}
public void RandomColor()
    {
        int prevBiomeIndex = 1 * biomeIndex;
        int prevcurrBand = 1 * currBand;
        float degree = Mathf.Atan(0.85f / 0.5f);
        for (int i = 0; i < 4; i++)
        {
            biomeIndex = i;
            currBand = i;
            intensityLevel = Random.Range(-1.5f, 1.5f);
            GetComponentInChildren<Slider>().value = intensityLevel;
            float x = Random.Range(0.1f, 0.9f);
       
            float y;
            if(x < 0.5f)
            {
                y = Random.Range(0.1f, Mathf.Tan(degree) * x); //to make sure the handle stays within triangle
            }
            else
            {
                y = Random.Range(0.1f, Mathf.Tan(degree)*(0.9f-x)); //to make sure the handle stays within triangle
            }


            Vector3 rndmHandle = new Vector3(x*size, y*size, handleInitZ);
            handle.transform.localPosition = rndmHandle;
            colorWeights = BaryCentric.getWeights(handle.transform.localPosition / size, vertices);
            UpdateTintColor(colorWeights, intensityLevel);
        }
        biomeIndex = prevBiomeIndex;
        currBand = prevcurrBand;
    }

public void UpdateTintColor(Vector3 weights, float power){
    float intensity = (weights.x + weights.y + weights.z) / 3f;
    float factor = Mathf.Pow(intensity, power);
    if (planet)
    { 
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.r = weights[0];
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.g = weights[1];
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.b = weights[2];
            //this way was weirdly different than just multiplying the weights first, and it is more stable across color assignments.
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.r *= factor;
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.g *= factor;
            planet.colorGenerator.settings.biomeColorSettings.biomes[biomeIndex].tint.b *= factor;
            planet.GenerateColors();
            bandPos[biomeIndex] = handle.transform.localPosition;
            bandWeights[biomeIndex] = weights;
            bandIntensity[biomeIndex] = power;
        }
    else
    {
        //makes sure that each band keeps the color that was selected before switching to other band, and that the handle reflects that color
        if (currBand == 0)
        {
            gasPlanet.ChangeBandColor1(weights, factor);
        }
        else if (currBand == 1)
        {
            gasPlanet.ChangeBandColor2(weights, factor);
        }
        else if (currBand == 2)
        {
            gasPlanet.ChangeBandColor3(weights, factor);
        }
        else
        {
            gasPlanet.ChangeStormColor(weights, factor);
        }
        bandPos[currBand] = handle.transform.localPosition;
        bandWeights[currBand] = weights;
        bandIntensity[currBand] = power;
    }
    
}


public void bandButtonClick(int idx)
{
    currBand = idx;
}
public void biomeButtonClick(int idx){
    biomeIndex = idx;
}

public void intensityOnChange(float value)
{
    colorWeights = BaryCentric.getWeights(handle.transform.localPosition / size, vertices);

    intensityLevel = value;
    UpdateTintColor(colorWeights, intensityLevel);
    UpdateColor(colorWeights, intensityLevel);
}




public Texture2D InitTexture(){
    int resolution = 256;
    Color[] colors = new Color[resolution*resolution];
    Texture2D tex = new Texture2D(resolution, resolution);
    Vector2 texCoord;
    Color color;
    Vector3 weights;
    float x, y;

    for(int i = 0; i < resolution; i++){
        for(int j = 0; j < resolution; j++){
            x = i / (float) resolution;
            y = j / (float) resolution;
            texCoord = new Vector2(x, y);
            weights = BaryCentric.getWeights(new Vector3(vertices[2].x * x, vertices[1].y * y, 0f), vertices);
            color = new Color(weights.x, weights.y, weights.z);
            for(int index = 0; index < 3; index++){
                if(weights[index] < 0.01){
                    color = new Color(0, 0, 0);
                }
            }
            colors[i * resolution + j] = color;
        }
    }
    tex.SetPixels(colors);
    tex.Apply();
    SaveTextureAsPNG(tex, "Assets/Materials/PlanetMaterials/Materials/colorMapTex.png");
    Debug.Log("saving");
    return tex;
}

public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath){
    byte[] _bytes =_texture.EncodeToPNG();
    System.IO.File.WriteAllBytes(_fullPath, _bytes);
}

}
