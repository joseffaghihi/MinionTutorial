using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_RandomSetNumbers : MonoBehaviour
    {
        /*                          MINION RANDOM SET NUMBERS
         *  This class is designed to generate a specific set of numbers that each minion will be assigned.
         *   This will help to control when the 'right' answer appears to the user -- without this
         *   enforcement control and due to randomness of generated at time, time will elapse of when the
         *   user is bored and when the answer appears.  This will control what numbers each minions will have
         *   and try to make it challenging to the user if needed or easier.
         *
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      Quadratic Equation TextBox
         *           |_ Problem Box
         *      Letter_Text
         *           |_ LetterBox
         *      Game Controller
         *           |_ GameEvent
         *
         *
         * GOALS:
         *  Fetch the number ranges
         *  Fetch for the expected answer
         *  Randomize the entire number set
         *  Place the right answer in a random index
         *  Regenerate when needed
         */

        #region Declaration and Initializations
        // Number set array
            private static short[] numberSetArray = new short[20];
        // Highlight Array Index
            private static short arrayCounter = 0;
        // OPTIONS
            // Allow answers to be repeated by pure luck by the RNG.
                private static bool option_AnswersRepeated;
            // Only allow the answer to be at the middle or tail of the array.
                private static bool option_AnswerTailArray;
        // Objects
            // Problem Box - To fetch random number
                private static ProblemBox scriptProblemBox;
            // Game Event - Fetch the 'correct' answer
                private static GameEvent scriptGameEvent;
        #endregion



        /// <summary>
        ///     Unity Function
        ///     
        ///     This will be automatically called when the object is in the scene
        /// </summary>
        private void Awake()
        {
            // Fetch the Problem Box class instance
                scriptProblemBox = GameObject.FindGameObjectWithTag("RandomNumberGenerator").GetComponent<ProblemBox>();
            // Fetch the Game Event class instance
                scriptGameEvent = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameEvent>();
        } // Awake()


        /// <summary>
        ///     DEBUG PURPOSES ONLY
        /// 
        ///     Output the contents within the array
        /// </summary>
        private static void Output(int index)
        {
            // Output where the answer was stored
                Debug.Log("Answer was selected at index: " + index);
            // Scan the entire array and output the data on all indexes
            for (short i = 0; i < numberSetArray.Length; i++)
                Debug.Log("Array RandSet at [" + i + "] set to: " + numberSetArray[i]);
        } // Output()



        /// <summary>
        ///     DEBUG PURPOSES ONLY
        ///     
        ///     Output the settings in this class
        /// </summary>
        private static void Output_Settings()
        {
            Debug.Log("OPTION: Allow repeated answers? - " + option_AnswersRepeated);
            Debug.Log("OPTION: Only allow answer at the tail or mid of the array? - " + option_AnswerTailArray);
        } // Output_Settings()



        /// <summary>
        ///     Fill the array with randomized numbers
        /// </summary>
        /// <param name="useLastKnownSettings">
        ///     When true, use the previous settings as given by the function 'Access_FillArray()'.
        /// </param>
        /// <param name="answerTailArray">
        ///     When true, only allow the answer to be at the middle or tail of the array.
        ///         Default value is false
        /// </param>
        /// <param name="answersRepeated">
        ///     When true, the answers can be repeated by luck by the RNG.  When false, the repetitive answers given by the RNG will be regenerated.
        ///         Default value is true
        /// </param>
        private static void FillArray(bool useLastKnownSettings, bool answerTailArray = false, bool answersRepeated = true)
        {
            // Update presets
            if (!useLastKnownSettings)
            {
                option_AnswersRepeated = answersRepeated;
                option_AnswerTailArray = answerTailArray;
            }

            int indexAnswer;

            // Retrive an answer and set it to the array
                indexAnswer = FillArray_AnswerPlacement(option_AnswerTailArray);

            // Fill the rest of the array
                FillArray_Fill(indexAnswer);

            // Check for duplicated answers within the array
                if (!option_AnswersRepeated)
                    FillArray_CheckDuplicateAnswers(indexAnswer);

            // Check to make sure that the array does not contain the answer at the header or before the middle of the array
            //  when 'answerTailArray' is true and 'answersRepeated' is also true.
                if (answersRepeated && answerTailArray)
                    FillArray_CheckAnswersHeaderArray(indexAnswer);

            // Debug Stuff
                Output(indexAnswer);
            // Settings Debug
                Output_Settings();
        } // FillArray()



        /// <summary>
        ///     Place the answer within the array
        ///     But hopefully place the answer not at the beginning of the array depending on the 'answerTailArray'
        /// </summary>
        /// <returns>
        ///     Highlighted Index that stores the answer
        /// </returns>
        private static int FillArray_AnswerPlacement(bool answerTailArray)
        {
            // Find a location to store the answer
                int indexHighlight = (answerTailArray) ?
                (Random.Range((numberSetArray.Length / 2), numberSetArray.Length)) // TRUE: Middle of the array size is now the lower bound, and the upper bound is the array size itself.
                : (Random.Range(0, numberSetArray.Length)); // FALSE: Lower bound is zero, and the upper bound is the array size.

            //Fetch the answer and store it at the desired index
                numberSetArray[indexHighlight] = FetchAnswer();

            // Return the selected index that contains the answer
                return indexHighlight;
        } // FillArray_AnswerPlacement()



        /// <summary>
        ///     When called, this function will locate any indexes that contains a duplicated answer, omitting the answer supplied by default.
        /// </summary>
        private static void FillArray_CheckDuplicateAnswers(int indexKey)
        {
            for (int i = 0; i < numberSetArray.Length; i++)
                // Check if the values are the same, and then check if the index highlighted is NOT the one selected to contain the answer
                if ((numberSetArray[i] == numberSetArray[indexKey]) && (indexKey != i))
                    // Duplicated answer
                    do
                        FillArray_Fill(indexKey, i);
                    while (numberSetArray[i] == numberSetArray[indexKey]);
        } // FillArray_CheckDuplicateAnswers()



        /// <summary>
        ///     This function will make sure that the answer didn't slip within the beginning of the array instead of the middle or tail of the array.
        ///     This can occur by pure luck, when the answer is placed within the beginning of the array.  This function will remove contents within the
        ///     array's index, and replace it with another value.
        ///     
        ///     Do note that this assumes, when activated, the answer has to be from the middle or tail of the array.
        /// </summary>
        /// <param name="indexKey">
        ///     Holds the selected answer.
        /// </param>
        private static void FillArray_CheckAnswersHeaderArray(int indexKey)
        {
            // To avoid finding the length everytime, just place it in a variable
            int midArray = (numberSetArray.Length) / 2;

            // Scan the array from the beginning to the middle - exactly.
            for (int i = 0; i <= midArray; i++)
                // Is the index of the array the answer?  If true, change it
                if (numberSetArray[i] == numberSetArray[indexKey])
                    numberSetArray[i] = FillArray_AvoidDuplicate_Refill(indexKey);
        } // FillArray_CheckAnswersHeaderArray()



        /// <summary>
        ///     This function will try to create a new randomized number that is not the duplicated answer.
        /// </summary>
        /// <param name="indexKey">
        ///     The index of the array that contains the answers in which should be avoided; no duplicates
        /// </param>
        /// <returns>
        ///     A different randomized number that is not the duplicated answer
        /// </returns>
        private static short FillArray_AvoidDuplicate_Refill(int indexKey)
        {
            // Declare a new variable that will hold the new value.
            short newNumber;
            // ----

            // Contiously find a new number that is _NOT_ the answer.
            do
                newNumber = (short)scriptProblemBox.Access_GetRandomNumber();
            while (newNumber == numberSetArray[indexKey]);

            // Return the new number to the calling function
            return newNumber;
        } // FillArray_AvoidDuplicate_Refill()



        /// <summary>
        ///     Fill the array with randomized numbers
        /// </summary>
        /// <param name="answerIndex">
        ///     The array index address that holds the official answer.
        /// </param>
        /// <param name="indexSelected">
        ///     Only select one index that must be changed.
        ///         Default value is -255, which signifies the entire array must be changed.
        /// </param>
        private static void FillArray_Fill(int answerIndex, int indexSelected = -255)
        {
            // Selected index only
            if (indexSelected != -255)
                numberSetArray[indexSelected] = (short)scriptProblemBox.Access_GetRandomNumber();

            // Entire array
            else
                for (short i = 0; i < numberSetArray.Length; i++)
                    if (i != answerIndex) // Make sure the answer doesn't get erased by accident
                        numberSetArray[i] = (short)scriptProblemBox.Access_GetRandomNumber();
        } // FillArray_Fill()



        /// <summary>
        ///     Retrive the answer as selected by the generated quadratic equation and selected index.
        /// </summary>
        /// <returns>
        ///     Index or Answer
        /// </returns>
        private static short FetchAnswer()
        {
            // Use the already implemented algorithm in GameEvent to get the answer (or Index)
                return (short)scriptGameEvent.Access_GetQuadraticEquation_Index();
        } // FetchAnswer()



        /// <summary>
        ///     Assign the minion a pre-cached number that was already generated by the array.
        ///         This will determine if that minion will have the correct answer or not.
        /// </summary>
        /// <returns>
        ///     Cached number from the array
        /// </returns>
        private static int GetNumber()
        {
            // Check to see if the array data has already been exhausted.
                GetNumber_CheckHighlightPosition();
            // Retrieve the number at array on the highlighted index.
                int value = numberSetArray[arrayCounter];
            // Increment the index highlighter
                arrayCounter++;

            return value;
        } // GetNumber()



        /// <summary>
        ///     Check the array index highlighter (or counter) and see if the array elements has been exhausted.
        ///         If the elements have been exhausted, regenerate the array again.
        /// </summary>
        private static void GetNumber_CheckHighlightPosition()
        {
            // When the array contents has been exhausted, re-generate the array.
            if (numberSetArray.Length == arrayCounter)
            {
                // Reset the highlight back to zero.
                    arrayCounter = 0;
                // Regenerate; using previous configurations
                    FillArray(true);
            } // if
        } // GetNumber_CheckHighlightPosition()



        /// <summary>
        ///     Assign the minion a pre-cached number that was already generated by the array.
        ///         This will determine if that minion will have the correct answer or not.
        /// </summary>
        /// <returns>
        ///     Cached number from the array
        /// </returns>
        public static int Access_GetNumber()
        {
            return GetNumber();
        } // GetNumber()



        /// <summary>
        ///     Regenerate the Minion's number algorithm
        /// </summary>
        /// <param name="answerTailArray">
        ///     When true, only allow the answer to be at the middle or tail of the array.
        ///         Default value is false
        /// </param>
        /// <param name="answersRepeated">
        ///     When true, the answers can be repeated by luck by the RNG.  When false, the repetitive answers given by the RNG will be regenerated.
        ///         Default value is true
        /// </param>
        public void Access_FillArray(bool answerTailArray = true, bool answersRepeated = true)
        {
            FillArray(false, answerTailArray, answersRepeated);
        } // Access_FillArray()
    }
} // 168