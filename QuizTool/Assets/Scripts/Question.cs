// Creates a class for each question
[System.Serializable]
public class Question
{
    // Stores the question name as a string
    public string m_strQuestion;

    // Boolean indicates whether the question is true or false
    public bool m_bIsTrue;

    public Question(string strQuestion, bool bIsTrue)
    {
        this.m_strQuestion = strQuestion;
        this.m_bIsTrue = bIsTrue;
    }
}
