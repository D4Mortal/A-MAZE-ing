using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentMusic : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Music");
        if (gameObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
