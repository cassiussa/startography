using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using BodyElements;
using CustomMath;
using Elements;
using ImportData;
using SimpleJSON;


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
	public Vector3d positionInOrbit;
	
}

[System.Serializable]
public class MoonList {
	public string name;
	public Moon key;
	public GameObject value;
	public Vector3d positionInOrbit;
	
}

[DisallowMultipleComponent]
public class CreateSolarSystems : MonoBehaviour {
	
	public List<StarList> stars = new List<StarList>();
	public List<PlanetList> planets = new List<PlanetList>();
	public List<MoonList> moons = new List<MoonList>();

	public JSONNode importedData;
	string JSONData;    // Holds the data.json file data

	void Awake () {
		JSONData = Data.LoadJson(); // Read from the data.json file
		
		importedData = JSON.Parse(JSONData); // Parse the data into a formatted string variable


		// Generate Stars
		for (int iteratorA=0; iteratorA < importedData["star"].Count; iteratorA++) {
			Star _star = new Star ();
			_star.DateLastUpdated  = importedData ["star"] [iteratorA] ["dateLastUpdate"];
			_star.Name             = importedData ["star"] [iteratorA] ["name"];
			_star.RightAscension   = importedData ["star"] [iteratorA] ["rightAscension"];
			_star.Declination      = importedData ["star"] [iteratorA] ["declination"];
			_star.Mass             = new Element ("Stellar Mass", ParseDouble(importedData ["star"] [iteratorA]["stellarMass"]), "Kg", 0.0d, "SI", "StarTography 1.0", _star.DateLastUpdated);
			_star.Radius           = new Element ("Stellar Radius", ParseDouble(importedData ["star"] [iteratorA] ["stellarRadius"]), "meter", 0.0d, "SI", "StarTography 1.0", _star.DateLastUpdated);
			//_star.Radius.Value    *= 100d; // An example of how to multiply by Stellar Radii
			_star.Distance         = new Element("Distance", ParseDouble(importedData ["star"] [iteratorA] ["distance"]), "meter", 0.0d, "SI", "StarTography 1.0", _star.DateLastUpdated);
			_star.Luminosity       = new Element("Optical Magnitude", ParseDouble(importedData ["star"] [iteratorA] ["opticalMagnitude"]), "lum", 0.0d, "SI", "StarTography 1.0", _star.DateLastUpdated);
			_star.Temperature      = new Element("Temperature", ParseDouble(importedData ["star"] [iteratorA] ["temperature"]), "celcius", 0.0d, "SI", "StarTography 1.0", _star.DateLastUpdated);

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
			JSONNode planetDataList = importedData["star"][iteratorA]["planets"];
			for (int iteratorB=0; iteratorB < planetDataList.Count; iteratorB++) {
				JSONNode planetData = planetDataList[iteratorB];
				Planet _planet          = new Planet();
				_planet.Name            = planetData["name"];
				_planet.DateLastUpdated = GetString(planetData, "dateLastUpdate", _star.DateLastUpdated);
				SetOrbitElements(_planet, planetData, "Planet");

				GameObject _planetSystem           = new GameObject("Planetary System: "+_planet.Name);
				_star.ChildPlanets.Add (_planetSystem);
				_planet.ParentStar                 = _starSystem;

				PlanetList _planets_ = new PlanetList();
				_planets_.name       = _planet.Name;
				_planets_.key        = _planet;
				_planets_.value      = _planetSystem;
				_planets_.positionInOrbit = CalculateOrbitPosition(_planet, iteratorB, planetDataList.Count);
				planets.Add (_planets_);

				JSONNode moonDataList = planetData["moons"];
				for (int iteratorC=0; iteratorC < moonDataList.Count; iteratorC++) {
					JSONNode moonData = moonDataList[iteratorC];
					Moon _moon            = new Moon();
					_moon.Name            = moonData["name"];
					_moon.DateLastUpdated = GetString(moonData, "dateLastUpdate", _planet.DateLastUpdated);
					SetOrbitElements(_moon, moonData, "Moon");

					GameObject _moonSystem           = new GameObject("Moon System: "+_moon.Name);
					_planet.ChildMoons.Add(_moonSystem);
					/*GameObject _moonGameObject       = new GameObject("Moon: "+_moon.Name);
					_moonGameObject.transform.parent = _moonSystem.transform;*/
					_moon.ParentPlanet               = _planetSystem;

					MoonList _moons_ = new MoonList();
					_moons_.name     = _moon.Name;
					_moons_.key      = _moon;
					_moons_.value    = _moonSystem;
					_moons_.positionInOrbit = CalculateOrbitPosition(_moon, iteratorC, moonDataList.Count);
					moons.Add (_moons_);
				}
			}
		}

	}


	private static string GetString(JSONNode source, string key, string fallback) {
		JSONNode node = source[key];
		if (HasJsonValue(node))
			return node.Value;
		return string.IsNullOrEmpty(fallback) ? "Unknown" : fallback;
	}

	private static void SetOrbitElements(OrbitElement orbitElement, JSONNode source, string bodyType) {
		string lastUpdated = string.IsNullOrEmpty(orbitElement.DateLastUpdated) ? "Unknown" : orbitElement.DateLastUpdated;

		orbitElement.SemiMajorAxis       = CreateElement(source, "semiMajorAxis", bodyType + " Semi-major Axis", "au", lastUpdated);
		orbitElement.Eccentricity        = CreateElement(source, "eccentricity", bodyType + " Eccentricity", "ratio", lastUpdated);
		orbitElement.Inclination         = CreateElement(source, "inclination", bodyType + " Inclination", "degree", lastUpdated);
		orbitElement.ArgumentOfPariapsis = CreateElement(source, "argumentOfPariapsis", bodyType + " Argument of Pariapsis", "degree", lastUpdated);
		orbitElement.Longitude           = CreateElement(source, "longitude", bodyType + " Longitude of Ascending Node", "degree", lastUpdated);
		orbitElement.MeanAnomaly         = CreateElement(source, "meanAnomaly", bodyType + " Mean Anomaly", "degree", lastUpdated);
		orbitElement.Period              = CreateElement(source, "orbitalPeriod", bodyType + " Orbital Period", "day", lastUpdated);
		orbitElement.EccentricAnomaly    = CreateElement(source, "eccentricAnomaly", bodyType + " Eccentric Anomaly", "degree", lastUpdated);
		orbitElement.TrueAnomaly         = CreateElement(source, "trueAnomaly", bodyType + " True Anomaly", "degree", lastUpdated);
	}

	private static Element CreateElement(JSONNode source, string key, string name, string measurement, string lastUpdated) {
		JSONNode node = source[key];
		if (!HasJsonValue(node))
			return null;

		return new Element(name, ParseDouble(node), measurement, 0.0d, "SI", "StarTography 1.0", lastUpdated);
	}

	private static bool HasJsonValue(JSONNode node) {
		return node != null && !string.IsNullOrEmpty(node.Value);
	}

	private static double ParseDouble(JSONNode node) {
		return double.Parse(node.Value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
	}

	private static Vector3d CalculateOrbitPosition(OrbitElement orbitElement, int siblingIndex, int siblingCount) {
		if (orbitElement == null || orbitElement.SemiMajorAxis == null)
			return new Vector3d();

		double semiMajorAxis = Maths.InMM(orbitElement.SemiMajorAxis);
		double eccentricity = ClampEccentricity(GetElementValue(orbitElement.Eccentricity, 0d));
		double inclination = GetElementValue(orbitElement.Inclination, 0d) * Maths.Deg2Rad;
		double longitude = GetElementValue(orbitElement.Longitude, 0d) * Maths.Deg2Rad;
		double argumentOfPariapsis = GetElementValue(orbitElement.ArgumentOfPariapsis, 0d) * Maths.Deg2Rad;
		double trueAnomaly = GetOrbitAngleDegrees(orbitElement, siblingIndex, siblingCount) * Maths.Deg2Rad;

		double orbitalRadius = semiMajorAxis * (1d - (eccentricity * eccentricity)) / (1d + (eccentricity * Math.Cos(trueAnomaly)));
		double angleInOrbit = argumentOfPariapsis + trueAnomaly;

		double cosLongitude = Math.Cos(longitude);
		double sinLongitude = Math.Sin(longitude);
		double cosInclination = Math.Cos(inclination);
		double sinInclination = Math.Sin(inclination);
		double cosAngle = Math.Cos(angleInOrbit);
		double sinAngle = Math.Sin(angleInOrbit);

		return new Vector3d(
			orbitalRadius * ((cosLongitude * cosAngle) - (sinLongitude * sinAngle * cosInclination)),
			orbitalRadius * ((sinLongitude * cosAngle) + (cosLongitude * sinAngle * cosInclination)),
			orbitalRadius * (sinAngle * sinInclination)
		);
	}

	private static double GetOrbitAngleDegrees(OrbitElement orbitElement, int siblingIndex, int siblingCount) {
		if (orbitElement.TrueAnomaly != null)
			return orbitElement.TrueAnomaly.Value;
		if (orbitElement.MeanAnomaly != null)
			return orbitElement.MeanAnomaly.Value;
		if (orbitElement.EccentricAnomaly != null)
			return orbitElement.EccentricAnomaly.Value;
		if (siblingCount <= 0)
			return 0d;

		return (360d * siblingIndex) / siblingCount;
	}

	private static double GetElementValue(Element element, double fallback) {
		return element == null ? fallback : element.Value;
	}

	private static double ClampEccentricity(double eccentricity) {
		if (double.IsNaN(eccentricity))
			return 0d;

		return Math.Min(Math.Max(eccentricity, 0d), 0.999999d);
	}

	void Start() {


		// Prepare the solar system parent
		foreach(StarList star in stars) {
			star.value.transform.position      = star.positionInSpace;
			GameObject _starGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Star object
			_starGameObject.name               = "Star: "+star.name;
			_starGameObject.transform.SetParent(star.value.transform, false);
			_starGameObject.transform.localPosition = Vector3.zero;
			SphereCollider sphereCollider      = _starGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger           = false;
			sphereCollider.radius              = 1f;

			GameObject _starDistanceColliders           = new GameObject("Star: "+star.name+": Distance Colliders");
			_starDistanceColliders.transform.SetParent(star.value.transform, false);
			_starDistanceColliders.transform.localPosition = Vector3.zero;

			Element _starSize = new Element("Radius of the Star", star.key.Radius.Value, "stellarRadius", 0.0d, "si", "Allen's Astrophysical Quantities 4th Edition", star.key.DateLastUpdated);
			Debug.Log ("Before: "+_starSize.Name+" "+_starSize.Value+" "+_starSize.Measurement);
			_starSize.ToMM();  // To go to actual size instead of Base Units, we would do ToGM()
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
				_starDistanceCollider.transform.SetParent(_starDistanceColliders.transform, false);
				_starDistanceCollider.transform.localPosition = Vector3.zero;
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
			planet.value.transform.SetParent(planet.key.ParentStar.transform, false);
			planet.value.transform.localPosition = planet.positionInOrbit;
			GameObject _planetGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Planet object
			_planetGameObject.name               = "Planet: "+planet.name;
			_planetGameObject.transform.SetParent(planet.value.transform, false);
			_planetGameObject.transform.localPosition = Vector3.zero;
			SphereCollider sphereCollider        = _planetGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger             = false;
			sphereCollider.radius                = 1f;
		}


		// Prepare the moon system parent
		foreach (MoonList moon in moons) {
			moon.value.transform.SetParent(moon.key.ParentPlanet.transform, false);
			moon.value.transform.localPosition = moon.positionInOrbit;
			GameObject _moonGameObject         = GameObject.CreatePrimitive(PrimitiveType.Sphere);  // Create the Moon object
			_moonGameObject.name               = "Moon: "+moon.name;
			_moonGameObject.transform.SetParent(moon.value.transform, false);
			_moonGameObject.transform.localPosition = Vector3.zero;
			SphereCollider sphereCollider      = _moonGameObject.GetComponent<SphereCollider>();
			sphereCollider.isTrigger           = false;
			sphereCollider.radius              = 1f;
		}

	}

}
