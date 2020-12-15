using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCOs : MonoBehaviour
{

    private bool showing = false;
    GameObject newPbtn;
    GameObject newAbtn;
    public GameObject asteroid;

    private void Awake()
    {
        newPbtn= transform.Find("NewPlanet").gameObject;
        newAbtn = transform.Find("NewAsteroid").gameObject;
    }

    public void onClickAddNew()
    {

        if (!showing)
        {
            newAbtn.SetActive(true);
            newPbtn.SetActive(true);
            iTween.MoveBy(newPbtn, iTween.Hash("y", 200, "easeType", "easeInOutExpo", "time", 0.3));
            iTween.ScaleBy(newPbtn, iTween.Hash("amount", new Vector3(5, 5, 5), "easeType", "easeInOutExpo", "time", 0.3));
            iTween.FadeTo(newPbtn, iTween.Hash("alpha", 1, "easeType", "easeInOutExpo", "time", 0.3));

            iTween.MoveBy(newAbtn, iTween.Hash("y", 400, "easeType", "easeInOutExpo", "time", 0.3));
            iTween.ScaleBy(newAbtn, iTween.Hash("amount", new Vector3(5, 5, 5), "easeType", "easeInOutExpo", "time", 0.3));
            iTween.FadeTo(newAbtn, iTween.Hash("alpha", 1, "easeType", "easeInOutExpo", "time", 0.3));
            showing = true;
        }
        else
        {
            minimize(0);
        }
    }

    public void onObjectChosen()
    {
        minimize(0.4);
    }

    private void minimize(double delay)
    {
        iTween.MoveBy(newPbtn, iTween.Hash("y", -200, "easeType", "easeInOutExpo", "time", 0.3, "delay", delay));
        iTween.ScaleBy(newPbtn,
            iTween.Hash(
                "amount", new Vector3(0.2f, 0.2f, 0.2f),
                "easeType", "easeInOutExpo",
                "time", 0.3,
                "oncomplete", "hideBtn",
                "oncompletetarget", gameObject,
                "oncompleteparams", newPbtn,
                "delay", delay)
            );
        iTween.FadeTo(newPbtn, iTween.Hash("alpha", 0, "easeType", "easeInOutExpo", "time", 0.3, "delay", delay));

        iTween.MoveBy(newAbtn, iTween.Hash("y", -400, "easeType", "easeInOutExpo", "time", 0.3, "delay", delay));
        iTween.ScaleBy(newAbtn,
            iTween.Hash(
                "amount", new Vector3(0.2f, 0.2f, 0.2f),
                "easeType", "easeInOutExpo",
                "time", 0.3,
                "oncomplete", "hideBtn",
                "oncompletetarget", gameObject,
                "oncompleteparams", newAbtn,
                "delay", delay)
            );
        iTween.FadeTo(newAbtn, iTween.Hash("alpha", 0, "easeType", "easeInOutExpo", "time", 0.3, "delay", delay));
        showing = false;
    }

    public void hideBtn(GameObject btn)
    {
        btn.SetActive(false);
    }

    public void AddAsteroid()
    {
        var planets = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject co in planets)
        {
            if (!co.staticBody && !co.isShot)
            {
                Destroy(co.gameObject);
            }
        }
        GameObject asteroidGO = Instantiate(asteroid);
        asteroidGO.GetComponent<CelestialObject>().SetName("Asteroid");

        //asteroidGO.GetComponent<CelestialObject>().SetMass();
        GameObject ARSessOrig = GameObject.Find("AR Session Origin");
        ARPlacementTrajectory placement = ARSessOrig.GetComponent<ARPlacementTrajectory>();
        placement.setGOtoInstantiate(asteroidGO);
    }
}
