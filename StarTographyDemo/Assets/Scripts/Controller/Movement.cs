using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
			Debug.Log ("Up Arrow pushed");
		if (Input.GetKeyDown(KeyCode.DownArrow))
			Debug.Log ("Down Arrow pushed");
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			Debug.Log ("Left Arrow pushed");
		if (Input.GetKeyDown(KeyCode.RightArrow))
			Debug.Log ("Right Arrow pushed");
	}
}
