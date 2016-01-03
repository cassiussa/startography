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
		string name = other.gameObject.name;
		if (name == "Faster") {
			Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Fast;
		} else if (name == "Fast") {
			Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.Medium;
		} else if (name == "Slow") {
			Debug.Log ("Slow");
			cameraSpeedStates.state = CameraSpeedStates.State.Slow;
		} else if (name == "Slower") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		} else if (name == "Slowest") {
			Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.Slowest;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		string name = other.gameObject.name;
		if (name == "Slowest") {
			Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		} else if (name == "Slower") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slow;
		} else if (name == "Slow") {
			Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.Medium;
		} else if (name == "Fast") {
			Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Fast;
		} else if (name == "Faster") {
			Debug.Log ("Faster");
			cameraSpeedStates.state = CameraSpeedStates.State.Faster;
		}
	}
}
