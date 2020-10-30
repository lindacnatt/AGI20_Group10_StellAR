using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanetShaderMAterialPropertyBlock : MonoBehaviour {

    [SerializeField] Color MyColor = new Color();
    private MaterialPropertyBlock myMatBlock = new MaterialPropertyBlock();
    private MeshRenderer myMeshRenderer = new MeshRenderer();
    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();

    }
    private void Update()
    {
        myMatBlock.SetColor("BandColorStorm", MyColor);
        myMeshRenderer.SetPropertyBlock(myMatBlock);
    }
}
