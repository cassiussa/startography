using UnityEngine;
using System.Collections;

public class PositionProcessing : Positioning {

	ObjectData objectDataScript;								// The script that we get initial telemetry from
	StarData starDataScript;
	public Vector3d position;
	public Vector3d angle;

	float random;
	void Awake() {
		objectDataScript = GetComponent<ObjectData> ();
		starDataScript = GetComponent<StarData> ();
		if (objectDataScript) {
			position = S3dToV3d(objectDataScript.coordinates);
		} else if (starDataScript) {
			Debug.Log ("My god! It's full of stars!");
			position = S3dToV3d(objectDataScript.coordinates);
		} else {
			Debug.Log ("It's not an object or a star?");
			position = S3dToV3d(objectDataScript.coordinates);
		}

		angle = new Vector3d (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		random = Random.Range (-10.0f, -5.0f);
	}

	// Update is called once per frame
	void Update () {
		//position.z = position.z+50d;
		//transform.position = V3dToV3 (position);	// Convert from Vector3d double to native Vector3 float and move the gameObject into position

		angle = new Vector3d (angle.x, angle.y + (random*Time.deltaTime), angle.z);
		transform.eulerAngles = V3dToV3(angle);
	}
}
