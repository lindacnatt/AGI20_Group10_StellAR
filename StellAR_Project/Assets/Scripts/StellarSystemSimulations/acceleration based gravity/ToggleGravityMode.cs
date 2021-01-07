using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGravityMode : MonoBehaviour
{
   public static bool nBodyGravity = true;
   public Toggle tgl;

  
  public void ChangeToggleStart(){
      tgl.isOn=nBodyGravity;
  }

  
   public void Toggle(){
       nBodyGravity = !nBodyGravity;
       TrajectoryLineAnimation.traj.enabled=false;
       TrajectoryLineAnimation.traj.positionCount=0;
       TrajectorySimulation.destroyLine=false;
       
   }
}
