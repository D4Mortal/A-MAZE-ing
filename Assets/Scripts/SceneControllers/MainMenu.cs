using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameClick()
    {
        SceneManager.LoadScene("15_15Maze");
    }


    public void InstructionsClick()
    {
        SceneManager.LoadScene("InstructionsScene");
    }


    public void TutorialClick()
    {
        SceneManager.LoadScene("Tutorial");
    }


    public void QuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }

}
