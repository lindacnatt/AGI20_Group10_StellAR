using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class Scaling : MonoBehaviour
{


    public float varianceInDistances = 5.0F; 
    private float touchDelta = 0.0F; 
    private Vector2 prevDist = new Vector2(0, 0); 
    private Vector2 curDist = new Vector2(0, 0); 
    private ARSessionOrigin SessionOrigin;
    public Transform ContentHolder;
    public Text debug;

    // Start is called before the first frame update
    void Awake()
    {
        SessionOrigin = GetComponent<ARSessionOrigin>();
        SessionOrigin.MakeContentAppearAt(ContentHolder, ContentHolder.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches

            prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));

            touchDelta = curDist.magnitude - prevDist.magnitude;

            float zoomfactor = (Mathf.Clamp(Mathf.Abs(touchDelta / 100), 0f, 100f));
            if ((touchDelta + varianceInDistances < 1) /*&& (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed)*/) //decrease scale of sessionorigin means a zoom out
            {
                //debug.text = SessionOrigin.transform.localScale.x.ToString() + "out";
                if (SessionOrigin.transform.localScale.x > -8f)
                {
                    SessionOrigin.transform.localScale = new Vector3(SessionOrigin.transform.localScale.x - zoomfactor, SessionOrigin.transform.localScale.y - zoomfactor, SessionOrigin.transform.localScale.z - zoomfactor);
                }
                else
                    SessionOrigin.transform.localScale = new Vector3(-8, -8, -8);


            }
            else if ((touchDelta + varianceInDistances > 1) /*&& (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed)*/) //increase scale of sessionorigin means a zoom in
            {
                //debug.text = SessionOrigin.transform.localScale.x.ToString() + "in";
                if (SessionOrigin.transform.localScale.x < 1.5f)
                {
                    SessionOrigin.transform.localScale = new Vector3(SessionOrigin.transform.localScale.x + zoomfactor, SessionOrigin.transform.localScale.y + zoomfactor, SessionOrigin.transform.localScale.z + zoomfactor);
                }
                else
                    SessionOrigin.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);


            }



        }
    }

    public void OnClickZoom()
    {
        SessionOrigin.transform.localScale *= 1.1f;
       
    }
}
