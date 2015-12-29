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
	Vector3 originalLocalScale;
	double localScaleRatio = 0;

	int layerMask;

	// If it contains a light, there's a number of things we'll need to do.  For now, create the variables
	Light light;
	float lightRange;
	Dictionary<string, GameObject> lightGameObjects = new Dictionary<string, GameObject>();
	Dictionary<string, Light> lights = new Dictionary<string, Light>();


	PositionProcessing positionProcessingScript;


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

		// Get the original localScale of the gameObject to use for reference later when rescaling based on State
		originalLocalScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);

		if (!scaleStatesParents)
			scaleStatesParents = GameObject.Find ("/ScaleStates");
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
			layerMask = 8;
			lightRange = light.range;		// cache the original light Range
			for(int i=0;i<5;i++) {	// Limit to the 5 smallest scales.  Anything beyond that would be crazy
				lightGameObjects.Add (inputs[i],new GameObject("Light - "+inputs[i]));			// Create empty gameObjects on the fly and reference them in the Dictionary
				lightGameObjects[inputs[i]].transform.parent = scaleStateParent[inputs[i]];		// Set this gameObject's parent to the appropriate scale's gameObject container
				lightGameObjects[inputs[i]].layer = i+layerMask;								// Set the layer.  Note that 8 is the lowest layer we've made
				lights.Add (inputs[i],lightGameObjects[inputs[i]].AddComponent<Light>());		// Add the Light component to the gameObjects
				float calculatedRange = (float)((light.range/measurements[i]) * maxUnits);		// Range of the light depending on State
				lights[inputs[i]].range = calculatedRange;										// Copy the light's Range from the original light's Range
				lights[inputs[i]].intensity = light.intensity;									// as well as the light's intensity
				lights[inputs[i]].color = light.color;											// and the light's colour
				lights[inputs[i]].cullingMask = 1 << i+layerMask;								// Now set the culling mask for the light
			}
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
		for (int i=measurements.Length-1; i >= 0; i--) {
			double thisMeasurement = System.Math.Abs(measurements[i]);	// Cache the value instead of calculating it for each comparison
			if (System.Math.Abs(positionProcessingScript.position.x) >= thisMeasurement || 
			    System.Math.Abs(positionProcessingScript.position.y) >= thisMeasurement || 
			    System.Math.Abs(positionProcessingScript.position.z) >= thisMeasurement) {
				thisScale = scales[inputsRevised[i]];					// inputsRevised[i] is a string that is a key for the scales dictionary
				break;													// Break the loop as soon as we've found the scale.  Continue with Update() function 
			}
		}
		if (state != thisScale)											// Only perform the state transition if we're not already in the same state
			SetState (thisScale);										// Assign the scale that was determined by distance from origin Vector3(0,0,0)
	}



	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	void SubMillion() {													// This State is heavily commented as each other state uses same conditions
		layerMask = 8;													// Set the index of the layer this State uses
		gameObject.layer = layerMask;									// Set the layer that this scale State resides in
		CalculatePosition (MK, positionProcessingScript.position);		// Calculate the relative position based on real position and scale of this State
		CalculateLocalScale(MK);										// Calculate the gameObject scale based on original scale and the scale of this State

		inputsRevised = new string[] { "SM", "MK" };					// Specify only the scale States immediately surrounding this state so we can keep loop to minimum as there
		measurements = new double[] { SM, MK };							// is no point looping through every possible state since - we can only jump up or down one state at a time

		gameObject.transform.parent = scaleStateParent["SM"];			// Set this gameObject's parent to the appropriate scale's gameObject container

		if (light) {													// Check if this gameObject is, or contains, a light
			light.cullingMask = 1 << layerMask;							// set the culling mask to use the Nth layer
			float calculatedRange = (float)((lightRange/SM) * maxUnits);	// Range of the light depending on State
			light.range = calculatedRange;								// Set the light's Range for the original light
			light.enabled = true;										// and if it does, then enable it for this State
			Lights("SM");
		}
	}

	void MillionKilometers() {
		layerMask = 9;
		gameObject.layer = layerMask;
		CalculatePosition (AU, positionProcessingScript.position);
		CalculateLocalScale(AU);
		inputsRevised = new string[] { "SM", "MK", "AU" };
		measurements = new double[] { SM, MK, AU };
		gameObject.transform.parent = scaleStateParent["MK"];

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
		gameObject.layer = layerMask;
		CalculatePosition (LH, positionProcessingScript.position);
		CalculateLocalScale(LH);
		inputsRevised = new string[] {  "MK", "AU", "LH" };
		measurements = new double[] { MK, AU, LH};
		gameObject.transform.parent = scaleStateParent["AU"];

		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/AU) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
		}
	}
	
	void LightHour() {
		layerMask = 11;
		gameObject.layer = layerMask;
		CalculatePosition (Ld, positionProcessingScript.position);
		CalculateLocalScale (Ld);
		inputsRevised = new string[] { "AU", "LH", "Ld" };
		measurements = new double[] { AU, LH, Ld };
		gameObject.transform.parent = scaleStateParent["LH"];

		if (light) {
			light.cullingMask = 1 << layerMask;
			float calculatedRange = (float)((lightRange/LH) * maxUnits);
			light.range = calculatedRange;
			light.enabled = true;
		}
	}
	
	void LightDay() {
		layerMask = 12;
		gameObject.layer = layerMask;
		CalculatePosition (LY, positionProcessingScript.position);
		CalculateLocalScale(LY);
		inputsRevised = new string[] { "LH", "Ld", "LY"};
		measurements = new double[] { LH, Ld, LY };
		gameObject.transform.parent = scaleStateParent["Ld"];

		if (light)
			light.enabled = false;
	}

	void LightYear() {
		layerMask = 13;
		gameObject.layer = layerMask;
		CalculatePosition (PA, positionProcessingScript.position);
		CalculateLocalScale(PA);
		inputsRevised = new string[] { "Ld", "LY", "PA" };
		measurements = new double[] { Ld, LY, PA };
		gameObject.transform.parent = scaleStateParent["LY"];

		if (light)
			light.enabled = false;
	}

	void Parsec() {
		layerMask = 14;
		gameObject.layer = layerMask;
		CalculatePosition (LD, positionProcessingScript.position);
		CalculateLocalScale(LD);
		inputsRevised = new string[] { "LY", "PA", "LD" };
		measurements = new double[] { LY, PA, LD };
		gameObject.transform.parent = scaleStateParent["PA"];

		if (light)
			light.enabled = false;
	}

	void LightDecade() {
		layerMask = 15;
		gameObject.layer = layerMask;
		CalculatePosition (LC, positionProcessingScript.position);
		CalculateLocalScale(LC);
		inputsRevised = new string[] { "PA", "LD", "LC" };
		measurements = new double[] { PA, LD, LC };
		gameObject.transform.parent = scaleStateParent["LD"];

		if (light)
			light.enabled = false;
	}

	void LightCentury() {
		layerMask = 16;
		gameObject.layer = layerMask;
		CalculatePosition (LM, positionProcessingScript.position);
		CalculateLocalScale(LM);
		inputsRevised = new string[] { "LD", "LC", "LM" };
		measurements = new double[] { LD, LC, LM };
		gameObject.transform.parent = scaleStateParent["LC"];

		if (light)
			light.enabled = false;
	}

	void LightMillenium() {
		layerMask = 17;
		gameObject.layer = layerMask;
		CalculatePosition (LDM, positionProcessingScript.position);
		CalculateLocalScale(LDM);
		inputsRevised = new string[] { "LC", "LM" };
		measurements = new double[] { LC, LM };
		gameObject.transform.parent = scaleStateParent["LM"];

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
		if (_cacheState != state) {
			_cacheState = state;
			localScaleRatio = (MK / value);// / maxUnits;
			gameObject.transform.localScale = new Vector3 ((float)(originalLocalScale.x*localScaleRatio),
			                                               (float)(originalLocalScale.y*localScaleRatio),
			                                               (float)(originalLocalScale.z*localScaleRatio));
		}
	}


	/*
	 * This function iterates through the 5 smallest States and for each one
	 * it will either enable or disable the auto-generated light.  If the original
	 * light is in the current State, then the auto-generated light will be 
	 * disabled, and vice versa.
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
