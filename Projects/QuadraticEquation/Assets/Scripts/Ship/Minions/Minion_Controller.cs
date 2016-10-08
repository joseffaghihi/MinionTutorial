using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class Minion_Controller : MonoBehaviour
    {


        /*                                  MINION CONTROLLER
         * This script is designed to centralize the minion's attributes and behavior to meet the condition's within the environment.
         * 
         * GOALS:
         *      Centralization of behavior and attributes
         *      Ability to fight with another collided minion
         *          Try's to self determine which one is the alpha male.
         */



        // Declarations and Initializations
        // ---------------------------------
        // These can be adjusted within Unity's inspector
            // Speed
                // Climbing the Ladder
                    // [MINION_CONTROLLER] This var takes the local ranges of (min, max) and uses a RNG to get a unique speed value.
                        private float speedClimbLadder;
                    // Local Minimum Boundary Range
                        public float speedClimbLadder_Minimum;
                    // Local Maximum Boundary Range
                        public float speedClimbLadder_Maximum;
                // Running
                    // [MINION_CONTROLLER] This var takes the local ranges of (min, max) and uses a RNG to get a unique speed value.
                        private float speedRunning;
                    // Local Minimum Boundary Range
                        public float speedRunning_Minimum;
                    // Local Maximum Boundary Range
                        public float speedRunning_Maximum;
            // Minion Selected\Flick
                // Thrust Force
                        public float thrustForce;
                // Thrust Direction
                        public Vector3 thrustDirection;
        // ----




        // Generate a random climbing speed.
        private float GenerateClimbSpeed()
        {
            // Prevent negated values
                if (speedClimbLadder_Minimum < 0)
                    speedClimbLadder_Minimum = (speedClimbLadder_Minimum * -1);
                if (speedClimbLadder_Maximum < 0)
                    speedClimbLadder_Maximum = (speedClimbLadder_Maximum * -1);
            
            // Pass through the RNG
                return Random.Range(speedClimbLadder_Minimum, speedClimbLadder_Maximum);
        } // GenerateClimbSpeed()



        // Generate a random running speed.
        private float GenerateRunningSpeed()
        {
            // Prevent negated values
                if (speedRunning_Minimum < 0)
                    speedRunning_Minimum = (speedRunning_Minimum * -1);
                if (speedRunning_Maximum < 0)
                    speedRunning_Maximum = (speedRunning_Maximum * -1);

            // Pass through the RNG
                return Random.Range(speedRunning_Minimum, speedRunning_Maximum);
        } // GenerateRunningSpeed()



        // Returns the Climb Speed value to the calling script
        public float ClimbSpeed
        {
            get {
                    return GenerateClimbSpeed();
                } // get
        } // ClimbSpeed



        // Returns the Running Speed value to the calling script
        public float RunningSpeed
        {
            get {
                    return GenerateRunningSpeed();
                } // get
        } // RunningSpeed



        // Returns the Thrust Force value to the calling script
        public float ThurstForce
        {
            get {
                    return thrustForce;
                } // get
        } // ThrustForce



        // Returns the Thrust Direction to the calling script
        public Vector3 ThrustDirection
        {
            get {
                    return thrustDirection;
                } // get
        } // ThrustDirection



        // Additional Member Variables
        //   These variables will be used for avoiding cross-values because when Actor2 touches Actor1, 
        //    both actors will execute the collision code, thus - we allow the Actor2 and Actor1 to call
        //    MinionCollision(), but disallow Actor1 and Actor2 from calling the function.
        //    Elaborative:
        //     ACTOR2 & ACTOR1 -> MinionCollision()
        //     ACTOR1 & ACTOR2 -> (DISALLOW) MinionCollision()
            private int minionCollision_Actor1;
            private int minionCollision_Actor2;
        // Minion Collision; determine which one is the alpha-male!
        public int MinionCollision(int minionActor1, int minionActor2)
        {
            // Avoid cross-values
            if (MinionCollision_CrossValues(minionActor1, minionActor2) == false)
                // Select the minion that will be thrown off the ladder
                    return MinionCollision_Process(minionActor1, minionActor2);
            else
                return 0; // POSSIBLE BUG!  If the actor's instance ID is 0, it'll be selected.  [NG]
        } // MinionCollision()



        // Determine which minion is going to be thrown off the ladder 
        private int MinionCollision_Process(int actor1, int actor2)
        {
            // Update the member variables
                minionCollision_Actor1 = actor1;
                minionCollision_Actor2 = actor2;
            // Clear the member variables once their values become irrelevant.
                StartCoroutine(MinionCollision_ThrashValues());

            // randomly select the minion that'll be thrown off
            if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                return actor1;
            else
                return actor2;
        } // MinionCollision_Process()



        // Check to make sure that the other actor is not reporting the same values. 
        private bool MinionCollision_CrossValues(int actor1, int actor2)
        {
            if (actor2 == minionCollision_Actor1 && actor1 == minionCollision_Actor2)
                // Cross-Value detected
                return true;
            else
                return false;
        } // MinionCollision_CrossValues()



        // After so long, thrash the values as they'll become irrelevant
        private IEnumerator MinionCollision_ThrashValues()
        {
            // Wait before thrashing
                yield return new WaitForSeconds(0.3f);
            // Thrash the values
                minionCollision_Actor1 = 0;
                minionCollision_Actor2 = 0;
        } // MinionCollision_ThrashValues()
    } // End of Class
} // namespace