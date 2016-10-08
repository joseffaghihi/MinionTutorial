using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class TutorialSkipButton : MonoBehaviour
    {

        /*                    TUTORIAL SKIP BUTTON
         * This script, being simple, allows the player to skip the entire tutorial sequence.
         *      This is done by sending a broadcast signal (or event) to abort or stop the tutorial sequence.
         *  
         * GOALS:
         *  When activated (or true), stop the tutorial sequence and start the game or the next part of the game.
         */


        // Declarations and Initializations
        // ---------------------------------
            // Accessors and Communication
                public delegate void SkipTutorial();
                public static event SkipTutorial SkipTutorialDemand;
        // ----

        

        /// <summary>
        ///     Send signal to stop the tutorial for those listening scripts.
        /// </summary>
        private void Destroy()
        {
            SkipTutorialDemand();
        } // Destroy()



        /// <summary>
        ///     A public access-bridge which will allow the call to the 'Destroy()' function.
        ///     This is accessed by the Unity UI Button controls via Inspector.
        /// </summary>
        public void Access_Destroy()
        {
            Destroy();
        } // Access_Destroy
    } // End of Class
} // Namespace