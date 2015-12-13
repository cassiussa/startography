using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;	// For iterating through all the transforms

public class SolarSystemScaleStates : MonoBehaviour {
	public enum State { Initialize, SetupState, Entered, Local, Far, Gone }
	
	public State state = State.Initialize;
	State _prevState;
	State _cacheState;
	
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
		SetState(State.SetupState);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
					case State.Initialize:
						break;
					case State.SetupState:
						SetupState ();
						break;
					case State.Entered:
						Entered ();
						break;
					case State.Local:
						Local ();
						break;
					case State.Far:
						Far ();
						break;
					case State.Gone:
						Gone ();
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

	
	void SetupState() {
		// Get all the child gameObjects so that we can disable them when the camera is not within view of the solar system
		SetState(State.Far);
	}

	void Local() {

		_cacheState = state;
	}

	void Entered() {

		_cacheState = state;
	}

	void Gone() {

		_cacheState = state;
	}

	void Far() {
		// Check to see if it's already in the inactive state because if it is, we don't want to try and set it again.
		// Because if we do, the SolarSystemStates is unaware.
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
		_cacheState = state;
	}
	
}