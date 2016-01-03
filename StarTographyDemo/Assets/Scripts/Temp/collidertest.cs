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
		if (tag == "LY") {
			//Debug.Log ("Faster");
			cameraSpeedStates.state = CameraSpeedStates.State.LY;
		} else if (tag == "Ld") {
			//Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Ld;
		} else if (tag == "LH") {
			//Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.LH;
		} else if (tag == "AU") {
			//Debug.Log ("Slow");
			cameraSpeedStates.state = CameraSpeedStates.State.AU;
		} else if (tag == "MK") {
			//Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.MK;
		} else if (tag == "SM") {
			//Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.SM;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		string tag = other.tag;
		if (tag == "SM") {
			//Debug.Log ("Slowest");
			cameraSpeedStates.state = CameraSpeedStates.State.MK;
		} else if (tag == "MK") {
			//Debug.Log ("Slower");
			cameraSpeedStates.state = CameraSpeedStates.State.AU;
		} else if (tag == "AU") {
			//Debug.Log ("Medium");
			cameraSpeedStates.state = CameraSpeedStates.State.LH;
		} else if (tag == "LH") {
			//Debug.Log ("Fast");
			cameraSpeedStates.state = CameraSpeedStates.State.Ld;
		} else if (tag == "Ld") {
			//Debug.Log ("Faster");
			cameraSpeedStates.state = CameraSpeedStates.State.LY;
		} else if (tag == "LY") {
			//Debug.Log ("Fastest");
			cameraSpeedStates.state = CameraSpeedStates.State.PA;
		}
	}
}
