using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour{
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
    Mesh terrainFaceMesh;
    [HideInInspector]
    public List<Vector3> hitCoords;
    Vector3[] vertices;

    [SerializeField]
    public float brushSize = 0.2f;
    float time;
    [Range(0, 2)]

    // for being able to use different noise types
    public int type = 0;
    public List<Vector3>[] hitCoordsArr = new List<Vector3>[2]; 
    MotherPlanet planet;
    
    void Start(){
        planet = GetComponent<MotherPlanet>();
        time = Time.fixedTime + 0.1f; 
    }

    // TODO: Fix so that different noise functions use different brushes!!! 
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Time.fixedTime >= time){
            time += 0.1f; // how often should we add a point
            if(Physics.Raycast(ray, out hit)){
                selection = hit.transform;
                //selectionRenderer = selection.GetComponent<Renderer>();
                if(Input.GetMouseButton(0)){
                    hitCoords.Add(selection.InverseTransformPoint(hit.point)); 
                    planet.UpdateMesh();
                }
            }
        }   
    }
    
    public List<Vector3> GetPaintedVertices(){
        return hitCoords;
    }

    public int GetNoiseType(){
        return type;
    }
    
}
