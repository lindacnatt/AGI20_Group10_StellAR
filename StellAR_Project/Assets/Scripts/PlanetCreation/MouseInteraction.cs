using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour{
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
    Mesh terrainFaceMesh;
    public List<Vector3> hitCoords;
    Vector3[] vertices;
    Planet planet;
    bool craterPlacement = false;
    bool placingCrater = false;

        [SerializeField]
    public float brushSize = 0.1f;
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
        {
            craterPlacement ^= true;
        }
        if (placingCrater)
        {
            planet = gameObject.GetComponent<Planet>();
            planet.PlaceCrater(selection.InverseTransformPoint(hit.point));
        }
        if (Input.GetMouseButtonUp(0))
        {
            placingCrater = false;
        }
            if (Physics.Raycast(ray, out hit)){
            selection = hit.transform;
            //selectionRenderer = selection.GetComponent<Renderer>();
            if(Input.GetMouseButtonDown(0)){
                if (craterPlacement)
                {
                    placingCrater = true;
                }
                else
                {
                    hitCoords.Add(selection.InverseTransformPoint(hit.point));
                }
            }
        }
    }

    Mesh getCurrentFace(Vector3 rayDirection, Transform planet){
        float angle;
        Vector3 faceForward;
        for(int i = 0; i < 6; i++){
            faceForward = planet.GetChild(i).forward;
            angle = Vector3.Angle(rayDirection, faceForward);
            if(angle < 45){
                return planet.GetChild(i).GetComponent<MeshFilter>().mesh;
            }
        }
        return null;
    }
    public List<Vector3> GetPaintedVertices(){
        foreach (Vector3 point in hitCoords){
            //Debug.Log(point);
        }
        return hitCoords;
    }
    
}
