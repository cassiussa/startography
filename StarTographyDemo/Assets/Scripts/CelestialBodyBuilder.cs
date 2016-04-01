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

public class CelestialBodyBuilder : MonoBehaviour {

	public enum CelestialBodyType {
		Star,
		Planet,
		Moon
	}

	public CelestialBodyType celestialBodyType;
	public string name;

	// Star Variables
	public string rightAscension;
	public string declination;
	public double distance;
	public float opticalMagnitude;
	public float temperature;
	public double stellarMass;
	public double stellarRadius;
	public string dateLastUpdate;
	public GameObject[] planets;

	// Planet Variables
	public int numPlanetsInSystem;
	public float orbitalPeriod;
	public float semiMajorAxis;
	public float eccentricity;
	public float inclination;
	public double planetMass;
	public float planetRadius;
	public GameObject star;
	public GameObject planet;
	public GameObject[] moons;

	// Moon Variables
	public int numMoonsInSystem;
	public float moonMass;
	public float moonRadius;

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
		mesh.transform.parent = gameObject.transform;
		GameObject localColliders = new GameObject ("LocalColliders");
		localColliders.transform.parent = gameObject.transform;
		GameObject A = new GameObject ("A");
		GameObject B = new GameObject ("B");
		GameObject C = new GameObject ("C");
		GameObject D = new GameObject ("D");
		A.transform.parent = localColliders.transform;
		B.transform.parent = localColliders.transform;
		C.transform.parent = localColliders.transform;
		D.transform.parent = localColliders.transform;
		if(celestialBodyType != CelestialBodyType.Star) {
			GameObject trail = new GameObject ("Trail");
			trail.transform.parent = gameObject.transform;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
