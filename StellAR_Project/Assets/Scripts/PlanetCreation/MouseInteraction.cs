using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour{
    
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
    [SerializeField]

    Mesh terrainFaceMesh;
    Vector3 hitCoord;
    Vector3[] vertices;
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)){
            selection = hit.transform;
            selectionRenderer = selection.GetComponent<Renderer>();
            
            if(selectionRenderer != null){
                if(Input.GetMouseButtonDown(0)){
                    hitCoord = transform.InverseTransformPoint(hit.point);
                    terrainFaceMesh = getCurrentFace(ray.direction, selection);
                    vertices = terrainFaceMesh.vertices;
                    Debug.Log(vertices[0]);
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
    
}
