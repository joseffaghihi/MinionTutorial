using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class MusicVolumeTuner : MonoBehaviour
    {

        /*                          MUSIC VOLUME TUNER
         * This script will adjust the volume of the music playing in the background given special conditions.
         * 
         * 
         * Goals:
         *      Reduce music volume during Tutorial mode
         *      Restore music volume when returning to 'Normal' mode.
         */



        // Declarations and Initializations
        // ---------------------------------
        // Volume Tuner
            // Normal background music level
                public float volumeNormal = 1.0f;
            // Tutorial background music level
                public float volumeTutorial = 0.05f;
        // ----



        // Signal Listener: Detected
        private void OnEnable()
        {
            // Listen for tutorial sequence started
                GameController.TutorialStateStart += MusicTurner_Reduce;
            // Listen for tutorial sequence stopped
                TutorialMovie_0.TutorialStateEnded += MusicTurner_Return;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Listen for tutorial sequence started
                GameController.TutorialStateStart -= MusicTurner_Reduce;
            // Listen for tutorial sequence stopped
                TutorialMovie_0.TutorialStateEnded -= MusicTurner_Return;
        } // OnDisable()



        // Tutorial is running, reduce the volume of the background music.
        private void MusicTurner_Reduce()
        {
            // Make sure that the value is not negative
                MusicTurnerCheckValue(1);
            // Update the music volume
                GetComponent<AudioSource>().volume = volumeTutorial;
        } // MusicTurner_Reduce()



        // Tutorial has ended, revert the volume of the background music.
        private void MusicTurner_Return()
        {
            // Make sure that the value is not a negative
                MusicTurnerCheckValue(0);
            // Update the music volume
                GetComponent<AudioSource>().volume = volumeNormal;
        } // MusicTurner_Return()



        // Check the values; prevent negated values
        //  IIF (if only if) x < 0, flip the sign.
        private void MusicTurnerCheckValue(short checkMode = 9999)
        {
            // Check Normal Volume
            if (checkMode == 0 || checkMode == 9999)
            {
                if (volumeNormal < 0)
                    volumeNormal = (volumeNormal * -1);
            } // Check Normal Volume


            // Check Tutorial Volume
            if (checkMode == 1 || checkMode == 9999)
            {
                if (volumeTutorial < 0)
                    volumeTutorial = (volumeTutorial * -1);
            } // Check Tutorial Volume
        } // MusicTurnerCheckValue()
    } // End Class
} // Namespace