using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace MinionMathMayhem_Ship
{
	public class FeedbackLetterText : MonoBehaviour {

		// text component on the fb_letter game object
		private Text texty;

		void OnEnable()
		{
			// Subscribes Reset() to the resetToEmpty delegate
			// in FeedbackAnimations class
			FeedbackAnimations.resetToEmpty += Reset;
		}

		void OnDisable()
		{
			// unsubscription for the corresponding
			// subscription
			FeedbackAnimations.resetToEmpty -= Reset;
		}

		void Start()
		{
			texty = GetComponent<Text>();
		}


		// This functions resets the fb_letter gameObject's
		// text component to display an empty string
		private void Reset()
		{
			texty.text = "";
		}

		// Changes the fb_letter's text
		private void FeedbackLetterChange(char lett)
		{
			texty.text = lett.ToString ();
		}

		public void Access_FeedbackLetterChange(char letty)
		{
			FeedbackLetterChange (letty);
		}

	} // class
} // namespace