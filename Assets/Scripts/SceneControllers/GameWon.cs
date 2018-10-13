using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorFree : MonoBehaviour {

    // Use this for initialization
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
