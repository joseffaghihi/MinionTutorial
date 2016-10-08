using UnityEngine;
using UnityEngine.UI; // Allow the use of the 'text' component
using System.Collections;


namespace MinionMathMayhem_Ship
{
    public class GameEvent : MonoBehaviour
    {
        /*                                  GAME EVENT
         * Within this script, this will manage the game flow and game attributes dynamically.
         *     This will check if the user has the correct answer, toggle the score value (mainly sending you a signal),
         *     clear the scene by expunging all of the actors within the scene, and anything else that has value within
         *     the game-play aspect.
         *     
         * 
         * 
         * GOALS:
         *  Check if the user has the correct or incorrect answer.
         *  Send a signal to update the score
         *  Send a signal to thrash the scene of all actors
         */



        // Declarations and Initializations
        // ---------------------------------
            // Spawner Toggle
                private bool SpawnMinions;
            // Sounds
                // Game Sounds
                    public AudioSource gameSounds;
                // Incorrect Answer
                    public AudioClip failSound;
                // Game Over
                    public AudioClip gameOverSound;
                // Animations
                    // What-Is Index Object
                        private Animator eventLetterAnim; // [DC]
            // Feedback Animations Component
                   private FeedbackAnimations feedbackAnims; // -------DC------- // 9/11/15
			//score and wrongScore animator components
					private Animator scoreAnim;
					private Animator wrongScoreAnim;
            // Skip Equation Generate; useful for allowing the end-user to re-use the same equation and evaluate its indexes.
                public short skipGenerateEquationThreshold = 0;
            // Skip Equation Generate internal
                private short skipGenerateEquationInternal = 0;

            // GameObjects
		// Score GameObject
		public GameObject score;
		// wrongScore gameObject
		public GameObject wrongScore;
				// Feedback answer 
					public GameObject feedbackAnswer;
					FeedbackAnswer answerFeed;
				// Feedback letter
					public GameObject feedbackLetter;
					FeedbackLetterText letterFeed;
        	    // Quadratic Equation Updated; 'What Is' message
                    public GameObject msgWhatIs;
                // Event Letter Change
                    public GameObject EventLetterChange;
        // Feedback Canvas Controller
                     public GameObject feedbackController; // -------DC------- // 9/11/15
            // Accessors and Communication
                // Final Destroyer
                    public FinalDestroyer scriptFinalDestroyer;
                // Quadratic Equation Index Letter Box
                    public LetterBox scriptLetterBox;
                // Even Letter Change
                    public EventLetterChange scriptEventLetterChange;
                // Quadratic Equation Problem Box
                    public ProblemBox scriptProblemBox;
                // Scores
                    public Score scriptScore;
                // Game Controller
                    public GameController scriptGameController;
                // What-Is Object
                    private WhatIsDisplay whatIsDisplay; // [DC]
				// Positive Text Drop
					private PositiveTextDrop textDrop;	// [DC]
                // Request Grace-Time Period; Broadcast Event
                    public delegate void RequestGraceTimePeriodSig();
                    public static event RequestGraceTimePeriodSig RequestGraceTime;
                // Minion Random Number Set
                    private Minion_RandomSetNumbers scriptMinion_RandomSetNumbers;
                // HUD Elements
		            public GameObject scoreText;
		            public GameObject wrongScoreText;
		            private Animator scoreTextAnim;
		            private Animator wrongScoreTextAnim;
        // ----




        // Signal Listener: Detected
        private void OnEnable()
        {
            // Final Destroyer
                FinalDestroyer.GameEventSignal += Driver;
            // Kill minion's from the scene
                GameController.GameStateEnded += MinionGenocide;
            // Kill minion's from the scene [x2]
                GameController.KillMinionsDemand += MinionGenocide;
        } // OnEnable()



        // Signal Listener: Deactivate
        private void OnDisable()
        {
            // Final Destroyer
                FinalDestroyer.GameEventSignal -= Driver;
            // Kill minion's from the scene
                GameController.GameStateEnded -= MinionGenocide;
            // Kill minion's from the scene [x2]
                GameController.KillMinionsDemand -= MinionGenocide;
        } // OnDisable()



        // Specialized initialization
        private void Awake()
        {
            // Initializations
                // Event Letter Animations
                    eventLetterAnim = msgWhatIs.GetComponent<Animator>(); // finds the what-is text G.O. and gets the animator.
                // Positive text
			        textDrop = GetComponent<PositiveTextDrop>();
				// FeedbackAnswer script on the feedbackAnswer gameObject
					answerFeed = feedbackAnswer.GetComponent<FeedbackAnswer>();
					letterFeed = feedbackLetter.GetComponent<FeedbackLetterText>();
            // feedback animation
                    feedbackAnims = feedbackController.GetComponent<FeedbackAnimations>(); // -------DC------- // 9/11/15
			// score and wrongScore animator initializations;
				scoreAnim = score.GetComponent<Animator>();
				wrongScoreAnim = wrongScore.GetComponent<Animator>();
			// score and wrongScore animators
			    scoreTextAnim = scoreText.GetComponent<Animator>();
			    wrongScoreTextAnim = wrongScoreText.GetComponent<Animator>();
            // Minion Random Number Set
                scriptMinion_RandomSetNumbers = GameObject.FindGameObjectWithTag("GameController").GetComponent<Minion_RandomSetNumbers>();
        } // Awake()



        // This function is immediately executed once the actor is in the game scene.
        private void Start()
        {
            // Sequentialized Order Elements [Generating equation, index letter, minion's rand num set]
                FirstRun();
            // Reference initialization
                whatIsDisplay = GetComponent<WhatIsDisplay>();
            // First make sure that all the scripts and actors are properly linked
                CheckReferences();
        } // Start()



        /// <summary>
        ///     This function helps resolve un-sequatial issues by enforcing sequential execution
        ///         This will generate the equation, randomly select an Quadratic Index, and any
        ///         other elements as needed.
        /// </summary>
        private void FirstRun()
        {
            // Create a new equation via Problem Box
                scriptProblemBox.Access_Generate();
            // Randomly select index
                scriptLetterBox.Access_Generate();
            // Set the randomized number sets for the minions
                scriptMinion_RandomSetNumbers.Access_FillArray();
        } // FirstRun()



        // Consistently check the minion actors that has reached the exit map spot
        private void Driver()
        {
            if (scriptFinalDestroyer.ActorIdentity == GetQuadraticEquation_Index())
                // Correct Answer
                StartCoroutine("AnswerCorrect");
            else
                // Incorrect Answer
                AnswerIncorrect();
        } // Driver()



        // When the user has the correct answer, this function will be executed
        private IEnumerator AnswerCorrect()
        {	
			// Activates the scorepop animation on the Score_img gameObject
			scoreAnim.SetTrigger ("ScorePop");
			yield return new WaitForSeconds(.05f);
			scoreTextAnim.SetTrigger ("ScorePop");
                AnswerCorrect_UpdateScore();
            // Pause the spawners
                SpawnerToggleValue();
            // Murder the minions!
                MinionGenocide();
			// [DC] drops in the correct text
				textDrop.Drop ();
			yield return new WaitForSeconds(2.0f);
			// Changes the fb_answers text to the right number
			answerFeed.Access_FeedbackNumberChange(GetQuadraticEquation_Index());
			letterFeed.Access_FeedbackLetterChange(scriptLetterBox.Access_SelectedIndex);
			// Update the score
			// Play feedback Animations
				feedbackAnims.FeedbackAnimsPlay(); // -------DC------- // 9/11/15
            // Slight pause
                yield return (StartCoroutine(WaitTimer(0.5f)));
            // Is the game over?
                if (scriptGameController.GameOver == false)
                {
                    // Generate a new equation
                        AnswerCorrect_Generate();
                    // Delay
                        yield return new WaitForSeconds(2.0f);
                    // Display the 'What-is' messages
                        DisplayWhatIsHUD();
                    // Issue a delay before activating the spawners.
                        RequestGraceTime();
                } // if (game is over)
            // Resume the spawners
                SpawnerToggleValue();
            // ----
            yield return null;
        } // AnswerCorrect()



        // Properly handle the 'What is' 
        private void DisplayWhatIsHUD()
        {
            // Update the Event Letter Change; this displays the index letter [A|B|C] that is displayed with the 'What Is'
                scriptEventLetterChange.Access_UpdateIndex();
            // Display the what is
                whatIsDisplay.Access_NextLetterEventPlay(0f); // [DC]
        } // DisplayWhatIsHUD()



        // When the answer was correct, update the score board
        private void AnswerCorrect_UpdateScore()
        {
            scriptScore.AccessUpdateScoreCorrect();
        } // AnswerCorrect_UpdateScore()



        // When the answer was correct, temporarily stop the spawner
        private void MinionGenocide()
        {
            if (MinionGenocide_CheckMinions() != false)
            {
                // Fetch all of the minions in one array 
                    GameObject[] minionsInScene = GameObject.FindGameObjectsWithTag("Minion");
                // Kill them 
                    for (int i = 0; i < minionsInScene.Length; i++)
                        DestroyObject(minionsInScene[i]);
            } // if

        } // AnswerCorrect_MinionGenocide()



        // Check all actors within the scene and find an actor with tag 'Minion'
        private bool MinionGenocide_CheckMinions()
        {
            // Find 'any' GameObject that has the tag 'Minion' attached to it.
            if (GameObject.FindGameObjectWithTag("Minion") == null)
                // If there is no minions in the scene
                return false;
            else
                // There is minions in the scene
                return true;
        } // MinionGenocide_CheckMinions()



        // Generate a new quadratic equation
        private void AnswerCorrect_Generate()
        {
            // If the game is not over, generate a new equation
            if (scriptGameController.GameOver == false)
            {

                    // Should the user continue to evaluate the equation again?
                    if (skipGenerateEquationInternal < skipGenerateEquationThreshold)
                        // Yes, do not re-generate the quation
                        ++skipGenerateEquationInternal; // Update the internal counter
                    else
                    {
                        // Generate a new equation
                        scriptProblemBox.Access_Generate();
                        skipGenerateEquationInternal = 0;  // Reset the counter
                    }
                    
                    scriptLetterBox.Access_Generate();
                    scriptMinion_RandomSetNumbers.Access_FillArray();
                // Notify the user of index update
            } // If
        } // AnswerCorrect_Generate()



        // A temporary pause function
        // This is useful for merely doing a temporary pause or wait within the code execution.
        private IEnumerator WaitTimer(float time)
        {
            yield return new WaitForSeconds(time);
        } // WaitTimer()



        // When the game starts once the tutorial has finished, this script will run any actions required when the game begins.
        private IEnumerator FirstRun_Animations()
        {
            // Notify the user of index update
                DisplayWhatIsHUD();
            yield return new WaitForSeconds(2f);
        } // FirstRun_Animations()



        // This function will kindly access the FirstRun_Animation, due to the protection level.
        public void Access_FirstRun_Animations()
        {
            StartCoroutine(FirstRun_Animations());
        } // Access_FirstRun_Animations()



        // When the user has the incorrect answer, this function will be executed
        private void AnswerIncorrect()
        {
            // Update the score
				wrongScoreAnim.SetTrigger ("OopsiesPop");
				wrongScoreTextAnim.SetTrigger ("WrongPop");
                scriptScore.AccessUpdateScoreIncorrect();
            // Play Sounds
                AnswerIncorrect_Sounds();
        } // AnswerIncorrect()



        // When the answer is incorrect, this will play a sound clip.
        private void AnswerIncorrect_Sounds()
        {
            GetComponent<AudioSource>().clip = failSound;
            GetComponent<AudioSource>().Play();
        } // AnswerIncorrect_Sounds()



        // This function is only going to flip the bit of the Spawner value.
        private void SpawnerToggleValue()
        {
            SpawnMinions = !SpawnMinions;
        } // SpawnerToggleValue()


        // When invoked, this will return the current Quadratic index used in the Letter Box

        private int GetQuadraticEquation_Index()
        {
            switch (scriptLetterBox.Access_SelectedIndex)
            {
                case 'A':
                    return scriptProblemBox.Index_A;
                case 'B':
                    return scriptProblemBox.Index_B;
                case 'C':
                    return scriptProblemBox.Index_C;
                default:
                    return 9999;
            } // Switch
        } // GetQuadraticEquation_Index()



        /// <summary>
        ///     Fetch the correct answer expected from the end user
        /// </summary>
        /// <returns>
        ///     Expected answer
        /// </returns>
        public int Access_GetQuadraticEquation_Index()
        {
            return (GetQuadraticEquation_Index());
        } // Access_GetQuadraticEquation_Index()



        // Return the value of the Spawners behavior; should they be on or off at this time.
        public bool AccessSpawnMinions
        {
            get {
                    return SpawnMinions;
                } // get
        } // AccessSpawnMinions



        // This function will check to make sure that all the references has been initialized properly.
        private void CheckReferences()
        {
            if (scriptFinalDestroyer == null)
                MissingReferenceError("Final Destroy");
            if (scriptLetterBox == null)
                MissingReferenceError("Letter Box");
            if (scriptProblemBox == null)
                MissingReferenceError("Problem Box");
            if (msgWhatIs == null)
                MissingReferenceError("What Is [object]");
            if (EventLetterChange == null)
                MissingReferenceError("Event Letter Change");
            if (scriptScore == null)
                MissingReferenceError("Scores");
            if (scriptGameController == null)
                MissingReferenceError("Game Controller");
            if (eventLetterAnim == null)
                MissingReferenceError("Event Letter Animation");
            if (whatIsDisplay == null)
                MissingReferenceError("What Is Display Object");
        } // CheckReferences()



        // When a reference has not been properly initialized, this function will display the message within the console and stop the game.
        private void MissingReferenceError(string refLink = "UNKNOWN_REFERENCE_NOT_DEFINED")
        {
            Debug.LogError("Critical Error: Could not find a reference to [ " + refLink + " ]!");
            Debug.LogError("  Can not continue further execution until the internal issues has been resolved!");
            Time.timeScale = 0; // Halt the game
        } // MissingReferenceError()
    } // End of Class
} // Namespace