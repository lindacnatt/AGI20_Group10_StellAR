using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{

    Mesh mesh;
    MeshRenderer meshRenderer;
    Vector3[] vertices;
    int[] triangles;
    Ray ray;
    RaycastHit2D hit;
    Transform selection;
    Renderer selectionRenderer;

    public Material material;

    [Range(1,10)]
    public float size;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = material;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertices = new[] {
            new Vector3(0,0,0),
            new Vector3(0.5f,0.866025404f,0)*size,
            new Vector3(1,0,0)*size
        };
        mesh.vertices = vertices;

        triangles = new[]{0,1,2};

        mesh.triangles = triangles;

        
    }

    // Update is called once per frame
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics2D.Raycast(ray.origin, ray.direction)){
            Debug.Log("Hej");
            selection = hit.transform;
            //selectionRenderer = selection.GetComponent<Renderer>();
            if(Input.GetMouseButtonDown(0)){
                //Debug.Log(BaryCentric.getWeights(selection.InverseTransformPoint(hit.point), vertices));
            }
        }
    }
}
