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
}
