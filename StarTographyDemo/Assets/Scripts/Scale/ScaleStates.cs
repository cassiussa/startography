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
	double[] measurements;	// Array of the measurements of the distance types
	public State thisScale;

	// These are used for assigning the container gameObjects upon change in States
	GameObject scaleStatesParents;
	Dictionary<string, Transform> scaleStateParent = new Dictionary<string, Transform>();

	// For knowing what localScale to set for a gameObject in any given State
	Vector3 originalLocalScale;
	double localScaleRatio = 0;

	// If it contains a light
	Light light;
	Dictionary<string, GameObject> generatedLightGameObjects = new Dictionary<string, GameObject>();
	Dictionary<string, Light> generatedLights = new Dictionary<string, Light>();

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
			for(int i=0;i<5;i++) {
				generatedLightGameObjects.Add (inputs[i],new GameObject(inputs[i]+" Light"));
				generatedLights.Add (inputs[i],generatedLightGameObjects[inputs[i]].AddComponent<Light>());
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
				thisScale = scales[inputs[i]];							// inputs[i] is a string that is a key for the scales dictionary
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
		gameObject.layer = 8;											// Set the layer that this scale State resides in
		CalculatePosition (MK, positionProcessingScript.position);		// Calculate the relative position based on real position and scale of this State
		CalculateLocalScale(MK);										// Calculate the gameObject scale based on original scale and the scale of this State

		inputs = new string[] { "SM", "MK" };							// Specify only the scale States immediately surrounding this state so we can keep loop to minimum as there
		measurements = new double[] { SM, MK };							// is no point looping through every possible state since - we can only jump up or down one state at a time

		gameObject.transform.parent = scaleStateParent["SM"];			// Set this gameObject's parent to the appropriate scale's gameObject container

		if (light) {													// Check if this gameObject is, or contains, a light
			light.cullingMask = 1 << 8;									// set the culling mask to use the Nth layer
			light.enabled = true;										// and if it does, then enable it for this State
		}
	}

	void MillionKilometers() {
		gameObject.layer = 9;
		CalculatePosition (AU, positionProcessingScript.position);
		CalculateLocalScale(AU);
		inputs = new string[] { "SM", "MK", "AU" };
		measurements = new double[] { SM, MK, AU };
		gameObject.transform.parent = scaleStateParent["MK"];

		if (light) {
			light.cullingMask = 1 << 9;
			light.enabled = true;
		}
	}
	
	void AstronomicalUnit() {
		gameObject.layer = 10;
		CalculatePosition (LH, positionProcessingScript.position);
		CalculateLocalScale(LH);
		inputs = new string[] {  "MK", "AU", "LH" };
		measurements = new double[] { MK, AU, LH};
		gameObject.transform.parent = scaleStateParent["AU"];

		if (light) {
			light.cullingMask = 1 << 10;
			light.enabled = true;
		}
	}
	
	void LightHour() {
		gameObject.layer = 11;
		CalculatePosition (Ld, positionProcessingScript.position);
		CalculateLocalScale (Ld);
		inputs = new string[] { "AU", "LH", "Ld" };
		measurements = new double[] { AU, LH, Ld };
		gameObject.transform.parent = scaleStateParent["LH"];

		if (light) {
			light.cullingMask = 1 << 11;
			light.enabled = true;
		}
	}
	
	void LightDay() {
		gameObject.layer = 12;
		CalculatePosition (LY, positionProcessingScript.position);
		CalculateLocalScale(LY);
		inputs = new string[] { "LH", "Ld", "LY"};
		measurements = new double[] { LH, Ld, LY };
		gameObject.transform.parent = scaleStateParent["Ld"];

		if (light)
			light.enabled = false;
	}

	void LightYear() {
		gameObject.layer = 13;
		CalculatePosition (PA, positionProcessingScript.position);
		CalculateLocalScale(PA);
		inputs = new string[] { "Ld", "LY", "PA" };
		measurements = new double[] { Ld, LY, PA };
		gameObject.transform.parent = scaleStateParent["LY"];

		if (light)
			light.enabled = false;
	}

	void Parsec() {
		gameObject.layer = 14;
		CalculatePosition (LD, positionProcessingScript.position);
		CalculateLocalScale(LD);
		inputs = new string[] { "LY", "PA", "LD" };
		measurements = new double[] { LY, PA, LD };
		gameObject.transform.parent = scaleStateParent["PA"];

		if (light)
			light.enabled = false;
	}

	void LightDecade() {
		gameObject.layer = 15;
		CalculatePosition (LC, positionProcessingScript.position);
		CalculateLocalScale(LC);
		inputs = new string[] { "PA", "LD", "LC" };
		measurements = new double[] { PA, LD, LC };
		gameObject.transform.parent = scaleStateParent["LD"];

		if (light)
			light.enabled = false;
	}

	void LightCentury() {
		gameObject.layer = 16;
		CalculatePosition (LM, positionProcessingScript.position);
		CalculateLocalScale(LM);
		inputs = new string[] { "LD", "LC", "LM" };
		measurements = new double[] { LD, LC, LM };
		gameObject.transform.parent = scaleStateParent["LC"];

		if (light)
			light.enabled = false;
	}

	void LightMillenium() {
		gameObject.layer = 17;
		CalculatePosition (LDM, positionProcessingScript.position);
		CalculateLocalScale(LDM);
		inputs = new string[] { "LC", "LM" };
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
}
