using UnityEngine;
using System.Collections;

public class ObjectData : Functions {

	ScaleStates scaleStatesScript;

	public String3d coordinates;
	//public string coordX = "0";
	//public string coordY = "0";
	//public string coordZ = "0";

	public String3d radius;

	void Awake() {
		scaleStatesScript = GetComponent<ScaleStates> ();
		if (radius.x == "" || radius.y == "" || radius.z == "")
			Debug.LogError ("The Radius hasn't been set correctly for this object.", gameObject);

		scaleStatesScript.originalLocalScale = S3dToV3d(radius);
		scaleStatesScript.originalLocalScale.x *= 2;
		scaleStatesScript.originalLocalScale.y *= 2;
		scaleStatesScript.originalLocalScale.z *= 2;
	}
}
