using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class VoiceOver : MonoBehaviour
    {
        /*                    VOICE OVER TUTORIAL
         * This script manages the tutorial that is given by the Minion Spirit.
         *  
         * GOALS:
         *  Manage the Minion Spirit's tutorial
         *  Interrupt tutorial
         */



        // Declarations and Initializations
        // ---------------------------------
            // Tutorial Instructions: Voice
                public AudioClip[] voiceOver = new AudioClip[10];
            // Tutorial skip signal 
                public bool tutorialSkip = false;

            // Accessors and Communication
                // Tutorial State: Finished
                    public delegate void TutorialStateEventEnded();
                    public static event TutorialStateEventEnded TutorialStateEnded;
        // ----




        // Signal Listener: Detected
        private void OnEnable()
        {
            GameController.TutorialStateStart += EnableTutorial;
            TutorialSkipButton.SkipTutorialDemand += SkipTutorial;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            GameController.TutorialStateStart -= EnableTutorial;
            TutorialSkipButton.SkipTutorialDemand -= SkipTutorial;
        } // OnDisable()



        // When the signal has been detected, start the tutorial algorithm.
        private void EnableTutorial()
        {
            StartCoroutine(PlayTutorial());
        } // EnableTutorial()



        // When the signal has been detected, flip the bit 
        private void SkipTutorial()
        {
            tutorialSkip = !tutorialSkip;
        } // SkipTutorial()



        // Plays the audio clips; tutorial sequence
        private IEnumerator PlayTutorial()
        {
            foreach (AudioClip tutorialClip in voiceOver)
            {
                // Run tutorial
                if (tutorialSkip == false)
                {
                    GetComponent<AudioSource>().clip = tutorialClip;
                    GetComponent<AudioSource>().Play();

                    // Check to see if the user is skipping the tutorial before issuing a wait.
                    for (int i = 0; i < tutorialClip.length && tutorialSkip == false; i++)
                        yield return new WaitForSeconds(1);
                } // if

                // Stop the audio that is currently being played.  This is useful when the skip tutorial has been activated.
                GetComponent<AudioSource>().Stop();

            } // foreach

            // turn off the tutorial mode
            TutorialStateEnded();

        } // PlayTutorial()
    }  // End of Class
} // Namepsace