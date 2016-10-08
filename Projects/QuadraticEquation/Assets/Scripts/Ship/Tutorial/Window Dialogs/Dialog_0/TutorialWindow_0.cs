using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MinionMathMayhem_Ship
{
    public class TutorialWindow_0 : MonoBehaviour
    {
        /*                          TUTORIAL WINDOW DIALOG
         * This script will run the window dialog tutorial when activated - automatically
         * 
         * 
         * Important Notes:
         *      Execution Order
         *          http://docs.unity3d.com/Manual/class-ScriptExecution.html
         *
         * 
         * Goals:
         *      Display the desired dialog tutorial
         *      Send an event signal that the tutorial has ended.
         */


        // Declarations and Initializations
        // ---------------------------------
            // Specific window dialog components
                //  Rules Control on the rulesCanvas GameObject
                    public RulesControl rulesControl;
                // Critical components; used for activating and deactivating the objects
                    public List<GameObject> tutorialObjectArray = new List<GameObject>();
        // ---------------------------------



        /// <summary>
        ///     Built-In Unity function
        ///     Automatically executes as soon as the actor is activated within the virtual world.
        /// 
        ///     NOTE:
        ///         This is the SECOND function to be called; the first is typically the Awake() function.
        /// </summary>
        private void OnEnable()
        {
            // Enable the essential critical components necessary
                ToggleGameObjects_Array(true);
            // Display the tutorial
                Tutorial_Play();
        } // OnEnable()



        /// <summary>
        ///     Activates the essential tutorial components necessary for the functionality to work as intended.
        /// </summary>
        /// <param name="state">
        ///     When true, enables all of the listed GameObjects tied into the array.
        ///     When false, disables all of the listed GameObjects tied into the array.
        /// </param>
        private void ToggleGameObjects_Array(bool state)
        {
            for(int i = 0; i < tutorialObjectArray.Count; i++)
            {
                tutorialObjectArray[i].SetActive(state);
            }
        } // ToggleGameObjects_Array()



        /// <summary>
        ///     Displays the tutorial on the screen
        /// 
        ///     NOTE: This calls an coroutine as the window dialog manages itself.
        /// </summary>
        private void Tutorial_Play()
        {
            StartCoroutine(Tutorial_Play_Yield());
        } // Tutorial_Play()



        /// <summary>
        ///     Starts the window dialog tutorial
        /// </summary>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator Tutorial_Play_Yield()
        {
            // Call the tutorial at its entry point
                yield return (StartCoroutine(rulesControl.Access_WaitForRulesToFinish()));

            // Finished tutorial

            // Deactivate the tutorial components
                ToggleGameObjects_Array(false);
            // Deactivate self
                gameObject.SetActive(false);

            // Close 
                yield break;
        } // Tutorial_Play_Yield()
    } // End of Class
} // Namespace