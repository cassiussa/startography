using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSpeedStates : Functions {

	public enum State { 
		Initialize, 
		A,B,C,D,E,F,G,H,I,J,K,L,M
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

		distanceCheck.Add (100000, State.A);
		distanceCheck.Add (1000000, State.B);
		distanceCheck.Add (10000000, State.C);
		distanceCheck.Add (100000000, State.D);
		distanceCheck.Add (1000000000, State.E);
		distanceCheck.Add (10000000000, State.F);
		distanceCheck.Add (100000000000, State.G);
		distanceCheck.Add (1000000000000, State.H);
		distanceCheck.Add (10000000000000, State.I);
		distanceCheck.Add (100000000000000, State.K);
		distanceCheck.Add (1000000000000000, State.L);
		distanceCheck.Add (10000000000000000, State.M);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
				case State.Initialize:
					break;
				case State.A:
					A ();
					break;
				case State.B:
					B ();
					break;
				case State.C:
					C ();
					break;
				case State.D:
					D ();
					break;
				case State.E:
					E ();
					break;
				case State.F:
					F ();
					break;
				case State.G:
					G ();
					break;
				case State.H:
					H ();
					break;
				case State.I:
					I ();
					break;
				case State.K:
					K ();
					break;
				case State.L:
					L ();
					break;
				case State.M:
					M ();
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



	
	void A() {								// This State is heavily commented as each other state uses same conditions		
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 30d;
		positionScript.holdTimeMax = 300d;
		_cacheState = state;
	}

	void B() {
		positionScript.holdTimeMin = 300d;
		positionScript.holdTimeMax = 3000d;
		_cacheState = state;
	}

	void C() {	
		positionScript.holdTimeMin = 3000d;
		positionScript.holdTimeMax = 30000d;
		_cacheState = state;
	}

	void D() {	
		positionScript.holdTimeMin = 30000d;
		positionScript.holdTimeMax = 300000d;
		_cacheState = state;
	}

	void E() {	
		positionScript.holdTimeMin = 300000d;
		positionScript.holdTimeMax = 3000000d;
		_cacheState = state;
	}

	void F() {	
		positionScript.holdTimeMin = 3000000d;
		positionScript.holdTimeMax = 30000000d;
		_cacheState = state;
	}

	void G() {
		positionScript.holdTimeMin = 30000000d;
		positionScript.holdTimeMax = 300000000d;
		_cacheState = state;
	}

	void H() {	
		positionScript.holdTimeMin = 300000000d;
		positionScript.holdTimeMax = 3000000000d;
		_cacheState = state;
	}

	void I() {	
		positionScript.holdTimeMin = 3000000000d;
		positionScript.holdTimeMax = 30000000000d;
		_cacheState = state;
	}

	void J() {	
		positionScript.holdTimeMin = 30000000000d;
		positionScript.holdTimeMax = 300000000000d;
		_cacheState = state;
	}

	void K() {	
		positionScript.holdTimeMin = 300000000000d;
		positionScript.holdTimeMax = 3000000000000d;
		_cacheState = state;
	}

	void L() {	
		positionScript.holdTimeMin = 3000000000000d;
		positionScript.holdTimeMax = 30000000000000d;
		_cacheState = state;
	}

	void M() {	
		positionScript.holdTimeMin = 30000000000000d;
		positionScript.holdTimeMax = 300000000000000d;
		_cacheState = state;
	}
	

}
