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
}

[System.Serializable]
public class PlanetList {
	public string name;
	public GameObject value;
	public Planet key;
	
}

[System.Serializable]
public class MoonList {
	public string name;
	public GameObject value;
	public Moon key;
	
}

public class Planets : MonoBehaviour {
	
	public List<StarList> stars = new List<StarList>();
	public List<PlanetList> planets = new List<PlanetList>();
	public List<MoonList> moons = new List<MoonList>();

	public JSONNode importedData;
	string JSONData;    // Holds the data.json file data

	void Awake () {
		JSONData = Data.data.ReadToEnd (); // Read from the data.json file
		Data.data.Close ();
		
		importedData = JSON.Parse(JSONData); // Parse the data into a formatted string variable

		for (int iteratorA=0; iteratorA<importedData["star"].Count; iteratorA++) {
			Star _star = new Star ();
			_star.Mass = new Element ("Stellar Mass", double.Parse(importedData ["star"] [iteratorA]["stellarMass"]), "Kg", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Radius = new Element ("Stellar Radius", double.Parse(importedData ["star"] [iteratorA] ["stellarRadius"]), "meter", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Radius.Value *= 100d; // An example of how to multiply by Stellar Radii
			_star.DateLastUpdated = importedData ["star"] [iteratorA] ["dateLastUpdate"];
			_star.Name = importedData ["star"] [iteratorA] ["name"];
			_star.RightAscension = importedData ["star"] [iteratorA] ["rightAscension"];
			_star.Declination = importedData ["star"] [iteratorA] ["declination"];
			_star.Distance = new Element("Distance", double.Parse(importedData ["star"] [iteratorA] ["distance"]), "meter", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Distance.ToKM();
			//Debug.Log(_star.Distance);
			//Debug.Log (Maths.InKM(_star.Distance));
			Element testTime = new Element("Seconds", 30d, "second", 0d, "SI", "StarTography", "2016-10-10");
			//Debug.Log (Maths.InMinutes(testTime));

			RightAscension rightAscension = new RightAscension("00 42 30");
			//new Declination("+41 12 00");
			Declination declination = new Declination("+41d12m00s");
			//RightAscension rightAscension = new RightAscension(1d, 44d, 4.091d);
			//Declination declination = new Declination(-15d, 56d, 14.89d);
			Vector3 pos = Maths.SphericalToCartesianCoords(11.9d, rightAscension, declination);
			//Debug.LogError (pos);
			_star.Luminosity = new Element("Optical Magnitude", double.Parse(importedData ["star"] [iteratorA] ["opticalMagnitude"]), "lum", 0.0d, "SI", "StarTography 1.0", "2016-10-10");
			_star.Temperature = new Element("Temperature", double.Parse(importedData ["star"] [iteratorA] ["temperature"]), "celcius", 0.0d, "SI", "StarTography 1.0", "2016-10-10");





			GameObject _starSystem = new GameObject("Star System: "+_star.Name);
			_starSystem.transform.position = pos;
			GameObject _starGameObject = new GameObject("Star: "+_star.Name);
			_starGameObject.transform.parent = _starSystem.transform;
			
			MeshFilter meshFilter         = _starGameObject.AddComponent<MeshFilter>();
			SphereCollider sphereCollider = _starGameObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger      = false;
			sphereCollider.radius         = 1f;
			MeshRenderer meshRenderer     = _starGameObject.AddComponent<MeshRenderer>();

			StarList _stars_ = new StarList();
			_stars_.name = _star.Name;
			_stars_.key = _star;
			_stars_.value = _starSystem;
			stars.Add (_stars_);

			for (int iteratorB=0; iteratorB<importedData["star"][iteratorA]["planets"].Count; iteratorB++) {
				Planet _planet = new Planet();
				_planet.Name = importedData["star"][iteratorA]["planets"][iteratorB]["name"];
				_planet.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["dateLastUpdate"];


				GameObject _planetSystem = new GameObject("Planet System: "+_planet.Name);
				_planetSystem.transform.parent = _starSystem.transform;
				GameObject _planetGameObject = new GameObject("Planet: "+_planet.Name);
				_planetGameObject.transform.parent = _planetSystem.transform;
				MeshFilter pmeshFilter         = _planetGameObject.AddComponent<MeshFilter>();
				SphereCollider psphereCollider = _planetGameObject.AddComponent<SphereCollider>();
				psphereCollider.isTrigger      = false;
				psphereCollider.radius         = 1f;
				MeshRenderer pmeshRenderer     = _planetGameObject.AddComponent<MeshRenderer>();

				_star.ChildPlanets.Add (_planetSystem);
				_planet.ParentStar = _starSystem;


				PlanetList _planets_ = new PlanetList();
				_planets_.name = _planet.Name;
				_planets_.key = _planet;
				_planets_.value = _planetSystem;
				planets.Add (_planets_);

				//planets.Add(_planet, _planetGameObject);

				for (int iteratorC=0; iteratorC<importedData
				     ["star"][iteratorA]
				     ["planets"][iteratorB]
				     ["moons"].Count; iteratorC++) {
					Moon _moon = new Moon();
					_moon.Name = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["name"];
					_moon.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["moons"][iteratorC]["dateLastUpdate"];


					GameObject _moonSystem = new GameObject("Moon System: "+_moon.Name);
					_moonSystem.transform.parent = _planetSystem.transform;
					GameObject _moonGameObject = new GameObject("Moon: "+_moon.Name);
					_moonGameObject.transform.parent = _moonSystem.transform;
					MeshFilter mmeshFilter         = _moonGameObject.AddComponent<MeshFilter>();
					SphereCollider msphereCollider = _moonGameObject.AddComponent<SphereCollider>();
					msphereCollider.isTrigger      = false;
					msphereCollider.radius         = 1f;
					MeshRenderer mmeshRenderer     = _moonGameObject.AddComponent<MeshRenderer>();

					_moon.ParentPlanet = _planetSystem;
					_planet.ChildMoons.Add(_moonSystem);

					MoonList _moons_ = new MoonList();
					_moons_.name = _moon.Name;
					_moons_.key = _moon;
					_moons_.value = _moonSystem;
					moons.Add (_moons_);

					//moons.Add(_moon, _moonGameObject);
				}
			}
		}

	}

	void Start() {
		/*foreach(Star star in stars) {
			GameObject _system = new GameObject("System: "+star.Name);
			GameObject _star = new GameObject("Star: "+star.Name);
			_star.transform.parent = _system.transform;

			MeshFilter meshFilter         = _star.AddComponent<MeshFilter>();
			SphereCollider sphereCollider = _star.AddComponent<SphereCollider>();
			sphereCollider.isTrigger      = false;
			sphereCollider.radius         = 1f;
			MeshRenderer meshRenderer     = _star.AddComponent<MeshRenderer>();
		}

		foreach (Planet planet in planets) {
			GameObject _planet = new GameObject("Planet: "+planet.Name);
			
			MeshFilter meshFilter         = _planet.AddComponent<MeshFilter>();
			SphereCollider sphereCollider = _planet.AddComponent<SphereCollider>();
			sphereCollider.isTrigger      = false;
			sphereCollider.radius         = 1f;
			MeshRenderer meshRenderer     = _planet.AddComponent<MeshRenderer>();
		}

		foreach (Moon moon in moons) {
			GameObject _moon = new GameObject("Moon: "+moon.Name);
			
			MeshFilter meshFilter         = _moon.AddComponent<MeshFilter>();
			SphereCollider sphereCollider = _moon.AddComponent<SphereCollider>();
			sphereCollider.isTrigger      = false;
			sphereCollider.radius         = 1f;
			MeshRenderer meshRenderer     = _moon.AddComponent<MeshRenderer>();
		}*/
	}

}
