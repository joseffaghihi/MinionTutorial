using UnityEngine;
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class Minion_LifeSpan : MonoBehaviour
    {
        /*
         *                                           MINION LIFE SPAN
         * This is a simple script which calculates the minion's time of birth (when span or activated) to its death.
         * 
         * 
         * GOALS:
         *      Setup it's own time of birth.
         *      Relay it's time of death.
         *      Relay if the actor is still alive.
         */



        // Declarations and Initializations
        // ---------------------------------
            // Time of Birth
                private float timeOfBirth;
            // Time of Death
                private float? timeOfDeath;
        // ---------------------------------


        // When actor is activated, execute function.
        private void Start()
        {
            // Initialize it's time of birth
                timeOfBirth = Time.time;
        } // Start()



        // Initialize the time of death var.
        private void UpdateTimeOfDeath()
        {
            timeOfDeath = Time.time;
        } // UpdateTimeOfDeath()



        // Return the actor's life span from its birth to its death.
        private float OutputLifeSpan()
        {
            return ((timeOfDeath ?? 0) - timeOfBirth);
        } // outputLifeSpan()



        // Is the actor still alive?
        private bool OutputIsActorAlive()
        {
            if (timeOfDeath == null)
                return false;
            else
                return true;
        } // outputActorStatus()



        // Access the UpdateTimeOfDeath function, since it's private.
        public void Access_UpdateTimeOfDeath()
        {
            UpdateTimeOfDeath();
        } // Access_UpdateTimeOfDeath()



        // Access the OutputLifeSpan function, since it's private.
        public float Access_OutputLifeSpan
        {
            get
            {
                return OutputLifeSpan();
            }
        } // Access_OutputLifeSpan()



        // Access the OutputIsActorAlive function, since it's private.
        public bool Access_OutputIsActorAlive
        {
            get
            {
                return OutputIsActorAlive();
            }
        } // Access_OutputIsActorAlive()
    } // End of Class
} // End of Namespace