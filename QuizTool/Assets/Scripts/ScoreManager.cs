//--------------------------------------------------------------------------------
// Script holds all the functions regarding the score and amount of questions.
//--------------------------------------------------------------------------------

// Lists all the usings the Title script will need
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Integer stores the user's score for the quiz
    [SerializeField]
    private static int m_nScore;

    // Integer indicates how many questions there are in a quiz
    [SerializeField]
    private static int m_nQuestionsAmount;

    //--------------------------------------------------------------------------------
    // Function resets the score to zero when quiz is restarted, exited or completed.
    //--------------------------------------------------------------------------------
    public void ResetScore()
    {
        m_nScore = 0;
    }

    //--------------------------------------------------------------------------------
    // Function allows other classes to set the value of the questions amount int.
    //--------------------------------------------------------------------------------
    public void AddOneToScore()
    {
        m_nScore++;
    }

    //--------------------------------------------------------------------------------
    // Function allows other classes to access the score integer.
    //
    // Return:
    //      Returns the user's score as an integer.
    //--------------------------------------------------------------------------------
    public int GetScore()
    {
        return m_nScore;
    }

    //--------------------------------------------------------------------------------
    // Function allows other classes to set the value of the questions amount int.
    //
    // Param:
    //      nQuestionsAmount: An int representing how many questions are in the quiz.
    //--------------------------------------------------------------------------------
    public void SetQuestionsAmount(int nQuestionsAmount)
    {
        m_nQuestionsAmount = nQuestionsAmount;
    }

    //--------------------------------------------------------------------------------
    // Function allows other classes to access the questions integer.
    //
    // Return:
    //      Returns the amount of questions in a quiz as an integer.
    //--------------------------------------------------------------------------------
    public int GetQuestionsAmount()
    {
        return m_nQuestionsAmount;
    }
}
