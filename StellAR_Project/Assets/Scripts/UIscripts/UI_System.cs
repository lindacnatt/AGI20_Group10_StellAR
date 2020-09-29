using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stellar.UI
{
    
    public class UI_System : MonoBehaviour
    {

        #region Variables
        public Component[] screens = new Component[0];
        #endregion

        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            screens = GetComponentsInChildren<UI_Screen>(true);
        }
    

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
