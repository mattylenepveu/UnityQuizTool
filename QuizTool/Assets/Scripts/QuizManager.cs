using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Question[] m_questions;
    private static List<Question> m_unanswered;

    private Question m_current;

    [SerializeField]
    private Text m_questionText;

    [SerializeField]
    private Text m_trueAnswerText;

    [SerializeField]
    private Text m_falseAnswerText;

    [SerializeField]
    private Animator m_anim;

    [SerializeField]
    private float m_fTimeBetweenQuestions = 1.0f;

    private void Start()
    {
        if (m_unanswered == null || m_unanswered.Count == 0)
        {
            m_unanswered = m_questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    private void SetCurrentQuestion()
    {
        int nRandomQuestionIndex = Random.Range(0, m_unanswered.Count);
        m_current = m_unanswered[nRandomQuestionIndex];

        m_questionText.text = m_current.m_strQuestion;

        if (m_current.m_bIsTrue)
        {
            m_trueAnswerText.text = "CORRECT!";
            m_falseAnswerText.text = "WRONG!";
        }
        else
        {
            m_trueAnswerText.text = "WRONG!";
            m_falseAnswerText.text = "CORRECT!";
        }
    }

    public void UserSelectTrue()
    {
        m_anim.SetTrigger("True");

        if (m_current.m_bIsTrue)
        {
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }

        StartCoroutine(QuestionTransition());
    }

    public void UserSelectFalse()
    {
        m_anim.SetTrigger("False");

        if (m_current.m_bIsTrue)
        {
            Debug.Log("WRONG!");
        }
        else
        {
            Debug.Log("CORRECT!");
        }

        StartCoroutine(QuestionTransition());
    }

    private IEnumerator QuestionTransition()
    {
        m_unanswered.Remove(m_current);

        yield return new WaitForSeconds(m_fTimeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
