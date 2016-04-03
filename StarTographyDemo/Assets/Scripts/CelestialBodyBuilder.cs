/*
 * This script is responsible for analyzing the type of celestial body
 * it is attached to and making decisions on what other components to
 * add to it, as well as holding onto any applicable Arrays, Lists, 
 * and Dictionaries it may need for faster access to sibling, child,
 * or parent objects for any given solar system.
 */

using UnityEngine;
using System;
using System.Collections;
using Functions;

public class CelestialBodyBuilder : MonoBehaviour {

	public enum CelestialBodyType {
		Star,
		Planet,
		Moon
	}

	public CelestialBodyType celestialBodyType;
	public string bodyName;

	// Star Variables
	public string rightAscension;
	public string declination;
	public double distance;
	public float opticalMagnitude;
	public float temperature;
	public double stellarMass;
	public double stellarRadius;
	public string dateLastUpdate;
	[Space(20)]
	[Header("Child planets")]
	public GameObject[] planets;
	
	// Planet Variables
	public int numPlanetsInSystem;
	public float orbitalPeriod;
	public float semiMajorAxis;
	public float eccentricity;
	public float inclination;
	public double planetMass;
	public float planetRadius;
	//[Space(20)]
	[Header("Parent star")]
	public GameObject star;
	[Space(20)]
	[Header("Parent planet")]
	public GameObject planet;
	[Header("Child moons")]
	public GameObject[] moons;

	// Moon Variables
	public int numMoonsInSystem;
	public float moonMass;
	public float moonRadius;

	public Function.Vector3d coordinates = new Function.Vector3d(0,0,0);	// The starting position in meters of this celestial body
	public Function.Vector3d radius = new Function.Vector3d(0,0,0);		// This measurement is in meters as opposed to relative solar, jupiter, or earth radii
	public float luminosity;


	/* 
	 * We need to use Start() and not Awake().  The reason for this
	 * is that we are adding this script to an instantiated gameObject
	 * and then immediately assigning the enum type.  However it seems
	 * that the Awake() function happens before the enum assigment is
	 * performed by the FormatImportData.cs script (even though it
	 * happens on the next line).  So for now I'll use the Start() function
	 * since it's no problem at this point.
	 * 
	 * Addendum.  This may no longer be true as now we're setting the
	 * gameObject and this script to disabled upon instantiation so that
	 * we can populate the variable values for every item in the JSON
	 * config file before enabling them all at once.  This way, I'm hoping
	 * that we keep proper sync as everything is first loaded in full, then
	 * started instead of started as each is loaded individually
	 */
	void Awake () {
		//Debug.LogError (celestialBodyType, gameObject);

		/*
		 * The below instantiations are temporary until I can figure
		 * out a way to instantiate them via the Editor options
		 */
		GameObject mesh = new GameObject("Mesh");
		mesh.transform.parent = transform;
		GameObject starGlow = new GameObject ("StarGlow");
		starGlow.transform.parent = transform;
		GameObject starGlowMain = new GameObject ("StarGlowMain");
		starGlowMain.transform.parent = starGlow.transform;
		GameObject localColliders = new GameObject ("LocalColliders");
		localColliders.transform.parent = transform;
		localColliders.AddComponent<Rigidbody> ();
		localColliders.rigidbody.useGravity = false;
		localColliders.rigidbody.isKinematic = true;

		float colliderRadius = 0.1f;
		for(int localCols1=1;localCols1<5;localCols1++) {
			GameObject go1 = new GameObject("LocalCollider"+localCols1.ToString());
			go1.transform.parent = localColliders.transform;
			go1.AddComponent<SphereCollider>();
			go1.collider.isTrigger = true;
			go1.GetComponent<SphereCollider>().radius = colliderRadius;
			colliderRadius *= 10;
		}

		if (celestialBodyType != CelestialBodyType.Star) {
			GameObject trail = new GameObject ("Trail");
			trail.transform.parent = gameObject.transform;
		} else {
			GameObject solarSystemSphere = new GameObject("Solar System Sphere");
			solarSystemSphere.transform.parent = transform.parent;
			for(int localCols=5;localCols<15;localCols++) {
				GameObject go2 = new GameObject("LocalCollider"+localCols.ToString());
				go2.transform.parent = localColliders.transform;
				go2.AddComponent<SphereCollider>();
				go2.collider.isTrigger = true;
				go2.GetComponent<SphereCollider>().radius = colliderRadius;
				colliderRadius *= 10;
			}


			GameObject markerAU = new GameObject (gameObject.name+" [MARKER] AU");
			GameObject markerLightHour = new GameObject (gameObject.name+" [MARKER] Light Hour");
			GameObject markerLightDay = new GameObject (gameObject.name+" [MARKER] Light Day");
			GameObject markerLightYear = new GameObject (gameObject.name+" [MARKER] Light Year");
			markerAU.transform.parent = transform;
			markerLightHour.transform.parent = transform;
			markerLightDay.transform.parent = transform;
			markerLightYear.transform.parent = transform;
		}

	}

	void Update() {
		if (coordinates.x != 0) {
			Debug.LogError (coordinates.x);
		}
	}
}

