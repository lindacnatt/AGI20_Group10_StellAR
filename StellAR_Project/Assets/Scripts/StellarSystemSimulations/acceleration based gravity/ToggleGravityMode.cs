using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGravityMode : MonoBehaviour
{
   public static bool nBodyGravity;

  void Awake(){
      nBodyGravity=true;
  }
   public void Toggle(){
       nBodyGravity = !nBodyGravity;
   }

    /*// Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.G)){
            nBodyGravity = !nBodyGravity;
        }
    }*/
}
