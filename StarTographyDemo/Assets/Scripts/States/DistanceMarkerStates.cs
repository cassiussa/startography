using UnityEngine;
using System.Collections;

public class DistanceMarkerStates : MonoBehaviour {

	public enum State { Initialize, Active, Inactive, FadeIn, FadeOut }

	public State state = State.Initialize;
	State _prevState;
	State _cacheState;

	public FadeDistanceMarker fadeDistanceMarkerScript;
	public GameObject[] children = new GameObject[5];

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
		/* Uncomment the below line, "_cacheState = newState;", if we want to
		 * perform state every Update(), then comment out all the "if(state != _cacheState)"
		 * statements in the State functions below
		 */
		//_cacheState = newState;
	}

	void Active() {
		if (state != _cacheState) {
			fadeDistanceMarkerScript.enabled = true;
			foreach (GameObject child in children) {
				child.SetActive(true);
			}
			print ("active state");
			_cacheState = state;
		}

	}

	void Inactive() {
		//print ("Inactive State");

		// TODO: Can't get into this state because the FadeDistanceMarker script sends us to Active state every frame we're not in FadeIn or FadeOut states
		foreach (GameObject child in children) {
			child.SetActive(false);
		}
		fadeDistanceMarkerScript.enabled = false;
		_cacheState = state;
	}

	void FadeIn() {
		if (state != _cacheState) {
			print ("FadeIn state");
			_cacheState = state;
		}
	}

	void FadeOut() {
		if (state != _cacheState) {
			print ("FadeOut state");
			_cacheState = state;
		}
	}

}
