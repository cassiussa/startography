using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elements;
using SimpleJSON;
using BodyElements;
using ImportData;
using System.IO;


public class Planets : MonoBehaviour {

	//public OrbitElement earth;
	public List<CelestialBody> celestialBodies = new List<CelestialBody>();
	public List<Star> stars = new List<Star>();
	public List<Planet> planets = new List<Planet>();
	public List<Moon> moons = new List<Moon>();
	public JSONNode importedData;
	string JSONData;    // Holds the data.json file data

	void Start ()
	{
		JSONData = Data.data.ReadToEnd (); // Read from the data.json file
		Data.data.Close ();
		
		importedData = JSON.Parse(JSONData); // Parse the data into a formatted string variable


		List<OrbitElement> _orbitElements = new List<OrbitElement>();
		List<BodyElement> _bodyElements = new List<BodyElement>();


		for (int iteratorA=0; iteratorA<importedData["star"].Count; iteratorA++) {
			//CelestialBody _celestialBody = new CelestialBody();
			Star _star = new Star ();
			_star.Mass = new Element ("m", "Stellar Mass", double.Parse(importedData ["star"] [iteratorA]["stellarMass"]), "Kg", 0.0d, "SI", "StarTography 1.0");
			_star.Radius = new Element ("m", "Stellar Radius", double.Parse(importedData ["star"] [iteratorA] ["stellarRadius"]), "Meters", 0.0d, "SI", "StarTography 1.0");
			_star.Radius.Value *= 100d; // An example of how to multiple by Stellar Radii
			_star.DateLastUpdated = importedData ["star"] [iteratorA] ["dateLastUpdate"];
			_star.Name = importedData ["star"] [iteratorA] ["name"];
			_star.RightAscension = importedData ["star"] [iteratorA] ["rightAscension"];
			_star.Declination = importedData ["star"] [iteratorA] ["declination"];
			_star.Distance = new Element("d", "Distance", double.Parse(importedData ["star"] [iteratorA] ["distance"]), "meters", 0.0d, "SI", "StarTography 1.0");
			_star.Luminosity = new Element("l", "Optical Magnitude", double.Parse(importedData ["star"] [iteratorA] ["opticalMagnitude"]), "lum", 0.0d, "SI", "StarTography 1.0");
			_star.Temperature = new Element("t", "Temperature", double.Parse(importedData ["star"] [iteratorA] ["temperature"]), "celcius", 0.0d, "SI", "StarTography 1.0");
			stars.Add (_star);

			for (int iteratorB=0; iteratorB<importedData["star"][iteratorA]["planets"].Count; iteratorB++) {
				Planet _planet = new Planet();
				_planet.Name = importedData["star"][iteratorA]["planets"][iteratorB]["name"];
				_planet.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["dateLastUpdate"];

				planets.Add(_planet);

				for (int iteratorC=0; iteratorC<importedData["star"][iteratorA]["planets"][iteratorB]["moons"].Count; iteratorC++) {
					Moon _moon = new Moon();
					_moon.Name = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["name"];
					_moon.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["dateLastUpdate"];
					
					moons.Add(_moon);
					
					
				}
			}
		}

		// Sample of iterating over the Star array of stars
		/*for (int iteratorB=0; iteratorB<importedData["star"].Count; iteratorB++) {

			OrbitElement _orbitElement = new OrbitElement();
			BodyElement _bodyElement = new BodyElement();
			CelestialBody _celestialBody = new CelestialBody();

			_orbitElement.SemiMajorAxis = new Element("a", "Semi-Major Axis", double.Parse(importedData["star"][0]["planets"][iteratorB]["semiMajorAxis"]), "fraction", 0.0d, "SI", "StarTography 1.0");
			_orbitElement.Eccentricity = new Element("e", "Eccentricity", double.Parse(importedData["star"][0]["planets"][iteratorB]["eccentricity"]), "fraction", 0.0d, "SI", "StarTography 1.0");
			_orbitElement.Inclination = new Element ("i", "Inclination", double.Parse(importedData["star"][0]["planets"][iteratorB]["inclination"]), "ratio", 0.0d, "SI", "StarTography 1.0");
			_orbitElements.Add (_orbitElement);

			// Set up the _bodyElements values here
			_bodyElements.Add (_bodyElement);

			// Add the Name, OrbitElement and BodyElement to the CelesialBodies List
			_celestialBody.Name = importedData["star"][0]["planets"][iteratorB]["name"];
			_celestialBody.OrbitElement = _orbitElement;
			_celestialBody.BodyElement = _bodyElement;
			celestialBodies.Add (_celestialBody);
		}*/

		// Enter in some example data for Earth
		/*earth.SemiMajorAxis = new Element ("a", "Semi-Major Axis", 0.1234567890123456789, "fraction", 0.0d, "SI", "StarTography 1.0");
		earth.Eccentricity = new Element ("e", "Eccentricity", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.EccentricAnomaly = new Element ("E", "Eccentric Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.Inclination = new Element ("i", "Inclination", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.Perigee = new Element ("ω", "Argument of Perigee", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.RightAscension = new Element ("Ω", "Right Ascension of the Ascending Node", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");
		earth.MeanAnomaly = new Element ("M", "Mean Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");*/
		//earth.TrueAnomaly = new Element ("v", "True Anomaly", 0.1d, "ratio", 0.0d, "SI", "StarTography 1.0");

		//Debug.Log ("Value of the Earth's Semi Major Axis is: "+earth.SemiMajorAxis.Value);
	}

}
