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
		double lightYears = parsecToLightYear (parsecDistance);	// Parsecs to Lightyears
		Debug.Log (lightYears + " Light Years");
		dmsToDeg (declination);						// degrees, (arc)minutes, (arc)seconds
		hmsToDeg (rightAscention);					// Hours, Minutes, Seconds
		double mkm = JLYtoMKM (lightYears);
		Debug.Log (mkm + "M km");
	}

}
