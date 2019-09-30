// Lists all the classes the QuizManager will need
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
    public Question[] m_questions;

    // List stores all the unanswered questions in the quiz
    private static List<Question> m_unanswered;

    // Represents the current question that needs to be displayed
    private Question m_current;

    // Field represents the text where the question will be shown
    [SerializeField]
    private Text m_questionText;
    
    // Shows either correct or wrong indicating whether true is correct for a question
    [SerializeField]
    private Text m_trueAnswerText;

    // Shows either correct or wrong indicating whether false is right for a question
    [SerializeField]
    private Text m_falseAnswerText;

    [SerializeField]
    private Image m_trueButton;

    [SerializeField]
    private Image m_falseButton;

    private static ScoreManager m_scoreManager;

    // Indicates the waiting time between questions in seconds
    [SerializeField]
    private float m_fTimeBetweenQuestions = 3.0f;

    private float m_fTimer;

    [SerializeField]
    private float m_fFlashRate = 0.02f;

    private bool m_bTrueSelected;

    private bool m_bFalseSelected;

    //--------------------------------------------------------------------------------
    // Function is called when script is first called.
    //--------------------------------------------------------------------------------
    private void Start()
    {
        m_scoreManager = GetComponent<ScoreManager>();

        m_fTimer = 0.0f;

        m_bTrueSelected = false;

        m_bFalseSelected = false;

        m_trueButton.color = Color.white;

        // Checks if there are any questions in the unanswered questions list
        if (m_unanswered == null || m_unanswered.Count == 0)
        {
            // Puts all inputted questions into the unanswered list
            m_unanswered = m_questions.ToList<Question>();

            m_scoreManager.ResetScore();

            m_scoreManager.SetQuestionsAmount(m_unanswered.Count);
        }

        // Calls set question function for UI to show first question
        SetCurrentQuestion();
    }

    private void Update()
    {
        if (m_bTrueSelected)
        {
            // Checks if the true button was selected
            if (m_current.m_bIsTrue)
            {
                CorrectAnswer(true);
            }
            // Else calls the wrong answer function if the false button was selected
            else
            {
                WrongAnswer(true);
            }

            // Quiz waits however long the QuestionTransition function returns
            StartCoroutine(QuestionTransition());
        }
        
        if (m_bFalseSelected)
        {
            // Checks if the false button was selected
            if (!m_current.m_bIsTrue)
            {
                CorrectAnswer(false);

                m_scoreManager.AddOneToScore();
            }
            // Else calls the wrong answer function if the true button was selected
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

        // Checks if the current question is true
        if (m_current.m_bIsTrue)
        {
            // Sets text to correct if the user answers true
            m_trueAnswerText.text = "CORRECT!";

            // Sets text to wrong if the user answers false
            m_falseAnswerText.text = "WRONG!";
        }
        // Else if the current question is false
        else
        {
            // Sets text to wrong if the user answers true
            m_trueAnswerText.text = "WRONG!";

            // Sets text to correct if the user answers false
            m_falseAnswerText.text = "CORRECT!";
        }
    }

    //--------------------------------------------------------------------------------
    // Function runs if the user clicks the true button.
    //--------------------------------------------------------------------------------
    public void UserSelectTrue()
    {
        m_bTrueSelected = true;
    }

    //--------------------------------------------------------------------------------
    // Function runs if the user clicks the false button.
    //--------------------------------------------------------------------------------
    public void UserSelectFalse()
    {
        m_bFalseSelected = true;
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

        if (m_unanswered.Count == 0)
        {
            Debug.Log("FIN");
        }
        else
        {
            // Reloads the scene so the next question can be displayed
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CorrectAnswer(bool bSelectedAnswer)
    {
        if (bSelectedAnswer)
        {
            m_fTimer += Time.deltaTime;

            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_trueButton.color = Color.green;
            }
            else
            {
                m_trueButton.color = Color.white;
            }
        }
        else
        {
            m_fTimer += Time.deltaTime;

            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_falseButton.color = Color.green;
            }
            else
            {
                m_falseButton.color = Color.white;
            }
        }
    }

    private void WrongAnswer(bool bSelectedAnswer)
    {
        if (bSelectedAnswer)
        {
            m_fTimer += Time.deltaTime;

            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_trueButton.color = Color.grey;
            }
            else
            {
                m_trueButton.color = Color.white;
            }
        }
        else
        {
            m_fTimer += Time.deltaTime;

            if (Mathf.Sin(m_fTimer / m_fFlashRate) >= 0)
            {
                m_falseButton.color = Color.grey;
            }
            else
            {
                m_falseButton.color = Color.white;
            }
        }
    }
}
