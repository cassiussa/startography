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
	Vector3d prevLocalScale = new Vector3d (1d, 1d, 1d);
	double localScaleRatio = 0;

	int layerMask;

	// If it contains a light, there's a number of things we'll need to do.  For now, create the variables
	Light light;
	float lightRange;
	Dictionary<string, GameObject> lightGameObjects = new Dictionary<string, GameObject>();
	Dictionary<string, Light> lights = new Dictionary<string, Light>();


	PositionProcessing positionProcessingScript;
	Positioning positioningScript;

	Transform localColliders;
	Transform systemColliders;

	public List<GameObject> meshes;
	public bool isSun = false;
	///[HideInInspector]
	public bool hittingCamera = false;


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
		if(!positioningScript)
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
			scaleStateParent.Add (inputs[i], GameObject.Find (scaleStatesParents.name + inputs[i]).transform);
		}

		/* 
		 * Create a series of lights as this gameObject contains a light.  We need to create one
		 * for each of the the smallest 5 states.  We can then position each light where it would
		 * be represented in that state's space and cast light on the gameObjects within the
		 * layer.
		 */
		light = GetComponent<Light>();
		if(light) {
			layerMask = 8;																		// 8 is the lowest layer we can manually create or edit
			lightRange = light.range;															// cache the original light Range
			for(int i=0;i<5;i++) {																// Limit to the 5 smallest scales.  Anything beyond that would be crazy
				lightGameObjects.Add (inputs[i],new GameObject("Light - "+gameObject.name));	// Create empty gameObjects on the fly and reference them in the Dictionary
				GameObject lightGameObject = lightGameObjects[inputs[i]];						// Create a variable for this GameObject for faster processing
				lightGameObject.transform.parent = scaleStateParent[inputs[i]];					// Set this gameObject's parent to the appropriate scale's gameObject container
				double thisMeasurement = measurements[i];										// Cache the measurement for this iteration to save processing
				Vector3d thisPosition = new Vector3d(											// Set the initial position of the new light gameObjects
					((System.Math.Abs(positionProcessingScript.position.x)/thisMeasurement)*maxUnits),
					((System.Math.Abs(positionProcessingScript.position.y)/thisMeasurement)*maxUnits),
					((System.Math.Abs(positionProcessingScript.position.z)/thisMeasurement)*maxUnits));
				lightGameObject.transform.position = V3dToV3(thisPosition);
				lightGameObject.layer = i+layerMask;											// Set the layer.  Note that 8 is the lowest layer we've made
				lights.Add (inputs[i],lightGameObject.AddComponent<Light>());					// Add the Light component to the gameObjects
				float calculatedRange = (float)((light.range/measurements[i]) * maxUnits);		// Range of the light depending on State
				lights[inputs[i]].range = calculatedRange;										// Copy the light's Range from the original light's Range
				lights[inputs[i]].intensity = light.intensity;									// as well as the light's intensity
				lights[inputs[i]].color = light.color;											// and the light's colour
				lights[inputs[i]].cullingMask = 1 << i+layerMask;								// Now set the culling mask for the light
			}
		}

		localColliders = transform.Find ("LocalColliders");
		systemColliders = transform.Find ("SystemColliders");
		if (localColliders)
			if(!systemColliders)
				Debug.LogWarning ("There doesn't appear to be a systemColliders gameObject, but there is a localColliders.  Usually when there's a localColliders there should also be a systemColliders gameObject.", gameObject);
	
		meshes.Add (gameObject);
		if (gameObject.renderer)
			meshes.Add (gameObject);
		foreach (Transform child in transform) {
			if (child.gameObject.renderer)
				meshes.Add (child.gameObject);
		}
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			//if (_cacheState != state) {	// Commented out because we need to run state every frame
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
			//}
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

		if (hittingCamera == false && state != thisScale) {
			//Debug.LogError ("switch to LightHour");
			SetState (State.LightHour);
		}

		if (state != thisScale)																// Only perform the state transition if we're not already in the same state
			SetState (thisScale);															// Assign the scale that was determined by distance from origin Vector3(0,0,0)


	}



	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	void SubMillion() {															// This State is heavily commented as each other state uses same conditions
		layerMask = 8;															// Set the index of the layer this State uses										

		CalculatePosition (SM, positionProcessingScript.position, positioningScript.camPosition);	// Calculate the relative position based on real position and scale of this State
		if (_cacheState != state) {												// Without this we get crazy bugs.  Don't know why.  It needs to be here for code efficiency anyways!
			foreach (GameObject mesh in meshes) {								// Iterate through the array of child gameObjects with mesh renderers attached
				mesh.layer = layerMask;											// Set the layer that this scale State resides in
			}
			CalculateLocalScale(SM);											// Calculate the gameObject scale based on original scale and the scale of this State
			ColliderScale();
			inputsRevised = new string[] { "SM", "MK" };						// Specify only the scale States immediately surrounding this state so we can keep loop to minimum as there
			measurements = new double[] { SM, MK };								// is no point looping through every possible state since - we can only jump up or down one state at a time

			gameObject.transform.parent = scaleStateParent ["SM"];				// Set this gameObject's parent to the appropriate scale's gameObject container
			_cacheState = state;
		}

		if (light) {															// Check if this gameObject is, or contains, a light
			light.cullingMask = 1 << layerMask;									// set the culling mask to use the Nth layer
			float calculatedRange = (float)((lightRange/SM) * maxUnits);		// Range of the light depending on State
			light.range = calculatedRange;										// Set the light's Range for the original light
			light.enabled = true;												// If this gameObject contains a light then enable it for this State
			Lights("SM");														// Activate or deactivate the lights, depending on state
		}
	}

	void MillionKilometers() {
		layerMask = 9;

		CalculatePosition (MK, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale(MK);
			ColliderScale();
			inputsRevised = new string[] { "SM", "MK", "AU" };
			measurements = new double[] { SM, MK, AU };
			gameObject.transform.parent = scaleStateParent ["MK"];
			_cacheState = state;
		}

		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/MK) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
			Lights("MK");
		}
	}
	
	void AstronomicalUnit() {
		layerMask = 10;

		CalculatePosition (AU, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale(AU);
			//transform.localScale = new Vector3 (transform.localScale.x * 4, transform.localScale.y * 4, transform.localScale.z * 4);
			ColliderScale();
			inputsRevised = new string[] {  "MK", "AU", "LH" };
			measurements = new double[] { MK, AU, LH };
			gameObject.transform.parent = scaleStateParent ["AU"];
			_cacheState = state;
		}
		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/AU) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
			Lights("AU");
		}


	}
	
	void LightHour() {
		layerMask = 11;

		CalculatePosition (LH, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
					/*if (isSun == false) {
						if (_prevState == State.AstronomicalUnit)
							mesh.transform.localScale = new Vector3(1200f,1200f,1200f);
						else if(_prevState == State.LightDay)
							mesh.transform.localScale =  new Vector3(1f,1f,1f);
					}*/
				
			}
			CalculateLocalScale (LH);
			//transform.localScale = new Vector3 (transform.localScale.x * 50, transform.localScale.y * 50, transform.localScale.z * 50);
			ColliderScale();
			inputsRevised = new string[] { "AU", "LH", "Ld" };
			measurements = new double[] { AU, LH, Ld };
			gameObject.transform.parent = scaleStateParent ["LH"];
			_cacheState = state;
		}
		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/LH) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
			Lights("LH");
		}

	}
	
	void LightDay() {
		layerMask = 12;

		CalculatePosition (Ld, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (Ld);
			//transform.localScale = new Vector3 (transform.localScale.x * 10, transform.localScale.y * 10, transform.localScale.z * 10);
			ColliderScale();
			inputsRevised = new string[] { "LH", "Ld", "LY"};
			measurements = new double[] { LH, Ld, LY };
			gameObject.transform.parent = scaleStateParent ["Ld"];
			_cacheState = state;
		}

		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/LH) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
			Lights("Ld");
		}

	}

	void LightYear() {
		layerMask = 13;

		CalculatePosition (LY, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (LY);
			ColliderScale();
			inputsRevised = new string[] { "Ld", "LY", "PA" };
			measurements = new double[] { Ld, LY, PA };
			gameObject.transform.parent = scaleStateParent ["LY"];
			_cacheState = state;
		}
		if (light)
			light.enabled = false;
	}

	void Parsec() {
		layerMask = 14;

		CalculatePosition (PA, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (PA);
			ColliderScale();
			inputsRevised = new string[] { "LY", "PA", "LD" };
			measurements = new double[] { LY, PA, LD };
			gameObject.transform.parent = scaleStateParent ["PA"];
			_cacheState = state;
		}
		if (light)
			light.enabled = false;
	}

	void LightDecade() {
		layerMask = 15;

		CalculatePosition (LD, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (LD);
			ColliderScale();
			inputsRevised = new string[] { "PA", "LD", "LC" };
			measurements = new double[] { PA, LD, LC };
			gameObject.transform.parent = scaleStateParent ["LD"];
			_cacheState = state;
		}
		if (light)
			light.enabled = false;
	}

	void LightCentury() {
		layerMask = 16;

		CalculatePosition (LC, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (LC);
			ColliderScale();
			inputsRevised = new string[] { "LD", "LC", "LM" };
			measurements = new double[] { LD, LC, LM };
			gameObject.transform.parent = scaleStateParent ["LC"];
			_cacheState = state;
		}
		if (light)
			light.enabled = false;
	}

	void LightMillenium() {
		layerMask = 17;

		CalculatePosition (LM, positionProcessingScript.position, positioningScript.camPosition);
		if (_cacheState != state) {
			foreach (GameObject mesh in meshes) {
				mesh.layer = layerMask;
			}
			CalculateLocalScale (LM);
			ColliderScale();
			inputsRevised = new string[] { "LC", "LM" };
			measurements = new double[] { LC, LM };
			gameObject.transform.parent = scaleStateParent ["LM"];
			_cacheState = state;
		}
		if (light)
			light.enabled = false;
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
		prevLocalScale = V3ToV3d(gameObject.transform.localScale);
		gameObject.transform.localScale = new Vector3 (
			(float)((originalLocalScale.x / value) * maxUnits),
			(float)((originalLocalScale.y / value) * maxUnits),
			(float)((originalLocalScale.z / value) * maxUnits));
	}

	/*
	 * We may need to rescale the proximity collider children of a body.
	 * The localColliders children rescale locally so that a smaller body
	 * gets a smaller collider.  The systemColliders are a set size which
	 * does not depend on the size of the body it's a child of.  This way
	 * we should get consistent results for long-distance speeds and 
	 * relevent speeds when we're much closer to a body
	 */
	private void ColliderScale() {
		if (localColliders) {
			/*transform.localScale = new Vector3 (
				transform.localScale.x * 7500f, 
				transform.localScale.y * 7500f, 
				transform.localScale.z * 7500f);

			localColliders.localScale = new Vector3 (
				localColliders.localScale.x / 7500f, 
				localColliders.localScale.y / 7500f, 
				localColliders.localScale.z / 7500f);*/
		}
		// Get the ratio of old scale to new so we can adjust the static-sized colliders
		if (systemColliders) {
			//Debug.LogError ("orig: "+prevLocalScale.x+", cur: "+transform.localScale.x); 
			Vector3d newLocalScale = new Vector3d(
				prevLocalScale.x/transform.localScale.x, 
				prevLocalScale.y/transform.localScale.y, 
				prevLocalScale.z/transform.localScale.z);
			
			systemColliders.localScale = V3dToV3 (newLocalScale);
		}
	}

	/*
	 * This function iterates through the 5 smallest States and for each one
	 * it will either enable or disable the auto-generated light.  If the original
	 * light is in the current State, then the auto-generated light will be 
	 * disabled, and vice versa.  This way we ensure that we have consistent lighting
	 * on any given body as it goes from one state to the next.
	 */
	void Lights(string value) {
		for(int i=0;i<5;i++) {
			if(inputs[i] != value)
				lightGameObjects[inputs[i]].SetActive(true);
			else
				lightGameObjects[inputs[i]].SetActive(false);
		}
	}
}
