using UnityEngine;
using System.Collections;

public class PositionProcessing : Positioning {
	/*
	 * This script deals with the position changes of the planets based
	 * on their orbits.  It does not deal with the user position at all.
	 * If you need to make changes to that, do it in the Positioning.ca
	 * script.
	 */

	ObjectData objectDataScript;								// The script that we get initial telemetry from
	public Vector3d position = new Vector3d(0d,0d,0d);			// We need to be sure to assign a value as multiple external scripts rely on it

	float random;
	void Awake() {
		objectDataScript = GetComponent<ObjectData> ();
		if (GetComponent<ObjectData> ()) {
			position = S3dToV3d (GetComponent<ObjectData> ().coordinates);
		}
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			//Debug.Log ("My god! It's full of stars!");
		} else {
			//Debug.Log ("It's not an object or a star?");
		}
	}

	// I don't know why, but this update needs to be here or stuff breaks
	void Update () {
		
	}
}
