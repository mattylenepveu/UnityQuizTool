using System.Collections;
using UnityEngine;

public class LoadQuestions : MonoBehaviour
{
    public string m_strData = "Questions";
    private string m_strContents;
    private TextAsset m_txtAsset;

    // Start is called before the first frame update
    void Start()
    {
        m_txtAsset = (TextAsset)Resources.Load(m_strData);
        m_strContents = m_txtAsset.text;
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUILayout.Label(m_strContents);
    }

    public void ReadTextFile()
    {
        // Split questions into a list 

        // Initiate for loop that iterates through for the amount of questions

        // Make sure the text file has questions in it

        // Add questions from text file to questions array one by one
    }
}
