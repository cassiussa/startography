using UnityEngine;
using System.Collections;

public class PositionProcessing : Positioning {

	ObjectData objectDataScript;								// The script that we get initial telemetry from
	StarData starDataScript;
	public Vector3d position;// = new Vector3d(0d,0d,0d);

	float random;
	void Start() {
		objectDataScript = GetComponent<ObjectData> ();
		starDataScript = GetComponent<StarData> ();
		if (objectDataScript)
			position = new Vector3d (objectDataScript.coordX, objectDataScript.coordY, objectDataScript.coordZ);
		else if (starDataScript) {
			Debug.Log ("My god! It's full of stars!");
			position = new Vector3d (0d, 0d, 0d);
		} else {
			Debug.Log ("It's not an object or a star?");
			position = new Vector3d (0d, 0d, 0d);
		}
		Debug.Log ("Position = "+transform.position);
		//Debug.Log ("Vector3d = (" + position.x + "," + position.y + "," + position.z + ")");
		random = Random.Range (-20.0F, 20.0F);
	}

	// Update is called once per frame
	void LateUpdate () {
		position = new Vector3d (position.x, position.y, position.z+10d);
		//transform.position = V3dToV3 (position);	// Convert from Vector3d double to native Vector3 float and move the gameObject into position
		//transform.Rotate(Vector3.up * Time.deltaTime*random, Space.World);
	}
}
