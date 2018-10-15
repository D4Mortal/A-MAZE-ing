using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class KeysLeft : MonoBehaviour {

    private int keyCount;
    public keyManager keyManager;
    public Text KeyCountText;
    // Use this for initialization
    void Start () {

        this.keyManager = GameObject.Find("KeyManager").GetComponent<keyManager>();
        keyCount = keyManager.keys;
        KeyCountText.text = "Keys left: " + keyCount.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        keyCount = keyManager.keys;
        KeyCountText.text = "Keys left: " + keyCount.ToString();
    }
}
