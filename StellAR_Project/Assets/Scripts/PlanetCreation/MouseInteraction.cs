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
    void Start(){
        time = Time.fixedTime + 0.05f;
    }
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Time.fixedTime >= time){
            time += 0.05f; // how often should we add a point
            if(Physics.Raycast(ray, out hit)){
                selection = hit.transform;
                //selectionRenderer = selection.GetComponent<Renderer>();
                if(Input.GetMouseButton(0)){
                    hitCoords.Add(selection.InverseTransformPoint(hit.point)); 
                }
            }
        }   
    }
    
    public List<Vector3> GetPaintedVertices(){
        foreach (Vector3 point in hitCoords){
            Debug.Log(point);
        }
        return hitCoords;
    }
    
}
