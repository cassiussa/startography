using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {
	/*
	public ScaleStates scaleStatesScript;
	public int collisionCount = 0;				// This way we can know if we're not colliding with anything

	void Start() {
		if (GetComponent("ScaleStates") != null)
			scaleStatesScript = GetComponent<ScaleStates> ();
		if (!scaleStatesScript)
			scaleStatesScript = transform.parent.GetComponent<ScaleStates> ();
		if (!scaleStatesScript)
			Debug.LogError ("Unable to assign the ScaleStates script to the scaleStatesScript variable", gameObject);

		Debug.LogError ("gameojb = " + gameObject.name, gameObject);
	}

	void Update () {
		//Debug.LogError ("collisionCount = " + collisionCount);
		if (collisionCount == 0 && scaleStatesScript.hittingCamera == true) {
			scaleStatesScript.hittingCamera = false;
		} else if (collisionCount > 0 && scaleStatesScript.hittingCamera == false) {
			scaleStatesScript.hittingCamera = true;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "MainCamera") {
			//Debug.LogError (gameObject.name, gameObject);
			collisionCount++;
			//Debug.Log ("in collisionCount = " + collisionCount);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "MainCamera") {
			//Debug.LogError (gameObject.name, gameObject);
			collisionCount--;
			//Debug.Log ("collisionCount = " + collisionCount);
		}
	}
*/
}
