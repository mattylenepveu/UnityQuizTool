//--------------------------------------------------------------------------------
// Script manages the logic of the main quiz.
//--------------------------------------------------------------------------------

// Lists all the usings the QuizManager will need
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

// Creates the QuizManager class
public class QuizManager : MonoBehaviour
{
    // Initializes an array of questions where users can create questions in inspector
    private static Question[] m_questions;

    // List stores all the unanswered questions in the quiz
    private static List<Question> m_unanswered;

    // An enumerator that indicates if true, false or no answer has been selected
    private enum ENUM_QUIZSTATE { Unselected, TrueSelected, FalseSelected };

    // Provides a reference to the quiz state enumerator
    private ENUM_QUIZSTATE m_quizState;

    // Represents the current question that needs to be displayed
    private Question m_current;

    // Field represents the text where the question will be shown
    [SerializeField]
    private Text m_questionText;

    // Varaiable allows the text that displays the score to be changed in this script
    [SerializeField]
    private Text m_playerScore;

    // Allows the text that displays the total questions to be changed in this script
    [SerializeField]
    private Text m_totalQuestions;

    [SerializeField]
    private Text m_questionNumber;

    // Represents the true button as an image
    [SerializeField]
    private Image m_trueButton;

    // Image represents the false button in the quiz
    [SerializeField]
    private Image m_falseButton;

    // Static variable gives the script access to the score manager script
    private static ScoreManager m_scoreManager;

    // Allows this script to access variables and functions from load questions script
    private static LoadQuestions m_loadQuestions;

    // Indicates the waiting time between questions once an answer has been selected
    [Range(1.0f, 5.0f)]
    [SerializeField]
    private float m_fTimeBetweenQuestions;

    // Represents how quickly the button colour will change once selected
    [Range(10.0f, 20.0f)]
    [SerializeField]
    private float m_fFlashRate;

    // Keeps track of the time to calculate sin for the flashing
    private float m_fTimer;

    // Stores the amount of questions involved in the quiz
    private static int m_nTotalQuestions;

    // Keeps track of the score throughout the quiz
    private static int m_nPlayerScore;

    private static int m_nQuestionNumber;

    // Indicates what the scene number is for the results screen in Build Settings
    [SerializeField]
    private int m_nResultsIndex;

    //--------------------------------------------------------------------------------
    // Function is called when script is first called.
    //--------------------------------------------------------------------------------
    private void Start()
    {
        // Gets the scoremanager script component off the same object this script is on
        m_scoreManager = GetComponent<ScoreManager>();

        // Accesses the Load Question script component
        m_loadQuestions = GetComponent<LoadQuestions>();

        // Sets the timer to zero as a default value
        m_fTimer = 0.0f;

        // Sets the flash rate as the reciprical of itself
        m_fFlashRate = 1 / m_fFlashRate;

        // Initially sets the quiz state to be unselected
        m_quizState = ENUM_QUIZSTATE.Unselected;

        // Checks if there are any questions in the unanswered questions list
        if (m_unanswered == null || m_unanswered.Count == 0)
        {
            // Accesses the questions sorted in the Load Question script
            m_questions = m_loadQuestions.GetQuestions();

            // Puts all obtained questions into the unanswered list
            m_unanswered = m_questions.ToList<Question>();

            // Resets the score when a new quiz has been started
            m_scoreManager.ResetScore();

            // Sets the amount of questions in the score manager to equal the unanswered count
            m_scoreManager.SetQuestionsAmount(m_unanswered.Count);

            m_nQuestionNumber = 1;
        }
        else
        {
            m_nQuestionNumber++;
        }

        // Obtains the score from the score manager which should be zero here
        m_nPlayerScore = m_scoreManager.GetScore();

        // Updates the score to the canvas by converting the score int to a string
        m_playerScore.text = m_nPlayerScore.ToString();

        // Stores the amount of questions in a new variable 
        m_nTotalQuestions = m_scoreManager.GetQuestionsAmount();

        // Converts last int to a string then applies that to the total questions UI text
        m_totalQuestions.text = m_nTotalQuestions.ToString();

        m_questionNumber.text = m_nQuestionNumber.ToString();

        // Calls set question function for UI to show first question
        SetCurrentQuestion();
    }

    //--------------------------------------------------------------------------------
    // Function is called every frame and updates the scene when called.
    //--------------------------------------------------------------------------------
    private void Update()
    {
        // Detects if the true button has been selected
        if (m_quizState == ENUM_QUIZSTATE.TrueSelected)
        {
            // Checks if the true button was selected
            if (m_current.m_bIsTrue)
            {
                // Calls correct answer function passing in true as true was selected
                CorrectAnswer(true);
            }
            // Else calls the wrong answer function and passes in true as true was selected
            else
            {
                WrongAnswer(true);
            }

            // Quiz waits however long the QuestionTransition function returns
            StartCoroutine(QuestionTransition());
        }

        // Checks to see if the false button has been selected
        if (m_quizState == ENUM_QUIZSTATE.FalseSelected)
        {
            // Checks if the false button was selected
            if (!m_current.m_bIsTrue)
            {
                // Calls correct answer function passing in false as false was selected
                CorrectAnswer(false);
            }
            // Else calls the wrong answer function and passes in false as that was selected
            else
            {
                WrongAnswer(false);
            }

            // Quiz waits however long the QuestionTransition function returns
            StartCoroutine(QuestionTransition());
        }
    }

    //--------------------------------------------------------------------------------
    // Function sets a question for the quiz to display.
    //--------------------------------------------------------------------------------
    private void SetCurrentQuestion()
    {
        // Randomly selects a number between 0 and unanswered list count
        int nRandomQuestionIndex = Random.Range(0, m_unanswered.Count);

        // Grabs random question from unanswered list using the random int
        m_current = m_unanswered[nRandomQuestionIndex];

        // Calls the current question's string and stores as the question text for the UI
        m_questionText.text = m_current.m_strQuestion;
    }

    //--------------------------------------------------------------------------------
    // Function runs if the user clicks the true button.
    //--------------------------------------------------------------------------------
    public void UserSelectTrue()
    {
        // Makes sure that this is the first time a button has been clicked
        if (m_quizState == ENUM_QUIZSTATE.Unselected)
        {
            // Sets the quiz state to state the the true button has been selected
            m_quizState = ENUM_QUIZSTATE.TrueSelected;

            // Adds a point to the score if true is the correct answer to question
            if (m_current.m_bIsTrue)
            {
                AddPointToScore();
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Function runs if the user clicks the false button.
    //--------------------------------------------------------------------------------
    public void UserSelectFalse()
    {
        // Makes sure that this is the first time a button has been clicked
        if (m_quizState == ENUM_QUIZSTATE.Unselected)
        {
            // Sets the quiz state to state the the true button has been selected
            m_quizState = ENUM_QUIZSTATE.FalseSelected;

            // Adds a point to the score if false is the correct answer to question
            if (!m_current.m_bIsTrue)
            {
                AddPointToScore();
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Function waits a certain amount of time before quiz moves to next question.
    //
    // Return:
    //      Returns a float indicating the amount of time quiz waits.
    //--------------------------------------------------------------------------------
    private IEnumerator QuestionTransition()
    {
        // Removes the current question from the unanswered list
        m_unanswered.Remove(m_current);

        // Code waits the amount of seconds being passed in before moving to next line
        yield return new WaitForSeconds(m_fTimeBetweenQuestions);

        // Loads the results scene if there are no questions left in the unanswered list
        if (m_unanswered.Count == 0)
        {
            SceneManager.LoadScene(m_nResultsIndex);
        }
        // Else reloads the scene so the next question can be displayed
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //--------------------------------------------------------------------------------
    // Function makes the correct answer button flash green when clicked.
    //
    // Param:
    //      bSelectedAnswer: A bool indicating if true or false was selected.
    //--------------------------------------------------------------------------------
    private void CorrectAnswer(bool bSelectedAnswer)
    {
        // Timer begins counting up for sin calculations for flash
        m_fTimer += Time.deltaTime;

        // Checks to see if true was selected by the user
        if (bSelectedAnswer)
        {
            // Turns the true button green if answer to sin is positive
            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_trueButton.color = Color.green;
            }
            // Else adds no colour to the true button if sin is negative
            else
            {
                m_trueButton.color = Color.white;
            }
        }
        // Else if false was the chosen answer
        else
        {
            // Turns the false button green if answer to sin is positive
            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_falseButton.color = Color.green;
            }
            // Else adds no colour to the false button if sin is negative
            else
            {
                m_falseButton.color = Color.white;
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Function makes the wrong answer button flash grey when clicked.
    //
    // Param:
    //      bSelectedAnswer: A bool indicating if true or false was selected.
    //--------------------------------------------------------------------------------
    private void WrongAnswer(bool bSelectedAnswer)
    {
        // Timer begins counting up for sin calculations for flash
        m_fTimer += Time.deltaTime;

        // Checks to see if true was selected by the user
        if (bSelectedAnswer)
        {
            // Turns the true button grey if answer to sin is positive
            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_trueButton.color = Color.grey;
            }
            // Else adds no colour to the true button if sin is negative
            else
            {
                m_trueButton.color = Color.white;
            }
        }
        // Else if false was the chosen answer
        else
        {
            // Turns the false button grey if answer to sin is positive
            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_falseButton.color = Color.grey;
            }
            // Else adds no colour to the false button if sin is negative
            else
            {
                m_falseButton.color = Color.white;
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Adds a point to the score and updates the score for all components.
    //--------------------------------------------------------------------------------
    private void AddPointToScore()
    {
        // Adds a point to the player score in the score manager
        m_scoreManager.AddOneToScore();

        // Obtains and stores the newly updated score in player score int
        m_nPlayerScore = m_scoreManager.GetScore();

        // Updates the score to the canvas by converting the score int to a string
        m_playerScore.text = m_nPlayerScore.ToString();
    }
}
