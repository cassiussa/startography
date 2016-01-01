using UnityEngine;
using System.Collections;

public class ObjectData : Functions {

	ScaleStates scaleStatesScript;

	public string coordX = "0";
	public string coordY = "0";
	public string coordZ = "0";

	double xRad = 0d;
	public string xRadius = "";
	double yRad = 0d;
	public string yRadius = "";

	void Awake() {
		scaleStatesScript = GetComponent<ScaleStates> ();
		if (xRadius == "" || yRadius == "")
			Debug.LogError ("The X or Y Radius hasn't been set for this object.", gameObject);
		xRad = double.Parse (xRadius);
		yRad = double.Parse (yRadius);
		// Get the original localScale of the gameObject to use for reference later when rescaling based on State
		scaleStatesScript.originalLocalScale = new Vector3d (xRad*2d, yRad*2d, xRad*2d);
	}
}
