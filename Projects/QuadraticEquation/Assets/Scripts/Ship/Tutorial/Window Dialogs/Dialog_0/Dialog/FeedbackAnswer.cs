// This script supplies the fb_answer object with the correct number
// matching the A,B, or C in the quadratic equation textbox.
// Whichever one the top-right UI element is asking for, this script provides
// the answer

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MinionMathMayhem_Ship
{
    public class FeedbackAnswer : MonoBehaviour
    {
        private Text text;


		void OnEnable()
		{
			// subscribes the reset function to the deletage
			// in the FeedbackAnimations class
			FeedbackAnimations.resetToEmpty += Reset;
		}

		void OnDisable()
		{
			// unsubscribes the reset function from the
			// resetToEmpty delegate variable
			FeedbackAnimations.resetToEmpty -= Reset;
		}

        // Use this for initialization
        void Start()
        {
			// text initialized to the text componenet on
			// the fb_answer gameObject
            text = GetComponent<Text>();
        }

		// This function empties the text component's
		// text
		private void Reset()
		{
			text.text = "";
		}

		// Public functions for other classes
		// to call Feedback Letter Change
		public void Access_FeedbackNumberChange(int answer)
		{
			FeedbackNumberChange(answer);
		}

		// Changes the feedback letter
		// a, b, or c so when it is displayed, it will have
		// the right correct number inside
		private void FeedbackNumberChange(int answer)
		{
			text.text = answer.ToString ();
		}
    } // class
} // namepace
