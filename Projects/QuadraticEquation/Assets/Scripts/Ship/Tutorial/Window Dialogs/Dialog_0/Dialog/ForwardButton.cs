using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class ForwardButton : MonoBehaviour
    {

        // Data Members private and public ***************************************** //

        public AudioSource clickSource;             // refernce to audio source with click sound

        // User-Defined Methods **************************************************** //

        /// <summary>
        ///	Public function to give access to call PressForward()
        /// </summary>
        public void Press()
        {
            PressForward();
        }

        /// <summary>
        /// Increments iterator and calls the function that will 
        /// Set the rules page to be displayed to the next page
        /// Like turning pages
        /// </summary>
        private void PressForward()
        {
            clickSource.Play();
            if (RulesControl.iterator >= 1 && RulesControl.iterator < 4)
            {
                RulesControl.iterator++;
                RulesControl.RulesPageFlip();
            }
        }
    } // end class
} // end namespace