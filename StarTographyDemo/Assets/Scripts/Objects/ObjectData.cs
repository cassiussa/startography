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

		scaleStatesScript.thisLocalScale = S3dToV3d(radius);
		scaleStatesScript.thisLocalScale.x *= 2;
		scaleStatesScript.thisLocalScale.y *= 2;
		scaleStatesScript.thisLocalScale.z *= 2;
	}
}
