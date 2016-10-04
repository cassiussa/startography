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
	//public List<object> celestialBodies = new List<object>();
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

		for (int iteratorA=0; iteratorA<importedData["star"].Count; iteratorA++) {
			Star _star = new Star ();
			_star.Mass = new Element ("m", "Stellar Mass", double.Parse(importedData ["star"] [iteratorA]["stellarMass"]), "Kg", 0.0d, "SI", "StarTography 1.0");
			_star.Radius = new Element ("m", "Stellar Radius", double.Parse(importedData ["star"] [iteratorA] ["stellarRadius"]), "meter", 0.0d, "SI", "StarTography 1.0");
			_star.Radius.Value *= 100d; // An example of how to multiply by Stellar Radii
			_star.DateLastUpdated = importedData ["star"] [iteratorA] ["dateLastUpdate"];
			_star.Name = importedData ["star"] [iteratorA] ["name"];
			_star.RightAscension = importedData ["star"] [iteratorA] ["rightAscension"];
			_star.Declination = importedData ["star"] [iteratorA] ["declination"];
			_star.Distance = new Element("d", "Distance", double.Parse(importedData ["star"] [iteratorA] ["distance"]), "meter", 0.0d, "SI", "StarTography 1.0");
			_star.Distance.ToMM();
			_star.Luminosity = new Element("l", "Optical Magnitude", double.Parse(importedData ["star"] [iteratorA] ["opticalMagnitude"]), "lum", 0.0d, "SI", "StarTography 1.0");
			_star.Temperature = new Element("t", "Temperature", double.Parse(importedData ["star"] [iteratorA] ["temperature"]), "celcius", 0.0d, "SI", "StarTography 1.0");

			stars.Add (_star);

			for (int iteratorB=0; iteratorB<importedData["star"][iteratorA]["planets"].Count; iteratorB++) {
				Planet _planet = new Planet();
				_planet.Name = importedData["star"][iteratorA]["planets"][iteratorB]["name"];
				_planet.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["dateLastUpdate"];

				planets.Add(_planet);

				for (int iteratorC=0; iteratorC<importedData
				     ["star"][iteratorA]
				     ["planets"][iteratorB]
				     ["moons"].Count; iteratorC++) {
					Moon _moon = new Moon();
					_moon.Name = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["name"];
					_moon.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["dateLastUpdate"];
					
					moons.Add(_moon);
				}
			}
		}

	}

}
