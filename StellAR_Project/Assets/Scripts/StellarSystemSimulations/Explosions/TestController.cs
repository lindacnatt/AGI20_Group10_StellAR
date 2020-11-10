using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update
    bool move;
    public Rigidbody rb;
    public float thrust = 1.0f;
    void Start()
    {
        move = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        bool mousedown = Input.GetMouseButtonDown(0);
        bool mouseup = Input.GetMouseButtonUp(0);

        if (mousedown)
        {
            move = true;
        }
        if (mouseup)
        {
            move = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.z -= 1;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.z += 1;
            this.transform.position = position;
        }
        if (move)
        {
            Vector3 position = this.transform.position;
            if (mouseX != 0)
            {
                //position.x += mouseX;
                rb.AddForce(transform.right * mouseX * 10);
            }
            if (mouseY != 0)
            {
                //position.y += mouseY;
                rb.AddForce(transform.up * mouseY * 10);
            }
        }
    }
}
