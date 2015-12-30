using UnityEngine;
using System.Collections;

public class ObjectData : Functions {

	ScaleStates scaleStatesScript;

	public string coordX = "10";
	public string coordY = "10";
	public string coordZ = "10";

	public string xRadius = "10";
	public string yRadius = "10";

	[HideInInspector]
	public double x = 10d;
	[HideInInspector]
	public double y = 10d;
	[HideInInspector]
	public double z = 10d;
	void Awake() {

		x = double.Parse (coordX);
		y = double.Parse (coordY);
		z = double.Parse (coordZ);


		if (yRadius != "")
			yRadius = xRadius;

		scaleStatesScript = GetComponent<ScaleStates> ();
		// Get the original localScale of the gameObject to use for reference later when rescaling based on State
		scaleStatesScript.originalLocalScale = new Vector3d (double.Parse(xRadius)*2, double.Parse(yRadius)*2, double.Parse(xRadius)*2);
	}
}
