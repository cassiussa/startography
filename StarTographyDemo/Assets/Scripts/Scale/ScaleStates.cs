using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ScaleStates : DataFunctions {
	public enum State { Initialize, MillionKilometers, AstronomicalUnit, LightHour, LightDay, LightYear, Parsec, LightDecade, LightCentury, LightMillenium }

	public State state = State.Initialize;
	State _prevState;
	State _cacheState;

	/* Check to see what type of item this script is attached to.  For example,
	 * if it's a camera, we'll update the clipping upon state change.  If it's
	 * something else, like a planet or star, we'll change the scale and position
	 * to the appropriate location and size.
	 */
	//Vector3d position;
	Vector3d curDubPos = new Vector3d(0d,0d,0d);	// This should be updated in Awake, based on input data
	Vector3d curScale = new Vector3d (1d, 1d, 1d);	// The scale of the object, based on input data
	public StarData starDataScript;

	Dictionary<string, State> scales = new Dictionary<string, State>();	// Allows us to convert string as variable names
	string[] inputs;		// Array of strings of distance types
	double[] measurements;	// Array of the measurements of the distance types
	State thisScale;

	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	
	void Awake() {
		// Do any general system initialization stuff here.
		//SetState(State.DetermineState);

		// A list of strings we can perform conditionals on and then assign a state
		scales.Add ("MK", State.MillionKilometers);
		scales.Add ("AU", State.AstronomicalUnit);
		scales.Add ("LH", State.LightHour);
		scales.Add ("Ld", State.LightDay);
		scales.Add ("LY", State.LightYear);
		scales.Add ("PA", State.Parsec);
		scales.Add ("LD", State.LightDecade);
		scales.Add ("LC", State.LightCentury);
		scales.Add ("LM", State.LightMillenium);

		inputs = new string[] { "MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		measurements = new double[] { MK, AU, LH, Ld, LY, PA, LD, LC, LM };

		/* Assign the types values to the below variables.  Doing it this
		 * way will help us to save on resources, so we can use booleans
		 * wherever possible.
		 */

		//position = new Vector3d (0d, 0d, 0d);
		if (!starDataScript)
			Debug.LogError ("There is no StarData script assigned.", gameObject);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
				case State.Initialize:
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
			}
			yield return null;
		}
	}



	void Update() {
		/* Count down instead of up because the vast majority of objects will 
		 * be closer to LightMillenium, LightCentury and LightDecade
		 * than they will be to millions of MillionKilometers
		*/
		for (int i=measurements.Length-1; i >= 0; i--) {
			if (System.Math.Abs(transform.position.x) > System.Math.Abs(measurements[i]) || 
			    System.Math.Abs(transform.position.y) > System.Math.Abs(measurements[i]) || 
			    System.Math.Abs(transform.position.z) > System.Math.Abs(measurements[i])) {
				thisScale = scales[inputs[i]];
				break;	// Break the loop as soon as we've found the scale
			}
		}

		if (state != thisScale)		// Only perform the state transition if we're not already in the same state
			SetState (thisScale);	// Assign the scale that was determined by distance from origin Vector3(0,0,0)

		curDubPos = new Vector3d(curDubPos.x+100000, curDubPos.y, curDubPos.z);
		Vector3 newPosition = V3dToV3 (curDubPos);
		curScale = new Vector3d (curScale.x, curScale.y, curScale.z);
		Vector3 scale = ScaledToScale (curScale);
		//transform.position = newPosition;
		//transform.scale = scale;
	}

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	void MillionKilometers() {
		_cacheState = state;
	}
	
	void AstronomicalUnit() {
		_cacheState = state;
	}
	
	void LightHour() {
		_cacheState = state;
	}
	
	void LightDay() {
		_cacheState = state;
	}

	void LightYear() {
		_cacheState = state;
	}

	void Parsec() {
		_cacheState = state;
	}

	void LightDecade() {
		_cacheState = state;
	}

	void LightCentury() {
		_cacheState = state;
	}

	void LightMillenium() {
		_cacheState = state;
	}
}
