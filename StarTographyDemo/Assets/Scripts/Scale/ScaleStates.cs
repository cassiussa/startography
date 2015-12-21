using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class ScaleStates : DataFunctions {
	public enum State { Initialize, DetermineState, MillionKilometers, AstronomicalUnit, LightHour, LightDay, LightYear, Parsec, LightDecade, LightCentury, LightMillenium }

	public State state = State.Initialize;
	State _prevState;
	State _cacheState;
	

	Dictionary<string, State> scales = new Dictionary<string, State>();	// Allows us to convert string as variable names
	string[] inputs;		// Array of strings of distance types
	double[] measurements;	// Array of the measurements of the distance types

	Dictionary<double, double> scaleBoundaries = new Dictionary<double, double>();	// Contains the upper and lower boundary for each scale

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
		SetState(State.DetermineState);

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

		scaleBoundaries.Add (MK,0);
		scaleBoundaries.Add (1, LH/AU);
		Debug.Log ("bounds = "+scaleBoundaries [1]);

	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
				case State.Initialize:
					break;
				case State.DetermineState:
					DetermineState ();
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
		State thisScale = State.DetermineState;

		/* Count down instead of up because the vast majority of objects will 
		 * be closer to LightMillenium, LightCentury and LightDecade
		 * than they will be to millions of MillionKilometers
		*/
		for (int i=measurements.Length-1; i >= 0; i--) {
			if (transform.position.x > System.Math.Abs(measurements[i]) || 
			    transform.position.y > measurements[i] || 
			    transform.position.z > measurements[i]) {
				thisScale = scales[inputs[i]];
				break;	// Break the loop as soon as we've found the scale
			}
		}
		SetState(thisScale);	// Assign the scale that was determined by distance from origin Vector3(0,0,0)
	}

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}



	void DetermineState() {
		State thisScale = State.DetermineState;

		/* Count down instead of up because the vast majority of objects will 
		 * be closer to LightMillenium, LightCentury and LightDecade
		 * than they will be to millions of MillionKilometers
		*/
		int i = measurements.Length - 1;
		for (i=measurements.Length-1; i >= 0; i--) {
			if (transform.position.x > System.Math.Abs(measurements[i]) || 
								transform.position.y > measurements[i] || 
								transform.position.z > measurements[i]) {
				thisScale = scales[inputs[i]];
				break;	// Break the loop as soon as we've found the scale
			}
		}

		Debug.LogError(transform.position.x + "KM = "+ conDist (transform.position.x, "MK", inputs [i]) + " " +inputs[i] );
		SetState(thisScale);
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
