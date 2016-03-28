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

		/*
		 * Examples of how we can directly access items
		 * within the multi-dimensional array in order to
		 * use them.
		 * 
		 * EXAMPLE USAGES
		 * int a = 0;
		 * int b = 1;
		 * int c = 0;
		 * 
		 * The type of a moon's "orbitalPeriod" variable is Float
		 * float something = celestialBodies.star[a].planets[b].moons[c].orbitalPeriod;
		 * 
		 * The type of a star's "rightAscension" variable is String
		 * string _ra = celestialBodies.star[a].rightAscension;
		 */
		Debug.Log (celestialBodies.star[0].rightAscension);


		for(int s=0;s<celestialBodies.star.Length;s++) {
			celestialBodies.star[s].gameObject = new GameObject();
			celestialBodies.star[s].gameObject.name = "Star_"+celestialBodies.star[s].name;
			for(int p=0;p<celestialBodies.star[s].planets.Length;p++) {
				celestialBodies.star[s].planets[p].gameObject = new GameObject();
				celestialBodies.star[s].planets[p].gameObject.name = "Planet_"+celestialBodies.star[s].planets[p].name;

				for(int m=0;m<celestialBodies.star[s].planets[p].moons.Length;m++) {
					celestialBodies.star[s].planets[p].moons[m].gameObject = new GameObject();
					celestialBodies.star[s].planets[p].moons[m].gameObject.name = "Moon_"+celestialBodies.star[s].planets[p].moons[m].name;
				}
			}
			print (celestialBodies.star[s].name);
		}

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
	public string name = null;

	[JSONItem("id",typeof(int))]
	public int id = 0;
	
	// The gameObject that we'll instantiate for this star
	public GameObject gameObject = null;
	
	[JSONItem("rightAscension", typeof(string))]
	public string rightAscension = null;
	
	[JSONItem("declination", typeof(string))]
	public string declination = null;
	
	[JSONItem("distance", typeof(float))]
	public float distance = 0f;
	
	[JSONItem("opticalMagnitude", typeof(float))]
	public float opticalMagnitude = 0f;
	
	[JSONItem("temperature", typeof(float))]
	public float temperature = 0f;
	
	[JSONItem("stellarMass", typeof(float))]
	public float stellarMass = 0f;
	
	[JSONItem("stellarRadius", typeof(float))]
	public float stellarRadius = 0f;
	
	[JSONItem("dateLastUpdate", typeof(string))]
	public string dateLastUpdate = null;

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

	// The gameObject that we'll instantiate for this planet
	public GameObject gameObject = null;
	
	[JSONItem("status", typeof(bool))]
	public bool status = true;
	
	[JSONItem("numPlanetsInSystem", typeof(int))]
	public int numPlanetsInSystem = 0;
	
	[JSONItem("orbitalPeriod", typeof(float))]
	public float orbitalPeriod = 0f;
	
	[JSONItem("semiMajorAxis", typeof(float))]
	public float semiMajorAxis = 0f;
	
	[JSONItem("eccentricity", typeof(float))]
	public float eccentricity = 0f;
	
	[JSONItem("inclination", typeof(float))]
	public float inclination = 0f;
	
	[JSONItem("planetMass", typeof(float))]
	public float planetMass = 0f;
	
	[JSONItem("planetRadius", typeof(float))]
	public float planetRadius = 0f;

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

	// The gameObject that we'll instantiate for this moon
	public GameObject gameObject = null;
	
	[JSONItem("status", typeof(bool))]
	public bool status = true;
	
	[JSONItem("numMoonsInSystem", typeof(int))]
	public int numMoonsInSystem = 0;
	
	[JSONItem("orbitalPeriod", typeof(float))]
	public float orbitalPeriod = 0f;
	
	[JSONItem("semiMajorAxis", typeof(float))]
	public float semiMajorAxis = 0f;
	
	[JSONItem("eccentricity", typeof(float))]
	public float eccentricity = 0f;
	
	[JSONItem("inclination", typeof(float))]
	public float inclination = 0f;
	
	[JSONItem("moonMass", typeof(float))]
	public float moonMass = 0f;
	
	[JSONItem("moonRadius", typeof(float))]
	public float moonRadius = 0f;
	
}