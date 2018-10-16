using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentMusic : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Music");
        DontDestroyOnLoad(gameObject);
    }
}
