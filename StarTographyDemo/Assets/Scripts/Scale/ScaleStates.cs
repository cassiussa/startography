using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ScaleStates : Functions {
	public enum State { Initialize, MillionKilometers, AstronomicalUnit, LightHour, LightDay, LightYear, Parsec, LightDecade, LightCentury, LightMillenium }

	public State state = State.Initialize;
	State _prevState;
	State _cacheState;

	/* Check to see what type of item this script is attached to.  For example,
	 * if it's a camera, we'll update the clipping upon state change.  If it's
	 * something else, like a planet or star, we'll change the scale and position
	 * to the appropriate location and size.
	 */

	Dictionary<string, State> scales = new Dictionary<string, State>();	// Allows us to convert string as variable names
	string[] inputs;		// Array of strings of distance types
	double[] measurements;	// Array of the measurements of the distance types
	public State thisScale;

	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	

	PositionProcessing positionProcessingScript;

	void Awake() {
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

		positionProcessingScript = GetComponent<PositionProcessing> ();
		if (!positionProcessingScript)
			Debug.LogError ("The PositionProcessing script appears to be missing", gameObject);
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
			if (System.Math.Abs(positionProcessingScript.position.x) > System.Math.Abs(measurements[i]) || 
			    System.Math.Abs(positionProcessingScript.position.y) > System.Math.Abs(measurements[i]) || 
			    System.Math.Abs(positionProcessingScript.position.z) > System.Math.Abs(measurements[i])) {
				thisScale = scales[inputs[i]];
				break;	// Break the loop as soon as we've found the scale
			} else {
				thisScale = State.MillionKilometers;
			}
		}
		if (state != thisScale)				// Only perform the state transition if we're not already in the same state
			SetState (thisScale);			// Assign the scale that was determined by distance from origin Vector3(0,0,0)
	}



	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	void MillionKilometers() {
		float x = (float)(positionProcessingScript.position.x * (10000d / 1000000d));
		float y = (float)(positionProcessingScript.position.y * (10000d / 1000000d));
		float z = (float)(positionProcessingScript.position.z * (10000d / 1000000d));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}
	
	void AstronomicalUnit() {
		float x = (float)(positionProcessingScript.position.x * (10000d / 149597870.7d));
		float y = (float)(positionProcessingScript.position.y * (10000d / 149597870.7d));
		float z = (float)(positionProcessingScript.position.z * (10000d / 149597870.7d));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
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
