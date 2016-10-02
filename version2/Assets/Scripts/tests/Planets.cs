using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elements;
using SimpleJSON;
using BodyElements;
using ImportData;
using System.IO;


public class Planets : MonoBehaviour {

	public Orbit earth;
	public List<Orbit> celestialBodies = new List<Orbit>();
	public JSONNode importedData;
	public string JSONData;    // Holds the data.json file data
	// Use this for initialization
	void Start ()
	{
		//earth.OrbitalPeriod = new Element ("p", "Orbit Period", 1, "days", 0.0d, "SI", "StarTography 1.0");
		//StreamReader data = File.OpenText(fileName);
		JSONData = Data.data.ReadToEnd ();
		Data.data.Close ();
		
		importedData = JSON.Parse(JSONData);
		
		// Sample of iterating over the Star array of stars
		for (int j=0; j<importedData["star"].Count; j++) {
			double e = double.Parse(importedData["star"][0]["planets"][j]["eccentricity"]);
			double a = double.Parse(importedData["star"][0]["planets"][j]["semiMajorAxis"]);
			double i = double.Parse(importedData["star"][0]["planets"][j]["inclination"]);

			Orbit body = new Orbit();
			body.SemiMajorAxis = new Element("a", "Semi-Major Axis", a, "fraction", 0.0d, "SI", "StarTography 1.0");
			body.Eccentricity = new Element("e", "Eccentricity", e, "fraction", 0.0d, "SI", "StarTography 1.0");
			body.Inclination = new Element ("i", "Inclination", i, "ratio", 0.0d, "SI", "StarTography 1.0");
			celestialBodies.Add (body);

		}
		
		//earth.OrbitalPeriod = new Element (Data.JSONData);
		earth.SemiMajorAxis = new Element ("a", "Semi-Major Axis", 0.1234567890123456789, "fraction", 0.0d, "SI", "StarTography 1.0");
		earth.Eccentricity = new Element ("e", "Eccentricity", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.EccentricAnomaly = new Element ("E", "Eccentric Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.Inclination = new Element ("i", "Inclination", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.Perigee = new Element ("ω", "Argument of Perigee", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.RightAscension = new Element ("Ω", "Right Ascension of the Ascending Node", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.MeanAnomaly = new Element ("M", "Mean Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		//earth.TrueAnomaly = new Element ("v", "True Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");

		Debug.Log ("Value of the Earth's Semi Major Axis is: "+earth.SemiMajorAxis.Value);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
