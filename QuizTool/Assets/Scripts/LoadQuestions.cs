using System.Collections;
using UnityEngine;

public class LoadQuestions : MonoBehaviour
{
    public string m_strFileName = "Questions";

    private static Question[] m_questions;

    private string[] m_strContents;

    private int m_nTotalQuestions;

    // Start is called before the first frame update
    void Start()
    {
        m_strContents = System.IO.File.ReadAllLines("./Assets/Resources/" + m_strFileName + ".txt");

        m_nTotalQuestions = m_strContents.Length / 2;

        m_questions = new Question[m_nTotalQuestions];

        ReadTextFile();
    }

    public void ReadTextFile()
    {
        for (int i = 0; i < m_nTotalQuestions; i++)
        {
            int nTemp = i * 2;
            int nTempTwo = (i * 2) + 1;

            if (m_strContents[nTempTwo] == "t" || m_strContents[nTempTwo] == "T")
            {
                m_questions[i] = new Question(m_strContents[nTemp], true);
            }
            else
            {
                m_questions[i] = new Question(m_strContents[nTemp], false);
            }
        }
    }

    public Question[] GetQuestions()
    {
        return m_questions;
    }

    public int GetTotalQuestions()
    {
        return m_nTotalQuestions;
    }
}
