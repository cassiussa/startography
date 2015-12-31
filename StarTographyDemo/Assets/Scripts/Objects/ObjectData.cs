using UnityEngine;
using System.Collections;

public class ObjectData : Functions {

	ScaleStates scaleStatesScript;

	public string coordX = "0";
	public string coordY = "0";
	public string coordZ = "0";

	public double xRadius = 0d;
	public double yRadius = 0d;
	
	void Awake() {
		scaleStatesScript = GetComponent<ScaleStates> ();
		// Get the original localScale of the gameObject to use for reference later when rescaling based on State
		scaleStatesScript.originalLocalScale = new Vector3d (xRadius*2d, yRadius*2d, xRadius*2d);
	}
}
