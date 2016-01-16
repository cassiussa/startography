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
	StarData starDataScript;
	public Vector3d position;

	float random;
	void Awake() {
		objectDataScript = GetComponent<ObjectData> ();
		starDataScript = GetComponent<StarData> ();
		if (GetComponent<ObjectData> ()) {
			position = S3dToV3d(GetComponent<ObjectData> ().coordinates);
		} else if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			Debug.Log ("My god! It's full of stars!");
			position = S3dToV3d(GetComponent<DistanceMarkerData> ().coordinates);
		} else {
			Debug.Log ("It's not an object or a star?");
			//position = GetComponent<SunLightPosition>().position;
		}

	}

	// I don't know why, but this update needs to be here or stuff breaks
	void Update () {

	}
}
