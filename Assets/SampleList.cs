using UnityEngine;
using System.Collections;

public class SampleList : MonoBehaviour {
	//Use arrays only in very specific circumstances
	public int[][] testArray = new int[2][];

	public int[,] array = new int[1,1];

	// Use this for initialization
	void Start () {
		//Arrays are not initialized with values already in them, this includes arrays of arrays.
		testArray [0] = new int[3];
		testArray [0] [1] = 1;
		Debug.Log (testArray [0][1]);

		array [0, 0] = 3;
		Debug.Log (array [0, 0]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
