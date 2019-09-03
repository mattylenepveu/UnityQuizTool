using UnityEngine;

// Creates the class for the ScoreManager
public class ScoreManager : MonoBehaviour
{
    // Integer stores the user's score for the quiz
    [SerializeField]
    private int m_nScore;

    // Integer indicates how many questions there are in a quiz
    [SerializeField]
    private int m_nQuestionsAmount;

    //--------------------------------------------------------------------------------
    // Function resets the score to zero when quiz is restarted, exited or completed.
    //--------------------------------------------------------------------------------
    public void ResetScore()
    {
        m_nScore = 0;
    }

    //--------------------------------------------------------------------------------
    // Function adds one to score and is called when user gets a question correct.
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
    // Function allows other classes to set a value for the questions integer.
    //
    // Param:
    //      nAmount: An integer that represents the new value of questions int.
    //--------------------------------------------------------------------------------
    public void SetQuestionsAmount(int nAmount)
    {
        m_nQuestionsAmount = nAmount;
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
