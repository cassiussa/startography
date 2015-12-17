using UnityEngine;
using System.Collections;

public class StarData : StarDataFunctions {

	public float solarMass;
	public float solarRadii;
	public float effectiveTemperature;
	public float opticalMagnitude;
	public float parsecDistance;
	public string declination;
	public string rightAscention;

	public double meters;

	// Functions are called from StarDataFunctions.cs
	void Start() {
		double lightYears = parsecToLightYear (parsecDistance);		// Parsecs to Lightyears
		Debug.Log (lightYears + " Light Years");
		double dec = dmsToDeg (declination);							// degrees, (arc)minutes, (arc)seconds
		Debug.Log (dec + " degrees declination");
		double ra = hmsToDeg (rightAscention);						// Hours, Minutes, Seconds
		Debug.Log(ra+" degrees Right Ascension");
		double mkm = jlyToMkm (lightYears);							// Julian Light Years to Millions of Kilometers
		Debug.Log (mkm + " x 1,000,000 km per Julian Light Year");
		double kms = jlyToKms (lightYears);							// Julian Light Years to Kilometers
		Debug.Log (kms + " km per Julian Light Year");

		double angularDist = getAngDis (hmsToDeg (rightAscention), dmsToDeg (declination), 15d, 100d);			// ra1 (right ascension), dec1 (declination), ra2, dec2
		Debug.Log (angularDist + " angular distance");
	}

}
