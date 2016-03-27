/*
 * This script is what takes the data from a JSON formatted text file
 * and makes sense of the data.  It uses the JsonSerializer.cs script
 * to perform the operations.
 * 
 * When referring to the flowchart in Issue #45, this script represents
 * the "Import Data" phase.
 * 
 * In general, first we build an array of stars, which contains a
 * child array of planets, which contains a child array of moons.
 * 
 * It is assumed that the JSON contents have already made sure we
 * don't have duplicates and that solar systems with multiple planets
 * already have those planets set into an array for their parent star.
 * 
 * It's likely that, for moons, we'll only need this for the Earth
 * solar system for the next number of years.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using JSON;

public class FormatImportData : MonoBehaviour {
	[SerializeField] private CelestialBodies celestialBodies = null;

	private void Start () {
		string json = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts/config.json"));
		this.celestialBodies = (CelestialBodies)JSONSerialize.Deserialize(typeof(CelestialBodies), json);
		//Debug.Log (celestialBodies);

	}

}

[System.Serializable]
public class CelestialBodies {
	/*
	 * Create an array of type Star.  This will
	 * hold onto a list of stars that will be input via
	 * text file from the StarTography website.
	 */
	[JSONArray("star", typeof(Star))]
	public Star[] star;
}

[System.Serializable]
public class Star {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name;

	[JSONItem("id",typeof(int))]
	public int id = 0;
	
	[JSONItem("rightAscension", typeof(string))]
	public string rightAscension;
	
	[JSONItem("declination", typeof(string))]
	public string declination;
	
	[JSONItem("distance", typeof(float))]
	public float distance;
	
	[JSONItem("opticalMagnitude", typeof(float))]
	public float opticalMagnitude;
	
	[JSONItem("temperature", typeof(float))]
	public float temperature;
	
	[JSONItem("stellarMass", typeof(float))]
	public float stellarMass;
	
	[JSONItem("stellarRadius", typeof(float))]
	public float stellarRadius;
	
	[JSONItem("dateLastUpdate", typeof(string))]
	public string dateLastUpdate;

	/*
	 * Create an array of type Planet.  This will
	 * hold onto a list of planets that will be input via
	 * text file for a particular parent star.
	 */
	[JSONArray("planets", typeof(Planet))]
	public Planet[] planets;
}

[System.Serializable]
public class Planet {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name = null;
	
	[JSONItem("status", typeof(bool))]
	public bool status = true;
	
	[JSONItem("numPlanetsInSystem", typeof(int))]
	public int numPlanetsInSystem;
	
	[JSONItem("orbitalPeriod", typeof(float))]
	public float orbitalPeriod;
	
	[JSONItem("semiMajorAxis", typeof(float))]
	public float semiMajorAxis;
	
	[JSONItem("eccentricity", typeof(float))]
	public float eccentricity;
	
	[JSONItem("inclination", typeof(float))]
	public float inclination;
	
	[JSONItem("planetMass", typeof(float))]
	public float planetMass;
	
	[JSONItem("planetRadius", typeof(float))]
	public float planetRadius;

	/*
	 * Create an array of type Moon.  This will
	 * hold onto a list of moons that will be input via
	 * text file for a particular parent planet.
	 */
	[JSONArray("moons", typeof(Moon))]
	public Moon[] moons;
	
}

[System.Serializable]
public class Moon {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name = null;
	
	[JSONItem("status", typeof(bool))]
	public bool status = true;
	
	[JSONItem("numMoonsInSystem", typeof(int))]
	public int numMoonsInSystem;
	
	[JSONItem("orbitalPeriod", typeof(float))]
	public float orbitalPeriod;
	
	[JSONItem("semiMajorAxis", typeof(float))]
	public float semiMajorAxis;
	
	[JSONItem("eccentricity", typeof(float))]
	public float eccentricity;
	
	[JSONItem("inclination", typeof(float))]
	public float inclination;
	
	[JSONItem("moonMass", typeof(float))]
	public float moonMass;
	
	[JSONItem("moonRadius", typeof(float))]
	public float moonRadius;
	
}