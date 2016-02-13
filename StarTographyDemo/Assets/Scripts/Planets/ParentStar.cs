using UnityEngine;
using System.Collections;

public class ParentStar : Functions {

	public GameObject parentStar;
	public ObjectData starObjectDataScript;
	public double avgOrbitRadius;
	public double orbitCircumference;
	public double orbitTrailLength;
	public PlanetOrbitPathTrail planetOrbitPathTrailScript;

	double solarMass;
	double julianYear;

	// Use this for initialization
	void Start () {
		solarMass = 2.7d;													// Temporary - in solar mass units
		julianYear = 0.91517192982456d;										// Temporary - years in earth year units 1 year = 365.25 days
		// Calculate the average radius of the orbit of this planet.
		avgOrbitRadius = AvgOrbitRad (SolsToKilos (solarMass), JulianYearToSeconds (julianYear));
		orbitCircumference = 2d * avgOrbitRadius * PI;						// Circumference = radius X pi
		orbitTrailLength = orbitCircumference / 2d;							// The orbit trail length is half the length of the circumference
		if (orbitTrailLength != 0 && planetOrbitPathTrailScript) {
			planetOrbitPathTrailScript.segmentLength = orbitTrailLength / planetOrbitPathTrailScript.lineSegments / 1000;
		}
	}

	void Update() {

	}

}
