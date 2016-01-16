using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSpeedStates : Functions {

	public enum State { Initialize,A,B,C,D,E,F,G,H,I,J,K,L,M,N }
	
	public State state = State.Initialize;
	State _prevState;
	State _cacheState;
	
	public Dictionary<double, State> distanceCheck = new Dictionary<double, State>();				// So that we can get the States based on the double value (key)
	public Dictionary<Collider, double> currentCollisions = new Dictionary<Collider, double>();		// So that we can remove correct colliders and find the appropriate key for the distanceCheck dictionary

	public void OnScaleCollision() {
		double size = 10000000000000000000000000d;													// Assign a crazy-high number we know we'll never reach as the start point
		foreach (KeyValuePair<Collider, double> currentCollision in currentCollisions) {			// Iterate through each Collider:double pair in the currentCollisions Dictionary
			if(currentCollision.Value < size) {														// See if the double of this iteration is smaller than all previous iterations
				size = currentCollision.Value;														// If it's smaller, assign it as the new smallest
			}
		}
		SetState (distanceCheck[size]);																// We've found the smallest scale that we're currently colliding with, so set the appropriate state
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

		distanceCheck.Add (10000, State.A);
		distanceCheck.Add (100000, State.B);				// See distanceCheck variable creation for details
		distanceCheck.Add (1000000, State.C);
		distanceCheck.Add (10000000, State.D);
		distanceCheck.Add (100000000, State.E);
		distanceCheck.Add (1000000000, State.F);
		distanceCheck.Add (10000000000, State.G);
		distanceCheck.Add (100000000000, State.H);
		distanceCheck.Add (1000000000000, State.I);
		distanceCheck.Add (10000000000000, State.J);
		distanceCheck.Add (100000000000000, State.K);
		distanceCheck.Add (1000000000000000, State.L);
		distanceCheck.Add (10000000000000000, State.M);
		distanceCheck.Add (100000000000000000, State.N);
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
				case State.J:
					J ();
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
				case State.N:
					N ();
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
	

	void A() {	
		positionScript.holdTimeMin = 3d;
		positionScript.holdTimeMax = 30d;
		_cacheState = state;
	}

	void B() {
		positionScript.holdTimeMin = 30d;
		positionScript.holdTimeMax = 300d;
		_cacheState = state;
	}

	void C() {	
		positionScript.holdTimeMin = 300d;
		positionScript.holdTimeMax = 3000d;
		_cacheState = state;
	}

	void D() {	
		positionScript.holdTimeMin = 3000d;
		positionScript.holdTimeMax = 30000d;
		_cacheState = state;
	}

	void E() {	
		positionScript.holdTimeMin = 30000d;
		positionScript.holdTimeMax = 300000d;
		_cacheState = state;
	}

	void F() {	
		positionScript.holdTimeMin = 300000d;
		positionScript.holdTimeMax = 3000000d;
		_cacheState = state;
	}

	void G() {
		positionScript.holdTimeMin = 3000000d;
		positionScript.holdTimeMax = 30000000d;
		_cacheState = state;
	}

	void H() {	
		positionScript.holdTimeMin = 30000000d;
		positionScript.holdTimeMax = 300000000d;
		_cacheState = state;
	}

	void I() {	
		positionScript.holdTimeMin = 300000000d;
		positionScript.holdTimeMax = 3000000000d;
		_cacheState = state;
	}

	void J() {	
		positionScript.holdTimeMin = 3000000000d;
		positionScript.holdTimeMax = 30000000000d;
		_cacheState = state;
	}

	void K() {	
		positionScript.holdTimeMin = 30000000000d;
		positionScript.holdTimeMax = 300000000000d;
		_cacheState = state;
	}

	void L() {	
		positionScript.holdTimeMin = 300000000000d;
		positionScript.holdTimeMax = 3000000000000d;
		_cacheState = state;
	}

	void M() {	
		positionScript.holdTimeMin = 3000000000000d;
		positionScript.holdTimeMax = 30000000000000d;
		_cacheState = state;
	}

	void N() {	
		positionScript.holdTimeMin = 30000000000000d;
		positionScript.holdTimeMax = 300000000000000d;
		_cacheState = state;
	}
	

}
