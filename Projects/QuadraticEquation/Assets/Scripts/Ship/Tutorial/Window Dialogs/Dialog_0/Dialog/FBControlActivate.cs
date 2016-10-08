// This script will be attached to the fb_control object and allow the Feedback
// canvas to activate its animation

using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class FBControlActivate : MonoBehaviour {

		// 'fb_control' gameObject's animator component
		private Animator controlAnim;
		// 'fb_control' gameObject's animation clip
		// purpose to be used to get the animation lengths
		// to use as an argument for the yield statement in
		// the PlayControlAnimation coroutine
		// public Animation fbCtrlAnimClip;

		void Awake()
		{
			// initializes animator
			controlAnim = GetComponent<Animator>();
		}

		// Plays the one animation on the control object by 
		// setting the trigger in the animator for the fb_control
		// Then pause execution for the length of the animation clip
		private void PlayControlAnimation()
		{
			controlAnim.SetTrigger ("Drop");
		}

		// THis function calls the private function PlayControlAnimation()
		// the fb_control object will play its one animation and then go
		// back to its default position
		public void PlayControlAnim()
		{
			PlayControlAnimation();
		}


		
	} // End class
} // End namespace
