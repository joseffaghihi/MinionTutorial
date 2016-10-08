// This script provides a public function to activate the animation
// from the FeedbackAnimations script on the 'fb_letter' gameObject

using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{

	public class FBLetterAnimActivate : MonoBehaviour {

		// Animator of current gameObject, fb_letter
		private Animator anim;

		// initializes fb_letter animator
		void Awake()
		{
			anim = GetComponent<Animator>();
		}

		// Plays animation of fb_letter
		private void PlayLetterAnimation()
		{
			anim.SetTrigger ("Push");
		}

		// Public function for FeedbackAnimations script to call PlayLetterAnimation()
		// WIll play the one animation on the Feedback Canvas' Letter display object
		public void PlayLetterAnim()
		{
			PlayLetterAnimation ();
		}
	} // End FBLetterAnimActivate class
} // End namespace