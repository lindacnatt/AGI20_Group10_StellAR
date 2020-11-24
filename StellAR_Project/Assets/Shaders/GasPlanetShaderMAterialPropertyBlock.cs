using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanetShaderMAterialPropertyBlock : CelestialObject {

    //The color of the object
    public Color BandColorStorm; 
    public float StormSize;
    public float StormSpeed;
    public float StormStrength;
    public float StormPlacement;
    public Vector2 SpiralDefinition;

    public float BandScale1;
    public float BandXSeed1;
    public float BandYSeed1;
    public Color BandColor1;
    
    public float BandScale2;
    public float BandXSeed2;
    public float BandYSeed2;
    public Color BandColor2;


    
    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    public void ChangeFloatProperty(float value)
    {

    }

    void OnValidate()
    {
        //create propertyblock only if none exists
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();
        //Get a renderer component either of the own gameobject or of a child
        Renderer renderer = GetComponentInChildren<Renderer>();
        //set the color property
        
        propertyBlock.SetColor("_BandColorStorm", BandColorStorm);
        propertyBlock.SetFloat("_StormSize", StormSize);
        propertyBlock.SetFloat("_StormStrength", StormStrength);
        propertyBlock.SetFloat("_StormSpeed", StormSpeed);
        propertyBlock.SetFloat("_StormPlacement", StormPlacement);
        propertyBlock.SetVector("_SpiralDefinition", SpiralDefinition);

        propertyBlock.SetFloat("_BandScale1", BandScale1);
        propertyBlock.SetFloat("_BandXSeed1", BandXSeed1);
        propertyBlock.SetFloat("_BandYSeed1", BandYSeed1);
        propertyBlock.SetColor("_BandColor1", BandColor1);

        propertyBlock.SetFloat("_BandScale2", BandScale2);
        propertyBlock.SetFloat("_BandXSeed2", BandXSeed2);
        propertyBlock.SetFloat("_BandYSeed2", BandYSeed2);
        propertyBlock.SetColor("_BandColor2", BandColor2);


        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
} 
