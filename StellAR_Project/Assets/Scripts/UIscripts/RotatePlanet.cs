using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    private Touch touch;
    private Vector2 _startingPos;
    private Quaternion rotationY;

    private float rotateSpeedMod = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedMod, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
        
    }
}
