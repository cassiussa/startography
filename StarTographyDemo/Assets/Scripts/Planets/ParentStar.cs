using UnityEngine;
using System.Collections;

public class ParentStar : Functions {

	public GameObject parentStar;
	public ObjectData starObjectDataScript;
	public double solarMass;
	public double julianYear;
	public double avgOrbitRadius;

	// Use this for initialization
	void Awake () {
		//Debug.Log ("parent star = "+parentStar);
		//Debug.LogError ("parent star = "+parentStar,gameObject);
		//starObjectDataScript = parentStar.GetComponent<ObjectData> ();
		solarMass = 2.7d;													// Temporary
		julianYear = 326.03d;
		avgOrbitRadius = AvgOrbitRad (SolsToKilos (solarMass), JulianYearToSeconds (julianYear));
	}

	void Update() {

	}

}
