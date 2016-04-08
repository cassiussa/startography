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
	public Position[] positionScripts;
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
		//Debug.LogError (celestialBodyType, gameObject);

		//gameObject.AddComponent<Position> ();						// The position data for this celestial gameObject

		/*
		 * The below instantiations are temporary until I can figure
		 * out a way to instantiate them via the Editor options
		 */
		GameObject mesh = MakeSphereMesh("Mesh", transform, false);


		// If the celestial object is something other than a Star
		if (celestialBodyType != CelestialBodyType.Star) {
			/* 
			 * Create a new collider gameObject that doesn't have a trigger. This
			 * will be used to make sure that the viewer doesn't end up inside
			 * a celestial body
			 */
			GameObject go1 = MakeSphereCollider("Local Hard Collider", transform, 0.75f, false);

			if(bodyName == "Earth") {
				Material earthMaterial = Resources.Load("Material/Earth 1") as Material;		// Get the CelestialSphere material from the 'Resources' folder
				mesh.renderer.material = new Material(earthMaterial);
				print ("It's earth");
			}
			GameObject trail = new GameObject ("Trail");
			trail.transform.parent = gameObject.transform;
		} else {																				// This is a star so do Star type instantiations
			/*
			 * DistanceArrays are attached to Stars.  It holds onto the local solar
			 * system's last known celestial body positions
			 */
			gameObject.AddComponent<DistanceArrays> ();

			GameObject localColliders = new GameObject ("Local Colliders");						// Create the star's collider parent
			localColliders.transform.parent = transform;
			localColliders.AddComponent<Rigidbody> ();											// Add the rigidbody to the collider parent
			localColliders.rigidbody.useGravity = false;										// We don't want to use gravity
			localColliders.rigidbody.isKinematic = true;										// Set it as Kinematic
			GameObject starGlow = new GameObject ("Star Glow");
			starGlow.transform.parent = transform;

			GameObject starGlowMain = new GameObject ("Main Star Glow");
			starGlowMain.transform.parent = starGlow.transform;

			GameObject solarSystemSphere = new GameObject ("Solar System Sphere");
			solarSystemSphere.transform.parent = transform;

			/*
			 * Make the Solar Sphere for this star
			 * and assign the necessary scripts, positions,
			 * rotations, etc
			 */
			for(int solSphere=0;solSphere<4;solSphere++) {
				GameObject thisSolarSystemSphere = MakeSphereMesh("Solar System Sphere Outer", solarSystemSphere.transform, false);
				Material celestialSphereMaterial = Resources.Load("Material/CelestialSphere") as Material;	// Get the CelestialSphere material from the 'Resources' folder
				thisSolarSystemSphere.renderer.material = new Material(celestialSphereMaterial);	// Assign the material to the Material variable
				if(solSphere == 1 || solSphere == 3) {												// Check if this is the 2nd or 4th sphere so we can reverse its normals
					thisSolarSystemSphere.name = "Solar System Sphere Inner";						// Name the GameObject
					thisSolarSystemSphere.AddComponent<ReverseNormals>();							// Reverse the normals to point inwards
				}
			}

			float colliderRadius = 1f;
			for(int localCols=0;localCols<15;localCols++) {
				GameObject go2 = MakeSphereCollider("Local Collider "+localCols.ToString(), localColliders.transform, colliderRadius, true);
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

	public GameObject MakeSphereCollider(string name, Transform parent, float radius, bool isTrigger) {
		GameObject go = new GameObject(name);
		go.transform.parent = parent;
		go.AddComponent<SphereCollider>();
		go.collider.isTrigger = isTrigger;
		go.GetComponent<SphereCollider>().radius = radius;
		return go.gameObject;
	}
	public GameObject MakeSphereMesh(string name, Transform parent, bool hasCollider) {
		GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);				// Create a sphere primitive
		if(!hasCollider)																// Check if this gameObject should have a collider or not
			Destroy (mesh.collider);													// Remove the collider that is automatically added when we create the primitive
		mesh.name = name;																// Name the gameObject
		mesh.transform.parent = parent;													// Assign the parent of this GameObject
		Mesh meshSphere = (Mesh)Resources.Load("Mesh/Planet-10000-kms",typeof(Mesh));	// Get the pre-made mesh
		mesh.GetComponent<MeshFilter>().mesh = meshSphere;								// Assign the mesh from Resources to the gameObject
		Quaternion newRot = new Quaternion();											// Set up a temporary Quaternion to build the new rotation
		newRot.eulerAngles = new Vector3(-90,0,0);										// Reset the rotation as this was from Blender
		mesh.transform.localRotation = newRot;											// Set the rotation of the star
		return mesh.gameObject;															// Send the gameObject return
	}

	public Vector3d vec = new Vector3d(10,20,30);
	public Vector3d vec2 = new Vector3d(4,4,4);
	void Update() {
		if (coordinates.x != 0) {
			Debug.LogError (coordinates.x);
		}
		//print (vec.Scale(0.5).x);
		// We can now add two Vector3d values together more easily.
	}
}

