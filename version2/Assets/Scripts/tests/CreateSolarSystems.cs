using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elements;
using SimpleJSON;
using BodyElements;
using ImportData;
using System.IO;
using CustomMath;


[System.Serializable]
public class StarList {
	public string name;
	public Star key;
	public GameObject value;
	public Vector3d positionInSpace;
}

[System.Serializable]
public class PlanetList {
	public string name;
	public Planet key;
	public GameObject value;
	
}

[System.Serializable]
public class MoonList {
	public string name;
	public Moon key;
	public GameObject value;
	
}

public class CreateSolarSystems : MonoBehaviour {
	
	public List<StarList> stars = new List<StarList>();
	public List<PlanetList> planets = new List<PlanetList>();
	public List<MoonList> moons = new List<MoonList>();

	public JSONNode importedData;
	string JSONData;    // Holds the data.json file data

	void Awake () {
		JSONData = Data.data.ReadToEnd (); // Read from the data.json file
		Data.data.Close ();
		
		importedData = JSON.Parse(JSONData); // Parse the data into a formatted string variable

		// Generate Stars
		for (int iteratorA=0; iteratorA < importedData["star"].Count; iteratorA++) {
			Star _star = new Star ();
			_star.Mass             = new Element ("Stellar Mass", double.Parse(importedData ["star"] [iteratorA]["stellarMass"]), "Kg", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Radius           = new Element ("Stellar Radius", double.Parse(importedData ["star"] [iteratorA] ["stellarRadius"]), "meter", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Radius.Value    *= 100d; // An example of how to multiply by Stellar Radii
			_star.DateLastUpdated  = importedData ["star"] [iteratorA] ["dateLastUpdate"];
			_star.Name             = importedData ["star"] [iteratorA] ["name"];
			_star.RightAscension   = importedData ["star"] [iteratorA] ["rightAscension"];
			_star.Declination      = importedData ["star"] [iteratorA] ["declination"];
			_star.Distance         = new Element("Distance", double.Parse(importedData ["star"] [iteratorA] ["distance"]), "meter", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Luminosity       = new Element("Optical Magnitude", double.Parse(importedData ["star"] [iteratorA] ["opticalMagnitude"]), "lum", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Temperature      = new Element("Temperature", double.Parse(importedData ["star"] [iteratorA] ["temperature"]), "celcius", 0.0d, "SI", "StarTography 1.0", "2016-10-10");

			_star.Distance.ToM();

			// Create the encompassing solar system object
			RightAscension rightAscension  = new RightAscension(_star.RightAscension);           // Get the string Right Ascension value of this star
			Declination declination        = new Declination(_star.Declination);                 // Get the string Declination value of this star
			StarList _stars_               = new StarList();  // List of type Star
			_stars_.positionInSpace        = Maths.SphericalToCartesianCoords(_star.Distance.Value, rightAscension, declination);  // Verified correct results - Get the cartesian coordinates
			_stars_.name                   = _star.Name;
			_stars_.key                    = _star;
			GameObject _starSystem         = new GameObject("Solar System: "+_star.Name);  // Create a new GameObject to encompass this solar system
			_stars_.value                  = _starSystem;
			stars.Add (_stars_); // Add to the List of type Star

			// Create the star object
			GameObject _starGameObject              = new GameObject("Star: "+_star.Name);
			_starGameObject.transform.parent        = _starSystem.transform;
			_starGameObject.transform.localPosition = new Vector3(0,0,0);   // TODO: Temporary

			// Generate Planets
			for (int iteratorB=0; iteratorB < importedData["star"][iteratorA]["planets"].Count; iteratorB++) {
				Planet _planet          = new Planet();
				_planet.Name            = importedData["star"][iteratorA]["planets"][iteratorB]["name"];
				_planet.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["dateLastUpdate"];

				GameObject _planetSystem           = new GameObject("Planetary System: "+_planet.Name);
				_star.ChildPlanets.Add (_planetSystem);
				GameObject _planetGameObject       = new GameObject("Planet: "+_planet.Name);
				_planetGameObject.transform.parent = _planetSystem.transform; // TODO: Move this into the Start() loop for StarList
				_planet.ParentStar                 = _starSystem;

				PlanetList _planets_ = new PlanetList();
				_planets_.name       = _planet.Name;
				_planets_.key        = _planet;
				_planets_.value      = _planetSystem;
				planets.Add (_planets_);

				for (int iteratorC=0; iteratorC < importedData["star"][iteratorA]["planets"][iteratorB]["moons"].Count; iteratorC++) {
					Moon _moon            = new Moon();
					_moon.Name            = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["name"];
					_moon.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["dateLastUpdate"];

					GameObject _moonSystem           = new GameObject("Moon System: "+_moon.Name);
					_planet.ChildMoons.Add(_moonSystem);
					GameObject _moonGameObject       = new GameObject("Moon: "+_moon.Name);
					_moonGameObject.transform.parent = _moonSystem.transform;
					_moon.ParentPlanet               = _planetSystem;

					MoonList _moons_ = new MoonList();
					_moons_.name     = _moon.Name;
					_moons_.key      = _moon;
					_moons_.value    = _moonSystem;
					moons.Add (_moons_);
				}
			}
		}

	}

	void Start() {
		foreach(StarList star in stars) {
			//GameObject _system = new GameObject("System: "+star.Name);
			//GameObject _star = new GameObject("Star: "+star.Name);
			//_star.transform.parent = _system.transform;
			//star.value.transform.parent    = star.value.
			star.value.transform.position  = (Vector3)star.positionInSpace;
			MeshFilter meshFilter          = star.value.AddComponent<MeshFilter>();
			SphereCollider sphereCollider  = star.value.AddComponent<SphereCollider>();
			sphereCollider.isTrigger       = false;
			sphereCollider.radius          = 1f;
			MeshRenderer meshRenderer      = star.value.AddComponent<MeshRenderer>();
		}

		foreach (PlanetList planet in planets) {
			planet.value.transform.parent   = planet.key.ParentStar.transform;
			planet.value.transform.position = planet.key.ParentStar.transform.position;
			MeshFilter meshFilter           = planet.value.AddComponent<MeshFilter>();
			SphereCollider sphereCollider   = planet.value.AddComponent<SphereCollider>();
			sphereCollider.isTrigger        = false;
			sphereCollider.radius           = 1f;
			MeshRenderer meshRenderer       = planet.value.AddComponent<MeshRenderer>();
		}

		foreach (MoonList moon in moons) {
			moon.value.transform.parent     = moon.key.ParentPlanet.transform;
			moon.value.transform.position   = moon.key.ParentPlanet.transform.position;
			MeshFilter meshFilter           = moon.value.AddComponent<MeshFilter>();
			SphereCollider sphereCollider   = moon.value.AddComponent<SphereCollider>();
			sphereCollider.isTrigger        = false;
			sphereCollider.radius           = 1f;
			MeshRenderer meshRenderer       = moon.value.AddComponent<MeshRenderer>();
		}

	}

}
