using UnityEngine;
using System.Collections;

namespace MinionMathMayhem_Ship {
	public class ExitRules : MonoBehaviour {
// ----------------------------------- Data Members Private and Public ------------------------- //
//********************************************************************************************** //

		// Game Object References
		private GameObject next;			// reference to 'Rules Canvas/GoForward'
		private GameObject back;			// reference to 'Rules Canvas/GoBack'
		private GameObject pgOne;			// reference to 'Rules Canvas/Rules_pg01'
		private GameObject pgTwo;			// reference to 'Rules Canvas/Rules_pg02'
		private GameObject exit;	
		private RulesControl rulesControl;

		public AudioSource clickSource; 	// reference to the 'Rules Canvas' audio source
		public GameObject rulesCanvas;		// reference to the 'Rules Canvas' game Object


// ----------------------------------- Unity Event Functions ----------------------------------- //
//  ******************************************************************************************** //

		// Initializes references and checks for initialization
		void Awake() {
			next = GameObject.Find ("GoForward");
			if(next == null)
				print("next was not initialized");
			back = GameObject.Find ("GoBack");
			if(back == null)
				print("back was not initialized");
			pgOne = GameObject.Find ("Rules_pg01");
			if(pgOne == null)
				print("pgOne was not initialized");
			pgTwo = GameObject.Find ("Rules_pg02");
			if(pgTwo == null)
				print("pgTwo was not initialized");
			exit = GameObject.Find ("ExitButton");
				if(exit == null)
				print ("exitbutton was not initialized");

			// Iniitialize rulesControl to the RulesControl component on the
			// RulesCanvas game object and displays checks in console
			rulesControl = rulesCanvas.GetComponent<RulesControl>();
				if(rulesControl != null) {
				print ("rulesControl has been initiated"); 
				} else {
					print("RulesControl not initialized");
				}
		}


// ----------------------------------- User Defined Functions Private and Public -------------------------- //
// ******************************************************************************************************** //
		public void Access_ClickExit() {
			Time.timeScale = 1.0f;
			ClickExit ();
		}


		// Plays the click noise first and then
		// Deactivates all Components of the rules canvas besides the rules canvas itself
		private void ClickExit() {
			clickSource.Play ();
			rulesControl.SetControl();
			next.SetActive (false);
			back.SetActive (false);
			pgOne.SetActive(false);
			pgTwo.SetActive(false);
			exit.SetActive (false);
		}

	} // end class
} // End namepace

