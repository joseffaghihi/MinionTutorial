using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

namespace MinionMathMayhem_Ship
{
    public class TutorialMain : MonoBehaviour
    {
        /*                                                                      TUTORIAL MAIN
         * This class is designed to manage and render the requested tutorial or tutorials -- as it is possible to run atleast one movie tutorial and one window tutorial.  As designed, -
         *  this will manage how the movie and window sequences work and make sure that they are set-up properly for an easy transition from the virtual world environment.  This class -
         *  can only be called by broadcasted events, and will broadcast an event signal when finished.
         *
         *  GOALS:
         *   Setup the environment for the requested Movie or Window.
         *   Select (or randomly select) the movie or window from the array list
         *   Play the tutorial(s)
         *   When finished, let listening classes\scripts know.
         */


            
        // Declarations and Initializations
        // ---------------------------------
            // Tutorial Arrays
                // Window
                    public List<GameObject> tutorialWindowArray = new List<GameObject>();
                // Movie
                    public List<GameObject> tutorialMovieArray = new List<GameObject>();
            // Switches
                // This variable will help assure the state of the tutorial execution.
                    private bool tutorialExecutionState = false;
            // Timed-Out Controlls
                // Enable Feature
                    public bool enableForceTimeOut = true;
                // Minutes to forcibly time out
                    public float timedOut_Seconds = 210f;
            // Accessors and Communication
                // Finished tutorial sequence signal
                    public delegate void TutorialSequenceFinishedSig();
                    public static event TutorialSequenceFinishedSig TutorialFinished;
        // ---------------------------------




        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Detected (or heard)
        /// </summary>
        private void OnEnable()
        {
            GameController.TutorialSequence += TutorialMain_Driver_Accessor;
        } // OnEnable()



        /// <summary>
        ///     Unity Function
        ///     Signal Listener: Deactivated
        /// </summary>
        private void OnDisable()
        {
            GameController.TutorialSequence -= TutorialMain_Driver_Accessor;
        } // OnDisable()



        /// <summary>
        ///     Make sure that either (or both) the tutorial movie or window has been selected.
        /// </summary>
        /// <param name="tutorialNovie">
        ///     Tutorial movie switch
        /// </param>
        /// <param name="tutorialWindow">
        ///     Tutorial window switch
        /// </param>
        /// <returns>
        ///     False; if there was no error
        ///     True; if there was an error.  Nothing was selected.
        /// </returns>
        private bool TutorialMain_CheckCallErrors(bool tutorialNovie, bool tutorialWindow)
        {
            if (!tutorialNovie && !tutorialWindow)
                return true;
            else
                return false;
        } // TutorialMain_CheckCallErrors()



        /// <summary>
        ///     This is a bridge function to call the 'TutorialMain_Driver'
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will display a movie [can be concurrent with tutorialWindow].  Default is false.
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will display a window [can be concurrent with tutorialMovie].  Default is false.
        /// </param>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <param name="randomTutorialType">
        ///     When true, this will randomly select the other tutorial types in which to play or display.  This requires atleast two or more tutorial types.
        /// </param>
        private void TutorialMain_Driver_Accessor(bool tutorialMovie = false,
                                        bool tutorialWindow = false,
                                        int PlayIndex = 0,
                                        bool randomIndex = false,
                                        bool randomTutorialType = false)
        {
            StartCoroutine(TutorialMain_Driver(tutorialMovie, tutorialWindow, PlayIndex, randomIndex, randomTutorialType));
        } // TutorialMain_Driver_Accessor()



        /// <summary>
        ///     Main tutorial sequence driver that manages how the tutorials are to be displayed and what type.
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will display a movie [can be concurrent with tutorialWindow].  Default is false.
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will display a window [can be concurrent with tutorialMovie].  Default is false.
        /// </param>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Driver(bool tutorialMovie = false,
                                        bool tutorialWindow = false,
                                        int PlayIndex = 0,
                                        bool randomIndex = false,
                                        bool randomTutorialType = false)
        {           
            // Make sure there is no errors
            if (TutorialMain_CheckErrors(tutorialMovie, tutorialWindow, PlayIndex))
                yield break;
            // ----

            // If randomized tutorial type was requested
            if (randomTutorialType && (tutorialMovie && tutorialWindow))
            {
                if (System.Convert.ToBoolean(UnityEngine.Random.Range(0, 2)))
                    // Movie
                    yield return (StartCoroutine(TutorialMain_Driver_Play_Movie(PlayIndex, randomIndex)));
                else
                    // Window
                    yield return (StartCoroutine(TutorialMain_Driver_Play_Window(PlayIndex, randomIndex)));
            }
            // If randomized tutorial was not requested
            else
            {
                // Play the tutorials as requested
                    if (tutorialMovie)
                        yield return (StartCoroutine(TutorialMain_Driver_Play_Movie(PlayIndex, randomIndex)));
                    if (tutorialWindow)
                        yield return (StartCoroutine(TutorialMain_Driver_Play_Window(PlayIndex, randomIndex)));
            }
            

            // Finished tutorial
                TutorialMain_FinishedSignal();
        } // TutorialMain_Driver()



        /// <summary>
        ///     Execute the movie tutorial protocol
        /// </summary>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Driver_Play_Movie(int PlayIndex, bool randomIndex)
        {
            yield return (StartCoroutine(TutorialMain_Driver_RunTutorial_Movie(PlayIndex, randomIndex)));
        } // TutorialMain_Driver_Play_Movie()



        /// <summary>
        ///     Execute the window dialog tutorial protocol
        /// </summary>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Driver_Play_Window(int PlayIndex, bool randomIndex)
        {
            yield return (StartCoroutine(TutorialMain_Driver_RunTutorial_Window(PlayIndex, randomIndex)));
        } // TutorialMain_Driver_Play_Window()



        /// <summary>
        ///     Main tutorial sequence driver that manages how the tutorials are to be displayed and what type.
        /// </summary>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Driver_RunTutorial_Movie(int PlayIndex, bool randomIndex)
        {
            // Declarations
            // ----
                // Used for determining the index of the array that has been highlighted.
                    int index;
            // ----

            // Fetch the index
                index = Randomized(PlayIndex, randomIndex, tutorialMovieArray);
            // Flip the Tutorial State
                ToggleTutorialState();
            // Play the movie
                TutorialMain_Play_Movie(PlayIndex, randomIndex);
            // Check the tutorial state
                yield return (StartCoroutine(RunTimeExecution_BackEnd(true, false, index)));
        } // TutorialMain_Driver_RunTutorial_Movie()



        /// <summary>
        ///     Main tutorial sequence driver that manages how the tutorials are to be displayed and what type.
        /// </summary>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.  Default is 0.
        /// </param>
        /// <param name="randomIndex">
        ///     When true, this will randomize what tutorials (movie and/or window) is to be played; if part of the index array.  Default is false.
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TutorialMain_Driver_RunTutorial_Window(int PlayIndex, bool randomIndex)
        {
            // Declarations
            // ----
                // Used for determining the index of the array that has been highlighted.
                    int index;
            // ----

            // Fetch the index
                index = Randomized(PlayIndex, randomIndex, tutorialWindowArray);

            // Flip the Tutorial State
                ToggleTutorialState();
            // Render the dialog window
                TutorialMain_Play_Window(PlayIndex, randomIndex);
            // Check the tutorial state
                yield return (StartCoroutine(RunTimeExecution_BackEnd(false, true, index)));
        } // TutorialMain_Driver_RunTutorial_Window()



        /// <summary>
        ///     Calculates the index pointer when the randomization has been toggled.
        /// </summary>
        /// <param name="playIndex">
        ///     Requested index to be highlighted [ignored if randomIndex is TRUE]
        /// </param>
        /// <param name="randomIndex">
        ///     Randomize the highlighted index by taking the length of the List<>.
        /// </param>
        /// <param name="array">
        ///     List<> of the type of tutorial being used in this instance.
        /// </param>
        /// <returns>
        ///     Randomized value between 0 - List<> length.  Default is playIndex if randomization is not enabled.
        /// </returns>
        private int Randomized(int playIndex, bool randomIndex, List<GameObject> array)
        {
            // If randomization is not true, then return the default requested highlighted index
            if (!randomIndex)
                return playIndex;

            // With randomization enabled:
            // Make sure that the length of the list<> is atleast greater than '0', if not return index of '0'.
                if (array.Count == 0)
                    return 0;

            // Generate and return the randomized range.
                return Random.Range(0, array.Count);
        } // Randomized()



        /// <summary>
        ///     Backend spine that works with the timeout scheduler and tutorial session
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will execute the movie tutorial
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will run the Window Dialog tutorial
        /// </param>
        /// <param name="index">
        ///     Selects what highlighted index from the array is selected
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator RunTimeExecution_BackEnd(bool tutorialMovie, bool tutorialWindow, int index)
        {
            // Declarations and intializations
            // ----
                GameObject tutorialObject = tutorialMovie ? tutorialMovieArray[index] : tutorialWindowArray[index];
                IEnumerator checkActiveStatus = CheckActiveStatus(tutorialObject);
                IEnumerator timeOutScheduler = TimedOutFunction(tutorialMovie, tutorialWindow, index);
                IEnumerator runTimeExecutionState = RunTimeExecution_StatusCheck();
                
            // ----

            // Start the coroutines
                StartCoroutine(timeOutScheduler); // Run the time out scheduler
                StartCoroutine(checkActiveStatus); // Check status of the tutorial; is it active or disabled?

            yield return StartCoroutine(runTimeExecutionState);

            // Terminate active coroutines
                StopCoroutine(checkActiveStatus);

                // if the Timed-Out scheduler is running, destroy the instance
                if (enableForceTimeOut)
                    StopCoroutine(timeOutScheduler);
            
            
            // Finished
                yield break;
        } // RunTimeExecution_BackEnd()



        /// <summary>
        ///     Checks the value of the status variable and then closes once the executation state is false.
        /// </summary>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator RunTimeExecution_StatusCheck()
        {
            do
            {
                yield return new WaitForSeconds(0.3f);
            } while (tutorialExecutionState);

            yield break;
        } // RunTimeExecution_StatusCheck()



        /// <summary>
        ///     This allots a function so much time in order to run the tutorial.
        ///     This function is a stand-alone is hard-coded to forcibly terminate 'RunTimeExecution'.
        /// </summary>
        /// <param name="coroutineFunction">
        ///     Stored value used for termination of a instance of a coroutine
        /// </param>
        /// <param name="tutorialMovie">
        ///     Stored value; used for focibly killing tutorial.
        /// </param>
        /// <param name="tutorialWindow">
        ///     Stored value; used for focibly killing tutorial.  
        /// </param>
        /// <param name="index">
        ///     Stored value; use for focibly killing tutorial
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator TimedOutFunction(bool tutorialMovie, bool tutorialWindow, int index)
        {
            // Is Time-Out scheduler allowed to run?
                if (!enableForceTimeOut)
                    yield break; // Stop

            // Wait for the requested timed-out time schedule
                yield return new WaitForSeconds(timedOut_Seconds);

            // Is the tutorials still running?
            if (tutorialExecutionState)
            {
                ForcibleKillSignal(tutorialMovie, tutorialWindow, index);
                TutorialMain_Error(4);
            }

            // No tutorials are running, stop.
            else
                yield break;
        } // TimedOutFunction()



        /// <summary>
        ///     Monitors the state of the object if it's active; this function will self terminate once the targetted object is diabled from the hierarchy.
        /// </summary>
        /// <param name="tutorialObject">
        ///     Targgeted object to monitor
        /// </param>
        /// <returns>
        ///     Nothing useful
        /// </returns>
        private IEnumerator CheckActiveStatus(GameObject tutorialObject)
        {
            do
            {
                yield return new WaitForSeconds(0.5f);
            } while (tutorialObject.activeInHierarchy);

            // Flip the tutorial state variable to signify that the tutorial has finished
                ToggleTutorialState();

            yield break;
        } // CheckActiveStatus()



        /// <summary>
        ///     When a timeout occurs, this function will signal the running tutorial to terminate.
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will be used for focibly killing the tutorial.
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will be used for focibly killing the tutorial.
        /// </param>
        /// <param name="index">
        ///     Selects the index that is currently highlighted
        /// </param>
        private void ForcibleKillSignal(bool tutorialMovie, bool tutorialWindow, int index)
        {
            if (tutorialMovie)
                ToggleObjectActiveState(false, tutorialMovieArray[index]);
            if (tutorialWindow)
                ToggleObjectActiveState(false, tutorialWindowArray[index]);
        } // ForcibleKillSignal()



        /// <summary>
        ///     Calls the desired movie tutorial
        /// </summary>
        /// <param name="playIndex">
        ///     Select the index inwhich to play the movie
        /// </param>
        /// <param name="randomIndex">
        ///     Randomly select an index
        /// </param>
        private void TutorialMain_Play_Movie(int playIndex, bool randomIndex)
        {
            ToggleObjectActiveState(true, tutorialMovieArray[playIndex]);
        } // TutorialMain_Play_Movie()



        /// <summary>
        ///     Calls the desired window dialog tutorial
        /// </summary>
        /// <param name="playIndex">
        ///     Select the index inwhich to display the dialog window
        /// </param>
        /// <param name="randomIndex">
        ///     Randomly select an index
        /// </param>
        private void TutorialMain_Play_Window(int playIndex, bool randomIndex)
        {
            ToggleObjectActiveState(true, tutorialWindowArray[playIndex]);
        } // TutorialMain_Play_Window()



        /// <summary>
        ///     This function is used to check for possible errors that could commonly occur if the setup is incorrect.
        /// </summary>
        /// <param name="tutorialMovie">
        ///     When true, this will display a movie [can be concurrent with tutorialWindow].
        /// </param>
        /// <param name="tutorialWindow">
        ///     When true, this will display a window [can be concurrent with tutorialMovie].
        /// </param>
        /// <param name="PlayIndex">
        ///     Forcibly play or display the window within the exact index.
        /// </param>
        /// <returns>
        ///     true = Failure or there was error.
        ///     false = No errors detected.
        /// </returns>
        private bool TutorialMain_CheckErrors(bool tutorialMovie,
                                                bool tutorialWindow,
                                                int PlayIndex)
        {
            // Make sure that either the window or movie has been selected
            if (TutorialMain_CheckCallErrors(tutorialMovie, tutorialWindow))
            {
                TutorialMain_Error(1);
                TutorialMain_FinishedSignal();
                return true;
            } // if (Tutorial Mode error)

            // Make sure that the range is accessible; I.E. prevent out of range error
            if (tutorialMovie)
            {
                if (PlayIndex <= tutorialMovieArray.Count && PlayIndex >= 0)
                {
                    // Nothing todo; this statement is true and there is no error.
                }
                else
                {
                    TutorialMain_Error(5, "Movie Tutorial using index [" + PlayIndex + "]");
                    TutorialMain_FinishedSignal();
                    return true;
                }

            } // Tutorial Movie index length error

            // Make sure that the range is accessible; I.E. prevent out of range error
            if (tutorialWindow)
            {
                if (tutorialWindow && PlayIndex <= tutorialWindowArray.Count && PlayIndex >= 0)
                {
                    // Nothing todo; this statement is true and there is no error.
                }
                else
                {
                    TutorialMain_Error(5, "Dialog Window Tutorial using index [" + PlayIndex + "]");
                    TutorialMain_FinishedSignal();
                    return true;
                }
            } // Tutorial Window index length error


            // Make sure that there is a tutorial to be played
            if (tutorialMovie && tutorialMovieArray[PlayIndex] == null)
            {
                // The requested index doesn't exist
                TutorialMain_Error(2, PlayIndex.ToString());
                TutorialMain_FinishedSignal();
                return true;
            } // Tutorial Movie, make sure that the index location has been initialized

            // Make sure that there is a tutorial to be rendered
            else if (tutorialWindow && tutorialWindowArray[PlayIndex] == null)
            {
                // The requested index doesn't exist
                TutorialMain_Error(3, PlayIndex.ToString());
                TutorialMain_FinishedSignal();
                return true;
            } // Tutorial Window, make sure that the index location has been initialized


            // No errors detected
                return false;
        } // TutorialMain_CheckErrors()



        /// <summary>
        ///     Once the tutorial sequence is finished, notify all classes\scripts that the tutorial ended.
        /// </summary>
        private void TutorialMain_FinishedSignal()
        {
            TutorialFinished();
        } // TutorialMain_FinishedSignal()



        /// <summary>
        ///     When called, flips the Tutorial Execution State value to it's opposite value.
        /// </summary>
        private void ToggleTutorialState()
        {
            tutorialExecutionState = !tutorialExecutionState;
        } // ToggleTutorialState()



        /// <summary>
        ///     When true, this will enable the targetted object.
        ///     When false, this will disable the targetted object.
        /// </summary>
        /// <param name="state">
        ///     Toggles the state inwhich to either enable or disable the targetted object.
        /// </param>
        /// <param name="obj">
        ///     The targetted game object.
        /// </param>
        private void ToggleObjectActiveState(bool state, GameObject obj)
        {
            obj.SetActive(state);
        } // ToggleObjectActiveState()



        /// <summary>
        ///     Output an error message to the console
        /// </summary>
        /// <param name="errType">
        ///     Type of error:
        ///         0 = no error (default)
        ///         1 = No tutorial type was selected (movie nor window)
        ///         2 = The movie tutorial index is not valid or was never initialized within the Unity's Inspector.
        ///         3 = The dialog window tutorial index is not valid or was never initialized within the Unity's Inspector.
        ///         4 = Function timed out; prevented run-away (never ending) function
        /// </param>
        /// <param name="message">
        ///     Specific message used for the console along with the initial generic message.  Default is "".
        /// </param>
        private void TutorialMain_Error(ushort errType = 0, string message = "")
        {
            // Initalize a cache string var; we will use this for constructing the message.
                string consoleMessage = "Something happened"; // I decided to add Microsoft's famous error message!  I think this is verbose enough and is very clear as to how we can address the problems! :D [NG]
            
            // Construct the error message by scanning through the error library
                switch (errType)
                {
                    case 0:
                        consoleMessage = "Well this is odd; this shouldn't happen!  There is _NO_ error";
                        break;
                    case 1:
                        consoleMessage = "Tutorial protocol was called without playing any tutorials";
                        break;
                    case 2:
                        consoleMessage = "The movie tutorial index [" + message + "] does not exist!";
                        break;
                    case 3:
                        consoleMessage = "The dialog window tutorial index [" + message + "] does not exist!";
                        break;
                    case 4:
                        consoleMessage = "Timed_Out: Runaway function was terminated";
                        break;
                    case 5:
                        consoleMessage = "Index was invalid and is out of range: " + message;
                        break;
                } // switch()

            // Output the error message
                Debug.LogError("<!> ERROR <!> \n" + consoleMessage);
        } // TutorialMain_Error()
    } // End of Class
} // Namespace