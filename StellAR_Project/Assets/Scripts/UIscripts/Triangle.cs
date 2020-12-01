using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triangle : MonoBehaviour{
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
    GasPlanetShaderMAterialPropertyBlock gasPlanet;
    List<Button> bandButtons = new List<Button>();
    int currBand = 0;
    Vector3[] bandWeights = new Vector3[4];
    Vector3[] bandPos = new Vector3[4];
    MotherPlanet planet;
    

    public Material material;

    [Range(0,10)]
    public float size;
    RaycastHit hit2;

    void Awake(){
        if(gameObject.GetComponent<MeshFilter>() == null){
            Debug.Log("adding");
            gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            col = gameObject.AddComponent<PolygonCollider2D>();
        }
        else{
            Debug.Log("getting");
            //gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            col = gameObject.GetComponent<PolygonCollider2D>();
        }
         
        if(GameObject.FindGameObjectWithTag("Planet"))
        {
            planet = FindObjectsOfType<MotherPlanet>()[0];
        }
        else
        {
            gasPlanet = FindObjectsOfType<GasPlanetShaderMAterialPropertyBlock>()[0];
            foreach (Button child in GetComponentsInChildren<Button>(true))
            {
                child.gameObject.SetActive(true);
                bandButtons.Add(child);
                child.onClick.AddListener(() => bandButtonClick(bandButtons.IndexOf(child)));
                
            }
        }
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
        
        // set handle sprite
        handle = this.transform.GetChild(0).gameObject;
        handleInitZ = handle.transform.localPosition.z;
        Array.ForEach(bandPos, p => p = handle.transform.localPosition);
        // create shape of collider 
        colPoints = new Vector2[3];
        for(int i = 0; i < 3; i++){
            colPoints[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        col.pathCount = 1;
        col.SetPath(0, colPoints);

        //get initialweights
        Vector3 initWeights = BaryCentric.getWeights(handle.transform.position, vertices);
        Array.ForEach(bandWeights, w => w = initWeights);
    }
  
    void Update(){

        if (gasPlanet)
        {
            //makes sure that each band keeps the color that was selected before switching to other band, and that the handle reflects that color
            handle.transform.localPosition = new Vector3(bandPos[currBand].x, bandPos[currBand].y, handleInitZ);
            UpdateColor(bandWeights[currBand]);
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
                for (int i = 0; i < 3; i++) 
                { 
                    vertices[i] = mesh.vertices[i] / size; 
                }
                //the following line makes sure that the handle does not fuck off into oblivion on the z axis.
                handle.transform.localPosition = new Vector3(handle.transform.localPosition.x, handle.transform.localPosition.y, handleInitZ);
                Vector3 colorWeights = BaryCentric.getWeights(handle.transform.localPosition/size, vertices);
                
                UpdateTintColor(colorWeights);
                
               
                UpdateColor(colorWeights);
            }
        }  
    }
    void UpdateColor(Vector3 weights){
        float mult = 1f;
        //Debug.Log(weights);
        Color newColor = new Color(weights.x*mult, weights.y*mult, weights.z*mult, 1f*mult);
        //Debug.Log(meshRenderer);
        meshRenderer.material.color = newColor;
    }

    // void UpdatePlanetColor(Vector3 weights){
    //     Color newColor = new Color(weights.x, weights.y, weights.z, 0.5f);
    // }
    public void UpdateTintColor(Vector3 weights){
        if (planet)
        {
            planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.r = weights[0];
            planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.g = weights[1];
            planet.colorGenerator.settings.biomeColorSettings.biomes[0].tint.b = weights[2];
            planet.GenerateColors();
        }
        else
        {
            //makes sure that each band keeps the color that was selected before switching to other band, and that the handle reflects that color
            if (currBand == 0)
            {
                gasPlanet.ChangeBandColor1(weights);
            }
            else if (currBand == 1)
            {
                gasPlanet.ChangeBandColor2(weights);
            }
            else if (currBand == 2)
            {
                gasPlanet.ChangeBandColor3(weights);
            }
            else
            {
                gasPlanet.ChangeStormColor(weights);
            }
            bandPos[currBand] = handle.transform.localPosition;
            bandWeights[currBand] = weights;
        }
        
    }

    public void bandButtonClick(int idx)
    {
        currBand = idx;
        Debug.Log(currBand);
    }

}
