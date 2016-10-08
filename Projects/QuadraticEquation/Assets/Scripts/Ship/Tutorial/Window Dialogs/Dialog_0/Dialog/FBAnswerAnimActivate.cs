// This script provides a public function to activate the animation
// from the FeedbackAnimations script on the 'fb_answer' gameObject

using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class FBAnswerAnimActivate : MonoBehaviour {

		// animator for this, the 'fb_answer', gameObject
		private Animator anim;

		void Awake()
		{
			anim = GetComponent<Animator>();
		}

		// Plays the fb_answers Grow and Shrink animation
		private void PlayAnswerAnimation()
		{
			anim.SetTrigger("Push");
		}

		//  for Feedback Animations to call PlayAnswerAnimation()
		// Plays the animation on the number answer object 
		// in the feedback canvas
		public void PlayAnswerAnim()
		{
			PlayAnswerAnimation ();
		}

	} // End of FBAnswerAnimActivate class
} // End of namespace
