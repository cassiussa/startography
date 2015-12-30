using UnityEngine;
using System.Collections;

public class ObjectData : Functions {

	ScaleStates scaleStatesScript;

	public double coordX = 0d;
	public double coordY = 0d;
	public double coordZ = 0d;

	public double xRadius = 10d;
	public double yRadius = 10d;

	void Awake() {

		if (yRadius == 0d)
			yRadius = xRadius;

		scaleStatesScript = GetComponent<ScaleStates> ();
		// Get the original localScale of the gameObject to use for reference later when rescaling based on State
		scaleStatesScript.originalLocalScale = new Vector3d (xRadius*2, yRadius*2, xRadius*2);
	}
}
