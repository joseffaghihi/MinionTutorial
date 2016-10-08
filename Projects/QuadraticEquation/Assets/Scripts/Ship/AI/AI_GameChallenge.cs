using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_GameChallenge : MonoBehaviour
    {
        /*                                                  GAME ARTIFICIAL INTELLIGENCE
         *                                                        Game Challenge
         * This class is designed to adjust the difficulty of the map by adjusting the aspects of fundamental game mechcanics
         *
         * STRUCTUAL DEPENDENCY NOTES:
         *      AI_GameChallenge
         *          |_ AI_UserMastery
         *          |_ ProblemBox
         *
         * GOALS:
         *      Adjust the Dynamic Equation Generator (DEG) based on the user's mastery
         */



        // Declarations and Initializations
        // ---------------------------------
            // User's current grading evaluation
                // Grade letter
                    private char current_GradeLetter;
                // Grade percentage (Whole number; int)
                    private int current_Percentage;
                // How many times grade was checked
                    private int current_EvaluationPasses;
            // User's previous grading evaluation
                // Old grade letter
                    private char previous_GradeLetter;
                // Old grade percentage (Whole number)
                    private int previous_Percentage;
            // Challenge Settings
                // Has the DEG Complexity has ever been set?
                    private bool DEGComplex = false;
            // Inspector Settings
                // Run the complexity DEG after so many evaluation passes.
                    public int complexityExecuteGradePass = 3;
            // Communication between actors and components
                // Problem Box
                    public ProblemBox scriptProblemBox;
                // Delegates
                    public delegate void ProblemBox_Complexity(bool random, bool movie = false, bool window = false, int indexKey = 0);
                    public static event ProblemBox_Complexity ProblemBox_DEGComplexity;
                // Tutorial session (if the user isn't understanding the material)
                    public delegate void TutorialSessionDelegate(bool random, bool movie = false, bool window = false, int indexKey = 0);
                    public static event TutorialSessionDelegate TutorialSession;
        // ---------------------------------



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            AI_UserMastery.ReportPlayerGrade += Inspector_UserGrade;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            AI_UserMastery.ReportPlayerGrade -= Inspector_UserGrade;
        } // OnDisable()



        /// <summary>
        ///     Adjusts the equation to be easier or harder
        /// </summary>
        /// <param name="complexSwitch">
        ///     Adjusts the DEG's complexity level.  When true, this enables the equations dynamicness.  However false enforces the equation to follow the standard form of Ax^2 + Bx + C = 0
        /// </param>
        private void Toggle_DynamicEquationGenerator(bool complexSwitch)
        {
            scriptProblemBox.SwitchComplexityLevel(complexSwitch);
        } // Toggle_DynamicEquationGenerator()



        /// <summary>
        ///     Adjust the equation generator based on the user's performance
        /// </summary>
        private void Challenge_DEG_Critria()
        {
            if (current_Percentage >= 80)
            {
                if (complexityExecuteGradePass <= current_EvaluationPasses)
                {
                    // Tutorial purposes
                    if (!DEGComplex)
                    {
                        DEGComplex = true;
                        ProblemBox_DEGComplexity(false, true, false, 1);
                    } // Tutorial

                    scriptProblemBox.SwitchComplexityLevel(true);
                } // if DEG Toggle
            } // if: grade >= 80


            if (current_Percentage <= 0)
                TutorialSession(true);
        } // Challenge_DEG_Critria ()



        /// <summary>
        ///     Inspects the user's performance and determines how to control the game's environment; should the game be easier or harder?
        /// </summary>
        /// <param name="gradePercent">
        ///     Holds the user's grade precentage (Whole number)
        /// </param>
        private void Inspector_UserGrade(int gradePercent)
        {
            // Get all of the information that is needed
                current_Percentage = gradePercent;
                current_EvaluationPasses++;
            // ---

            // Adjust the game's performance based on user's performance
                Challenge_DEG_Critria();
        } // Inspector_UserGrade()
    } // End of Class
} // Namespace