using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour{
    Ray ray;
    RaycastHit hit;
    Transform selection;
    Renderer selectionRenderer;
 
    MotherPlanet planet;

    // planet shape stuff
    public bool craterPlacement;
    public float brushSize = 0.2f;
    public int noiseType = 0;
    public Vector3 interactionPoint;

    // sound stuff
    bool paintAudioPlaying;
    AudioSource paSource;

    void Start(){
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

        // only do stuff if mouse is down
        if(Input.GetMouseButton(0)){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                selection = hit.transform;    
                // add cases for your tag and do stuff
                switch(selection.gameObject.tag){
                    case "Planet":{
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
}
