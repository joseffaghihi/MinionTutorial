using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Score_Precentage : MonoBehaviour
    {
        /*                                      SCORE PRECENTAGE
         * This script is designed to calculate the percentage of how much the user has gotten right within the game.
         *  The purpose of the task is to take what the user has right over the total possible points and return the percentage in a form of a double.
         *  For example, (right/total = double).
         * 
         * 
         * Input/Output Interactions:
         *   CalculateScorePercentageInterface([int] Earned Points\Correct, [int] Total Points Possible)
         *      return value: Double
         * 
         * GOALS:
         *   Calculate user's score
         *      (correct / total_possible) = double value
         */


        // When called by other scripts; this will take the provided values and return the percentage of the score.

        /// <summary>
        ///     Calculate the percentage of the user's score.  The values that will be provided will be passed through the initial function that performs the calculation.
        /// </summary>
        /// <param name="earnedPoints">
        ///     Points earned
        /// </param>
        /// <param name="totalPoints">
        ///     Total points possible
        /// </param>
        /// <returns>
        ///     Percentage
        /// </returns>
        public double CalculateScorePercentageInterface (int earnedPoints = 0, int totalPoints = 0)
        {
            if (totalPoints != 0)
                return CalculateScorePercentage(earnedPoints, totalPoints);
            else
                // Total points == 0; invalid
                return -1; // <!>
        } // CalculateScorePercentage();



        /// <summary>
        ///     Calculate the percentage
        /// </summary>
        /// <param name="earnedPoints">
        ///     Points earned
        /// </param>
        /// <param name="totalPoints">
        ///     Total points possible
        /// </param>
        /// <returns>
        ///     Percentage
        /// </returns>
        private double CalculateScorePercentage (int earnedPoints, int totalPoints)
        {
            return (((double)earnedPoints / (double)totalPoints) * 100);
        } // CalculateScorePercentage();
    } // End of Class 
} // Namespace