using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanetShaderMAterialPropertyBlock : MonoBehaviour {

    //The color of the object
    public Color MaterialColor;
    public float StormSize;

    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    void OnValidate()
    {
        //create propertyblock only if none exists
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();
        //Get a renderer component either of the own gameobject or of a child
        Renderer renderer = GetComponentInChildren<Renderer>();
        //set the color property
        propertyBlock.SetColor("BandColorStorm", MaterialColor);
        propertyBlock.SetFloat("_StormSize", StormSize);
        propertyBlock.SetVector();
        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
} 
