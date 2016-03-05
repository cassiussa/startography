//using UnityEditor;
using UnityEngine;
using System.Collections;

public class ObjectData : Functions {
	public enum CelestialBodyType {
		Planet,
		Star,
		StarLight,
		SolarSystemSphere,
		UserInterface,
		DistanceMarker
	}
	ScaleStates scaleStatesScript;
	StarLightScaleStates starLightScaleStatesScript;

	public CelestialBodyType celestialBodyType;
	public String3d coordinates;
	public String3d radius;
	public float tilt;										// Angle of tilt in degrees
	public float mass;										// Measured in Jupiter Units
	public float orbitalPeriod;								// Measured in earth days
	public float parentStarMass;							// Measured in Solar Units
	public float temperature;
	public float luminosity;		// This shouldn' be visible.  Used to set amount of glow on a star
	public GameObject parentStarObject;
	public GameObject solarSystemSphere;

	void Awake() {
		Vector3d vRadius = S3dToV3d (radius);

		if (radius.x == "" || radius.y == "" || radius.z == "") Debug.LogError ("The Radius hasn't been set correctly for this object.", gameObject);

		scaleStatesScript = GetComponent<ScaleStates> ();
		starLightScaleStatesScript = GetComponent<StarLightScaleStates> ();
		if (celestialBodyType == CelestialBodyType.Star) {
			if(temperature <= 0) Debug.LogError ("Invalid temperature has been assigned to the star.",gameObject);
			gameObject.AddComponent<StarLightObjectBuilder> ();				// Take this out???

			if((vRadius.x+vRadius.y+vRadius.z)/3 < 1000) {					// This is likely in Solar radii, not actual size
				vRadius = new Vector3d(vRadius.x*radiusConstantSolar, vRadius.y*radiusConstantSolar, vRadius.z*radiusConstantSolar);
				radius = V3dToS3d(vRadius);									// Convert it back into the string variables
			}
			if(temperature < 1000) {										// This is likely in Solar temperatures, not actual size
				temperature = temperature * (float)radiusTemperatureSolar;
			}

			luminosity = Mathf.Pow(RadToSunRad((float)vRadius.x),2) * Mathf.Pow(TempToSunTemp(temperature),4);
		} else if (celestialBodyType == CelestialBodyType.Planet) {
			ObjectData objectDataParentStarScript = parentStarObject.GetComponent<ObjectData>();
			parentStarMass = objectDataParentStarScript.mass;
		}


		if (scaleStatesScript) {
			scaleStatesScript.thisLocalScale = vRadius;
			scaleStatesScript.thisLocalScale.x *= 2;
			scaleStatesScript.thisLocalScale.y *= 2;
			scaleStatesScript.thisLocalScale.z *= 2;
		} else if (starLightScaleStatesScript) {
			starLightScaleStatesScript.thisLocalScale = vRadius;
			starLightScaleStatesScript.thisLocalScale.x *= 2;
			starLightScaleStatesScript.thisLocalScale.y *= 2;
			starLightScaleStatesScript.thisLocalScale.z *= 2;
		}

	}
}
