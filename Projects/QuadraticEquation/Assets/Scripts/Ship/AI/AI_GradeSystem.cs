using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_GradeSystem : MonoBehaviour
    {
        /*
         *                                                   GAME ARTIFICIAL INTELLIGENCE
         *                                                          GRADE SYSTEM
         * This script is designed to monitors the players grade through out this entire level.
         *
         * NOTES:
         *  This AI Component only grades the player's overall score within the game; this script does not adjust the game at all but only monitors the scores.
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      User Mastery [AI]
         *          |_ Score [Score]
         *          |_ GameController [GameController]
         *
         * GOALS:
         *      Grades the player's overall grade within the game.
         *      Can be used on the intermission screen, info window, and on a user database.
         */



        // Declarations and Initializations
        // ---------------------------------
            // How many the user got wrong
                private int gradeIncorrect = 0;
            // How many the user got correcct
                private int gradeCorrect = 0;
        // ---------------------------------



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            // Update the score managed within the game
            AI_UserMastery.ReportOverallGradeUpdate += ScoreUpdate;
            GameController.GameStateRestart += ResetScores;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            // Update the score managed within the game
            AI_UserMastery.ReportOverallGradeUpdate -= ScoreUpdate;
            GameController.GameStateRestart -= ResetScores;
        } // OnDisable()



        /// <summary>
        ///     This function will scan its internal grade library to find the grade letter: A, B, C, D, F
        /// </summary>
        /// <output>
        ///     returns the grade letter.
        /// </output>
        private char PerformanceGradingLibrary()
        {
            // Declarations and Initializations
            // --------------------------------
                // Get the users grade
                int userGrade = CalculateUserGrade();
            // --------------------------------


            // Sorry for this long conditional, I couldn't find a nicer way to do this with a Switch statement :(
            if (95 < userGrade && userGrade <= 100)
                // Skill Level: Very-High
                return 'A';


            else if (90 < userGrade && userGrade <= 95)
                // Skill Level: Medium-High
                return 'A';

            else if (85 < userGrade && userGrade <= 90)
                // Skill Level: Medium
                return 'B';
                    
            else if (80 < userGrade && userGrade <= 85)
                //   Skill Level: Medium-Low
                return 'B';

            else if (75 < userGrade && userGrade <= 80)
                //  Skill Level: Low
                return 'C';

            else if (70 < userGrade && userGrade <= 75)
                //  Skill Level: WeakFoundation - Low
                return 'C';

            else if (65 < userGrade && userGrade <= 70)
                //  Skill Level: WeakFoundation - Medium
                return 'D';

            else if (60 < userGrade && userGrade <= 65)
                //  Skill Level: WeakFoundation - High
                return 'D';

            else if (userGrade <= 60)
                //  Skill Level: WeakFoundation - Failed
                return 'F';


            // Incase the grade parameter is something unpredictable, output the error on the terminal.
                Debug.Log("<!> ATTENTION: RUN AWAY DETECTED <!>");
                Debug.Log("Using grade value of: " + userGrade);

            // Since we need to return something; output an error char.
                return 'X';
        } // PerformanceGradingLibrary()



        /// <summary>
        ///     This function will adjust the overall grade that the user earned
        ///     This requires the tentative grading system [AI_UserMastery].
        ///         This dependency makes sure that we only capture the data that
        ///         is important - and not from the tutorial sections or other means
        ///         that could hurt the user's score.
        /// </summary>
        private void ScoreUpdate(bool score)
        {
            if (score)
                gradeCorrect++;
            else
                gradeIncorrect++;
        } // ScoreUpdate()



        /// <summary>
        ///     This function will output the user's grade by calculating the
        ///     possible score to the user's actual score
        /// </summary>
        /// <returns>
        ///     Users actual score; returns int
        /// </returns>
        private int CalculateUserGrade ()
        {
            return ((gradeCorrect / (gradeCorrect + gradeIncorrect)) * 100);
        } // CalculateUserGrade



        /// <summary>
        ///     This will reset the scores when the game restarts
        /// </summary>
        private void ResetScores()
        {
            gradeIncorrect = 0;
            gradeCorrect = 0;
        } // ResetAllScores()
    } // End of Class
} // Namespace