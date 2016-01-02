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

	/*void OnTriggerEnter(Collider other) {
		string name = other.gameObject.name;
		if (name == "Proximity Collider") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		} else if (name == "Label Collider") {
			Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.Slowest;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		string name = other.gameObject.name;
		if (name == "Proximity Collider") {
			Debug.Log ("Slow");
			cameraSpeedStates.state = CameraSpeedStates.State.Slow;
		} else if (name == "Label Collider") {
			Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.Slower;
		};
	}*/
}
