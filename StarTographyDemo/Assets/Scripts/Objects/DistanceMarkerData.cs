using UnityEngine;
using System.Collections;

public class DistanceMarkerData : Functions {
	public enum CelestialBodyType {
		Planet,
		Star,
		UserInterface
	}
	ScaleStates scaleStatesScript;

	public CelestialBodyType celestialBodyType;

	//[HideInInspector]
	public String3d coordinates;
	public String3d radius;

	void Start() {
		if (radius.x == "" || radius.y == "" || radius.z == "")
			Debug.LogError ("The Radius hasn't been set correctly for this object.", gameObject);

		scaleStatesScript = GetComponent<ScaleStates>();
		/*scaleStatesScript.thisLocalScale = S3dToV3d(radius);
		scaleStatesScript.thisLocalScale.x *= 2;
		scaleStatesScript.thisLocalScale.y *= 2;
		scaleStatesScript.thisLocalScale.z *= 2;*/
	}
}
