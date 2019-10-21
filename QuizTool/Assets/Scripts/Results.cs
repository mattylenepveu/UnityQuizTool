// Lists all the classes the Results will need
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Creates the Results class
public class Results : MonoBehaviour
{
    // Static variable gives the script access to the score manager script
    private static ScoreManager m_scoreManager;

    // Varaiable allows the text that displays the score to be changed in this script
    [SerializeField]
    private Text m_playerScore;

    // Allows the text that displays the total questions to be changed in this script
    [SerializeField]
    private Text m_totalQuestions;

    // Keeps track of the score throughout the quiz
    private static int m_nPlayerScore;

    // Stores the amount of questions involved in the quiz
    private static int m_nTotalQuestions;

    [SerializeField]
    private int m_nQuizIndex;

    //--------------------------------------------------------------------------------
    // Function is called when script is first called.
    //--------------------------------------------------------------------------------
    private void Start()
    {
        // Gets the scoremanager script component off the same object this script is on
        m_scoreManager = GetComponent<ScoreManager>();

        // Obtains the score from the score manager which should be zero here
        m_nPlayerScore = m_scoreManager.GetScore();

        // Updates the score to the canvas by converting the score int to a string
        m_playerScore.text = m_nPlayerScore.ToString();

        // Stores the amount of questions in a new variable 
        m_nTotalQuestions = m_scoreManager.GetQuestionsAmount();

        // Converts last int to a string then applies that to the total questions UI text
        m_totalQuestions.text = m_nTotalQuestions.ToString();
    }

    public void RestartQuiz()
    {
        SceneManager.LoadScene(m_nQuizIndex);
    }
}
