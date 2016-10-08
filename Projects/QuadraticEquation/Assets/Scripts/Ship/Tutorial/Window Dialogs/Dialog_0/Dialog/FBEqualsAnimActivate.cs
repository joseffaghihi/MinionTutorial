// This script provides a public function to activate the animation
// from the FeedbackAnimations script on the 'fb_equals' gameObject

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	public class FBEqualsAnimActivate : MonoBehaviour {

		private Animator anim;
		private Text equalsText;

		// subscribes Reset() to resetToEmpty()
		void OnEnable()
		{
			FeedbackAnimations.resetToEmpty += Reset;
		}
	
		// unsubscribes Reset() from resetToEmpty())event
		void OnDisable()
		{
			FeedbackAnimations.resetToEmpty -= Reset;
		}

		// initialize anim variable to the gameObjects animator
		// also initializes the Text component to equalsText variable
		void Awake()
		{
			anim = GetComponent<Animator>();
			equalsText = GetComponent<Text>();
		}

		private void Reset()
		{
			// Set the fb_equals gameObjects text
			// component to display an empty string
			equalsText.text = "";
		}

		// Plays the equals animation by setting the trigger in its animator
		private void PlayEqualsAnimation()
		{
			equalsText.text = "=";
			anim.SetTrigger ("Push");
		}

		// For FeedbackAnimations script to call PlayEqualsAnimation()
		// on the '=' sign on the feedback canvas
		public void PlayEqualsAnim()
		{
			PlayEqualsAnimation ();
		}
	} // End FBEqualsAnimActivate class
} // End namespace