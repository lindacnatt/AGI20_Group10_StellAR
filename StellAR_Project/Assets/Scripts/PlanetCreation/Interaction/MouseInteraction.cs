using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInteraction : MonoBehaviour{
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
    Mesh terrainFaceMesh;
    [HideInInspector]
    public List<Vector3> hitCoords;
    Vector3[] vertices;
    MotherPlanet planet;
    public bool craterPlacement;
    Button craterBtn;
    Text craterBtnTxt;
    //bool placingCrater = false;

    public float brushSize = 0.2f;
    [HideInInspector]
    public Vector3 interactionPoint;
    float timeToGo;
    [Range(0, 3)]
    public int noiseType = 0;
    Color toggledTxtColor = new Color(1.0f, 1.0f, 1.0f);
    Color untoggledTxtColor = new Color(0.5f, 0.5f, 0.5f);
    Color toggledColor = new Color(0.1f, 0.0f, 0.7f);
    Color untoggledColor = new Color(0.2f, 0.2f, 0.5f);
    bool paintAudioPlaying;
    AudioSource paSource;

    void Start(){
        planet = gameObject.GetComponent<MotherPlanet>();
        timeToGo = Time.fixedTime + 0.1f;
        craterPlacement = false;
        paintAudioPlaying = false;
        GameObject paGO = GameObject.Find("PaintPlanetAudio");
        if (paGO == null)
        {
            addSound();
        }
        else
        {
            paSource = paGO.GetComponent<AudioSource>();
        }
    }

    void Update(){
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.C))
        {
            craterPlacement ^= true;
        }
        paintAudioPlaying = false;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)){
            selection = hit.transform;
            if(craterPlacement){
                if(Input.GetMouseButton(0)){
                    paintAudioPlaying = true;
                    if (!paSource.isPlaying)
                    {
                        paSource.Play(0);
                    }
                    planet.shapeGenerator.craterGenerator.PlaceCrater(selection.InverseTransformPoint(hit.point));
                    planet.UpdateMesh();
                }
            }
            else{
                if(Input.GetMouseButton(0)){
                    paintAudioPlaying = true;
                    if (!paSource.isPlaying)
                    {
                        paSource.Play(0);
                    }
                    interactionPoint = selection.InverseTransformPoint(hit.point);
                    hitCoords.Add(interactionPoint);
                    planet.UpdateMesh();
                    //hitCoords.Add(selection.InverseTransformPoint(hit.point));
                }
            }   
        }
        if (!paintAudioPlaying)
        {
            if (paSource)
            {
                paSource.Pause();
            }
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

    void addSound()
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
}
