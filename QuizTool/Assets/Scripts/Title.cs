using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private int m_nQuizIndex;

    public void PlayButton()
    {
        SceneManager.LoadScene(m_nQuizIndex);
    }
}
