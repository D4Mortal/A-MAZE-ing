using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLost : MonoBehaviour {

    void Start()
    {
        SetCursorState();
    }
        void SetCursorState()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void BackClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
