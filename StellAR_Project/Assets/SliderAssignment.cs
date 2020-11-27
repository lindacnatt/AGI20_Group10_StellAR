using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAssignment : MonoBehaviour
{
    public Slider StormPlacement;
    public Slider StormSpeed;
    public Slider StormSize;
    public Slider BandScale1;
    public Slider BandScale2;
    public Button Reseed;
    private GasPlanetShaderMAterialPropertyBlock matBlock;
    // Start is called before the first frame update
    void Start()
    {
        matBlock = GameObject.FindGameObjectWithTag("GasPlanet").GetComponent<GasPlanetShaderMAterialPropertyBlock>();
        StormPlacement.onValueChanged.AddListener(delegate { matBlock.ChangeStormPlacement(StormPlacement.value); });
        StormSpeed.onValueChanged.AddListener(delegate { matBlock.ChangeStormSpeed(StormSpeed.value); });
        StormSize.onValueChanged.AddListener(delegate { matBlock.ChangeStormSize(StormSize.value); });
        BandScale1.onValueChanged.AddListener(delegate { matBlock.ChangeBandScale1(BandScale1.value); });
        BandScale2.onValueChanged.AddListener(delegate { matBlock.ChangeBandScale2(BandScale2.value); });
        Reseed.onClick.AddListener(delegate { matBlock.ReSeed();  });
    }

}
