using UnityEngine;
using System.Collections;

// This class is responsible for controlling the playback on the feedback animations
// 

namespace MinionMathMayhem_Ship
{

	public class FeedbackAnimations : MonoBehaviour {
		private GameObject letter; // Feedback Canvas/fb_control/'A' gameObject
		private GameObject equals; // Feedback Canvas/fb_control/'equals' gameObject
		private GameObject answer; // Feedback Canvas/fb_control/'Answer' gameObject
		private GameObject control; // Feedback Canvas/'fb_control' gameObject
		private FBLetterAnimActivate letterActivator; // FBLetterAnimActivate script on fb_letter gameObject
		private FBEqualsAnimActivate equalsActivator; // FBEqualsAnimActivate script on fb_answer gameObject
		private FBAnswerAnimActivate answerActivator; // FBAnswerAnimActivate script on fb_answer gameObject
		private FBControlActivate controlActivator; // FBControlActivate script on the fb_control gameObject ------------ need to make object 

		public float seconds = 0.0f; // initial value for seconds used in Coroutine
		public float addedSeconds = 0.0f;
		public float finalSeconds = 0.0f; // finals seconds for the end of the text drop
										  // should halt the next animation from playing

        // Use this for initialization
        // Initializes letter, equals, answer, and control
        // gameObjects and will print out the corresponding
        // warnings in console if initializations fail
		void Awake () {
			letter = GameObject.Find("fb_letter");
            if (letter == null)
                Debug.Log("letter not found!");
			equals = GameObject.Find("fb_equals");
            if (equals == null)
                Debug.Log("equals not found!");
			answer = GameObject.Find("fb_answer");
            if (answer == null)
                Debug.Log("answer not found!");
			control = GameObject.Find ("fb_control");
            if (control == null)
                Debug.Log("control not found!");
		}

		// Initializing Activator scripts on Feedback UI gameObjects------------------come back to this ----------------------------- check
		// Allows This class to call their functions that play their animations
		void Start() {
			letterActivator = letter.GetComponent<FBLetterAnimActivate>();
				if(letterActivator == null)
				Debug.Log ("letterActivator not initialized!");
			equalsActivator = equals.GetComponent<FBEqualsAnimActivate>();
				if(equalsActivator == null)
				Debug.Log ("equalsActivator not initialzied!");
			answerActivator = answer.GetComponent<FBAnswerAnimActivate>();
				if(answerActivator == null)
				Debug.Log ("answerActivator not initialized!");
			controlActivator = control.GetComponent<FBControlActivate>();
				if(controlActivator == null)
				Debug.Log ("controlActivator not initialized!");
		}

		// Plays all animations for Feedback Canvas as one combined animation
		private IEnumerator FeedbackAnimationsPlay(float seconds)
		{
			letterActivator.PlayLetterAnim();
			yield return new WaitForSeconds(seconds);
			equalsActivator.PlayEqualsAnim();
			yield return new WaitForSeconds(seconds);
			answerActivator.PlayAnswerAnim();
			yield return new WaitForSeconds(seconds + addedSeconds);
			controlActivator.PlayControlAnim();
			resetToEmpty();
			yield return new WaitForSeconds(finalSeconds);
		}


		// When called, this function displays an animation that gives the user feedback
		// The feedback is when the user gets the correct answer, 'A = 12' or whatever
		// The value is will be displayed on the screen
		public void FeedbackAnimsPlay()
		{
			// seconds in the argument is the global seconds variable
			StartCoroutine (FeedbackAnimationsPlay(seconds)); 
		}

        // Subscriptions to delegates and events
        
		// The purpose of this delegate will have the text of each
		// feedback animation part, which are the 
		// fb_control, fb_equals, and fb_answer set back to an empty string
		public delegate void ResetToEmpty();
		public static event ResetToEmpty resetToEmpty;
	} // End class
} // End namespace