using UnityEngine;
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class ShipAnimation : MonoBehaviour
    {
        // Declarations and Initializations
        // ---------------------------------
        // Ship Animator
            private Animator shipAnimation;
        // ----




        /// <summary>
        ///     Unity Function
        ///     This function will immediately and automatically execute when the actor is 'activated' within the virtual world.
        /// </summary>
        private void Start()
        {
            shipAnimation = GetComponent<Animator>();
        } // Start()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            GameController.GameStateEnded += Ship_Sink;
            GameController.GameStateRestart += Ship_Resurrect;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            GameController.GameStateEnded -= Ship_Sink;
            GameController.GameStateRestart -= Ship_Resurrect;
        } // OnDisable()



        /// <summary>
        ///     This function will rely a call to sink the ship.
        /// </summary>
        private void Ship_Sink()
        {
            AnimationSinking(true);
            AnimationResurrect(false);
        } // Ship_Sink()



        /// <summary>
        ///     When the game is restarted, this function will summon a new ship.
        /// </summary>
        private void Ship_Resurrect()
        {
            AnimationSinking(false);
            AnimationResurrect(true);
        } // Ship_Resurrect()



        /// <summary>
        ///     This function, will control the animation of the ship; wither its sinking or no longer sinking.
        /// </summary>
        /// <param name="state">
        ///     When set to true, the ship will sink.  When set to false the ship will no longer sink.
        /// </param>
        private void AnimationSinking(bool state)
        {
            if (state == true)
                shipAnimation.SetBool("Sink", true);
            else
                shipAnimation.SetBool("Sink", false);
        } // Ship_Sink()



        /// <summary>
        ///     This function will make the ship reappear on to the scene, that is if the parameter is true.
        /// </summary>
        /// <param name="state">
        ///     When set to true, the ship will reappear on to the scene.  However, when false, the ship will not have the resurrection set.
        /// </param>
        private void AnimationResurrect(bool state)
        {

             if (state == true)
                   shipAnimation.SetBool("StartOver", true);
             else
                 shipAnimation.SetBool("StartOver", false);
             
        } // AnimationResurrect()
    } // End of Class
} // Namespace