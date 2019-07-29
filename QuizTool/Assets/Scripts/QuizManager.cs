using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public Question[] m_questions;
    private static List<Question> m_unanswered;

    private Question m_current;

    private void Start()
    {
        if (m_unanswered == null || m_unanswered.Count == 0)
        {
            m_unanswered = m_questions.ToList<Question>();
        }

        GetRandomQuestion();
        Debug.Log(m_current.m_strQuestion + " is " + m_current.m_bIsTrue);
    }

    private void GetRandomQuestion()
    {
        int nRandomQuestionIndex = Random.Range(0, m_unanswered.Count);
        m_current = m_unanswered[nRandomQuestionIndex];

        m_unanswered.RemoveAt(nRandomQuestionIndex);
    }
}
