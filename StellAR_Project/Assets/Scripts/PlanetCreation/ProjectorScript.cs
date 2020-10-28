using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScript : MonoBehaviour
{   
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
    Quaternion rotation;

    // Update is called once per frame
    void Update(){ // changes the projection to point in the direction of the mouse
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rotation = Quaternion.LookRotation(ray.direction, new Vector3(1, 0, 0));
        transform.rotation = rotation;
    }
}
