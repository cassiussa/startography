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
	[Header("Child Bodies")]
	public GameObject[] bodies;
	public CelestialBodyBuilder[] celestialBodyBuilderScripts;
	public Position[] positionScripts;
	public ScaleStates[] scaleStatesScripts;
	public Vector3d[] realPositions;
	public Vector3d[] relativePositions;
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
	
	public Vector3d coordinates = new Vector3d(0,0,0);	// The starting position in meters of this celestial body
	public Vector3d radius = new Vector3d(0,0,0);		// This measurement is in meters as opposed to relative solar, jupiter, or earth radii
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
		if(gameObject.name == "[STAR] Sun [PLANET] Mercury")
			coordinates = new Vector3d(10,11,12);

		/*
		 * The below instantiations are temporary until I can figure
		 * out a way to instantiate them via the Editor options
		 */
		GameObject mesh = Function.MakeSphereMesh("Mesh", transform, false);


		/*
		 * the celestial body is not a star, so do any functionality
		 * or instantiatiuons that we may need.
		 */
		if (celestialBodyType != CelestialBodyType.Star) {
			/* 
			 * Create a new collider gameObject that doesn't have a trigger. This
			 * will be used to make sure that the viewer doesn't end up inside
			 * a celestial body
			 */
			GameObject hardCollider = Function.MakeSphereCollider("Local Hard Collider", transform, 0.75f, false);

			if(bodyName == "Earth") {
				Material earthMaterial = Resources.Load("Material/Earth 1") as Material;		// TODO: Deal with Materials.  Get the CelestialSphere material from the 'Resources' folder
				mesh.renderer.material = new Material(earthMaterial);
				print ("It's earth");
			}
			GameObject trail = new GameObject ("Trail");										// TODO: Handle the body trails
			trail.transform.parent = gameObject.transform;



		/* 
		 * The celestial body is of type Star.  Do any functionality
		 * or instantiations that we need that apply only to Stars
		 */
		} else {
			/*
			 * DistanceArrays are attached to Stars.  It holds onto the local solar
			 * system's last known celestial body positions.  The Position scripts
			 * are attached to all celestial bodies.  It holds onto their real position
			 * in space as well as their relative position to the camera.
			 */
			gameObject.AddComponent<DistanceArrays> ();										// Add the DistanceArrays script to this Star
			Position positionScript = gameObject.AddComponent<Position> ();					// Add the Position.cs script to this Star
			ScaleStates scaleStatesScript = gameObject.AddComponent<ScaleStates>();			// Add the ScaleStates.cs script to this Star
			positionScript.realPosition = coordinates;										// Assign the coordinates for this star into the realPosition variable on the Position script for this star
			gameObject.AddComponent<BuildStarColliders> ();									// Create colliders gameObjects, rigidbodies, and configs
			gameObject.AddComponent<BuildSolarSystemSphere> ();								// Make the Solar Sphere for this star, assign the necessary scripts, positions, rotations, etc
			gameObject.AddComponent<BuildDistanceMarkers> ();								// Create the Distance Marker gameObjects
			gameObject.AddComponent<BuildStarGlow> ();


		}

	}
	
}

