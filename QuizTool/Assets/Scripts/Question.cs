// Creates a class for each question
[System.Serializable]
public class Question
{
    // Stores the question name as a string
    public string m_strQuestion;

    // Boolean indicates whether the question is true or false
    public bool m_bIsTrue;

    //--------------------------------------------------------------------------------
    // Copy Constructor.
    //--------------------------------------------------------------------------------
    public Question(string strQuestion, bool bIsTrue)
    {
        // Sets the passed in string to equal the question string
        this.m_strQuestion = strQuestion;

        // Sets the passed in bool to equal the is true boolean
        this.m_bIsTrue = bIsTrue;
    }
}
