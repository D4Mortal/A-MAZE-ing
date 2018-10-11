using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeGeneration : MonoBehaviour {
    private GameObject floor;
    private int MAZE_SIZE = 10;


	// Use this for initialization
	void Start () {
        floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.name = "floor";
        Transform floorTransform = floor.transform.GetComponent<Transform>();
        floorTransform.localScale = new Vector3(MAZE_SIZE, 1, MAZE_SIZE);

        CreateMaze();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateMaze() {
        
    }
}
