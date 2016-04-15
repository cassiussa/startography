using UnityEngine;
using System.Collections;

public class DistanceMarkerState : MonoBehaviour {

	public enum State { Initialize, Active, Inactive, FadeIn, FadeOut }
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

	public double distanceMarkerSize;

	void Awake() {
		SetState (State.Inactive);
	}

	//Async version of Start.
	IEnumerator Start() {
		while (true) {
			switch (state) {
			case State.Initialize:
				break;
			case State.Active:
				Active ();
				break;
			case State.Inactive:
				Inactive ();
				break;
			case State.FadeIn:
				FadeIn ();
				break;
			case State.FadeOut:
				FadeOut ();
				break;
			}
			yield return null;
		}
	}

	void Update() {

	}

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
		_cacheState = newState;
	}


	/*
	 * These are the state definitions
	 * 
	 * If we need the state to occur every Update(), then don't
	 * do a _cacheState/state comparison - however, still perform
	 * the : _cacheState = state; at the end of the state's function
	 */
	void Active () {
		_cacheState = state;
	}

	void Inactive () {
		if (_cacheState != state) {
			Debug.LogError ("The state has been updated");
			_cacheState = state;
		}


	}

	void FadeIn () {
		Debug.LogError ("The state has been updated");
		_cacheState = state;
	}

	void FadeOut () {
		_cacheState = state;
	}
}
