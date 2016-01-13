using UnityEngine;
using System.Collections;

public class DistanceMarkerData : Functions {

	ScaleStates scaleStatesScript;

	public CelestialBodyType celestialBodyType;
	public String3d radius;

	void Awake() {
		if (radius.x == "" || radius.y == "" || radius.z == "")
			Debug.LogError ("The Radius hasn't been set correctly for this object.", gameObject);
	}
}
