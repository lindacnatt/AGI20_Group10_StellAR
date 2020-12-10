using Stellar.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationStepManager : MonoBehaviour
{
    public bool Gas = false;
    public bool Rock = false;
    public bool StartScreen = true;
    public UI_System ScreenSys;
    public UI_Screen Screen1;
    public UI_Screen Screen21;
    public UI_Screen Screen22;
    public UI_Screen Screen3;
    public UI_Screen Screen4;
    public UI_Screen Screen5;
    public GameObject GasPrefab;
    public GameObject RockPrefab;
    public float distcam;
    public float yaxis;

    public void ChangeScreen(UI_Screen nextScreen)
    {
        if (StartScreen)
        {
            if(Gas)
            {
                StartScreen = false;

                ScreenSys.SwitchScreens(aScreen: Screen21);
                
            }
            else if(Rock)
            {
                StartScreen = false;
                ScreenSys.SwitchScreens(aScreen: Screen22);
            }

        }
        else
        {
            ScreenSys.SwitchScreens(nextScreen);
        }
    }

    public void ToggleType(string pType)
    {
        if(pType == "Gas")
        {
            if (Gas)
            {
                return;
            }
            else
            {
                Gas = true;
                if(Rock == true)
                {
                    Rock = false;
                    Destroy(GameObject.FindGameObjectWithTag("Planet"));
                    Debug.Log("DESTROYROCK");
                }
                Instantiate(GasPrefab, new Vector3(0, yaxis, distcam), Quaternion.identity);

                //Debug.Log("MAKEGAS");
            }

        }
        else if(pType == "Rock")
        {
            if (Rock)
            {
                return;
            }
            else
            {
                Rock = true;
                if (Gas == true)
                {
                    Gas = false;
                    Destroy(GameObject.FindGameObjectWithTag("GasPlanet"));
                    Debug.Log("DESTROYGAS");
                }
                Instantiate(RockPrefab, new Vector3(0, 0.8f, 0), Quaternion.identity);
                Interactor interactor = GameObject.Find("Interactor").gameObject.GetComponent<Interactor>();
                interactor.planet = GameObject.FindGameObjectWithTag("Planet").gameObject.GetComponent<MotherPlanet>();
                //Debug.Log("MAKEREOCK");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
