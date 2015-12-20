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
		//double conversion = conDis (parsecDistance, "PA", "LY");		// Parsecs to Lightyears
		double dec = dmsToDeg (declination);							// degrees, (arc)minutes, (arc)seconds
		double ra = hmsToDeg (rightAscention);							// Hours, Minutes, Seconds
		double mkm = conDist (parsecDistance, "AU", "MK");				// Convert Distance [distance, from (string), to (string)]
		double conCamClip = ConCamClip (1d, "AU", "MK");
		Debug.Log (conCamClip);
		double angularDist = getAngDis (hmsToDeg (rightAscention), dmsToDeg (declination), 15d, 100d);			// ra1 (right ascension), dec1 (declination), ra2, dec2

		// We can create a Vector made up of doubles.
		Vector3d coords = new Vector3d(conDist(1d, "MK", "AU"), conDist(0.5d, "MK", "AU"), conDist(2d, "MK", "AU"));
		//Debug.Log("Coords = ("+coords.x+", "+coords.y+", "+coords.z+")");

		// We can convert a Vector3d to Vector3
		Vector3 newVector = V3dToV3 (new Vector3d (10424212411224d, 1.234124212344421d, 20d));
		//Debug.Log (newVector);
	}

}
