using UnityEngine;
using System.Collections;

public class ParentStar : Functions {

	//public GameObject parentStarObject;																// Assigned by the ScaleStates.cs script
	[HideInInspector]
	public ObjectData planetObjectDataScript;															// Assigned by the ScaleStates.cs script
	[HideInInspector]
	public ObjectData starObjectDataScript;																// Assigned by the ScaleStates.cs script
	public double avgOrbitRadius;
	public double orbitCircumference = 0d;
	public double orbitTrailLength = 0d;
	[HideInInspector]
	public PlanetOrbitPathTrail planetOrbitPathTrailScript;

	[HideInInspector]
	public double solarMass;
	[HideInInspector]
	public double orbitalPeriod;

	// Use this for initialization
	void Start () {
			//solarMass = starObjectDataScript.mass;															// Get the mass of the parent star from the parent star's ObjectData.cs script
		//orbitalPeriod = planetObjectDataScript.orbitalPeriod;													// Temporary - years in earth year units 1 year = 365.25 days
		if (solarMass != 0 && orbitalPeriod != 0) {
			// Calculate the average radius of the orbit of this planet, in kilometers
			avgOrbitRadius = AvgOrbitRad (SolsToKilos (solarMass), JulianYearToSeconds (orbitalPeriod)) / 1000;	// In Kilometers
			orbitCircumference = (2d * avgOrbitRadius * PI);												// (Kilometers) Circumference = radius X pi
			orbitTrailLength = orbitCircumference / 2d;														// The orbit trail length is half the length of the circumference
			planetOrbitPathTrailScript.segmentLength = orbitTrailLength / planetOrbitPathTrailScript.lineSegments;
		} else {
			Debug.LogError ("Somehow we got a 0 division. solarMass: "+solarMass+", orbitalPeriod: "+orbitalPeriod+" "+gameObject.name,gameObject);
			return;
		}
	}

	void Update() {

	}

}
