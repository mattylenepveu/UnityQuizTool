// Lists all the classes the LoadQuestions will need
using System.Collections;
using UnityEngine;

// Creates the LoadQuestions class
public class LoadQuestions : MonoBehaviour
{
    // Variable indicates the file name for the text document where the questions are
    [SerializeField]
    private string m_strFileName = "Questions";

    // Is where all the questions from the text document will be stored
    private static Question[] m_questions;

    // Array stores all the lines individually from the text document
    private string[] m_strContents;

    // Indicates how many questions are featured in the document
    private int m_nTotalQuestions;

    //--------------------------------------------------------------------------------
    // Function is called when script is first called.
    //--------------------------------------------------------------------------------
    void Start()
    {
        // Reads and stores all lines individually from the file into the contents array
        m_strContents = System.IO.File.ReadAllLines("./Assets/Resources/" + 
                                                    m_strFileName + ".txt");

        // Calculates the total questions by dividing the length of contents by two
        m_nTotalQuestions = m_strContents.Length / 2;

        // Declares a "new" array for the questions to be stored in
        m_questions = new Question[m_nTotalQuestions];

        // Calls and runs the "ReadTextFile" function
        ReadTextFile();
    }

    //--------------------------------------------------------------------------------
    // Function reads the lines from the text files and sorts them into questions.
    //--------------------------------------------------------------------------------
    public void ReadTextFile()
    {
        // Runs a for loop for the amount of questions in the text file
        for (int i = 0; i < m_nTotalQuestions; i++)
        {
            // Used to calculate the index for the questions in the contents array
            int nTemp = i * 2;

            // Calculates the index for the answer in the contents array
            int nTempTwo = (i * 2) + 1;

            // Checks if the current question is listed as true in the text doc
            if (m_strContents[nTempTwo] == "t" || m_strContents[nTempTwo] == "T")
            {
                // Stores the question in the questions array with its answer being true
                m_questions[i] = new Question(m_strContents[nTemp], true);
            }
            // Else it stores the question in array with false being the answer
            else
            {
                m_questions[i] = new Question(m_strContents[nTemp], false);
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Function returns all the questions when called.
    //
    // Return:
    //      Returns the questions in an array.
    //--------------------------------------------------------------------------------
    public Question[] GetQuestions()
    {
        return m_questions;
    }
}
