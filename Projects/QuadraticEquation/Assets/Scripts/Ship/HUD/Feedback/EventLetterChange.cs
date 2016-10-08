using UnityEngine;
using UnityEngine.UI; // Allow the use of the 'text' component
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class EventLetterChange : MonoBehaviour
    {

        /*                              EVENT LETTER CHANGE
         * This script is designed to display what 'letter' is to be selected when the 'What is...' appears on the screen.
         *  NOTE: This is NOT related to the 'Letter Box' object, which is an independent object.  This script will merely try to
         *   fetch the selected index letter (of the quadratic) and display it when the "What is" appears on the screen.
         *   
         * STRUCTURAL DEPENDENCY NOTES:
         *      GameEvent {Game Controller}
         *          |_ EventLetterChange
         *             |_ Letter Box
         * 
         * GOALS:
         *    Update the index letter [A|B|C] that will be used for the 'What Is...' message that is displayed on the screen.
         */




        // Declarations and Initializations
        // ---------------------------------
            // Accessors and Communication
                // Text UI component
                    private Text ThisText;
                // Link to the Letter Box's script; used for fetching char - index.
                    public LetterBox scriptLetterBox;
        // ----




        // Use this for initialization
        void Start()
        {
            // Initializations
                // Include the text UI component from this current object
                    ThisText = gameObject.GetComponent<Text>();
        } // Start()



        // Update the text component to hold the latest selected index from the 'Letter Box' object.
        private void UpdateIndex()
        {
            ThisText.text = scriptLetterBox.Access_SelectedIndex.ToString();
        } // UpdateIndex()



        // Would you kindly access a private function, UpdateIndex()? [NG] (I am touching a bit into Bioshock ;))
        public void Access_UpdateIndex()
        {
            UpdateIndex();
        } // Access_UpdateIndex()
    } // End of Class
} // Namepsace