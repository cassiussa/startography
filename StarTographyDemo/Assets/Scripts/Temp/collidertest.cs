using UnityEngine;
using System.Collections;

public class collidertest : MonoBehaviour {

	public CameraSpeedStates cameraSpeedStates;
	int i = 0;
	
	void Start () {
		cameraSpeedStates = GetComponent<CameraSpeedStates> ();
	}

	void Update() {
		if (i == 0)
			i = 1;
	}

	void OnTriggerEnter(Collider other) {
		if (i == 1) {
			string tag = other.tag;
			//Debug.Log ("OnTriggerEnter " + other.gameObject.name);
			// Order is from largest to smallest as smaller colliders for these are always inside larger ones
			if (tag == "LC") {
				cameraSpeedStates.state = CameraSpeedStates.State.LC;
			} else if (tag == "LD") {
				cameraSpeedStates.state = CameraSpeedStates.State.LD;
			} else if (tag == "PA") {
				cameraSpeedStates.state = CameraSpeedStates.State.PA;
			} else if (tag == "LY") {
				cameraSpeedStates.state = CameraSpeedStates.State.LY;
			} else if (tag == "Ld") {
				cameraSpeedStates.state = CameraSpeedStates.State.Ld;
			} else if (tag == "LH") {
				cameraSpeedStates.state = CameraSpeedStates.State.LH;
			} else if (tag == "AU") {
				cameraSpeedStates.state = CameraSpeedStates.State.AU;
			} else if (tag == "MK") {
				cameraSpeedStates.state = CameraSpeedStates.State.MK;
			} else if (tag == "SM") {
				cameraSpeedStates.state = CameraSpeedStates.State.SM;
			}
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		string tag = other.tag;
		// Order is from smallest to largest as smaller colliders for these are always inside larger ones
		// So when we exit a collider it means we've entered the next fastest state
		//Debug.LogError ("exited " + other.gameObject.name, gameObject);
		if (tag == "SM") {
			cameraSpeedStates.state = CameraSpeedStates.State.MK;
		} else if (tag == "MK") {
			cameraSpeedStates.state = CameraSpeedStates.State.AU;
		} else if (tag == "AU") {
			cameraSpeedStates.state = CameraSpeedStates.State.LH;
		} else if (tag == "LH") {
			cameraSpeedStates.state = CameraSpeedStates.State.Ld;
		} else if (tag == "Ld") {
			cameraSpeedStates.state = CameraSpeedStates.State.LY;
		} else if (tag == "LY") {
			cameraSpeedStates.state = CameraSpeedStates.State.PA;
		} else if (tag == "PA") {
			cameraSpeedStates.state = CameraSpeedStates.State.LD;
		} else if (tag == "LD") {
			cameraSpeedStates.state = CameraSpeedStates.State.LC;
		} else if (tag == "LC") {
			cameraSpeedStates.state = CameraSpeedStates.State.LM;
		}
	}
}
