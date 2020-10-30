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
       TrajectoryLineAnimation.traj.enabled=false;
       TrajectoryLineAnimation.traj.positionCount=0;
       TrajectorySimulation.destroyLine=false;
       
   }
}
