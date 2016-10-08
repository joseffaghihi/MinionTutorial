using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class AI_UserResponse : MonoBehaviour
    {


        /*                                                  GAME ARTIFICIAL INTELLIGENCE
         *                                                      User Response Rate
         * This script is designed to monitor how fast the user responds to the game by seeing how fast (or slow) the user is clicking the minions off from the ladders.
         *  It will be possible to slightly adjust the minions speed or how the minions are being spawned.  If the game to run too fast and the user is slow, this scrip
         *  should help aid this issue - atleast possible to adjust.
         *
         * STRUCTUAL DEPENDENCY NOTES:
         *      AI_UserResponse
         *          |_ AI_Main
         *          |_ Minion_Behavior
         *
         * GOALS:
         *      Record the user's average time in a small database
         *      Monitor the user's response time
         */

        
        

        // Declarations and Initializations
        // ---------------------------------
            // Entry counter within the array
                private uint counterMinionTime = 0;
            // Maximum record entries of the minion's life spawn within the array
                private const uint counterMinionTimeMax = 5;
            // Array for holding the time database
                private float[] minionTimeArray = new float[counterMinionTimeMax];
            // Cache the user's average time into one of these identifiers
                // Current time
                    private float avgRecordTime = 0f;
                // Previous time
                    private float avgRecordTime_old = 0f;
        // ---------------------------------




        /// <summary>
        ///     Check the minion time database; if the array has been filled - determine the minion speed based on the average time from the recorded times.
        /// </summary>
        public void Main()
        {
            // Only execute if the max indexes has been reached to fill the array
            if (counterMinionTime >= (counterMinionTimeMax))
            {
                // Compute the average time
                    avgRecordTime = AverageTime();
                // Reset the array index counter
                    counterMinionTime = 0;

                // Verbose; debug stuff
                    Debug.Log("Current Average Time Collected: " + avgRecordTime);
                    Debug.Log("Previous Average Time Collected: " + avgRecordTime_old);
                // ------

                // Calculate the user's current average and previously stored average time.
                    CalculateUserAverageTime();
            } // If
        } // Main()



        /// <summary>
        ///     This calculates the average life span of the minions based on the array database.
        ///     Computation is based on the standard average: Add all entries, divide by the index size.
        /// </summary>
        /// <returns>
        ///     The users average time; float
        /// </returns>
        private float AverageTime()
        {
            // Cached value
            float timeValue = 0f;

            // Added all the indexes
            for (int i = 0; i < counterMinionTimeMax; i++)
                timeValue += minionTimeArray[i];

            // Divide by the database (or array) size and return the value
            return (timeValue / counterMinionTimeMax);
        } // AverageTime()



        /// <summary>
        ///     Record the minion's life span within an array; if the array is full (if we reached the max count) then do not record the entry.
        ///     The array counter will be reset by the Minion Service function.
        /// </summary>
        /// <param name="time">
        ///     Recorded time
        /// </param>
        private void Database_MinionLifeSpan(float time)
        {
            if (counterMinionTime < counterMinionTimeMax)
            {
                minionTimeArray[counterMinionTime] = time;
                counterMinionTime++;
            }
        } // Database_MinionLifeSpan()



        /// <summary>
        ///     This is merely an accessor function that will kindly call the private function.
        /// </summary>
        /// <param name="time">
        ///     Recorded time
        /// </param>
        public void Access_Database_MinionLifeSpan(float time)
        {
            Database_MinionLifeSpan(time);
        } // Access_Database_MinionLifeSpan()



        /// <summary>
        ///     This function is designed to take the difference of the user's current average and the previous average and decide
        ///         wither or not to make the game more challenging or to make it easier for the player.
        /// </summary>
        private void CalculateUserAverageTime()
        {
            // If the game is just starting, do not calculate the time.  Otherwise, call a function will get the difference
            //  and determine the challenge within the game.
            if (avgRecordTime_old != 0f)
                CalculateUserAverageTime_TimeDifference();

            // Change the user's average time from current to old.
            CalculateUserAverageTime_ShiftTime();
        } // CalculateUserAverageTime()



        /// <summary>
        ///     Shift the users current average time to the 'old' stored time; this will be used to calculate the next average time.
        /// </summary>
        private void CalculateUserAverageTime_ShiftTime()
        {
            avgRecordTime_old = avgRecordTime;

            // Perhaps not needed, but thrash the stored value to null.
            avgRecordTime = 0f;
        } // CalculateUserAverageTime_ShiftTime()



        /// <summary>
        ///     Calculate the difference between the user's current average against the previous average time.
        /// </summary>
        private void CalculateUserAverageTime_TimeDifference()
        {
            //float timeDiff = (avgRecordTime - avgRecordTime_old);

            // TODO: Determine to increase or decrease challenge, include user 'Comfort Zone' range.
        } // CalculateUserAverageTime_TimeDifference()
    } // End of Class
} // Namespace