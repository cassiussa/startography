using UnityEngine;
using System.Collections;

public class StarData : Functions {

	public float solarMass;
	public float solarRadii;
	public float effectiveTemperature;
	public float opticalMagnitude;
	public float parsecDistance;
	public string declination;
	public string rightAscention;

	/*
	
	public ScaleStates.State state;
	ScaleStates.State _cacheState;
	ScaleStates scaleStatesScript;

	//public double meters;

	// Functions are called from StarDataFunctions.cs

	*/
	void Start() {
		/*
		
		scaleStatesScript = GetComponent<ScaleStates>();
		if (!scaleStatesScript)
			Debug.LogError ("The ScaleStates script is missing", gameObject);

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

		*/

	}

	void Update() {
		// Need to still figure out that the scale is being calculated correctly.  /100 is likely not correct
		// Earth r=6,371 km
		// Sun r=695,508 km
		
		// This needs to determine what state we're in so that it can do the correct calculation
		// The /100 right now represents the total distance in the 1Mkm scale divided by the 10,000 units
		// So 1,000,000/10,000 = 100 for Million Kilometers
		// 149597870.7 / 10000 = 14959.78707 for Astronomical Units
		/*

		if (scaleStatesScript.state != _cacheState) {
			double fraction = 1d;
			if (scaleStatesScript.state == ScaleStates.State.MillionKilometers)
				fraction = MK / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.AstronomicalUnit)
				fraction = AU / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightHour)
				fraction = LH / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightDay)
				fraction = Ld / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightYear)
				fraction = LY / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.Parsec)
				fraction = PA / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightDecade)
				fraction = LD / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightCentury)
				fraction = LC / 10000d;
			else if (scaleStatesScript.state == ScaleStates.State.LightMillenium)
				fraction = LM / 10000d;
			else Debug.LogError("State has changed but couldn't determine to which one, so scale couldn't be adjusted",gameObject);
			transform.localScale = new Vector3 ((float)solarRadii * ((float)radiusConstantSolar * 2) / (float)fraction,
		                                   (float)solarRadii * ((float)radiusConstantSolar * 2) / (float)fraction,
		                                   (float)solarRadii * ((float)radiusConstantSolar * 2) / (float)fraction);

			_cacheState = scaleStatesScript.state;
		}

		 */
	}

}
