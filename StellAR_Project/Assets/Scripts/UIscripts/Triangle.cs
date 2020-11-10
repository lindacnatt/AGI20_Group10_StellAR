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
    BaryCentric bary;
    GameObject handle;
    

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

        vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(0.5f,0.866025404f,0)*size,
            new Vector3(1,0,0)*size
        };
        mesh.vertices = vertices;

        triangles = new[]{0,1,2};

        mesh.triangles = triangles;

        Debug.Log(BaryCentric.getWeights(new Vector3(0.1f,0.1f,0.1f) , vertices));
        
        handle = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        if(hit){
            selection = hit.transform;
            selectionRenderer = selection.GetComponent<Renderer>();

            if(Input.GetMouseButtonDown(0) && selectionRenderer != null){
                handle.transform.position = hit.point;
                Debug.Log(BaryCentric.getWeights(selection.InverseTransformPoint(hit.point), vertices));
            }
        }
    }
}
