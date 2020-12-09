using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SavingSystem : MonoBehaviour
{
    int saveState;
    Text systemName;
    Color saveState0Col;
    Color saveState1Col;
    Color saveState2Col;
    Image buttonImg;
    Text btnTxt;
    GameObject systemNameGO;
    private float timer = 0.0f;
    SaveLoadScenes sceneMngr;

    GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        saveState = 0;
        saveState0Col = new Color(0.8f, 0.2f, 0.3f, 0);
        saveState1Col = new Color(0.8f, 0.2f, 0.3f, 1);
        saveState2Col = new Color(0.2f, 0.8f, 0.3f, 1);
        buttonImg = transform.Find("Save").GetComponent<Image>();
        systemNameGO = transform.Find("SystemName").gameObject;
        systemNameGO.SetActive(false);
        btnTxt = transform.Find("Save").GetComponentInChildren<Text>();
        systemName = transform.Find("SystemName").transform.Find("Text").GetComponent<Text>();
        sceneMngr = GameObject.Find("SceneManager").GetComponent<SaveLoadScenes>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bool saveUI = false;
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                if (results[0].gameObject.tag == "SaveUI")
                {
                    saveUI = true;
                }
            }
            if (!saveUI)
            {
                saveState = 0;
            }
        }

        if (saveState == 0)
        {
            buttonImg.color = saveState0Col;
            btnTxt.text = "Save";
            systemNameGO.SetActive(false);
        }

        if (saveState == 1)
        {
            buttonImg.color = saveState1Col;
            if (systemName.text.Length >= 3)
            {
                saveState = 2;
            }
        }

        if (saveState == 2)
        {
            buttonImg.color = saveState2Col;
            if (systemName.text.Length < 3)
            {
                saveState = 1;
            }
        }

        if (saveState == 3)
        {
            btnTxt.text = "Saving";
            sceneMngr.saveSpecificSystem(systemName.text);
            sceneMngr.ToggleSave();
            saveState = 4;
        }

        if (saveState == 4)
        {
            btnTxt.text = "Saved";
            timer += Time.deltaTime;
            if (timer > 2)
            {
                saveState = 0;
            }
        }
    }

    public void SaveSystem()
    {
        if (saveState == 0)
        {
            saveState = 1;
            buttonImg.color = saveState1Col;
            systemNameGO.SetActive(true);
        }
        if (saveState == 2)
        {
            saveState = 3;
        }
    }
}
