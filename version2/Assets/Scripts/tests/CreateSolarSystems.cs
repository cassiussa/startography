using UnityEngine;
using System;
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
			//_star.Radius.Value    *= 100d; // An example of how to multiply by Stellar Radii
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

			/* TODO: Change this FMS later.
			 * Check to see if this star is within the allowable
			 * viewing area.  If not, move it up a Layer until
			 * it is.
			 */

			/* We'll add the SystemScaleState file here and we can pass
			 * in the data that it'll need directly
			 */
			SystemScaleState systemScaleState = _starSystem.AddComponent<SystemScaleState>();
			systemScaleState.starList = _stars_;
			systemScaleState.Begin();



			// Generate Planets
			for (int iteratorB=0; iteratorB < importedData["star"][iteratorA]["planets"].Count; iteratorB++) {
				Planet _planet          = new Planet();
				_planet.Name            = importedData["star"][iteratorA]["planets"][iteratorB]["name"];
				_planet.DateLastUpdated = importedData["star"][iteratorA]["planets"][iteratorB]["dateLastUpdate"];

				GameObject _planetSystem           = new GameObject("Planetary System: "+_planet.Name);
				_star.ChildPlanets.Add (_planetSystem);
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
					/*GameObject _moonGameObject       = new GameObject("Moon: "+_moon.Name);
					_moonGameObject.transform.parent = _moonSystem.transform;*/
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


		// Prepare the solar system parent
		foreach(StarList star in stars) {
			star.value.transform.position      = star.positionInSpace;
			GameObject _starGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Star object
			_starGameObject.name               = "Star: "+star.name;
			_starGameObject.transform.parent   = star.value.transform;
			_starGameObject.transform.position = star.value.transform.position;
			SphereCollider sphereCollider      = _starGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger           = false;
			sphereCollider.radius              = 1f;

			GameObject _starDistanceColliders           = new GameObject("Star: "+star.name+": Distance Colliders");
			_starDistanceColliders.transform.parent     = star.value.transform;
			_starDistanceColliders.transform.position   = star.value.transform.position;

			Element _starSize = new Element("Radius of the Star", star.key.Radius.Value, "stellarRadius", 0.0d, "si", "Allen's Astrophysical Quantities 4th Edition", "2016-10-09");
			Debug.Log ("Before: "+_starSize.Name+" "+_starSize.Value+" "+_starSize.Measurement);
			_starSize.ToGM();
			Debug.Log ("After: "+_starSize.Name+" "+_starSize.Value+" "+_starSize.Measurement);

			//star.key.Radius.ToM();
			_starDistanceColliders.transform.localScale = new Vector3d(_starSize.Value, _starSize.Value, _starSize.Value);
			_starGameObject.transform.localScale = new Vector3d(_starSize.Value, _starSize.Value, _starSize.Value);

			// Create the first few distance colliders linearly
			float colliderRadiusScale = 0f;
			for(int i=1;i<19;i++) {
				if(i<4)
					colliderRadiusScale = i * 3f;
				else
					colliderRadiusScale = 10f * Mathf.Exp ((i-3)/2f);

				GameObject _starDistanceCollider           = new GameObject("Star: "+star.name+": Distance Collider "+i);
				_starDistanceCollider.transform.parent     = _starDistanceColliders.transform;
				_starDistanceCollider.transform.position   = star.value.transform.position;
				_starDistanceCollider.transform.localScale = new Vector3(1f,1f,1f);
				Rigidbody _sphereRigidbody                 = _starDistanceCollider.AddComponent<Rigidbody>();
				_sphereRigidbody.useGravity                = false;
				SphereCollider _sphereDistanceCollider     = _starDistanceCollider.AddComponent<SphereCollider>();
				_sphereDistanceCollider.isTrigger          = true;
				_sphereDistanceCollider.radius             = colliderRadiusScale;
			}
		}


		// Prepare the planetary system parent
		foreach (PlanetList planet in planets) {
			planet.value.transform.parent        = planet.key.ParentStar.transform;
			planet.value.transform.position      = planet.key.ParentStar.transform.position;
			GameObject _planetGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Planet object
			_planetGameObject.name               = "Planet: "+planet.name;
			_planetGameObject.transform.parent   = planet.value.transform;
			_planetGameObject.transform.position = planet.value.transform.position;
			SphereCollider sphereCollider        = _planetGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger             = false;
			sphereCollider.radius                = 1f;
		}


		// Prepare the moon system parent
		foreach (MoonList moon in moons) {
			moon.value.transform.parent        = moon.key.ParentPlanet.transform;
			moon.value.transform.position      = moon.key.ParentPlanet.transform.position;
			GameObject _moonGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Moon object
			_moonGameObject.name               = "Moon: "+moon.name;
			_moonGameObject.transform.parent   = moon.value.transform;
			_moonGameObject.transform.position = moon.value.transform.position;
			SphereCollider sphereCollider      = _moonGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger           = false;
			sphereCollider.radius              = 1f;
		}

	}

}
