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
		if (objectDataScript)
			position = new Vector3d (double.Parse (objectDataScript.coordX), double.Parse (objectDataScript.coordY), double.Parse (objectDataScript.coordZ));
		else if (starDataScript) {
			Debug.Log ("My god! It's full of stars!");
			position = new Vector3d (double.Parse (objectDataScript.coordX), double.Parse (objectDataScript.coordY), double.Parse (objectDataScript.coordZ));
		} else {
			Debug.Log ("It's not an object or a star?");
			position = new Vector3d (double.Parse (objectDataScript.coordX), double.Parse (objectDataScript.coordY), double.Parse (objectDataScript.coordZ));
		}

		angle = new Vector3d (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		random = Random.Range (-20.0f, 20.0f);
	}

	// Update is called once per frame
	void Update () {
		position.z = position.z+50d;
		//transform.position = V3dToV3 (position);	// Convert from Vector3d double to native Vector3 float and move the gameObject into position
		//angle = new Vector3d (angle.x, angle.y, angle.z);
		//transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y+2*Time.deltaTime, transform.eulerAngles.z);
		angle = new Vector3d (angle.x, angle.y + (random*Time.deltaTime), angle.z);
		transform.eulerAngles = V3dToV3(angle);
	}
}
