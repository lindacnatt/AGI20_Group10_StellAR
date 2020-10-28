using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityToggle : MonoBehaviour
{
   private Toggle toggle;
 
     private void Start()
     {
         toggle = GetComponent<Toggle>();
         toggle.onValueChanged.AddListener(OnToggleValueChanged);
     }
 
     private void OnToggleValueChanged(bool isOn)
     {
         ColorBlock cb = toggle.colors;
         if (isOn)
         {
             cb.normalColor = Color.green;
             cb.highlightedColor = Color.green;
         }
         else
         {
             cb.normalColor = Color.blue;
             cb.highlightedColor = Color.blue;
         }
         toggle.colors = cb;
     }
}
