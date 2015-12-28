using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ScaleStates : Functions {
	public enum State { Initialize, SubMillion, MillionKilometers, AstronomicalUnit, LightHour, LightDay, LightYear, Parsec, LightDecade, LightCentury, LightMillenium }

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
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
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
	

	void SubMillion() {
		gameObject.layer = 8;
		float x = (float)(positionProcessingScript.position.x * (10000d / MK));
		float y = (float)(positionProcessingScript.position.y * (10000d / MK));
		float z = (float)(positionProcessingScript.position.z * (10000d / MK));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void MillionKilometers() {
		gameObject.layer = 8;
		float x = (float)(positionProcessingScript.position.x * (10000d / AU));
		float y = (float)(positionProcessingScript.position.y * (10000d / AU));
		float z = (float)(positionProcessingScript.position.z * (10000d / AU));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}
	
	void AstronomicalUnit() {
		gameObject.layer = 9;
		float x = (float)(positionProcessingScript.position.x * (10000d / LH));
		float y = (float)(positionProcessingScript.position.y * (10000d / LH));
		float z = (float)(positionProcessingScript.position.z * (10000d / LH));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}
	
	void LightHour() {
		gameObject.layer = 10;
		float x = (float)(positionProcessingScript.position.x * (10000d / Ld));
		float y = (float)(positionProcessingScript.position.y * (10000d / Ld));
		float z = (float)(positionProcessingScript.position.z * (10000d / Ld));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}
	
	void LightDay() {
		gameObject.layer = 11;
		float x = (float)(positionProcessingScript.position.x * (10000d / LY));
		float y = (float)(positionProcessingScript.position.y * (10000d / LY));
		float z = (float)(positionProcessingScript.position.z * (10000d / LY));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void LightYear() {
		gameObject.layer = 12;
		float x = (float)(positionProcessingScript.position.x * (10000d / PA));
		float y = (float)(positionProcessingScript.position.y * (10000d / PA));
		float z = (float)(positionProcessingScript.position.z * (10000d / PA));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void Parsec() {
		gameObject.layer = 13;
		float x = (float)(positionProcessingScript.position.x * (10000d / LD));
		float y = (float)(positionProcessingScript.position.y * (10000d / LD));
		float z = (float)(positionProcessingScript.position.z * (10000d / LD));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void LightDecade() {
		gameObject.layer = 14;
		float x = (float)(positionProcessingScript.position.x * (10000d / LC));
		float y = (float)(positionProcessingScript.position.y * (10000d / LC));
		float z = (float)(positionProcessingScript.position.z * (10000d / LC));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void LightCentury() {
		gameObject.layer = 15;
		float x = (float)(positionProcessingScript.position.x * (10000d / LM));
		float y = (float)(positionProcessingScript.position.y * (10000d / LM));
		float z = (float)(positionProcessingScript.position.z * (10000d / LM));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}

	void LightMillenium() {
		gameObject.layer = 16;
		float x = (float)(positionProcessingScript.position.x * (10000d / LDM));
		float y = (float)(positionProcessingScript.position.y * (10000d / LDM));
		float z = (float)(positionProcessingScript.position.z * (10000d / LDM));
		Debug.Log ("original x = " + positionProcessingScript.position.x + ", recalculated = " + x);
		transform.position = new Vector3 (x, y, z);
		//_cacheState = state;
	}
}
