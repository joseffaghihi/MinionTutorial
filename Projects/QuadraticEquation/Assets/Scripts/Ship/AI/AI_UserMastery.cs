using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_UserMastery : MonoBehaviour
    {

        /*
         *                                                   GAME ARTIFICIAL INTELLIGENCE
         *                                                          USER MASTERY
         * This script monitors the user's performance with the material and tries to adjust based on the user's mastery.  If the user is doing exceptionally well, then this will try to enforce a more challenge for the user.  If the user is not
         *  well or failing, this AI component will try at best to keep the user in the game and try to enforce the material on the user.
         *
         * NOTES:
         *  This AI Component is mainly just a grading scale and tries at best to keep the player motivated; this doesn't change the environment by itself, it still requires the dependencies to push the changes.
         *
         * STRUCTURAL DEPENDENCY NOTES:
         *      User Mastery [AI]
         *          |_ Score [Score]
         *          |_ GameController [GameController]
         *
         * GOALS:
         *      Tries to keep the player involved and motivated
         *      Requests the environment or game to be more challenging or easier, based on user's mastery.
         *      Tries to make sure that the user understands that material while keeping the game fun.
         */



        // Declarations and Initializations
        // ---------------------------------
        // Game State; is the game over?
        private bool gameOver = false;
        // AI Grading system switch
        private bool gradeUserSwitch = false;
        // Determines if the newly incoming scores should be ignored; useful for on-screen or interactive tutorials that are self managed.
        private bool gradeUserHaltSwitch = false;
        // Activate this AI component when the possible score has reached been reached by specific value
        // NOTES: Higher the value, the longer it takes for the AI to run and monitor the user's performance.
        //          Shorter the value, the quicker it takes for the AI to run and monitor the user's performance.
        private const short userPrefScorePossible_EnableAI = 0;
        // Adjacent to the AI activation (see above variable), this variable holds the state of when the AI activates.
        // The idea of this variable is to allow the user to experience through the warm-up phase; once this variable
        // is equal to or greater than the variable above - then we're finished with the warm up phase.
        private static short userPrefScorePossible_Monitor = 0;
        // User Performance Array
        private static short userPrefArrayIndexSize = 3;
        private bool[] userPrefArray = new bool[userPrefArrayIndexSize];
        private short userPrefArrayIndex_HighLight = 0; // Use for scanning array
                                                        // Scan User Performance in 'x' tries - well after the AI does its first initial scan.
        // Events and Delegates
        // Minion Speed
        public delegate void MinionSpeedDelegate(float runningSpeed, float climbingSpped);
        public static event MinionSpeedDelegate MinionSpeed;

        // Report User's Grade
        //   gradePercent = Score (grade) percentage
        public delegate void UserGradedPerformance(int gradePercent);
        public static event UserGradedPerformance ReportPlayerGrade;

        // Overall grading system
        public delegate void UserOverallGradePerformance(bool score);
        public static event UserOverallGradePerformance ReportOverallGradeUpdate;
        // ---------------------------------




        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            Score.ScoreUpdate_Correct += onCall_ScoreCorrect;
            Score.ScoreUpdate_Incorrect += onCall_ScoreIncorrect;
            GameController.GameStateEnded += SignalOnGameOver;
            GameController.GameStateRestart += SignalOnReset;
            GameController.TutorialStateStart += SignalTutorial_Enable;
            GameController.TutorialStateEnd += SignalTutorial_Disable;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            Score.ScoreUpdate_Correct -= onCall_ScoreCorrect;
            Score.ScoreUpdate_Incorrect -= onCall_ScoreIncorrect;
            GameController.GameStateEnded -= SignalOnGameOver;
            GameController.GameStateRestart -= SignalOnReset;
            GameController.TutorialStateStart -= SignalTutorial_Enable;
            GameController.TutorialStateEnd -= SignalTutorial_Disable;
        } // OnDisable()



        /// <summary>
        ///     This daemon service will determine how the game should interact with the player; this is done by analyse -
        ///     the user's score and getting the user's grade (by percentage) and understand how well the end-user understands -
        ///     the material presented.
        /// 
        ///     What this Controls:
        ///         Toggle Dynamic Equation Generator's complexity level
        ///         Change the Minion's speed
        ///                 OR
        ///         Change the Minion actor spawner
        ///         Provide demonstration tutorials when needed
        ///         Kick the player if foundation is bad
        /// </summary>
        public void Main()
        {
            // Execute the tentative grading system
            // Periodically check the player's tentative score and determine the state of the game
            if (gradeUserSwitch && !gameOver && InspectQueries_Ready() && !gradeUserHaltSwitch)
            {
                userPrefArrayIndex_HighLight = 0;
                // Get the user's percentage rate and determine the game challenge
                ReportPlayerGrade(UserMasteryReport_Precentage());
            } // if Grading enabled
        } // Main()



        /// <summary>
        ///     When called; this will determine if there is enough data to inspect the user's tentative mastery to the material.
        /// </summary>
        /// <returns>
        ///     True = Ready for inspection
        ///     False = Not enough data gathered yet.
        /// </returns>
        private bool InspectQueries_Ready()
        {
            if (userPrefArrayIndex_HighLight > (userPrefArrayIndexSize - 1))
                return true;
            else
                return false;
        } // InspectQueries_Ready()



        /// <summary>
        ///     Update values within the array based on the user's actual performance.
        /// </summary>
        /// <param name="userFeedback">
        ///     True = Correct Answer; False = Wrong Answer.
        /// </param>
        private void ArrayUpdateField(bool userFeedback)
        {
            // Make sure that we're not overflowing the array, move the highlight to the start of the index if needed.
            if (userPrefArrayIndex_HighLight >= userPrefArrayIndexSize)
                userPrefArrayIndex_HighLight = 0;

            // Update the array at the highlighted index
            userPrefArray[userPrefArrayIndex_HighLight] = userFeedback;
            // Highlight the next index
            userPrefArrayIndex_HighLight++;
        } // ArrayUpdateField()



        /// <summary>
        ///     This function will retrieve the players percentage rate of the queries gathered
        /// </summary>
        /// <returns>
        ///     Percentage in integer form.
        /// </returns>
        private int UserMasteryReport_Precentage()
        {
            // Declarations and Initializations
            // --------------------------------
            // Find out how many the user got correct and not correct
            int incorrectScore = 0;
            int correctScore = 0;
            // --------------------------------

            // Scan the array and determine what the user got right or wrong.
            //  We're going to need this for statistics purposes.
            for (int i = 0; i < userPrefArrayIndexSize; i++)
            {
                if (userPrefArray[i])
                    correctScore++;
                else
                    incorrectScore++;
            } // Scan array's queries


            // Methodology: (EarnedPoints / PossiblePoints * 100)
            return ((correctScore / (correctScore + incorrectScore)) * 100);
        } // UserMasteryReport_Precentage()



        /// <summary>
        ///     Update the correct score for the Daemon service
        /// </summary>
        private void onCall_ScoreCorrect()
        {
            if (!gradeUserSwitch)
                AI_WarmUpPhase();
            else if (gradeUserHaltSwitch)
                return;
            else
            {
                // Update the array that holds the user performance
                ArrayUpdateField(true);
                // Notify the overall grading system
                ReportOverallGradeUpdate(true);
            }
        } // Update_CorrectScore()



        /// <summary>
        ///     Update the incorrect score for the Daemon service
        /// </summary>
        private void onCall_ScoreIncorrect()
        {
            if (!gradeUserSwitch)
                AI_WarmUpPhase();
            else if (gradeUserHaltSwitch)
                return;
            else
            {
                // Update the array that holds the user performance
                ArrayUpdateField(false);
                // Notify the overall grading system
                ReportOverallGradeUpdate(false);
            }

        } // Update_IncorrectScore()



        /// <summary>
        ///     Allow the ability to momentarily pause the entire grading system.
        ///     This can be useful for events that focuses the attention away from
        ///     the grading the user based on the nature of the game.
        /// </summary>
        /// <param name="state">
        ///     When true, this will pause the grading system - thus if any minions cross the FinalDestroyer line, their result will be ignored.
        ///     However, when false, the game runs as normal.
        /// </param>
        private void AI_PauseToggle(bool stateSwitch)
        {
            // Don't flush the state in memory and redo the state
            if (gradeUserHaltSwitch && stateSwitch)
                return;
            else
                gradeUserHaltSwitch = stateSwitch;
        } // AI_PauseToggle()



        /// <summary>
        ///     Check the status of the AI and determine when the AI grading system should be activated.
        ///     This functionality will allow the user to use the warm-up phase, which the game will
        ///     just toss a few tutorials at the user and examine the user's input.  After the tutorial (warm up),
        ///     activate the AI grading system.
        /// </summary>
        private void AI_WarmUpPhase()
        {
            if (userPrefScorePossible_Monitor < (userPrefScorePossible_EnableAI - 1))
                userPrefScorePossible_Monitor++;
            else
                gradeUserSwitch = true;
        } // AI_WarmUpPhase()



        /// <summary>
        ///     At restart, reset the mutable working variables to their default values.
        /// </summary>
        private void SignalOnReset()
        {
            // Flip the value of the game over state
            SignalOnGameOver();
            // Reset the Highlighter used in the performance array.
            userPrefArrayIndex_HighLight = 0;
        } // ResetScores()



        /// <summary>
        ///     Toggles the gameOver variable when the game has reached it's end.
        /// </summary>
        private void SignalOnGameOver()
        {
            gameOver = !gameOver;
        } // GameState_ToggleGameOver()



        /// <summary>
        /// 
        /// </summary>
        private void SignalTutorial_Enable()
        {
            AI_PauseToggle(true);
        } // SignalTutorial_Enable()



        /// <summary>
        /// 
        /// </summary>
        private void SignalTutorial_Disable()
        {
            AI_PauseToggle(false);
        } // SignalTutorial_Disable()
    } // End of Class
} // Namespace