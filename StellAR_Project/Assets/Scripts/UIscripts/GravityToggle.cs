using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityToggle : MonoBehaviour
{
   private Button button;
   private string  nbody = "Change to Nbody";
   private string source = "Change to Source";
   private Text txt;
   private int count = 0;
 
     private void Start()
     {
         button = GetComponent<Button>();
         txt = gameObject.GetComponentInChildren(typeof(Text)) as Text;
         txt.text = source;
     }
 
     public void ToggleGravity(){

         if(txt.text == source){
             txt.text = nbody;
             ToggleGravityMode.nBodyGravity = false;
             print("source");
         }
         else{
             txt.text = source;
             ToggleGravityMode.nBodyGravity = true;
             print("nbdoy");
         }
         TrajectoryLineAnimation.traj.enabled=false;
         TrajectoryLineAnimation.traj.positionCount=0;
         TrajectorySimulation.destroyLine=false;
        
     }
}
