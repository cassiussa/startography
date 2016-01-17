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
	double localScaleRatio = 0d;

	int layerMask;

	// If it contains a light, there's a number of things we'll need to do.  For now, create the variables
	Light starLight;
	//Dictionary<string, GameObject> lightGameObjects = new Dictionary<string, GameObject>();
	GameObject[] lightGameObjectsArray;
	StarLightScaleStates[] starLightScalesStatesScript;
	Dictionary<string, Light> lights = new Dictionary<string, Light>();
	
	PositionProcessing positionProcessingScript;
	Positioning positioningScript;

	public GameObject meshes;

	ObjectData objectDataScript;

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
					((System.Math.Abs (positionProcessingScript.position.x) / thisMeasurement) * maxUnits),
					((System.Math.Abs (positionProcessingScript.position.y) / thisMeasurement) * maxUnits),
					((System.Math.Abs (positionProcessingScript.position.z) / thisMeasurement) * maxUnits));

				lightGameObjectsArray[i] =  Instantiate (Resources.Load ("Prefabs/StarLightObject")) as GameObject;		// Instantiate the light and assign it into the array
				lightGameObjectsArray[i].name = gameObject.name+" Light";							// Rename the gameObject
				lightGameObjectsArray[i].transform.parent = scaleStateParent [inputs [i]];			// Set this gameObject's parent to the appropriate scale's gameObject container
				lightGameObjectsArray[i].transform.position = V3dToV3 (thisPosition);				// Assign the initial position of this light's gameObject
				lightGameObjectsArray[i].layer = i + layerMask;										// Set the layer.  Note that 8 is the lowest layer we've made
				starLightScalesStatesScript[i] = lightGameObjectsArray[i].GetComponent<StarLightScaleStates>();	// Get the StarLightScaleStates components from the instantiated star light gameObjects

				lights.Add (inputs [i], lightGameObjectsArray[i].GetComponent<Light> ());			// Add the Light component to the gameObjects
				float calculatedRange = (float)(measurements [i] * maxUnits);		// Range of the light depending on State

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
			// Add the visuals script centered around the star
			//gameObject.AddComponent<GenerateDistanceVisuals> ();
			gameObject.AddComponent<GenerateBodyColliders> ();										// Add the GenerateBodyColliders component to objects, such as Stars and Planets
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
			if (thisMeasurement > System.Math.Abs(positionProcessingScript.position.x+positioningScript.camPosition.x) && 
			    thisMeasurement > System.Math.Abs(positionProcessingScript.position.y+positioningScript.camPosition.y) && 
			    thisMeasurement > System.Math.Abs(positionProcessingScript.position.z+positioningScript.camPosition.z)) {
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
		CalculatePosition (SM, positionProcessingScript.position, positioningScript.camPosition);	// Calculate the relative position based on real position and scale of this State
		layerMask = 8;
		if (_cacheState != state) {																	// Without this we get crazy bugs.  Don't know why.  It needs to be here for code efficiency anyways!
			StateFunction(layerMask, SM, "SM", 1f, "", "SM", "MK", 0d, SM, MK);

			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {			// Check if this gameObject is, or contains, a light
				Lights(true, "MK", MK);																	// Activate or deactivate the lights, depending on state
			//	StarLightsFunction(0,StarLightScaleStates.State.SubMillion);
			}
		}
	}

	void MillionKilometers() {
		CalculatePosition (MK, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 9;
		if (_cacheState != state) {
			StateFunction(layerMask, MK, "MK", 1f, "SM", "MK", "AU", SM, MK, AU);

			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "MK", MK);
				//StarLightsFunction(1,StarLightScaleStates.State.MillionKilometers);
			}
		}
	}
	
	void AstronomicalUnit() {
		CalculatePosition (AU, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 10;
		if (_cacheState != state) {
			StateFunction(layerMask, AU, "AU", 1f, "MK", "AU", "LH", MK, AU, LH);

			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "AU", AU);
				//StarLightsFunction(2,StarLightScaleStates.State.AstronomicalUnit);
			}
		}
	}
	
	void LightHour() {
		CalculatePosition (LH, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 11;
		if (_cacheState != state) {
			StateFunction(layerMask, LH, "LH", 1f, "AU", "LH", "Ld", AU, LH, Ld);

			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "LH", LH);
				//StarLightsFunction(3,StarLightScaleStates.State.LightHour);
			}
		}
	}
	
	void LightDay() {
		CalculatePosition (Ld, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 12;
		if (_cacheState != state) {
			StateFunction(layerMask, Ld, "Ld", 1f, "LH", "Ld", "LY", LH, Ld, LY);

			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(true, "Ld", Ld);
				//StarLightsFunction(4,StarLightScaleStates.State.LightDay);
			}
		}
	}

	void LightYear() {
		CalculatePosition (LY, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 13;
		if (_cacheState != state) {
			StateFunction(layerMask, LY, "LY", 0f, "Ld", "LY", "PA", Ld, LY, PA);
			_cacheState = state;

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LY", LY);
			}
		}
	}

	void Parsec() {
		CalculatePosition (PA, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 14;
		if (_cacheState != state) {
			StateFunction(layerMask, PA, "PA", 0f, "LY", "PA", "LD", LY, PA, LD);
			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "PA", PA);
			}
		}
	}

	void LightDecade() {
		CalculatePosition (LD, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 15;
		if (_cacheState != state) {
			StateFunction(layerMask, LD, "LD", 0f, "PA", "LD", "LC", PA, LD, LC);
			_cacheState = state;

			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LD", LD);
			}
		}
	}

	void LightCentury() {
		CalculatePosition (LC, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 16;
		if (_cacheState != state) {
			StateFunction(layerMask, LC, "LC", 0f, "LD", "LC", "LM", LD, LC, LM);
			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LC", LC);
			}
		}
	}


	void LightMillenium() {
		CalculatePosition (LM, positionProcessingScript.position, positioningScript.camPosition);
		layerMask = 17;
		if (_cacheState != state) {
			StateFunction(layerMask, LM, "LM", 0f, "LC", "LM", "", LC, LM, 0d);
			_cacheState = state;
		
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
				Lights(false, "LM", LM);
			}
		}
	}


	void StateFunction(int layerMask, double scaleD, string scaleS, float meshScale, string beforeS, string currentS, string afterS, double beforeD, double currentD, double afterD) {
		gameObject.layer = layerMask;													// Set the layer, by number, to the appropriate layer mask
		if(meshes) meshes.layer = layerMask;											// Set the layer, by number, to the appropriate layer mask
		CalculateLocalScale (scaleD);													// Calculate the gameObject scale based on original scale and the scale of this State
		if(meshes) MeshScale(meshScale);												// If there's any items in the meshes variable, adjust the local scale appropriately
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
		//starLightScalesStatesScript[index].state = state;
	}

	/*
	 * We may need to rescale the proximity collider children of a body.
	 * The localColliders children rescale locally so that a smaller body
	 * gets a smaller collider.  The systemColliders are a set size which
	 * does not depend on the size of the body it's a child of.  This way
	 * we should get consistent results for long-distance speeds and 
	 * relevent speeds when we're much closer to a body
	 */
	private void MeshScale(float scale) {
		// Get the ratio of old scale to new so we can adjust the static-sized colliders
		Vector3d adjustedLocalScale = new Vector3d(scale, scale, scale);

		meshes.transform.localScale = V3dToV3 (adjustedLocalScale);
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
}
