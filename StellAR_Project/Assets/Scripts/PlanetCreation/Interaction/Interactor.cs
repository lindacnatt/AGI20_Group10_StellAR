using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour{
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
 
    public MotherPlanet planet;

    // planet shape stuff
    public float brushSize = 0.2f;
    [Range(-1, 2)]
    public int noiseType = 0;
    public Vector3 interactionPoint;

    // states
    bool canPaint;
    public bool craterPlacement;

    // sound stuff
    bool paintAudioPlaying;
    AudioSource paSource;

    //craterModeUIStuff & OceanToggleUIStuff
    Button craterBtn;
    Text craterBtnTxt;
    Color toggledTxtColor = new Color(1.0f, 1.0f, 1.0f);
    Color untoggledTxtColor = new Color(0.5f, 0.5f, 0.5f);
    Color toggledColor = new Color(0.1f, 0.0f, 0.7f);
    Color untoggledColor = new Color(0.2f, 0.2f, 0.5f);
    Button oceanBtn;
    Text oceanBtnTxt;

    void Start(){
        canPaint = false;
        craterPlacement = false;        
        GameObject paGO = GameObject.Find("PaintPlanetAudio");
        if (paGO == null){
            AddSound();
        }
        else{
            paSource = paGO.GetComponent<AudioSource>();
        }
        //GameObject MpGO = GameObject.Find("IcoSpherePlanet(Clone)");
        //Debug.Log(MpGO);
        //planet = GameObject.Find("IcoSpherePlanet(Clone)").GetComponent<MotherPlanet>();
        //Debug.Log(planet);
    }

    void Update(){
        paintAudioPlaying = false;
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
        {
            craterPlacement ^= true;
        }

        // only do stuff if mouse is down
        if(Input.GetMouseButton(0)){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                selection = hit.transform;    
                // add cases for your tag and do stuff
                switch(selection.gameObject.tag){
                    case "Planet":{
                        canPaint = (GameObject.Find("2ModifyMeshPlanetColorScreen") != null);
                        if(canPaint){
                            // Play audio
                            if(!paSource.isPlaying){
                                paSource.Play(0);
                            }
                            // place crater
                            if(craterPlacement){
                                selection.gameObject.GetComponent<MotherPlanet>().shapeGenerator.craterGenerator.PlaceCrater(selection.InverseTransformPoint(hit.point));
                            }
                            // place noise
                            else{
                                interactionPoint = selection.InverseTransformPoint(hit.point);
                            }
                            // update mesh 
                            selection.gameObject.GetComponent<MotherPlanet>().UpdateMesh();
                            paintAudioPlaying = true;
                        }
                        break;
                    }
                    case "GasPlanet": {
                        // Do something
                        Debug.Log("GasPlanet");
                        break;
                    }
                }
            }
        }
        if(!paintAudioPlaying){
            paSource.Pause();
        }
        
    }

    public void craterMode()
    {
        craterBtn = GameObject.Find("ToggleCraterMode").GetComponent<Button>();
        craterBtnTxt = craterBtn.GetComponentInChildren<Text>();
        craterPlacement ^= true;
        if (craterPlacement)
        {
            craterBtn.GetComponent<Image>().color = toggledColor;
            craterBtnTxt.color = toggledTxtColor;
        }
        else
        {
            craterBtn.GetComponent<Image>().color = untoggledColor;
            craterBtnTxt.color = untoggledTxtColor;
        }

    }

    public void toggleOcean()
    {
        planet.shapeSettings.zeroLvlIsOcean ^= true;
        oceanBtn = GameObject.Find("ToggleWater").GetComponent<Button>();
        oceanBtnTxt = oceanBtn.GetComponentInChildren<Text>();
        if (planet.shapeSettings.zeroLvlIsOcean)
        {
            oceanBtn.GetComponent<Image>().color = toggledColor;
            oceanBtnTxt.color = toggledTxtColor;
        }
        else
        {
            oceanBtn.GetComponent<Image>().color = untoggledColor;
            oceanBtnTxt.color = untoggledTxtColor;
        }
        planet.shapeGenerator.elevationMinMax = new MinMax();
        Debug.Log("Min: " + planet.shapeGenerator.elevationMinMax.Min);
        Debug.Log("Max: " + planet.shapeGenerator.elevationMinMax.Max);
        planet.UpdateMesh();
        planet.GenerateColors();
    }

    private void AddSound()
    {
        GameObject paGO = new GameObject("PaintPlanetAudio");
        paGO.AddComponent<AudioSource>();
        paSource = paGO.GetComponent<AudioSource>();
        paSource.clip = Resources.Load<AudioClip>("Audio/FX-Ambient/Spaceship Engine Light");
        paSource.volume = 0.75f;
        paSource.loop = true;
        paSource.playOnAwake = false;
        paSource.pitch = 0.5f;
        paGO.AddComponent<AudioHighPassFilter>();
        AudioHighPassFilter highPass = paGO.GetComponent<AudioHighPassFilter>();
        highPass.cutoffFrequency = 10;
        paGO.AddComponent<AudioLowPassFilter>();
        AudioLowPassFilter lowPass = paGO.GetComponent<AudioLowPassFilter>();
        lowPass.cutoffFrequency = 1500;
        paGO.AddComponent<AudioDistortionFilter>();
        AudioDistortionFilter dist = paGO.GetComponent<AudioDistortionFilter>();
        dist.distortionLevel = 0.85f;
    }

    public void UpdateNoiseType(int type){
        noiseType = type;
        craterPlacement = false;
    }
    public void ToggleCraterPlacement(){
        craterPlacement = !craterPlacement;
    }
}
