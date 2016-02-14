using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ScaleStates : Functions {
	public enum State { 
		Initialize, 
		SubMillion, 
		MillionKilometers, 
		AstronomicalUnit, 
		LightHour, 
		LightDay, 
		LightYear, 
		Parsec, 
		LightDecade, 
		LightCentury, 
		LightMillenium
	}
	
	public State state = State.Initialize;
	State _prevState;
	State _cacheState;

	/* 
	 * Check to see what type of item this script is attached to.  For example,
	 * if it's a camera, we'll update the clipping upon state change.  If it's
	 * something else, like a planet or star, we'll change the scale and position
	 * to the appropriate location and size.
	 */

	Dictionary<string, State> scales = new Dictionary<string, State>();	// Allows us to convert string as variable names
	string[] inputs;		// Array of strings of distance types
	string[] inputsRevised;	// Array of strings, taken from 'inputs' array, which can be updated
	double[] measurements;	// Array of the measurements of the distance types
	public State thisScale;

	// These are used for assigning the container gameObjects upon change in States
	GameObject scaleStatesParents;
	Dictionary<string, Transform> scaleStateParent = new Dictionary<string, Transform>();

	// For knowing what localScale to set for a gameObject in any given State
	public Vector3d originalLocalScale;
	public Vector3d thisLocalScale;
	Vector3d prevLocalScale = new Vector3d (1d, 1d, 1d);
	Vector3d newLocalScale = new Vector3d (1d, 1d, 1d);
	//double localScaleRatio = 0d;

	int layerMask;

	// If it contains a light, there's a number of things we'll need to do.  For now, create the variables
	Light starLight;
	//Dictionary<string, GameObject> lightGameObjects = new Dictionary<string, GameObject>();
	GameObject[] lightGameObjectsArray;
	StarLightScaleStates[] starLightScalesStatesScript;
	Dictionary<string, Light> lights = new Dictionary<string, Light>();
	
	PositionProcessing positionProcessingScript;
	Vector3d realPosition;								// We'll assign the positionProcessingScript.position Vector3d.  Both are same item as they're of type Object
	Positioning positioningScript;

	[HideInInspector]
	public GameObject meshes;
	Scaling meshesScalingScript;

	ObjectData objectDataScript;

	[HideInInspector]
	public GameObject[] distanceMarkerScaleObjects;
	[HideInInspector]
	public DistanceMarkerScaleStates[] distanceMarkerScaleScripts;

	[HideInInspector]
	public List<GameObject> allChildren = new List<GameObject>();
	[HideInInspector]
	public Transform starGlow;

	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	void Awake() {
		// A list of strings we can perform conditionals on and then assign a state
		scales.Add ("SM", State.SubMillion);
		scales.Add ("MK", State.MillionKilometers);
		scales.Add ("AU", State.AstronomicalUnit);
		scales.Add ("LH", State.LightHour);
		scales.Add ("Ld", State.LightDay);
		scales.Add ("LY", State.LightYear);
		scales.Add ("PA", State.Parsec);
		scales.Add ("LD", State.LightDecade);
		scales.Add ("LC", State.LightCentury);
		scales.Add ("LM", State.LightMillenium);

		inputs = new string[] { "SM", "MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		inputsRevised = inputs;		// Make a copy so that we can update the array to be smaller as desired
		measurements = new double[] { SM, MK, AU, LH, Ld, LY, PA, LD, LC, LM };


		positionProcessingScript = GetComponent<PositionProcessing> ();
		if (!positionProcessingScript)
			Debug.LogError ("The PositionProcessing script appears to be missing", gameObject);
		realPosition = positionProcessingScript.position;									// Literally the same item in memory
		positioningScript = GameObject.Find ("/Cameras").GetComponent<Positioning> ();
		if (!positioningScript)
			Debug.LogError ("The Positioning script appears to be missing from the 'Camera's gameObject", gameObject);

		/*
		 * Note that this is commented out because we still need to deal with how stars and other non-objects handle this
		 * during the simulation initialization.
		 */
		if (!scaleStatesParents)
			scaleStatesParents = GameObject.Find ("/Galaxy/ScaleStates");
		if (!scaleStatesParents)
			Debug.LogError ("The ScaleStates gameObject does not appear to be in the scene.  You need to add it to this scene.", gameObject);

		// Add all of the child ScaleState containers to the scaleStateParent dictionary
		for (int i=0; i<inputs.Length; i++) {
			scaleStateParent.Add (inputs [i], GameObject.Find (scaleStatesParents.name + inputs [i]).transform);
		}



		/* 
		 * Create a series of lights as this gameObject contains a light.  We need to create one
		 * for each of the the smallest 5 states.  We can then position each light where it would
		 * be represented in that state's space and cast light on the gameObjects within the
		 * layer.
		 */
		objectDataScript = GetComponent<ObjectData> ();

		starLight = GetComponent<Light> ();															// Add the Light component if there is one
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			lightGameObjectsArray = new GameObject[5];												// Set the size to 5 as that's the number of lights we create for 5 scale states
			starLightScalesStatesScript = new StarLightScaleStates[5];								// set the size to 5 as that's the number of lights we create for 5 scale states
			layerMask = 8;																			// 8 is the lowest layer we can manually create or edit
			for (int i=0; i<5; i++) {																// Limit to the 5 smallest scales.  Anything beyond that would be crazy			
				double thisMeasurement = measurements [i];											// Cache the measurement for this iteration to save processing
				Vector3d thisPosition = new Vector3d (												// Set the initial position of the new light gameObjects
                      ((System.Math.Abs (realPosition.x) / thisMeasurement) * maxUnits),
                      ((System.Math.Abs (realPosition.y) / thisMeasurement) * maxUnits),
                      ((System.Math.Abs (realPosition.z) / thisMeasurement) * maxUnits));

				lightGameObjectsArray[i] = Instantiate(Resources.Load ("Prefabs/StarLightObject")) as GameObject;	// Instantiate the light and assign it into the array
				lightGameObjectsArray[i].name = gameObject.name+" Light";							// Rename the gameObject
				lightGameObjectsArray[i].transform.parent = scaleStateParent [inputs [i]];			// Set this gameObject's parent to the appropriate scale's gameObject container
				lightGameObjectsArray[i].transform.position = V3dToV3 (thisPosition);				// Assign the initial position of this light's gameObject
				lightGameObjectsArray[i].layer = i + layerMask;										// Set the layer.  Note that 8 is the lowest layer we've made
				starLightScalesStatesScript[i] = lightGameObjectsArray[i].GetComponent<StarLightScaleStates>();	// Get the StarLightScaleStates components from the instantiated star light gameObjects
				starLightScalesStatesScript[i].starPositionProcessingScript = positionProcessingScript;// Assign this star's PositionProcess.cs script into the generated light so we can assign same positions

				lights.Add (inputs [i], lightGameObjectsArray[i].GetComponent<Light> ());			// Add the Light component to the gameObjects
				float calculatedRange = (float)(measurements [i] * maxUnits);						// Range of the light depending on State

				lights [inputs [i]].range = calculatedRange;										// Copy the light's Range from the original light's Range
				//lights [inputs [i]].intensity = light.intensity;									// as well as the light's intensity
				//lights [inputs [i]].color = light.color;											// and the light's colour
				lights [inputs [i]].cullingMask = 1 << i + layerMask;								// Now set the culling mask for the light

			}
			StarLightsFunction();
		}
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Planet ||
		    objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {				// Make sure that we're not trying to assign mesh to this if it's not an object that would have any (such as Starlight)
			meshes = gameObject.transform.Find ("Mesh").gameObject;
			gameObject.AddComponent<GenerateBodyColliders> ();										// Add the GenerateBodyColliders component to objects, such as Stars and Planets
		}

		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			StarColour starComponentScript = gameObject.AddComponent<StarColour>();													// Add the script that sets the colour of the star
			//StarColour starComponentScript = GetComponent<StarColour>();
			starComponentScript.sgtProminenceScript = meshes.GetComponent<SgtProminence>();
			starComponentScript.sgtCoronaScript = meshes.GetComponent<SgtCorona>();
			starComponentScript.meshes = meshes;													// Assign the mesh gameObject into the StarTemperatureColour.cs script
			starGlow = transform.Find ("StarGlow");
			starComponentScript.starGlow = starGlow;												// Assign the starGlow transform into the StarTemperatureColour.cs script
			starComponentScript.starRenderer = meshes.GetComponent<Renderer> ().materials;
			//gameObject.GetComponent<StarTemperatureColour>().Load ();
		}
		
		if (meshes) {
			meshes.AddComponent<Scaling> ();
			float meshScale = (float)((thisLocalScale.x / LH) * maxUnits) * 2000;
			meshesScalingScript = meshes.GetComponent<Scaling>();
		}

		// If this is a star then we need to prepare the Distance Markers
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			distanceMarkerScaleObjects = new GameObject[4];
			distanceMarkerScaleScripts = new DistanceMarkerScaleStates[4];
			for (int i=0; i<4; i++) {
				distanceMarkerScaleObjects[i] = Instantiate (Resources.Load ("Prefabs/DistanceMarker")) as GameObject;
				distanceMarkerScaleObjects[i].name = gameObject.name+" D.M.";
				distanceMarkerScaleScripts[i] = distanceMarkerScaleObjects[i].GetComponent<DistanceMarkerScaleStates>();
				distanceMarkerScaleScripts[i].star = gameObject;									// Assign this star to the 'star' variable on the instantiated Distance Marker

				if(i==0) distanceMarkerScaleScripts[i].size = DistanceMarkerScaleStates.Size.AstronomicalUnits;
				if(i==1) distanceMarkerScaleScripts[i].size = DistanceMarkerScaleStates.Size.LightHours;
				if(i==2) distanceMarkerScaleScripts[i].size = DistanceMarkerScaleStates.Size.LightDays;
				if(i==3) distanceMarkerScaleScripts[i].size = DistanceMarkerScaleStates.Size.LightYears;
			}
			distanceMarkerScaleObjects [0].name = distanceMarkerScaleObjects [0].name+" AU";
			distanceMarkerScaleObjects [1].name = distanceMarkerScaleObjects [1].name+" LH";
			distanceMarkerScaleObjects [2].name = distanceMarkerScaleObjects [2].name+" Ld";
			distanceMarkerScaleObjects [3].name = distanceMarkerScaleObjects [3].name+" LY";
		}

		// If this is a star, prepare the Solar System Sphere gameObject positioning
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			ObjectData objectDataSphereScript = objectDataScript.solarSystemSphere.GetComponent<ObjectData>();	// This is the ObjectData script on the Sphere gameObject
			objectDataSphereScript.parentStarObject = gameObject;									// Assign this star into the solar Sphere
			objectDataSphereScript.coordinates = objectDataScript.coordinates;						// Assign the same coordinates as the host star to the solar system's Sphere
			PositionProcessing positionProcessingSphereScript = objectDataScript.solarSystemSphere.GetComponent<PositionProcessing>();
		}

		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Planet) {			// Things we do if this of type Planet
			/*
			 * Add the LineRenderer Component and apply
			 * the necessary initial settings
			 */
			GameObject lineRendererObject = new GameObject();										// Create a new empty gameObject to use for the Line Renderer
			LineRenderer lineRenderer = lineRendererObject.AddComponent<LineRenderer>();			// Add the LineRenderer component, which will make the orbital path visible

			lineRendererObject.transform.parent = transform;										// Assign the gameObject as a child of this object
			lineRendererObject.transform.localPosition = new Vector3(0f,0f,0f);						// Set the position to the middle of the planet gameObject
			lineRendererObject.transform.localRotation = new Quaternion(0f,0f,0f,0f);				// Set the local rotation to match the planet
			lineRendererObject.name = gameObject.name+"__Orbit_Path";								// Name the Line Renderer orbital path gameObject
			//lineRenderer.material = new Material(Resources.Load("Material/PlanetOrbitPathTrail") as Material);	// Assign the default material
			lineRenderer.material = new Material(Shader.Find("Planet/PlanetOrbitPath"));
			lineRenderer.castShadows = false;														// Don't case shadows as this is UI
			lineRenderer.receiveShadows = false;													// No point in receiving shadows as this is UI
			Color startColor = new Color(1f, 0.7647f, 0.294f, 1f);									// Start color of 255,195,75,255
			Color endColor = new Color(1f, 0.7647f, 0.294f, 0f);									// End color of 255,195,75,0
			lineRenderer.SetColors(startColor, endColor);											// Set the Start and End colours of the LineRenderer material
			lineRenderer.SetWidth (0.025f,0.0f);													// Set the Start and End width of the line
			lineRenderer.useWorldSpace = true;														// Set to world space

			/*
			 * Add the LineRenderer script to handle the
			 * planet's orbital path visuals and positioning
			 */
			lineRendererObject.SetActive (false);													// Set as inactive.  Otherwise Awake function on lineRendererObject happens before we set its variables, just below
			PlanetOrbitPathTrail planetOrbitPathTrailScript = lineRendererObject.AddComponent<PlanetOrbitPathTrail>();
			planetOrbitPathTrailScript.lineRenderer = lineRenderer;

			/*
			 * We'll need to send the real position data, as opposed to the
			 * local Vector3 position, over to the PlanetOrbitPathTrail script.
			 * That's because it'll be used to check if we've gone far enough
			 * to instantiate the next segment of the LineRenderer.
			 */
			planetOrbitPathTrailScript.positionProcessingScript = positionProcessingScript;
			planetOrbitPathTrailScript.positioningScript = positioningScript;
			planetOrbitPathTrailScript.planetTransform = transform;
			planetOrbitPathTrailScript.scaleStatesScript = this;

			ParentStar parentStarScript = gameObject.AddComponent<ParentStar>();					// It's a planet so lets add the ParentStar script so we can do some calculations
			parentStarScript.planetObjectDataScript = GetComponent<ObjectData>();	// Add the ObjectData.cs script of this planet into the ParentStar.cs script

			/*
			 * Get the ObjectData script that's stored in the Parent Star's gameObject, 
			 * which itself has an ObjectData script with the value we're looking for
			*/
			ObjectData objectDataParentStarScript = objectDataScript.parentStarObject.GetComponent<ObjectData>();
			parentStarScript.starObjectDataScript = objectDataParentStarScript;
			parentStarScript.planetOrbitPathTrailScript = planetOrbitPathTrailScript;
			parentStarScript.solarMass = objectDataParentStarScript.mass;
			parentStarScript.orbitalPeriod = objectDataScript.orbitalPeriod;
			lineRendererObject.SetActive (true);													// Activate the object now that we've set its variables and Awake() can proceed without issue
		}

		// Create a list of all the child objects in this gameObject
		//allChildren.Add (transform.gameObject);
		Transform[] _allChildren = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform _child in _allChildren) {
			allChildren.Add (_child.gameObject);
			//Debug.LogError ("adding = " + _child.name,_child);
		}


	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			switch (state) {
			case State.Initialize:
				break;
			case State.SubMillion:
				SubMillion ();
				break;
			case State.MillionKilometers:
				MillionKilometers ();
				break;
			case State.AstronomicalUnit:
				AstronomicalUnit ();
				break;
			case State.LightHour:
				LightHour ();
				break;
			case State.LightDay:
				LightDay ();
				break;
			case State.LightYear:
				LightYear ();
				break;
			case State.Parsec:
				Parsec ();
				break;
			case State.LightDecade:
				LightDecade ();
				break;
			case State.LightCentury:
				LightCentury ();
				break;
			case State.LightMillenium:
				LightMillenium ();
				break;
			}
			yield return null;
		}
	}

	
	void Update() {

		/* 
		 * Count down instead of up because the vast majority of objects will 
		 * be closer to LightMillenium, LightCentury and LightDecade
		 * than they will be to millions of MillionKilometers
		 * 
		 * Can likely revise this later as I had to hack it and add the "SubMK" state to get it to work
		 * how I want.  Issue is that it bumps up to the state above the one we really want.
		*/
		// Iterates through an array and then uses the string within scales[string] dictionary key to attain dictionary value (State) 

		// This needs to be changed to Distance to origin, not just that x y or z are smaller.  A^2 + B^2 = C^2
		// Or does it need to be changed?  May not if every scale State has the same 'issue'.  If that makes it ok
		// I should leave it like this as it's faster processing than the Distance function
		for (int i=0;i<inputsRevised.Length; i++) {
			double thisMeasurement = System.Math.Abs(measurements[i]);						// Cache the value instead of calculating it for each comparison
			if (thisMeasurement > System.Math.Abs(realPosition.x+Functions.camPosition.x) && 
			    thisMeasurement > System.Math.Abs(realPosition.y+Functions.camPosition.y) && 
			    thisMeasurement > System.Math.Abs(realPosition.z+Functions.camPosition.z)) {
				thisScale = scales[inputsRevised[i]];										// inputsRevised[i-1] is a string that is a key for the scales dictionary
				break;																		// Break the loop as soon as we've found the scale.  Continue with Update() function
			}
		}

		if (state != thisScale)																// Only perform the state transition if we're not already in the same state
			SetState (thisScale);															// Assign the scale that was determined by distance from origin Vector3(0,0,0)

	}



	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	void SubMillion() {																				// This State is heavily commented as each other state uses same conditions
		transform.position = CalculatePosition (SM, realPosition, Functions.camPosition);	// Calculate the relative position based on real position and scale of this State
		layerMask = 8;
		if (_cacheState != state) {																	// Without this we get crazy bugs.  Don't know why.  It needs to be here for code efficiency anyways!
			StateFunction(layerMask, SM, "SM", 1f, "", "SM", "MK", 0d, SM, MK);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {			// Check if this gameObject is, or contains, a light
				Lights(true, "MK", MK);																	// Activate or deactivate the lights, depending on state
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.SubMillion);
				StartCoroutine(StarGlowResize(starGlow, 0.5f, 5f));
			}
			_cacheState = state;
		}
	}

	void MillionKilometers() {
		transform.position = CalculatePosition (MK, realPosition, Functions.camPosition);
		layerMask = 9;
		if (_cacheState != state) {
			StateFunction(layerMask, MK, "MK", 1f, "SM", "MK", "AU", SM, MK, AU);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "MK", MK);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.MillionKilometers);
				StartCoroutine(StarGlowResize(starGlow, 0.5f, 5f));
			}
			_cacheState = state;
		}
	}
	
	void AstronomicalUnit() {
		transform.position = CalculatePosition (AU, realPosition, Functions.camPosition);
		layerMask = 10;
		if (_cacheState != state) {
			StateFunction(layerMask, AU, "AU", 1f, "MK", "AU", "LH", MK, AU, LH);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "AU", AU);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.AstronomicalUnit);
				StartCoroutine(StarGlowResize(starGlow, 0.5f, 5f));
			}

			_cacheState = state;
		}
	}
	
	void LightHour() {
		transform.position = CalculatePosition (LH, realPosition, Functions.camPosition);
		layerMask = 11;
		if (_cacheState != state) {
			StateFunction(layerMask, LH, "LH", 1f, "AU", "LH", "Ld", AU, LH, Ld);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "LH", LH);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightHour);
				StartCoroutine(StarGlowResize(starGlow, 3f, 3f));
			}
			_cacheState = state;
		}
	}
	
	void LightDay() {
		transform.position = CalculatePosition (Ld, realPosition, Functions.camPosition);
		layerMask = 12;
		if (_cacheState != state) {
			StateFunction(layerMask, Ld, "Ld", 1f, "LH", "Ld", "LY", LH, Ld, LY);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "Ld", Ld);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightDay);
				StartCoroutine(StarGlowResize(starGlow, 3f, 3f));
			}
			_cacheState = state;
		}
	}

	void LightYear() {
		transform.position = CalculatePosition (LY, realPosition, Functions.camPosition);
		layerMask = 13;
		if (_cacheState != state) {
			StateFunction(layerMask, LY, "LY", 0f, "Ld", "LY", "PA", Ld, LY, PA);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LY", LY);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightYear);
				StartCoroutine(StarGlowResize(starGlow, 3f, 3f));
			}
			_cacheState = state;
		}
	}

	void Parsec() {
		transform.position = CalculatePosition (PA, realPosition, Functions.camPosition);
		layerMask = 14;
		if (_cacheState != state) {
			StateFunction(layerMask, PA, "PA", 0f, "LY", "PA", "LD", LY, PA, LD);
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "PA", PA);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.Parsec);
				StartCoroutine(StarGlowResize(starGlow, 3f, 5f));
			}
			_cacheState = state;
		}
	}

	void LightDecade() {
		transform.position = CalculatePosition (LD, realPosition, Functions.camPosition);
		layerMask = 15;
		if (_cacheState != state) {
			StateFunction(layerMask, LD, "LD", 0f, "PA", "LD", "LC", PA, LD, LC);

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LD", LD);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightDecade);
				StartCoroutine(StarGlowResize(starGlow, 3f, 5f));
			}
			_cacheState = state;
		}
	}

	void LightCentury() {
		transform.position = CalculatePosition (LC, realPosition, Functions.camPosition);
		layerMask = 16;
		if (_cacheState != state) {
			StateFunction(layerMask, LC, "LC", 0f, "LD", "LC", "LM", LD, LC, LM);
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LC", LC);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightCentury);
				StartCoroutine(StarGlowResize(starGlow, 3f, 5f));
			}
			_cacheState = state;
		}
	}


	void LightMillenium() {
		transform.position = CalculatePosition (LM, realPosition, Functions.camPosition);
		layerMask = 17;
		if (_cacheState != state) {
			StateFunction(layerMask, LM, "LM", 0f, "LC", "LM", "", LC, LM, 0d);
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LM", LM);
				DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale.LightMillenium);
				StartCoroutine(StarGlowResize(starGlow, 3f, 5f));
			}
			_cacheState = state;
		}
	}
	

	void StateFunction(int layerMask, double scaleD, string scaleS, float meshScale, string beforeS, string currentS, string afterS, double beforeD, double currentD, double afterD) {
		//gameObject.layer = layerMask;													// Set the layer, by number, to the appropriate layer mask
		for(int i=0;i<allChildren.Count;i++) {
			allChildren[i].layer = layerMask;
		}
		//if(meshes) meshes.layer = layerMask;											// Set the layer, by number, to the appropriate layer mask
		CalculateLocalScale (scaleD);													// Calculate the gameObject scale based on original scale and the scale of this State
		if(meshes) MeshScale(scaleD);													// If there's any items in the meshes variable, adjust the local scale appropriately
		gameObject.transform.parent = scaleStateParent [scaleS];						// Set this gameObject's parent to the appropriate scale's gameObject container

		// Specify only the scale States immediately surrounding this state so we can keep loop to minimum as there
		List<string> inputsRevisedList = new List<string> ();							// Can't resize arrays after being made, which led to complications with knowing if beforeS or afterS was missing
		if(beforeS != "") inputsRevisedList.Add (beforeS);								// Add beforeS if it isn't empty
		inputsRevisedList.Add (currentS);												// Add currentS to the list as it should always exist
		if(afterS != "") inputsRevisedList.Add (afterS);								// Add afterS if it isn't empty
		inputsRevised = inputsRevisedList.ToArray();									// Convert the List to an array

		// is no point looping through every possible state since - we can only jump up or down one state at a time
		List<double> measurementsList = new List<double> ();							// Can't resize arrays after being made, which led to complications with knowing if beforeS or afterS was missing
		if(beforeD != 0d) measurementsList.Add (beforeD);								// Add beforeS if it isn't empty
		measurementsList.Add (currentD);												// Add currentS to the list as it should always exist
		if(afterD != 0d) measurementsList.Add (afterD);									// Add afterS if it isn't empty
		measurements = measurementsList.ToArray();										// Convert the List to an array
	}


	/*
	 * Here we take the original scale of the object and modify it so that
	 * perspectively, it will look the appropriate size in any given scale
	 * state layer.  In other words if the object goes beyond the bounds of
	 * any one layer (ex 1MK) and into the next (ex 1AU), then the object
	 * will readjust its position to be closer to the camera (to ensure we	
	 * never go beyond the 10,000 unit limit) and the object will be resized
	 * (shrunk in this case) to account for the perspective difference, making
	 * it appear that nothing has happened.
	 */
	private void CalculateLocalScale(double value) {
		//prevLocalScale = V3ToV3d(gameObject.transform.localScale);
		if (gameObject.name == "DistanceMarker")
			Debug.LogError ("DistanceMarker " + thisLocalScale.x);
		newLocalScale = new Vector3d (
			(thisLocalScale.x / value) * maxUnits,
			(thisLocalScale.y / value) * maxUnits,
			(thisLocalScale.z / value) * maxUnits);

		gameObject.transform.localScale = V3dToV3(newLocalScale);
		if (value == MK)
			originalLocalScale = newLocalScale;


	}


	/*
	 * This function will assign the states that the instantiated star lights
	 * will be in.  Each star should move appropriately, but should always
	 * remain in the same scale state, unlike most objects
	 */
	private void StarLightsFunction() {
		starLightScalesStatesScript [0].thisScaleState = StarLightScaleStates.State.SubMillion;
		starLightScalesStatesScript [1].thisScaleState = StarLightScaleStates.State.MillionKilometers;
		starLightScalesStatesScript [2].thisScaleState = StarLightScaleStates.State.AstronomicalUnit;
		starLightScalesStatesScript [3].thisScaleState = StarLightScaleStates.State.LightHour;
		starLightScalesStatesScript [4].thisScaleState = StarLightScaleStates.State.LightDay;
	}

	/*
	 * We may need to rescale the proximity collider children of a body.
	 * The localColliders children rescale locally so that a smaller body
	 * gets a smaller collider.  The systemColliders are a set size which
	 * does not depend on the size of the body it's a child of.  This way
	 * we should get consistent results for long-distance speeds and 
	 * relevent speeds when we're much closer to a body
	 */
	private void MeshScale(double scale) {
		// Get the ratio of old scale to new so we can adjust the static-sized colliders
		//Vector3d adjustedLocalScale = new Vector3d(scale, scale, scale);

		/*
		 * This will allow the user to scale the planets and stars that
		 * are local so that they all become the same size
		 */
		meshesScalingScript.maxScale = (float)(236/newLocalScale.x);
	}

	/*
	 * This function iterates through the 5 smallest States and for each one
	 * it will either enable or disable the auto-generated light.  If the original
	 * light is in the current State, then the auto-generated light will be 
	 * disabled, and vice versa.  This way we ensure that we have consistent lighting
	 * on any given body as it goes from one state to the next.
	 */
	void Lights(bool isOn, string valueS, double valueD) {
		// Iterate through the 6 smallest states
		for(int i=0;i<5;i++) {
			lightGameObjectsArray[i].active = isOn;	// Enable or disable the Light component
		}
	}

	/*
	 * This function sends changes in state down to the DistanceMarker child
	 * gameObjects of Stars.  Since the local system's star is the central point
	 * from which it's planets orbit, the distance markers use the star as the
	 * local point of origin.
	 */
	void DistanceMarkerScaleUpdate(DistanceMarkerScaleStates.Scale newState) {
		for(int i=0; i<distanceMarkerScaleScripts.Length; i++) {
			distanceMarkerScaleScripts[i].scale = newState;
		}
	}
}
