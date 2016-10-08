using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class ParticleActivation : MonoBehaviour
    {
        /*                      PARTICLE ACTIVATION
         * This will display a graphical soft particle display on the actors when executed.
         * 
         * GOALS:
         *  Activate and display particles around (or at pivet) of the actor
         */



        // Declarations and Initializations
        // ---------------------------------
            // Particle
                private ParticleSystem particles;
        // ----



        // Use this for initialization
        void Start()
        {
            particles = gameObject.GetComponentInChildren<ParticleSystem>();
            CheckReferences();
        } // Start()



        // Display graphical particles on-screen.
        public void Emit()
        {
            particles.Play();
        } // Emit()



        // DEBUG: Check if particles has been properly initialized
        private void CheckReferences()
        {
            if (particles == null)
                Debug.Log("No Particle System Found on minion game object");
        } // CheckReferences()
    } // End of Class 
} // Namespace