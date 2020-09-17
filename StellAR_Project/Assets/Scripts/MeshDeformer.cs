using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformer : MonoBehaviour
{
    Mesh deformingMesh;
    Vector3[] originalVertices, displayedVertices;
    Vector3[] orgNormals;
    
    public int deformingFactor = 0;
    // Start is called before the first frame update
    void Start(){
        deformingMesh = GetComponent<MeshFilter>().sharedMesh;
        originalVertices = deformingMesh.vertices;
        displayedVertices = new Vector3[originalVertices.Length];
        orgNormals  = deformingMesh.normals;
        for(int i = 0; i < originalVertices.Length; i++){
            displayedVertices[i] = originalVertices[i];
        }
    }

    void OnValidate(){
        for(int i = 0; i < originalVertices.Length; i++){
            updateVertice(i);
        }
    }

    // Update is called once per frame
    void updateVertice(int i){
        float rand = Random.Range(.0f, 1.0f);
        displayedVertices[i] += displayedVertices[i] + orgNormals[i] * rand;
    }

}
