using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MinionMathMayhem_Ship
{
    public class MovieSetup_0 : MonoBehaviour
    {
        /*                      TUTORIAL MOVIE SETUP [INTRODUCTION]
         * This class is designed to monitor the nested components and follow with the Tutorial Algorithm compliances
         *
         *  NESTED COMPONENTS:
         *      TutorialSkipButton
         *      TutorialMovie_0
         */


        #region Declarations and Initializations
            public List<GameObject> tutorialObjectArray = new List<GameObject>();
        #endregion



        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes as soon as the actor is activated within the virtual world.
        /// 
        ///     Subscribe to events (or listen to)
        /// </summary>
        private void OnEnable()
        {
            TutorialSkipButton.SkipTutorialDemand += Kill;
            TutorialMovie_0.TutorialStateEnded += Kill;
            ToggleGameObjects_Array(true);
        } // OnEnable()



        /// <summary>
        ///     Built-In Unity Function
        ///     Automatically executes as soon as the actor is deactivated from the virtual world
        /// 
        ///     Unsubscribe to events
        /// </summary>
        private void OnDisable()
        {
            TutorialSkipButton.SkipTutorialDemand -= Kill;
            TutorialMovie_0.TutorialStateEnded -= Kill;
            ToggleGameObjects_Array(false);
        } // OnDisable()



        /// <summary>
        ///     Activates the essential tutorial components necessary for the functionality to work as intended.
        /// </summary>
        /// <param name="state">
        ///     When true, enables all of the listed GameObjects tied into the array.
        ///     When false, disables all of the listed GameObjects tied into the array.
        /// </param>
        private void ToggleGameObjects_Array(bool state)
        {
            for (int i = 0; i < tutorialObjectArray.Count; i++)
            {
                tutorialObjectArray[i].SetActive(state);
            }
        } // ToggleGameObjects_Array()



        /// <summary>
        ///     As compliance with the Tutorial Algorithm Protocol, disable self to signify that -
        ///         this entire tutorial has finished.
        /// </summary>
        private void Kill()
        {
            gameObject.SetActive(false);
        } // Kill()
    } // End of Class
} // Namespace