using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class BackButton : MonoBehaviour
    {

        // Data Members private and public ***************************************** //

        public AudioSource clickSource;         // reference to an audio sound that isn't in the game yet ------ 9/20/15 --------

        // User-Defined Methods **************************************************** //

        /// <summary>
        /// Public function to give access to call PressBack()
        /// </summary>

        public void Press()
        {
            PressBack();
        }

        /// <summary>
        /// Plays the sound for when it is clicked
        /// Checks to see if RulesControl.iterator is within the acceptable
        /// Range to decrement and if it is the method will
        /// Decrement iterator and check Rules page flip
        /// </summary>
        private void PressBack()
        {
            clickSource.Play();
            if (RulesControl.iterator > 1 && RulesControl.iterator <= 4)
            {
                RulesControl.iterator--;
                RulesControl.RulesPageFlip();
            }
        }
    } // end class
} // end namespace