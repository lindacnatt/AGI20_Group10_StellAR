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
    MotherPlanet planet;
    bool craterPlacement = false;
    //bool placingCrater = false;

    [SerializeField]
    public float brushSize = 0.2f;

    void Start(){
        planet = gameObject.GetComponent<MotherPlanet>();
    }

    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("toggle craterCreator");
            craterPlacement ^= true;
        }
        /*
        if (placingCrater)
        {
            //planet.PlaceCrater(selection.InverseTransformPoint(hit.point));
            planet.shapeGenerator.craterGenerator.CreateCrater(hit.point);
        }
        if (Input.GetMouseButtonUp(0))
        {
            placingCrater = false;
        }
        */
        if (Physics.Raycast(ray, out hit)){
            selection = hit.transform;
            if(craterPlacement){
                if(Input.GetMouseButtonDown(0)){
                    planet.shapeGenerator.craterGenerator.CreateCrater(hit.point);
                }
            }
            else{
                if(Input.GetMouseButton(0)){
                    hitCoords.Add(selection.InverseTransformPoint(hit.point));
                }
            }
           planet.UpdateMesh();
        }
    }
    public List<Vector3> GetPaintedVertices(){
        return hitCoords;
    } 
}
