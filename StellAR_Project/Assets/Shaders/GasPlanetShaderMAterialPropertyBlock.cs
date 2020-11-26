using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GasPlanetShaderMAterialPropertyBlock : CelestialObject {

    //The color of the object
    public Color BandColorStorm; 
    public float StormSize;
    public float StormSpeed;
    public float StormStrength;
    public float StormPlacement;

    public float BandScale1;
    public float BandXSeed1;
    public float BandYSeed1;
    public Color BandColor1;
    
    public float BandScale2;
    public float BandXSeed2;
    public float BandYSeed2;
    public Color BandColor2;

    public Color BandColor3;

    public string CurrentlyCustomizing;

    private Renderer TheRenderer;

    private bool SeedToggle;


    
    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    public void ChangeStormSize(float value)
    {
        StormSize = value;
      
    }
    public void ChangeStormSpeed(float value)
    {
        StormSpeed = value;

    }
    public void ChangeStormStrength(float value)
    {

        StormStrength = value;
    }
    public void ChangeStormPlacement(float value)
    {

        StormPlacement = value;
    }
    public void ChangeBandScale1(float value)
    {
        BandScale1 = value;

    }
    public void ChangeBandScale2(float value)
    {
        BandScale2 = value;

    }
    public void ReSeed(bool value)
    {
        if (value != SeedToggle)
        {
            BandXSeed1 = UnityEngine.Random.Range(0, 10);
            BandXSeed2 = UnityEngine.Random.Range(0, 10);
            BandYSeed1 = UnityEngine.Random.Range(0, 10);
            BandYSeed2 = UnityEngine.Random.Range(0, 10);



        }

    }
    public void ChangeBandColor1(float value)
    {
        BandColor1.r = value;
    }
    public void ChangeBandColor2(float value)
    {
        BandColor2.r = value;
    }
    public void ChangeBandColor3(float value)
    {
        BandColor3.r = value;
    }

    public void SetMaterial(_gasPlanet data)
    {
        BandColorStorm = data.BandColorStorm;
        StormSize = data.StormSize;
        StormSpeed = data.StormSpeed;
        StormStrength = data.StormStrength;
        StormPlacement = data.StormPlacement;
        BandScale1 = data.BandScale1;
        BandXSeed1 = data.BandXSeed1;
        BandYSeed1 = data.BandYSeed1;
        BandColor1 = data.BandColor1;
        BandScale2 = data.BandScale2;
        BandXSeed2 = data.BandXSeed2;
        BandYSeed2 = data.BandYSeed2;
        BandColor2 = data.BandColor2;
        BandColor3 = data.BandColor3;



    }

    private void Awake()
    {
        //create propertyblock only if none exists
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();
        //Get a renderer component either of the own gameobject or of a child
        TheRenderer = GetComponentInChildren<Renderer>();
    }
    void Update()
    {
       
        //set the color property
        
        propertyBlock.SetColor("_BandColorStorm", BandColorStorm);
        propertyBlock.SetFloat("_StormSize", StormSize);
        propertyBlock.SetFloat("_StormStrength", StormStrength);
        propertyBlock.SetFloat("_StormSpeed", StormSpeed);
        propertyBlock.SetFloat("_StormPlacement", StormPlacement);

        propertyBlock.SetFloat("_BandScale1", BandScale1);
        propertyBlock.SetFloat("_BandXSeed1", BandXSeed1);
        propertyBlock.SetFloat("_BandYSeed1", BandYSeed1);
        propertyBlock.SetColor("_BandColor1", BandColor1);

        propertyBlock.SetFloat("_BandScale2", BandScale2);
        propertyBlock.SetFloat("_BandXSeed2", BandXSeed2);
        propertyBlock.SetFloat("_BandYSeed2", BandYSeed2);
        propertyBlock.SetColor("_BandColor2", BandColor2);

        propertyBlock.SetColor("_BandColor3", BandColor3);


        //apply propertyBlock to renderer
        TheRenderer.SetPropertyBlock(propertyBlock);
    }
} 
