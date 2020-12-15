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
    Button mountainBtn;
    Button flattenBtn;
    Color toggledColor = new Color(0.2f, 0.1f, 0.6f, 1);
    Color untoggledColor = new Color(0.5f, 0.5f, 0.5f, 1);
    Toggle oceanToggle;


    private Touch touch;
    private Vector2 _startingPos;
    private Quaternion rotationY;

    private float rotateSpeedMod = 1f;

    private bool colorsSet = false;

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
    }

    void Update(){
        paintAudioPlaying = false;
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
        {
            craterPlacement ^= true;
        }
        if (!colorsSet)
        {
            if (craterBtn != null)
            {
                if (craterPlacement)
                {
                    craterBtn.GetComponent<Image>().color = toggledColor;
                }
                else
                {
                    craterBtn.GetComponent<Image>().color = untoggledColor;
                }
            }
            if (mountainBtn != null && flattenBtn != null)
            {
                if (!craterPlacement)
                {
                    if (noiseType == 0)
                    {
                        mountainBtn.GetComponent<Image>().color = toggledColor;
                        flattenBtn.GetComponent<Image>().color = untoggledColor;
                    }
                    else
                    {
                        mountainBtn.GetComponent<Image>().color = untoggledColor;
                        flattenBtn.GetComponent<Image>().color = toggledColor;
                    }
                }
                else
                {
                    mountainBtn.GetComponent<Image>().color = untoggledColor;
                    flattenBtn.GetComponent<Image>().color = untoggledColor;
                }
            }

            colorsSet = true;
        }
        // only do stuff if mouse is down
        if (Input.GetMouseButton(0) && Input.touchCount < 2){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                selection = hit.transform;    
                // add cases for your tag and do stuff
                switch(selection.gameObject.tag){
                    case "Planet":{
                        MotherPlanet planet = selection.gameObject.GetComponent<MotherPlanet>();
                        canPaint = (GameObject.Find("2ModifyMeshPlanetColorScreen") != null);
                        if(canPaint){
                            // Play audio
                            if(!paSource.isPlaying){
                                paSource.Play(0);
                            }
                            // place crater
                            if(craterPlacement){
                                    planet.shapeGenerator.craterGenerator.PlaceCrater(selection.InverseTransformPoint(hit.point));
                            }
                            // place noise
                            else
                            {
                                    interactionPoint = selection.InverseTransformPoint(hit.point);
                                    planet.shapeGenerator.craterGenerator.checkIfCrater(interactionPoint/planet.shapeGenerator.settings.radius);
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

    public void toggleOcean()
    {
        planet.shapeSettings.zeroLvlIsOcean ^= true;
        oceanToggle = GameObject.Find("ToggleWater").GetComponent<Toggle>();
        if (planet.shapeSettings.zeroLvlIsOcean)
        {
            oceanToggle.isOn = true;
        }
        else
        {
            oceanToggle.isOn = false;
        }
        planet.shapeGenerator.elevationMinMax = new MinMax();
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

    public void setBtns()
    {
        if (GameObject.Find("ToggleMountain") != null)
        {
            mountainBtn = GameObject.Find("ToggleMountain").GetComponent<Button>();
            flattenBtn = GameObject.Find("ToggleFlatten").GetComponent<Button>();
            craterBtn = GameObject.Find("ToggleCraterMode").GetComponent<Button>();
        }
    }

    public void UpdateNoiseType(int type){
        noiseType = type;
        craterPlacement = false;
        colorsSet = false;
    }
    public void ToggleCraterPlacement(){
        craterPlacement = !craterPlacement;
        colorsSet = false;
    }
    public void RandomMesh(){
        for (var i = 0; i <= 10; i++){
            interactionPoint = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            GameObject.Find("IcoSpherePlanet(Clone)").GetComponent<MotherPlanet>().UpdateMesh();
        }
        
    }
}
