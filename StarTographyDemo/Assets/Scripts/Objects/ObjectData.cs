using UnityEngine;
using System.Collections;




public class ObjectData : Functions {
	public enum CelestialBodyType {
		Planet,
		Star,
		StarLight,
		UserInterface
	}
	ScaleStates scaleStatesScript;
	StarLightScaleStates starLightScaleStatesScript;

	public CelestialBodyType celestialBodyType;

	public String3d coordinates;
	//public string coordX = "0";
	//public string coordY = "0";
	//public string coordZ = "0";

	public String3d radius;

	void Awake() {
		scaleStatesScript = GetComponent<ScaleStates> ();
		starLightScaleStatesScript = GetComponent<StarLightScaleStates> ();
		if (radius.x == "" || radius.y == "" || radius.z == "")
			Debug.LogError ("The Radius hasn't been set correctly for this object.", gameObject);

		if (scaleStatesScript) {
			scaleStatesScript.thisLocalScale = S3dToV3d (radius);
			scaleStatesScript.thisLocalScale.x *= 2;
			scaleStatesScript.thisLocalScale.y *= 2;
			scaleStatesScript.thisLocalScale.z *= 2;
		} else if (starLightScaleStatesScript) {
			starLightScaleStatesScript.thisLocalScale = S3dToV3d (radius);
			starLightScaleStatesScript.thisLocalScale.x *= 2;
			starLightScaleStatesScript.thisLocalScale.y *= 2;
			starLightScaleStatesScript.thisLocalScale.z *= 2;
		}
		if (celestialBodyType == CelestialBodyType.Star)
			gameObject.AddComponent<StarLightObjectBuilder> ();
	}
}
