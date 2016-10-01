using UnityEngine;
using System.Collections;
using Constants;
using OrbitElements;


public class Planets : MonoBehaviour {

	public Orbit earth = new Orbit(
		new Constant ("a", "Semi-Major Axis", 0.1234567890123456789d, "fraction", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("e", "Eccentricity", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("E", "Eccentric Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("i", "Inclination", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("ω", "Argument of Perigee", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("Ω", "Right Ascension of the Ascending Node", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("M", "Mean Anomaly, Angle Right Now", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0"),
		new Constant ("v", "True Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0")
	);

	// Use this for initialization
	void Start () {
		Debug.LogError ("Value of the Earth's Semi Major Axis is: "+earth.SemiMajorAxis.Value);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
