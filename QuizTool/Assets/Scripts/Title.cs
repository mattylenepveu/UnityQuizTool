//--------------------------------------------------------------------------------
// Script manages everything involved in the Title scene.
//--------------------------------------------------------------------------------

// Lists all the usings the Title script will need
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Creates the Title class
public class Title : MonoBehaviour
{
    // Stores the build index for the main quiz scene
    [SerializeField]
    private int m_nQuizIndex;

    //--------------------------------------------------------------------------------
    // Function loads the Main Quiz scene and is attached to the Play button.
    //--------------------------------------------------------------------------------
    public void PlayButton()
    {
        SceneManager.LoadScene(m_nQuizIndex);
    }
}
