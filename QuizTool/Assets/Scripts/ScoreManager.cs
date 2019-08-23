// Creates the class for the ScoreManager
[System.Serializable]
public class ScoreManager
{
    // Integer stores the user's score for the quiz
    private int m_nScore;

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
    public int ReturnScore()
    {
        return m_nScore;
    }
}
