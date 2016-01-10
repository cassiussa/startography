using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSpeedStates : Functions {

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


	//public List<double> collidingNow = new List<double>();
	public Dictionary<double, State> distanceCheck = new Dictionary<double, State>();	// So that we can get values
	public Dictionary<Collider, double> currentCollisions = new Dictionary<Collider, double>();	// So that we can remove correct colliders

	public void OnScaleCollision() {
		double size = 10000000000000000000000000d;
		foreach (KeyValuePair<Collider, double> currentCollision in currentCollisions) {
			if(currentCollision.Value < size) {
				
				size = currentCollision.Value;
			}
		}
		Debug.Log ("currentCollisions = "+distanceCheck[size]);
		
		SetState (distanceCheck[size]);
	}




	/* 
	 * Check to see what type of item this script is attached to.  For example,
	 * if it's a camera, we'll update the clipping upon state change.  If it's
	 * something else, like a planet or star, we'll change the scale and position
	 * to the appropriate location and size.
	 */

	Positioning positionScript;
	
	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	void Awake() {
		positionScript = GetComponent<Positioning> ();
		if (!positionScript)
			Debug.LogError ("There is no Positioning script attached to this gameObject.  It is required", gameObject);

		/*
		 * I'm adding these in here so that we can do a comparison to
		 * the value held in the first field, see which the smallestCollision
		 * variable falls within, and then set the relevant state thereof.
		 */

		distanceCheck.Add (SM, State.SubMillion);
		distanceCheck.Add (MK, State.MillionKilometers);
		distanceCheck.Add (AU, State.AstronomicalUnit);
		distanceCheck.Add (LH, State.LightHour);
		distanceCheck.Add (Ld, State.LightDay);
		distanceCheck.Add (LY, State.LightYear);
		distanceCheck.Add (PA, State.Parsec);
		distanceCheck.Add (LD, State.LightDecade);
		distanceCheck.Add (LC, State.LightCentury);
		distanceCheck.Add (LM, State.LightMillenium);
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

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}



		/* Ok to fix the collider issues, I think!
 		 * 
 		 * we need to make sure that the distances, such as SM, MillionKilometers, AstronomicalUnit, etc aren't just 8xlast_collider_size.  They need
 		 * to be relevant so that we can do better maxTime and minTime
 		 * 
 		 * something = body radius * 2
 		 * ScaleState size / something = collider radius
 		 * 
		*/



	
	void SubMillion() {								// This State is heavily commented as each other state uses same conditions		
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 30d;
		positionScript.holdTimeMax = 300d;
		_cacheState = state;
	}

	void MillionKilometers() {
		//Debug.LogError ("Entered state " + state);
		if (_cacheState == State.SubMillion) {
			positionScript.holdTimeMin = 300d;
			positionScript.holdTimeMax = 30000d;				// Scale up to the min speed of the next state
		} else if (_cacheState == State.AstronomicalUnit) {		// Came from a faster state so slow it down
			positionScript.holdTimeMin = 300d;
			positionScript.holdTimeMax = 3000d;					// Scale down, likely to the same as min speed for this state
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}
		_cacheState = state;
	}

	void AstronomicalUnit() {	
		//Debug.LogError ("Entered state " + state);
		if (_cacheState == State.MillionKilometers) {
			positionScript.holdTimeMin = 30000d;
			positionScript.holdTimeMax = 3000000d;
		} else if (_cacheState == State.LightHour) {		// Came from a faster state so slow it down
			positionScript.holdTimeMin = 30000d;
			positionScript.holdTimeMax = 300000d;
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}

		_cacheState = state;
	}

	void LightHour() {	
		if (_cacheState == State.AstronomicalUnit) {
			positionScript.holdTimeMin = 3000000d;
			positionScript.holdTimeMax = 30000000d;				// Scale up to the min speed of the next state
		} else if (_cacheState == State.LightDay) {				// Came from a faster state so slow it down
			positionScript.holdTimeMin = 3000000d;
			positionScript.holdTimeMax = 3000000d;					// Scale down, likely to the same as min speed for this state
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}

		//positionScript.holdTimeMin = 3000000d;
		//positionScript.holdTimeMax = 30000000d;
		_cacheState = state;
	}

	void LightDay() {	
		if (_cacheState == State.LightHour) {
			positionScript.holdTimeMin = 30000000d;
			positionScript.holdTimeMax = 300000000d;				// Scale up to the min speed of the next state
		} else if (_cacheState == State.LightYear) {				// Came from a faster state so slow it down
			positionScript.holdTimeMin = 30000000d;
			positionScript.holdTimeMax = 30000000d;					// Scale down, likely to the same as min speed for this state
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}
		//positionScript.holdTimeMin = 30000000d;
		//positionScript.holdTimeMax = 300000000d;
		_cacheState = state;
	}

	void LightYear() {	
		if (_cacheState == State.LightHour) {
			positionScript.holdTimeMin = 300000000d;
			positionScript.holdTimeMax = 109500000000d;				// Scale up to the min speed of the next state
		} else if (_cacheState == State.LightYear) {				// Came from a faster state so slow it down
			positionScript.holdTimeMin = 300000000d;
			positionScript.holdTimeMax = 300000000d;					// Scale down, likely to the same as min speed for this state
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}
		//positionScript.holdTimeMin = 300000000d;
		//positionScript.holdTimeMax = 109500000000d;
		_cacheState = state;
	}

	void Parsec() {
		if (_cacheState == State.LightYear) {
			positionScript.holdTimeMin = 109500000000d;
			positionScript.holdTimeMax = 328500000000d;				// Scale up to the min speed of the next state
		} else if (_cacheState == State.LightDecade) {				// Came from a faster state so slow it down
			positionScript.holdTimeMin = 109500000000d;
			positionScript.holdTimeMax = 109500000000d;					// Scale down, likely to the same as min speed for this state
		} else {
			Debug.LogError ("Skippined a state somehow.  This shouldn't happen",gameObject);
		}
		//positionScript.holdTimeMin = 109500000000d;
		//positionScript.holdTimeMax = 328500000000d;
		_cacheState = state;
	}

	void LightDecade() {	
		positionScript.holdTimeMin = 328500000000d;
		positionScript.holdTimeMax = 1095000000000d;
		_cacheState = state;
	}

	void LightCentury() {	
		positionScript.holdTimeMin = 1095000000000d;
		positionScript.holdTimeMax = 10950000000000d;
		_cacheState = state;
	}

	void LightMillenium() {	
		positionScript.holdTimeMin = 10950000000000d;
		positionScript.holdTimeMax = 109500000000000d;
		_cacheState = state;
	}
	

}
