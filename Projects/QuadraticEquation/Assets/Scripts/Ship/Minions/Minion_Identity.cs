using UnityEngine;
using UnityEngine.UI; // Used for 'text' type
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_Identity : MonoBehaviour
    {

        /*                      MINION IDENTITY
         * This script is designed for the minion to self generate its own randomized number given by a specific range.
         *  This essentially gives the minion its own unique number that they choose themselves.
         * 
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      |_ Problem Box
         * 
         * 
         * GOALS:
         *  Self-Generate a number
         */



        // Declarations and Initializations
        // ---------------------------------
            // This will hold the minion's unique number (or id)
                private int number;
            // This variable will hold the component to attach the self-assigned number on it's back.
                public Text numText;

            // Accessors and Communication
                // Hook onto the Randomization Number Set to retrive a unique number
                    //private Minion_RandomSetNumbers scriptMinion_RandomSetNumbers;
        // ----




        // Use this for initialization
        private void Awake()
        {
            // Initialize the component
                numText = GetComponentInChildren<Text>();
            // Find the Problem Box
                //scriptMinion_RandomSetNumbers = GameObject.FindGameObjectWithTag("GameController").GetComponent<Minion_RandomSetNumbers>();
        } // Awake()



        // This script is called once, after the actor has been placed in the scene
        private void Start()
        {
            // Fetch a random number from the Problem Box script.
                number = Minion_RandomSetNumbers.Access_GetNumber();
            // Put the self-assigned unique number on the minion's back
                numText.text = number.ToString();
        } // Start()



        // Return the value of the minion's self-assigned number.
        public int MinionNumber
        {
            get {
                    return number;
                } // get
        } // MinionNumber
    } // End of Class
} // Namespace