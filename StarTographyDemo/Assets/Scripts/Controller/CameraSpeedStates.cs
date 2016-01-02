using UnityEngine;
using System.Collections;

public class CameraSpeedStates : Functions {

	public enum State { 
		Initialize, 
		Slowest,
		Slower,
		Slow,
		Medium,
		Fast,
		Faster,
		Fastest
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
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
				case State.Initialize:
					break;
				case State.Slowest:
					Slowest ();
					break;
				case State.Slower:
					Slower ();
					break;
				case State.Slow:
					Slow ();
					break;
				case State.Medium:
					Medium ();
					break;
				case State.Fast:
					Fast ();
					break;
				}
			}
			yield return null;
		}
	}
	


	void Update() {
	}
	
	
	
	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	
	
	void Slowest() {								// This State is heavily commented as each other state uses same conditions		
		Debug.Log ("Slowest");
		positionScript.maxSpeed = 75000f;
		positionScript.holdTimeMin = 30f;
		positionScript.holdTimeMax = 300f;
		_cacheState = state;
	}

	void Slower() {
		Debug.Log ("Slower");
		positionScript.maxSpeed = 500000f;
		positionScript.holdTimeMin = 300f;
		positionScript.holdTimeMax = 3000f;
		_cacheState = state;
	}

	void Slow() {	
		Debug.Log ("Slow");
		positionScript.maxSpeed = 1000000f;
		positionScript.holdTimeMin = 3000f;
		positionScript.holdTimeMax = 30000f;
		_cacheState = state;
	}

	void Medium() {	
		Debug.Log ("Medium");
		positionScript.maxSpeed = 1000000f;
		positionScript.holdTimeMin = 30000f;
		positionScript.holdTimeMax = 300000f;
		_cacheState = state;
	}

	void Fast() {	
		Debug.Log ("Fast");
		positionScript.maxSpeed = 1000000f;
		positionScript.holdTimeMin = 15000f;
		positionScript.holdTimeMax = 1500000f;
		_cacheState = state;
	}
}
