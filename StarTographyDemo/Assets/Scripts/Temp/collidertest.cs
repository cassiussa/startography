using UnityEngine;
using System.Collections;

public class collidertest : MonoBehaviour {

	public CameraSpeedStates cameraSpeedStates;
	// Use this for initialization
	void Start () {
		cameraSpeedStates = GetComponent<CameraSpeedStates> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		string tag = other.tag;
		if (tag == "Faster") {
			Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Fast;
		} else if (tag == "Fast") {
			Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.Medium;
		} else if (tag == "Slow") {
			Debug.Log ("Slow");
			cameraSpeedStates.state = CameraSpeedStates.State.Slow;
		} else if (tag == "Slower") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		} else if (tag == "Slowest") {
			Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.Slowest;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		string tag = other.tag;
		if (tag == "Slowest") {
			Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		} else if (tag == "Slower") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slow;
		} else if (tag == "Slow") {
			Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.Medium;
		} else if (tag == "Fast") {
			Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Fast;
		} else if (tag == "Faster") {
			Debug.Log ("Faster");
			cameraSpeedStates.state = CameraSpeedStates.State.Faster;
		}
	}
}
