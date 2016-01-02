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
	
	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	void Awake() {
		
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {	// Commented out because we need to run state every frame
				switch (state) {
				case State.Initialize:
					break;
				case State.Slowest:
					Slowest ();
					break;
				case State.Slower:
					Slower ();
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
	
	
	void Slowest() {																		// This State is heavily commented as each other state uses same conditions		
		Debug.Log ("Slowest");
		_cacheState = state;
		
	}

	void Slower() {																		// This State is heavily commented as each other state uses same conditions		
		Debug.Log ("Slower");
		_cacheState = state;
		
	}
}
