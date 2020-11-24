 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Stellar.UI
{
    
    public class UI_System : MonoBehaviour
    {

        #region Variables
        [Header("Main Properties")]
        public UI_Screen _mStartScreen;
        [Header("system Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent();

        private Component[] screens = new Component[0];

        private UI_Screen previousScreen;
        public UI_Screen PreviousScreen{get{return previousScreen;}}

        public UI_Screen currentScreen;
        public UI_Screen CurrentScreen{get{return currentScreen;}}
        public float transitionTime;
        
        #endregion
        

        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            screens = GetComponentsInChildren<UI_Screen>(true);

            if(_mStartScreen){
                SwitchScreens(_mStartScreen);
            }
        }
        #endregion

        #region Helper Methods
        public void SwitchScreens(UI_Screen aScreen)
        {
            if(aScreen)
            {
                if(currentScreen)
                {
                    currentScreen.CloseScreen();
                    previousScreen = currentScreen;
                }

                currentScreen = aScreen;
                currentScreen.gameObject.SetActive(true);
                currentScreen.StartScreen();

                if(onSwitchedScreen !=null) {
                    onSwitchedScreen.Invoke();
                }

            }
        }
        public void GoToPreviousScreen(){
            if(previousScreen){
                SwitchScreens(previousScreen);
            }
        }
        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(WaitToLoadScene(sceneIndex));

        }

        IEnumerator WaitToLoadScene(int sceneIndex){
            
            //Add animation
            //Wait
            yield return new WaitForSeconds(transitionTime);
            //Load scene
            SceneManager.LoadScene(sceneIndex);
        }
        #endregion
    }
}
