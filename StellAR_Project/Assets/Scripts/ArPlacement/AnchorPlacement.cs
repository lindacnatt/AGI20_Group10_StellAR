using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class AnchorPlacement : MonoBehaviour
{
    private ARAnchorManager arAnchorManager;
    private GameObject spawnedAnchor; //instance of prefab that is placed
    public GameObject anchorPrefab; //the sun prefab
    public Camera ARCamera; //the camera
    private float distanceFromCamera = 3;
    private bool alreadyPlaced = false;
    public GameObject toEnable1; //To enable is everything that relies on having a sun to revolve around
    public GameObject toEnable2;
    public GameObject toEnable3; 
    public GameObject toDisable; //Tutorial text for sun placement


    private void Awake()
    {
        arAnchorManager = GetComponent<ARAnchorManager>();
        spawnedAnchor = Instantiate(anchorPrefab, ARCamera.transform.position + ARCamera.transform.forward * distanceFromCamera, ARCamera.transform.rotation);
    }

    private void Update()
    {
        if (!alreadyPlaced)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(1) == true)
            {
                /*Disable the following two lines to debug on a computer*/
                var anchor = arAnchorManager.AddAnchor(new Pose(spawnedAnchor.transform.position, spawnedAnchor.transform.rotation));
                spawnedAnchor.transform.SetParent(anchor.transform);
                
                toEnable1.SetActive(true);
                
                toEnable2.SetActive(true);
                
                toEnable3.SetActive(true);

                toDisable.SetActive(false);

                GetComponent<ARPlacementTrajectory>().enabled = true;
                alreadyPlaced = true;
                Destroy(this);
                return;
                
            }
            spawnedAnchor.transform.position = ARCamera.transform.position + ARCamera.transform.forward * distanceFromCamera;
            spawnedAnchor.transform.rotation = new Quaternion(0.0f, ARCamera.transform.rotation.y, 0.0f, ARCamera.transform.rotation.w);

        }

    }
}
