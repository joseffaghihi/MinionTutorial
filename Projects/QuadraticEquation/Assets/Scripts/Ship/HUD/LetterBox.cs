using UnityEngine;
using UnityEngine.UI; // To use the 'text' type, we must include this
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class LetterBox : MonoBehaviour
    {

        /*                         LETTER BOX
         * Within the Letter Box object, this will randomly select which letter index to select [A|B|C].
         *   This random generator chooses only the 'charactor' of the index: A, B, or C.  The output given is a char.
         * 
         * STRUCTURAL DEPENDENCY NOTES:
         *      GAME EVENT
         *        |_ Letter Box
         *        
         * INPUT \ OUTPUT
         *      INPUT:
         *          Access_Generate()
         *      OUTPUT:
         *          Access_SelectedIndex() {CHAR}
         *
         * 
         * Goals:
         *      Randomly select an index letter of the quadratic equation [A, B, C].
         */



        // Declarations and Initializations
        // ---------------------------------
            // Quadratic Equation Index Address
                private Text letterBox;
            // Selected Index
                private char indexChar;
            // Previous random number
                private int oldRand;
            // Accessors and Communication
                private GameController scriptGameController;
        // ----



        // Use this for initialization
        private void Awake()
        {
            // Reference initialization
                letterBox = GetComponent<Text>();
        } // Awake()



        // Select an index of the Quadratic Equation
        //  Example of Indexes: ( Ax^2 + Bx + C )
        private void Generate()
        {
            // Use the randomized var to choose which index to select
            switch (MoreRandomWithRandomThatIsRandom())
            {
                case 1:
                    letterBox.text = "A";
                    indexChar = 'A';
                    break;
                case 2:
                    letterBox.text = "B";
                    indexChar = 'B';
                    break;
                case 3:
                    letterBox.text = "C";
                    indexChar = 'C';
                    break;
                default:
                    letterBox.text = "ERR!"; // Display that there is an error in the box [NG]
                    indexChar = 'E';
                    Debug.LogError("!ERROR!: Failed to generate a legal letter for variable [ letterBox.text ]"); // Show that there was an error in the console [NG]
                    break;
            } // Switch
        } // Generate()



        /// <summary>
        ///     This function will avoid repetition with the previous old random generated sequence
        /// </summary>
        /// <param name="randMin">
        ///     Minimum integer value for random range
        /// </param>
        /// <param name="randMax_EXCLUSIVE">
        ///     Maximum [EXCLUSIVE] integer value for the random range
        /// </param>
        /// <returns>
        ///     Returns a new random value that shouldn't be the 
        /// </returns>
        private int MoreRandomWithRandomThatIsRandom(int randMin = 1, int randMax_EXCLUSIVE = 4)
        {
            int newRand;

            // Make sure that the previously generated random number does is not the same as the newly generated number
            do
            {
                newRand = Random.Range(randMin, randMax_EXCLUSIVE);

            } while (newRand == oldRand);

            // Store the new value to the previously generated sequence
                oldRand = newRand;

            return oldRand;
        } // MoreRandomWithRandomThatIsRandom()



        // This function will call the generator function (which is private).
        public void Access_Generate()
        {
            Generate();
        } // Access_Generate()



        // This function will allow other scripts to determine what index is selected.
        public char Access_SelectedIndex
        {
            get {
                    return indexChar;
                } // get
        } // Access_SelectedIndex
    } // End of Class
} // Namespace