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
using Functions;

public class FormatImportData : MonoBehaviour {
	[SerializeField] private CelestialBodies celestialBodies = null;

	private void Awake () {
		if(gameObject.name == "[STAR] Sun [PLANET] Mercury")
			Debug.LogError ("FormatImportData.cs Start()");
		string json = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts/config.json"));
		this.celestialBodies = (CelestialBodies)JSONSerialize.Deserialize(typeof(CelestialBodies), json);

		/*
		 * build the camera
		 */
		gameObject.AddComponent<BuildUniverse> ();
		gameObject.AddComponent<BuildGalaxy> ();
		gameObject.AddComponent<BuildCamera> ();
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


		/*
		 * Iterate through all the Stars in the JSON file and create their
		 * gameObjects, add their components, etc
		 *
		 * sIndex : Star Index
		 */
		for(int sIndex=0;sIndex<celestialBodies.star.Length;sIndex++) {
			/* 
			 * Add a new GameObject to represent this specific star.  We can then go
			 * ahead and add mesh, scripts, variables and other items to it as needed
			 */
			celestialBodies.star[sIndex].gameObject = new GameObject();
			celestialBodies.star[sIndex].gameObject.SetActive (false);
			celestialBodies.star[sIndex].gameObject.name = "[STAR] "+celestialBodies.star[sIndex].name;

			/* 
			 * Add the CelestialBodyBuilder.cs script as it will take over on building the objects.
			 * We'll need to tell the CelestialBodyBuilder.cs script what type of body this is first
			 */
			celestialBodies.star[sIndex].CelestialBodyBuilder = celestialBodies.star[sIndex].gameObject.AddComponent<CelestialBodyBuilder>();
			//celestialBodies.star[sIndex].CelestialBodyBuilder.enabled = false;
			celestialBodies.star[sIndex].CelestialBodyBuilder.celestialBodyType = CelestialBodyBuilder.CelestialBodyType.Star;

			// Send the variable data to each star's CelestialBodyBuilder.cs script
			celestialBodies.star[sIndex].CelestialBodyBuilder.bodyName = celestialBodies.star[sIndex].name;
			celestialBodies.star[sIndex].CelestialBodyBuilder.rightAscension = celestialBodies.star[sIndex].rightAscension;
			celestialBodies.star[sIndex].CelestialBodyBuilder.declination = celestialBodies.star[sIndex].declination;
			celestialBodies.star[sIndex].CelestialBodyBuilder.distance = celestialBodies.star[sIndex].distance;
			celestialBodies.star[sIndex].CelestialBodyBuilder.opticalMagnitude = celestialBodies.star[sIndex].opticalMagnitude;
			celestialBodies.star[sIndex].CelestialBodyBuilder.temperature = celestialBodies.star[sIndex].temperature;
			celestialBodies.star[sIndex].CelestialBodyBuilder.stellarMass = celestialBodies.star[sIndex].stellarMass;
			celestialBodies.star[sIndex].CelestialBodyBuilder.stellarRadius = celestialBodies.star[sIndex].stellarRadius;
			celestialBodies.star[sIndex].CelestialBodyBuilder.dateLastUpdate = celestialBodies.star[sIndex].dateLastUpdate;

			// Get the number of planets orbiting this star
			celestialBodies.star[sIndex].CelestialBodyBuilder.planets = new GameObject[celestialBodies.star[sIndex].planets.Length];
			// Iterate over this star's planets so that we can count the total moons for the star
			int starMoonCount = 0;
			// Iterate through each child planet of this star
			for(int pInd=0;pInd<celestialBodies.star[sIndex].planets.Length;pInd++) {
				// Add the length of the 'moons' array to the starMoonCount variable
				starMoonCount += celestialBodies.star[sIndex].planets[pInd].moons.Length;
				/* 
				 * Create a new array with the length of starMoonCount which adds 
				 * all moons for each planet iteration so we get the total moons around 
				 * all planets in this star system.
				 */
				celestialBodies.star[sIndex].CelestialBodyBuilder.moons = new GameObject[starMoonCount];
			}
			// Reset the starMoonCount variable
			starMoonCount = 0;


			/*
			 * Iterate through all the Planets in the JSON file and create their
			 * gameObjects, add their components, etc
			 *
			 * pIndex : Planet Index
			 */
			for(int pIndex=0;pIndex<celestialBodies.star[sIndex].planets.Length;pIndex++) {
				celestialBodies.star[sIndex].planets[pIndex].gameObject = new GameObject();
				celestialBodies.star[sIndex].planets[pIndex].gameObject.SetActive (false);
				celestialBodies.star[sIndex].planets[pIndex].gameObject.name = "[STAR] "+celestialBodies.star[sIndex].name+" [PLANET] "+celestialBodies.star[sIndex].planets[pIndex].name;

				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder = celestialBodies.star[sIndex].planets[pIndex].gameObject.AddComponent<CelestialBodyBuilder>();
				//celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.enabled = false;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.celestialBodyType = CelestialBodyBuilder.CelestialBodyType.Planet;

				// Send the variable data to each planets's CelestialBodyBuilder.cs scripts
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.bodyName = celestialBodies.star[sIndex].planets[pIndex].name;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.numPlanetsInSystem = celestialBodies.star[sIndex].planets[pIndex].numPlanetsInSystem;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.orbitalPeriod = celestialBodies.star[sIndex].planets[pIndex].orbitalPeriod;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.semiMajorAxis = celestialBodies.star[sIndex].planets[pIndex].semiMajorAxis;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.eccentricity = celestialBodies.star[sIndex].planets[pIndex].eccentricity;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.inclination = celestialBodies.star[sIndex].planets[pIndex].inclination;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.planetMass = celestialBodies.star[sIndex].planets[pIndex].planetMass;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.planetRadius = celestialBodies.star[sIndex].planets[pIndex].planetRadius;

				// Assign the parent star into the 'star' variable of this planet
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.star = celestialBodies.star[sIndex].gameObject;

				// Add this planet to the 'planets' array within the CelestrialBodyBuilder.cs script attached to this planet's parent star
				celestialBodies.star[sIndex].CelestialBodyBuilder.planets[pIndex] = celestialBodies.star[sIndex].planets[pIndex].gameObject;

				int planetMoonCount = celestialBodies.star[sIndex].planets[pIndex].moons.Length;
				celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.moons = new GameObject[planetMoonCount];

				/*
				 * Iterate through all the Moons in the JSON file and create their
				 * gameObjects, add their components, etc
				 *
				 * mIndex : Moon Index
				 */
				for(int mIndex=0;mIndex<celestialBodies.star[sIndex].planets[pIndex].moons.Length;mIndex++) {
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject = new GameObject();
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject.SetActive (false);
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject.name = "[STAR] "+celestialBodies.star[sIndex].name+" [PLANET] "+celestialBodies.star[sIndex].planets[pIndex].name+" [MOON] "+celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].name;

					/*
					 * Explanation:
					 * celestial bodies > this star > this star, this planet > this star, this planet, this moon's CelestialBodyBuilder.cs script
					 */
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject.AddComponent<CelestialBodyBuilder>();
					//celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.enabled = false;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.celestialBodyType = CelestialBodyBuilder.CelestialBodyType.Moon;

					// Send the variable data to each moon's CelestialBodyBuilder.cs scripts
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.bodyName = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].name;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.numMoonsInSystem = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].numMoonsInSystem;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.orbitalPeriod = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].orbitalPeriod;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.semiMajorAxis = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].semiMajorAxis;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.eccentricity = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].eccentricity;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.inclination = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].inclination;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.moonMass = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].moonMass;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.moonRadius = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].moonRadius;

					// Assign the parent star into the 'star' variable of this moon
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.star = celestialBodies.star[sIndex].gameObject;
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].CelestialBodyBuilder.planet = celestialBodies.star[sIndex].planets[pIndex].gameObject;

					celestialBodies.star[sIndex].CelestialBodyBuilder.moons[starMoonCount] = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject;
					starMoonCount++;

					// Assign this moon into the parent star's 'moons' array.
					celestialBodies.star[sIndex].planets[pIndex].CelestialBodyBuilder.moons[mIndex] = celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject;

					// Temporary until I figure out how to get the gameObjects instantiating on enum selection
					celestialBodies.star[sIndex].planets[pIndex].moons[mIndex].gameObject.SetActive (true);
				}

				// Temporary until I figure out how to get the gameObjects instantiating on enum selection
				celestialBodies.star[sIndex].planets[pIndex].gameObject.SetActive (true);
			}


			/* 
			 * Add all the planets and moons of this system into an array attached to the parent Star.
			 * This will be sent to the camera when the current Star system is the active (closest) one
			 * so that the camera can then iterate over each item, one per Update() to see which is the
			 * closest to the sun.  Calculating their distance is expensive, so we want to streamline
			 * the process
			 */
			// Get the size of the array based on the number of Planets plus the number of Moons in this star system
			int bodyArraySize = celestialBodies.star[sIndex].CelestialBodyBuilder.planets.Length + celestialBodies.star[sIndex].CelestialBodyBuilder.moons.Length;
			int bodyAndStarArraySize = celestialBodies.star.Length + celestialBodies.star[sIndex].CelestialBodyBuilder.planets.Length + celestialBodies.star[sIndex].CelestialBodyBuilder.moons.Length;
			//Debug.LogError ("bodyArraySize = "+bodyArraySize);
			//Debug.LogError ("bodyAndStarArraySize = "+bodyAndStarArraySize);
			// Create the array at the appropriate index size.
			// We will use these literal variables later from other scripts
			celestialBodies.star[sIndex].CelestialBodyBuilder.bodies = new GameObject[bodyArraySize];
			celestialBodies.star[sIndex].CelestialBodyBuilder.celestialBodyBuilderScripts = new CelestialBodyBuilder[bodyArraySize];
			celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts = new Position[bodyArraySize];
			celestialBodies.star[sIndex].CelestialBodyBuilder.scaleStatesScripts = new ScaleStates[bodyArraySize+sIndex];
			celestialBodies.star[sIndex].CelestialBodyBuilder.realPositions = new Vector3d[bodyArraySize];
			celestialBodies.star[sIndex].CelestialBodyBuilder.relativePositions = new Vector3d[bodyArraySize];


			// Iterate over each of the planets and add them individually into the new array
			for(int planetIndex = 0; planetIndex < celestialBodies.star[sIndex].CelestialBodyBuilder.planets.Length; planetIndex++) {
				celestialBodies.star[sIndex].CelestialBodyBuilder.bodies[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.planets[planetIndex];

				celestialBodies.star[sIndex].CelestialBodyBuilder.celestialBodyBuilderScripts[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.planets[planetIndex].GetComponent<CelestialBodyBuilder>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.planets[planetIndex].AddComponent<Position>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.scaleStatesScripts[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.planets[planetIndex].AddComponent<ScaleStates>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.realPositions[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndex].realPosition;

				celestialBodies.star[sIndex].CelestialBodyBuilder.relativePositions[planetIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndex].relativePosition;
			}

			// Iterate over each of the moons and add them individually into the new array
			int planetIndexSize = celestialBodies.star[sIndex].CelestialBodyBuilder.planets.Length;
			for(int moonIndex = 0; moonIndex < celestialBodies.star[sIndex].CelestialBodyBuilder.moons.Length; moonIndex++) {

				celestialBodies.star[sIndex].CelestialBodyBuilder.bodies[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.moons[moonIndex];

				celestialBodies.star[sIndex].CelestialBodyBuilder.celestialBodyBuilderScripts[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.moons[moonIndex].gameObject.GetComponent<CelestialBodyBuilder>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.moons[moonIndex].gameObject.AddComponent<Position>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.scaleStatesScripts[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.moons[moonIndex].gameObject.AddComponent<ScaleStates>();

				celestialBodies.star[sIndex].CelestialBodyBuilder.realPositions[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndexSize+moonIndex].realPosition;

				celestialBodies.star[sIndex].CelestialBodyBuilder.relativePositions[planetIndexSize+moonIndex] = 
					celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[planetIndexSize+moonIndex].relativePosition;
			}


			// Keep this here until I figure out how to make the literal connections between the
			// DistanceArray.cs's value for realPosition and the Position.cs value for realPosition
			// celestialBodies.star[sIndex].CelestialBodyBuilder.positionScripts[sIndex] = celestialBodies.star[sIndex].gameObject.AddComponent<Position>();
			// Temporary until I figure out how to get the gameObjects instantiating on enum selection
			celestialBodies.star[sIndex].gameObject.SetActive (true);
		}

		gameObject.AddComponent<BodyDistanceToCam>();
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

	// The gameObject that we'll instantiate for this star
	public GameObject gameObject = null;
	public CelestialBodyBuilder CelestialBodyBuilder = null;
}

[System.Serializable]
public class Planet {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name = null;

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

	// The gameObject that we'll instantiate for this planet
	public GameObject gameObject = null;
	public CelestialBodyBuilder CelestialBodyBuilder = null;
	
}

[System.Serializable]
public class Moon {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name = null;

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

	// The gameObject that we'll instantiate for this moon
	public GameObject gameObject = null;
	public CelestialBodyBuilder CelestialBodyBuilder = null;
}
