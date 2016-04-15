using UnityEngine;
using System.Collections;

public class DistanceMarkerStates : MonoBehaviour {

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
	

	void Update () {
		if(state == State.Initialize)
			SetState (State.Inactive);
	}

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
		_cacheState = newState;
	}

	void Active() {
		if (state != _cacheState) {
			Debug.LogError ("updated to Active state");
			_cacheState = state;
		}

	}

	void Inactive() {
		print ("Inactive State");
		_cacheState = state;
	}

	void FadeIn() {

		_cacheState = state;
	}

	void FadeOut() {

		_cacheState = state;
	}

}
