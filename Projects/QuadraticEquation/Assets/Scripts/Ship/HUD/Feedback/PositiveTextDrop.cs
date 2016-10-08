using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship
{
	// This script handles the dropping of the positive reinforcement text
	// This script also plays the audio that corresponds the the text being
	// Dropped
	
	public class PositiveTextDrop : MonoBehaviour
	{

		// Game Objects for the Positive reinforcement text [DC]
		public GameObject greatJob;
		public GameObject nice;
		public GameObject thatsIt;
		public GameObject wayToGo;
		public GameObject correct;

		// Audio references to be played along with the text
		public AudioClip greatJobAud;
		public AudioClip niceAud;
		public AudioClip thatItAud;
		public AudioClip wayToGoAud;
		public AudioClip randomAud;

		// Animator controllers for the positive reinforcement text [DC]
		private Animator greatJobAnim;
		private Animator niceAnim;
		private Animator thatsItAnim;
		private Animator wayToGoAnim;
		private Animator correctAnim;

		// Miscellaneous fields
		private GameObject gameController;		// GameController game object
		private AudioSource gameControllerAudSrc;		// Game controller audio source component

		void Awake()
		{
		// Animator Initialization
			greatJobAnim = greatJob.GetComponent<Animator>();
			niceAnim = nice.GetComponent<Animator>();
			thatsItAnim = thatsIt.GetComponent<Animator>();
			wayToGoAnim = wayToGo.GetComponent<Animator>();
			correctAnim = correct.GetComponent<Animator>();
		// Finds the GameController game object;
			gameController = GameObject.FindGameObjectWithTag("GameController");
			if(gameController == null)
			{
				Debug.Log ("No game controller object was found!");
			}
		// Audio source initialization - game controller
			gameControllerAudSrc = gameController.GetComponent<AudioSource>();
		}

		// Randomly selects positive reinforcement and displays it to
		// The screen after the user earns a point
		// The game controllers audio clip is then changed and the played to
		// Match the reinforcement displayed on the screen as it happens
		// If nothing is selected, and error displays in the console
		public void Drop()
		{
			switch (Random.Range (1, 6))
			{
			case 1:
				gameControllerAudSrc.clip = greatJobAud; 
				gameControllerAudSrc.Play ();
				greatJobAnim.SetTrigger ("Drop");
				break;
			case 2:
				gameControllerAudSrc.clip = niceAud;
				gameControllerAudSrc.Play ();
				niceAnim.SetTrigger ("Drop");
				break;
			case 3:
				gameControllerAudSrc.clip = thatItAud;
				gameControllerAudSrc.Play ();
				thatsItAnim.SetTrigger ("Drop");
				break;
			case 4:
				gameControllerAudSrc.clip = wayToGoAud;
				gameControllerAudSrc.Play ();
				wayToGoAnim.SetTrigger ("Drop");
				break;
			case 5:
				gameControllerAudSrc.clip = randomAud;
				gameControllerAudSrc.Play ();
				correctAnim.SetTrigger("Drop");
				break;
			default:
				Debug.Log ("Error in Random.Range");
				break;
			} // End switch
		} // End Drop
	} // End PositivetextDrop
} // End MinionMathMayhem_Ship namespace