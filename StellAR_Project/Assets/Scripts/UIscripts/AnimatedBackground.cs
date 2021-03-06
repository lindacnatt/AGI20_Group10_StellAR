﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
    
        Material mat = mr.material; //first copy

        Vector2 offset = mat.GetTextureOffset("_MainTex");

        offset.x += Time.deltaTime / 80f;
        offset.y += Time.deltaTime / 110f;


        mat.SetTextureOffset("_MainTex", offset);
    }
}
