using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class StarLightScaleStates : Functions {

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
	public State thisScaleState;
	
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
	float lightRange;
	//Dictionary<string, GameObject> lightGameObjects = new Dictionary<string, GameObject>();
	GameObject[] lightGameObjectsArray;
	Dictionary<string, Light> lights = new Dictionary<string, Light>();
	
	PositionProcessing positionProcessingScript;
	public PositionProcessing starPositionProcessingScript;
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
			scaleStateParent.Add (inputs [i], GameObject.Find (scaleStatesParents.name +"/"+ inputs [i]).transform);
		}
		
		
		
		/* 
		 * Create a series of lights as this gameObject contains a light.  We need to create one
		 * for each of the the smallest 5 states.  We can then position each light where it would
		 * be represented in that state's space and cast light on the gameObjects within the
		 * layer.
		 */
		objectDataScript = GetComponent<ObjectData> ();
		
		starLight = GetComponent<Light> ();															// Add the Light component if there is one
		
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
		positionProcessingScript.position = starPositionProcessingScript.position;					// Pull the real position from the star that generated this light and apply it every frame

		if (state != thisScaleState) {																// Only perform the state transition if we're not already in the same state
			SetState (thisScaleState);																// Assign the scale that was determined by distance from origin Vector3(0,0,0)
		}
	}
	
	
	
	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	
	
	void SubMillion() {																				// This State is heavily commented as each other state uses same conditions
		CalculatePosition (SM, positionProcessingScript.position, Functions.camPosition);	// Calculate the relative position based on real position and scale of this State
		layerMask = 8;
		if (_cacheState != state) {																	// Without this we get crazy bugs.  Don't know why.  It needs to be here for code efficiency anyways!						
			_cacheState = state;
		}
	}
	
	void MillionKilometers() {
		CalculatePosition (MK, positionProcessingScript.position, Functions.camPosition);
		layerMask = 9;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void AstronomicalUnit() {
		CalculatePosition (AU, positionProcessingScript.position, Functions.camPosition);
		layerMask = 10;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void LightHour() {
		CalculatePosition (LH, positionProcessingScript.position, Functions.camPosition);
		layerMask = 11;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void LightDay() {
		CalculatePosition (Ld, positionProcessingScript.position, Functions.camPosition);
		layerMask = 12;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void LightYear() {
		CalculatePosition (LY, positionProcessingScript.position, Functions.camPosition);
		layerMask = 13;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void Parsec() {
		CalculatePosition (PA, positionProcessingScript.position, Functions.camPosition);
		layerMask = 14;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void LightDecade() {
		CalculatePosition (LD, positionProcessingScript.position, Functions.camPosition);
		layerMask = 15;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	void LightCentury() {
		CalculatePosition (LC, positionProcessingScript.position, Functions.camPosition);
		layerMask = 16;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	
	
	void LightMillenium() {
		CalculatePosition (LM, positionProcessingScript.position, Functions.camPosition);
		layerMask = 17;
		if (_cacheState != state) {
			_cacheState = state;
		}
	}
	

	
}

