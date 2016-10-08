using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; // For the 'List' support.

namespace MinionMathMayhem_Ship
{
    public class ProblemBox : MonoBehaviour
    {

        /*                    PROBLEM BOX
         * This script will simply generate the quadratic equation by using the RNG within the given range specified within the inspector.
         * This script will generate a new quadratic equation either in standard form [Ax^2 + Bx + C = 0] or in complex form (one example) [ C = -Ax^2 - Bx] (another example) [ Bx + C = -Ax^2 ]
         *  If complexity level has been enabled, the user must move the index - that was choosen - and make sure that the index has is on the right side of the equation, else - the user _MUST_ consider wither or not the index is a postive number or a negative.
         *   
         *  
         * GOALS:
         *  Generate the quadratic equation indexes in standard form
         *  Generate the quadratic equation indexes in complexity form
         *  Return a random number that requires the RNG boundary
         */



        // Declarations and Initializations
        // ---------------------------------
            // Quadratic Equation Indexes
                private int index_A,
                            index_B,
                            index_C;
            // Quadratic Equation Indexes [Properties]
                private List<object> index_A_Prop = new List<object>();
                private List<object> index_B_Prop = new List<object>();
                private List<object> index_C_Prop = new List<object>();
            // Quadratic Equation Filler [Meaningless; only used for displaying on screen.  Cache arrays]
                private List<int?> DEG_DisplayLeft = new List<int?>();
                private List<int?> DEG_DisplayRight = new List<int?>();
            // Random Number Generator (RNG) range [minimum, maximum]
                public int minValue,
                           maxValue;
            // Accessors and Communication
                private Text problemBox;
            // Check if all indexes are in one side or mixed
                // 0 = Mixed
                // 1 = Left
                // 2 = Right
                    private short complexitySorting = 0;
            // Complexity Level
                // False: No terms shift to the right, all terms stay on the left.
                // True: All terms can shift left or right
                    public bool complexLevel = false;
        // ----



        // Use this for initialization
        private void Awake()
        {
            // Reference initialization
                problemBox = GetComponent<Text>();
        } // Awake()



        // Initialize the required indexes that will be used for the index properties.
        private void InitializeIndexProp()
        {
            // Index A
                InitializeIndexProp_IndexProperties(index_A_Prop);
            // Index B
                InitializeIndexProp_IndexProperties(index_B_Prop);
            // Index C
                InitializeIndexProp_IndexProperties(index_C_Prop);

            // Cache List for the HUD
                InitializeIndexProp_DEGDisplay(DEG_DisplayLeft);
                InitializeIndexProp_DEGDisplay(DEG_DisplayRight);
        } // InitializeIndexProp()



        // Initialize the Term Properties
        private void InitializeIndexProp_IndexProperties(List<object> objList)
        {
            objList.Add((int)0);
            objList.Add((char)'X');
        } // InitializeIndexProp_IndexProperties()



        // Initialize the DEG_Display[Left|Right] list
        private void InitializeIndexProp_DEGDisplay(List<int?> intList, uint listSize = 3)
        {
            for(int i = 0; i < listSize; ++i)
                intList.Add(null);
        } // InitializeIndexProp_DEGDisplay()



        // Generate the Quadratic Equation
        private void Generate()
        {
            // Initialize the required lists
                InitializeIndexProp();
            // Generate the new equation indexes
                Generate_Indexes();
            // Disallow all indexes to be 'zero'
                Prevent_NoEquation();
            // Check if the equation is on the left side, right side, or mixed.
                CheckIndexesSorting();
            // Translate the Indexes
                Generate_TranslateIndexes();
            // Sort the indexes in cached arrays
                Generate_DEGCacheSort();
            // Display the new equation
                Generate_Display_DEG();
            // Thrash Cache Array
                ThrashListCacheValues(DEG_DisplayLeft);
                ThrashListCacheValues(DEG_DisplayRight);
            // Thrash Index Array
                ThrashListIndexValues(index_A_Prop);
                ThrashListIndexValues(index_B_Prop);
                ThrashListIndexValues(index_C_Prop);
        } // Generate()




        // DYNAMIC EQUATION GENERATOR
        // ==================================================
        // --------------------------------------------------


        /// <summary>
        ///     This function will determine which 
        /// </summary>
        private void CheckIndexesSorting()
        {
            // Is the index sorting on the 'Left' side?
            if (((char)index_A_Prop[1] == 'L') && ((char)index_B_Prop[1] == 'L') && ((char)index_C_Prop[1] == 'L'))
                complexitySorting = 1;
            // Is the index sorting on the 'Right' side?
            else if (((char)index_A_Prop[1] == 'R') && ((char)index_B_Prop[1] == 'R') && ((char)index_C_Prop[1] == 'R'))
                complexitySorting = 2;
            // Assume the index sorting is mixed.
            else
                complexitySorting = 0;
        } // CheckIndexesSorting()



        // Generate the Quadratic Equation Indexes
        private void Generate_Indexes()
        {
            // LIST FORMAT STANDARD
            // --------------------
            // Index 0: Value [int]
            // Index 1: Position (left or right of the equal sign) [char]
            // ==============================

            // Index A
                index_A_Prop[0] = ((int)GetRandomNumber(true));
                index_A_Prop[1] = ((char)GetRandomPosition());
            // Index B
                index_B_Prop[0] = ((int)GetRandomNumber());
                index_B_Prop[1] = ((char)GetRandomPosition());
            // Index C
                index_C_Prop[0] = ((int)GetRandomNumber());
                index_C_Prop[1] = ((char)GetRandomPosition());
        } // Generate_Indexes()



        /// <summary>
        ///     Prevent all indexes to be zero; thus giving us no equation at all
        /// 
        ///     CRITICAL ATTENTION:
        ///         This function has the potential to kill Unity's main working thread!
        ///         If the Quadratic Indexes continue to be set as '0, 0, 0', this function will continue to loop until atleast one index is not '0'.
        /// </summary>
        private void Prevent_NoEquation()
        {
            while (((int)index_A_Prop[0] == 0) && ((int)index_B_Prop[0] == 0) && ((int)index_C_Prop[0] == 0))
            {
                Debug.LogWarning("[WARNING] Regenerating algebratic equation!" + "\n" +
                    "If Unity stops responding, this is likely the cause.");
                Generate_Indexes();
            } // Prevent 0's
        } // Prevent_NoEquation()



        // Sort the Dynamic Equation Generator in an array format; this will be eventually used to output to the screen.
        private void Generate_DEGCacheSort()
        {
            // Index: A
                if ((char)index_A_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[0] = (int)index_A_Prop[0];
                    DEG_DisplayRight[0] = null;
                }
                else
                {
                    // Right side of equals
                    DEG_DisplayRight[0] = (int)index_A_Prop[0];
                    DEG_DisplayLeft[0] = null;
                }


            // Index: B
                if ((char)index_B_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[1] = (int)index_B_Prop[0];
                    DEG_DisplayRight[1] = null;
                }
                else
                {
                    // Right side of equals
                    DEG_DisplayRight[1] = (int)index_B_Prop[0];
                    DEG_DisplayLeft[1] = null;
                }


            // Index: C
                if ((char)index_C_Prop[1] == (char)'L')
                {
                    // Left side of equals
                    DEG_DisplayLeft[2] = (int)index_C_Prop[0];
                    DEG_DisplayRight[2] = null;
                }
                else
                {  // Right side of equals
                    DEG_DisplayRight[2] = (int)index_C_Prop[0];
                    DEG_DisplayLeft[2] = null;
                }
        } // Generate_DEGCacheSort()



        // Try to put the cached lists together to be displayed on the screen
        private void Generate_Display_DEG()
        {
            // Evaluate the left side of the equation
                string displayCacheLeft = EvaluateIndexFields(DEG_DisplayLeft);
            // Evaluate the right side of the equation
                string displayCacheRight = EvaluateIndexFields(DEG_DisplayRight);

            // Display the data onto the dedicated Problem Box on the HUD
                problemBox.text = displayCacheLeft + " = " + displayCacheRight;

        } // Generate_Display_DEG()



        /// <summary>
        ///     Evaluates the index fields
        ///     This will try to pre-determine the possible combinations of the algebratic expression from one side, and also try to not display a zero coefficient.
        /// </summary>
        /// <param name="listIndexField">
        ///     Number set array
        /// </param>
        /// <returns>
        ///     String pattern of the algebratic expression from a specific side.
        /// </returns>
        private string EvaluateIndexFields(List<int?> listIndexField)
        {
            //Check combinations for: Index A
            // Ax^2 + Bx + C
                if ((listIndexField[0] != null && listIndexField[0] != 0) && (listIndexField[1] != null && listIndexField[1] != 0) && (listIndexField[2] != null && listIndexField[2] != 0))
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[1]) + " " + Generate_Display_TranslateValues(listIndexField[1]) + "x" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);

            // Ax^2 + Bx
                else if ((listIndexField[0] != null && listIndexField[0] != 0) && (listIndexField[1] != null && listIndexField[1] != 0))
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[1]) + " " + Generate_Display_TranslateValues(listIndexField[1]) + "x";

            // Ax^2 + C
                else if ((listIndexField[0] != null && listIndexField[0] != 0) && (listIndexField[2] != null && listIndexField[2] != 0))
                    return listIndexField[0].ToString() + "x^2" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);

            // Ax^2
                else if ((listIndexField[0] != null && listIndexField[0] != 0))
                    return listIndexField[0].ToString() + "x^2";
            // -----------


            // Check combinations for: Index B
            // Bx + C
                if ((listIndexField[1] != null && listIndexField[1] != 0) && (listIndexField[2] != null && listIndexField[2] != 0))
                    return listIndexField[1].ToString() + "x" + " " + Generate_Display_OperatorSign(listIndexField[2]) + " " + Generate_Display_TranslateValues(listIndexField[2]);
            // Bx
                else if ((listIndexField[1] != null && listIndexField[1] != 0))
                    return listIndexField[1].ToString() + "x";
            // -----------


            // Check combinations for: Index C
            // c
                if ((listIndexField[2] != null && listIndexField[2] != 0))
                    return listIndexField[2].ToString();
            // -----------

            // If nothing satisfies, then assume nothing in this side exists.
                return "0";
        } // EvaluateIndexFields()



        // Convert the operation sign (in the Quadratic Equation) to the desired sign.
        // For example, if the leading coefficient is a negative, then the 'plus' switches signs as negative.
        private char Generate_Display_OperatorSign(int? intNumber)
        {
            if (intNumber < 0)
                return '-';
            else
                return '+';
        } // Generate_Display_OperatorSign()



        // This method will truncate the negative number and output a 'positive' value; this function is to only
        private int? Generate_Display_TranslateValues(int? intNumber)
        {
            if (intNumber < 0)
                // Flip the negative number to a positive number; for display only that requires the negative to be truncated from the value specifically.
                return (intNumber * -1);
            else
                // Number is already positive.
                return intNumber;
        } // Generate_Display_TranslateValues()



        // Delete all values from the cache arrays
        private void ThrashListCacheValues(List<int?> intList)
        {
            for (int i = (intList.Count - 1); i >= 0; --i)
                intList.RemoveAt(i);
        } // ThrashArrayCacheValues()



        // Delete all values from the DEG Indexes
        private void ThrashListIndexValues(List<object> objList)
        {
            for (int i = (objList.Count - 1); i >= 0; --i)
                objList.RemoveAt(i);
        } // ThrashListIndexValues()



        /// <summary>
        ///     Translate the index properties into the index variables for ready use; but if the indexes are -
        ///         all on the left or right side, do not negate the values.
        ///     NOTE: Remember that the variables 'index_[A|B|C]' are for the minions for checking the answer.
        /// </summary>
        private void Generate_TranslateIndexes()
        {
            if (((char)index_A_Prop[1] == (char)'R') && complexitySorting == 0)
                index_A = -((int)index_A_Prop[0]);
            else
                index_A = ((int)index_A_Prop[0]);

            if (((char)index_B_Prop[1] == (char)'R') && complexitySorting == 0)
                index_B = -((int)index_B_Prop[0]);
            else
                index_B = ((int)index_B_Prop[0]);

            if (((char)index_C_Prop[1] == (char)'R') && complexitySorting == 0)
                index_C = -((int)index_C_Prop[0]);
            else
                index_C = ((int)index_C_Prop[0]);
        } // Generate_TranslateIndexes()



        // Return what position the index is currently located
        private char GetRandomPosition()
        {
            // All terms can shift left or right
            if (complexLevel == true)
            {
                if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                    // Left
                    return 'L';
                else
                    // Right
                    return 'R';
            }

            // All terms MUST remain on the left side
            else
                return 'L';
        } // GetRandomPosition()




        // END OF: DYNAMIC EQUATION GENERATOR
        // --------------------------------------------------
        // ==================================================


        
        /// <summary>
        ///     Toggles the DEG to be either difficult or easier
        /// </summary>
        /// <param name="complexSwitch">
        ///     Adjusts the DEG's complexity level.  When true, this enables the equations dynamicness.  However false enforces the equation to follow the standard form of Ax^2 + Bx + C = 0
        /// </param>
        public void SwitchComplexityLevel (bool complexSwitch = false)
        {
            // Only adjust if they are _NOT_ the same; avoid wasting resources
            if (complexSwitch != complexLevel)
                complexLevel = complexSwitch;
        } // ToggleComplexityLevel ()



        // Generate a randomized number given the [minimal, maximum] range.
        private int GetRandomNumber(bool preventNull = false)
        {
            // Generate a random number, within the given range.
                int randNumber = Random.Range(minValue, maxValue);

            // Prevent a '0' value if requested.
            if (randNumber == 0 && preventNull != false)
                return 1;
            else
                return randNumber;
        } // GetRandomNumber()



        // This function will call the RNG function (which is private) and return the value to the outside class
        public int Access_GetRandomNumber()
        {
            return (GetRandomNumber());
        } // Access_GetRandomNumber()



        // This function will call the Quadratic Equation Generator, as it is set to private.
        public void Access_Generate()
        {
            Generate();
        } // Access_Generate()



        // Returning Quadratic Equation Index: A
        public int Index_A
        {
            get {
                    return index_A;
                } // get
        } // Index_A



        // Returning Quadratic Equation Index: B
        public int Index_B
        {
            get {
                    return index_B;
                } // get
        } // Index_B



        // Returning Quadratic Equation Index: C
        public int Index_C
        {
            get {
                    return index_C;
                } // get
        } // Index_C
    } // End of Class
} // Namespace