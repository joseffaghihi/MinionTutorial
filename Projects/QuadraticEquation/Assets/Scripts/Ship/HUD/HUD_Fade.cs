using UnityEngine;
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class HUD_Fade : MonoBehaviour
    {
        /*
         *              HEADS UP DISPLAY FADE
         *
         * This class will simply have the HUD fade in and fade out depending on the situation.  The idea of this feature is to clear the screen 
         * when an event is happening.  An example of an event, tutorial, game over, and the like.
         * 
         * DEPENDENCY OBJECT NOTES:
         *      HUD_Fade
         *          |_ Score
         *          |_ Problem Box
         *          |_ Letter
         *          
         * GOALS:
         *      * Clear the HUD off the screen by fading it out
         *          The objects are NOT removed from the world!
         *      * Bring back the HUD on the screen by fading it back in.
         *
         */


        // Declarations and Initializations
        // ---------------------------------
            // Normal Fade Target [when the game is playing]
                // NOTE: This uses the value provided from the Canvas Group of the object.
                private float alphaChannelNormal;
            // Hide Fade Target [Fade out]
                private float alphaChannelHide = 0.0f;
            // Speed of fader
                public float alphaChangeSpeed = 0.03f;
        // ---------------------------------




        // Signal Listener: Detected
        private void OnEnable()
        {
            // Tutorial states
                //MoviePlay.TutorialStateEnded += Access_RestoreHUD;
                GameController.TutorialStateStart += Access_HideHUD;
                GameController.TutorialStateEnd += Access_RestoreHUD;
            // Game State
                GameController.GameStateEnded += Access_HideHUD;
                GameController.GameStateRestart += Access_RestoreHUD;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Tutorial states
                //MoviePlay.TutorialStateEnded -= Access_RestoreHUD;
                GameController.TutorialStateStart -= Access_HideHUD;
                GameController.TutorialStateEnd -= Access_RestoreHUD;
            // Game State
                GameController.GameStateEnded -= Access_HideHUD;
                GameController.GameStateRestart -= Access_RestoreHUD;
        } // OnDisable()



        // Immediately execute when the game object is available within the scene.
        private void Start()
        {
            // Make sure that the mutable variables do not contain negative values.
                CheckValues();
            // Initialize the default fade level with the desired current alpha value.
                alphaChannelNormal = gameObject.GetComponent<CanvasGroup>().alpha;
        } // Start()



        // Hide the HUD from the scene [NOTE: it's _NOT_ thrashed nor disabled]
        private IEnumerator HideHUD()
        {
            // Is the fader disabled?
            if (alphaChangeSpeed != (float)0)
            {
                // Is the HUD visually hidden?
                while (gameObject.GetComponent<CanvasGroup>().alpha != (float)alphaChannelHide)
                {
                    // Check in advanced if the fader has reached the lowest possible setting to avoid bad values.
                    if ((gameObject.GetComponent<CanvasGroup>().alpha - alphaChangeSpeed) <= alphaChannelHide)
                        // To avoid bad values [overage\underage], just set the HUD's alpha to the match the proper value
                        gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelHide;
                    else
                        // Update the HUD's alpha
                        gameObject.GetComponent<CanvasGroup>().alpha -= alphaChangeSpeed;
                    yield return null;
                } // while
            } // if
            else
                // Fader is disabled; immediately hide the HUD.
                gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelHide;

            yield return null;
        } // HideHUD()



        // Restore the HUD back to the scene
        private IEnumerator RestoreHUD()
        {
            // Is the fader disabled?
            if (alphaChangeSpeed != (float)0)
            {
                // Is the HUD back to normal?
                while (gameObject.GetComponent<CanvasGroup>().alpha != (float)alphaChannelNormal)
                {
                    // Check in advanced if the fader has reached the lowest possible setting to avoid bad values.
                    if ((gameObject.GetComponent<CanvasGroup>().alpha + alphaChangeSpeed) >= alphaChannelNormal)
                        // To avoid bad values [overage\underage], just set the HUD's alpha to the match the proper value
                        gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelNormal;
                    else
                        // Update the HUD's alpha
                        gameObject.GetComponent<CanvasGroup>().alpha += alphaChangeSpeed;
                    yield return null;
                } // while
            } // if
            else
                // Fader is disabled; immediately restore the HUD.
                gameObject.GetComponent<CanvasGroup>().alpha = alphaChannelNormal;
            
            yield return null;
        } // RestoreHUD()



        // Kindly call the HideHUD which is a Coroutine
        private void Access_HideHUD()
        {
            StartCoroutine(HideHUD());
        } // Access_HideHUD()



        // Kindly call the RestoreHUD which is a coroutine
        private void Access_RestoreHUD()
        {
            StartCoroutine(RestoreHUD());
        } // Access_RestoreHUD()



        // Prevent negative values
        private void CheckValues()
        {
            if (alphaChannelHide < 0)
                alphaChannelHide = (alphaChannelHide * -1);
            if (alphaChangeSpeed < 0)
                alphaChangeSpeed = (alphaChangeSpeed * -1);
            if (alphaChannelNormal < 0) // Added this just incase it's possible to add negative values within Unity's inspector.
                alphaChannelNormal = (alphaChannelNormal * -1);
        } // CheckValues()
    } // End of Class
} // Namespace