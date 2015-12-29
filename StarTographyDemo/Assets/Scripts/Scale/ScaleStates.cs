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


	Vector3 originalLocalScale;
	double localScaleRatio = 0;

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

		originalLocalScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
		Debug.LogError ("originalLocalScale = " + originalLocalScale);
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
		CalculatePosition (MK, positionProcessingScript.position);
		CalculateLocalScale(MK);
		//_cacheState = state;
	}

	void MillionKilometers() {
		gameObject.layer = 9;
		CalculatePosition (AU, positionProcessingScript.position);
		CalculateLocalScale(AU);
		//_cacheState = state;
	}
	
	void AstronomicalUnit() {
		gameObject.layer = 10;
		CalculatePosition (LH, positionProcessingScript.position);
		CalculateLocalScale(LH);
		//_cacheState = state;
	}
	
	void LightHour() {
		gameObject.layer = 11;
		CalculatePosition (Ld, positionProcessingScript.position);
		CalculateLocalScale(Ld);
		//_cacheState = state;
	}
	
	void LightDay() {
		gameObject.layer = 12;
		CalculatePosition (LY, positionProcessingScript.position);
		CalculateLocalScale(LY);
		//_cacheState = state;
	}

	void LightYear() {
		gameObject.layer = 13;
		CalculatePosition (PA, positionProcessingScript.position);
		CalculateLocalScale(PA);
		//_cacheState = state;
	}

	void Parsec() {
		gameObject.layer = 14;
		CalculatePosition (LD, positionProcessingScript.position);
		CalculateLocalScale(LD);
		//_cacheState = state;
	}

	void LightDecade() {
		gameObject.layer = 15;
		CalculatePosition (LC, positionProcessingScript.position);
		CalculateLocalScale(LC);
		//_cacheState = state;
	}

	void LightCentury() {
		gameObject.layer = 16;
		CalculatePosition (LM, positionProcessingScript.position);
		CalculateLocalScale(LM);
		//_cacheState = state;
	}

	void LightMillenium() {
		gameObject.layer = 17;
		CalculatePosition (LDM, positionProcessingScript.position);
		CalculateLocalScale(LDM);
		//_cacheState = state;
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
	 * 
	 * Still needs to be fixed as conditional is true until the scale is first
	 * changed.
	 */
	private void CalculateLocalScale(double value) {
		if (_prevState != state) {
			Debug.Log("originalLocalScale = "+originalLocalScale+", value = "+value);
			localScaleRatio = (MK / value);// / maxUnits;
			Debug.Log("localScaleRatio = "+localScaleRatio);
			gameObject.transform.localScale = new Vector3 ((float)(originalLocalScale.x*localScaleRatio),
			                                               (float)(originalLocalScale.y*localScaleRatio),
			                                               (float)(originalLocalScale.z*localScaleRatio));
		}
	}
}
