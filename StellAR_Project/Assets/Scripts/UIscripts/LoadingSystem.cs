using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class LoadingSystem : MonoBehaviour
{
    int loadState;
    Text systemName;
    Image buttonImg;
    Text btnTxt;
    GameObject loadViewGO;
    private float timer = 0.0f;
    SaveLoadScenes sceneMngr;
    string path;
    GameObject loadContentGO;
    public GameObject savedSystemPrefab;
    public GameObject delSystemPrefab;
    GameObject noCont;

    GraphicRaycaster m_Raycaster;
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        loadState = 0;
        buttonImg = transform.Find("Load").GetComponent<Image>();
        loadViewGO = transform.Find("ScrollView").gameObject;
        btnTxt = transform.Find("Load").GetComponentInChildren<Text>();
        sceneMngr = GameObject.Find("SceneManager").GetComponent<SaveLoadScenes>();
        path = Application.persistentDataPath + "/savedSystems/";
        loadContentGO = loadViewGO.transform.Find("Viewport").Find("Content").gameObject;
        noCont = loadContentGO.transform.Find("NoContent").gameObject;
        noCont.SetActive(false);
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            bool loadUI = false;
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                if (results[0].gameObject.tag == "LoadUI")
                {
                    loadUI = true;
                }
            }
            if (!loadUI)
            {
                loadState = 0;
            }
        }

        if (loadState == 0)
        {
            loadViewGO.SetActive(false);
            btnTxt.text = "Load";
        }

        else if (loadState == 1)
        {
            loadViewGO.SetActive(true);
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] info = dir.GetFiles("*.data");
            int noFiles = 0;
            foreach (Button btn in loadContentGO.GetComponentsInChildren<Button>())
            {
                Destroy(btn.gameObject);
            }
            foreach (FileInfo f in info)
            {
                noFiles++;
                string btnStr = Path.GetFileNameWithoutExtension(f.ToString());
                var button = Instantiate(savedSystemPrefab.GetComponent<Button>(), loadContentGO.transform);
                var rectTransform = button.GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(1, 1, 1);
                button.GetComponentInChildren<Text>().text = btnStr;
                button.tag = "LoadUI";
                button.transform.Find("Text").tag = "LoadUI";
                string name = button.GetComponentInChildren<Text>().text.ToString();
                button.onClick.AddListener(() => sceneMngr.loadSpecificSystem(name));

                var delButton = Instantiate(delSystemPrefab.GetComponent<Button>(), loadContentGO.transform);
                var delrectTransform = delButton.GetComponent<RectTransform>();
                delrectTransform.localScale = new Vector3(1, 1, 1);
                delButton.tag = "LoadUI";
                delButton.transform.Find("Text").tag = "LoadUI";
                delButton.onClick.AddListener(() => sceneMngr.delSpecificSystem(name));
            }
            if (noFiles == 0)
            {
                noCont.SetActive(true);
            }
            else
            {
                noCont.SetActive(false);
            }
            loadState = 2;
        }

        else if (loadState == 2)
        {
            loadViewGO.SetActive(true);
        }
    }

    public void LoadSystem()
    {
        if (loadState == 0)
        {
            loadState = 1;
        }
        else if (loadState == 3)
        {
            loadState = 2;
        }
    }
}
