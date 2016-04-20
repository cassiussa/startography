using UnityEngine;
using System.Collections;
using Functions;
using Globals;

public class ScaleStates : MonoBehaviour {

	public enum State { ScaleLayer1, ScaleLayer2, ScaleLayer3, ScaleLayer4, ScaleLayer5, ScaleLayer6, ScaleLayer7, ScaleLayer8, ScaleLayer9, ScaleLayer10, ScaleLayer11, ScaleLayer12, ScaleLayer13, ScaleLayer14, ScaleLayer15, ScaleLayer16, ScaleLayer17, ScaleLayer18 }

	public State state = State.ScaleLayer1;
	State _prevState;
	State _cacheState;

	#region Basic Getters/Setters
	public State CurrentState { get { return state; } }
	public State PrevState { get { return _prevState; } }
	#endregion

	public Position positionScript;
	public Vector3d realPosition;
	public Vector3d relativePosition;

	public double currentDistance;

	void Awake() {
		positionScript = gameObject.GetComponent<Position> ();

		realPosition = positionScript.realPosition;
	}

	IEnumerator Start() {
		while (true) {
			switch (state) {
			case State.ScaleLayer1:
				ScaleLayer1 ();
				break;
			case State.ScaleLayer2:
				ScaleLayer2 ();
				break;
			case State.ScaleLayer3:
				ScaleLayer3 ();
				break;
			case State.ScaleLayer4:
				ScaleLayer4 ();
				break;
			case State.ScaleLayer5:
				ScaleLayer5 ();
				break;
			case State.ScaleLayer6:
				ScaleLayer6 ();
				break;
			case State.ScaleLayer7:
				ScaleLayer7 ();
				break;
			case State.ScaleLayer8:
				ScaleLayer8 ();
				break;
			case State.ScaleLayer9:
				ScaleLayer9 ();
				break;
			case State.ScaleLayer10:
				ScaleLayer10 ();
				break;
			case State.ScaleLayer11:
				ScaleLayer11 ();
				break;
			case State.ScaleLayer12:
				ScaleLayer12 ();
				break;
			case State.ScaleLayer13:
				ScaleLayer13 ();
				break;
			case State.ScaleLayer14:
				ScaleLayer14 ();
				break;
			case State.ScaleLayer15:
				ScaleLayer15 ();
				break;
			case State.ScaleLayer16:
				ScaleLayer16 ();
				break;
			case State.ScaleLayer17:
				ScaleLayer17 ();
				break;
			case State.ScaleLayer18:
				ScaleLayer18 ();
				break;
			}
			yield return null;
		}
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

	void Update() {
		currentDistance = Vector3d.Distance(new Vector3d(0,0,0), relativePosition);

		/*
		 * The distance values below are multiples of 1,000km per Unity.
		 * In other words, the first distance (10,000,000km) represents
		 * 10,000 Units in Unity.
		 * 
		 * We test largest to smallest because the majority of the objects
		 * in the universe will be closer to the upper range vs the lower
		 * range.  For example, Alpha Centauri is 41,315,009,973,844.2km
		 * which is around layer 8-9.  So essentially all other star systems
		 * would be closer to ScaleLayer18 than ScaleLayer1.
		 * 
		 * TODO: This should later be changed to do a single check only when within
		 * a state.  Example.  ScaleLayer12 would check if we go into either
		 * ScaleLayer11 or ScaleLayer13 only.
		 */

	}

	void ScaleLayer1() {
		if(currentDistance >= 10000000d)
			SetState (State.ScaleLayer2);
		transform.position = new Vector3 ((float)relativePosition.x/10000, (float)relativePosition.y/10000, (float)relativePosition.z/10000);
	}

	void ScaleLayer2() {
		if(currentDistance >= 100000000d)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 10000000d)
			SetState (State.ScaleLayer1);
		transform.position = new Vector3 ((float)relativePosition.x/100000, (float)relativePosition.y/100000, (float)relativePosition.z/100000);
	}

	void ScaleLayer3() {
		if(currentDistance >= 1000000000d)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 10000000d)
			SetState (State.ScaleLayer2);
		transform.position = new Vector3 ((float)relativePosition.x/1000000, (float)relativePosition.y/1000000, (float)relativePosition.z/1000000);
	}

	void ScaleLayer4() {
		if(currentDistance >= 10000000000d)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 100000000d)
			SetState (State.ScaleLayer3);
		transform.position = new Vector3 ((float)relativePosition.x/10000000, (float)relativePosition.y/10000000, (float)relativePosition.z/10000000);
	}

	void ScaleLayer5() {
		if(currentDistance >= 100000000000d)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1000000000d)
			SetState (State.ScaleLayer4);
		transform.position = new Vector3 ((float)relativePosition.x/100000000, (float)relativePosition.y/100000000, (float)relativePosition.z/100000000);
	}

	void ScaleLayer6() {
		if(currentDistance >= 1000000000000d)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 10000000000d)
			SetState (State.ScaleLayer5);
		transform.position = new Vector3 ((float)relativePosition.x/1000000000, (float)relativePosition.y/1000000000, (float)relativePosition.z/1000000000);
	}

	void ScaleLayer7() {
		if(currentDistance >= 10000000000000d)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 100000000000d)
			SetState (State.ScaleLayer6);
		transform.position = new Vector3 ((float)relativePosition.x/10000000000, (float)relativePosition.y/10000000000, (float)relativePosition.z/10000000000);
	}

	void ScaleLayer8() {
		if(currentDistance >= 100000000000000d)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1000000000000d)
			SetState (State.ScaleLayer7);
		transform.position = new Vector3 ((float)relativePosition.x/100000000000, (float)relativePosition.y/100000000000, (float)relativePosition.z/100000000000);
	}

	void ScaleLayer9() {
		if(currentDistance >= 1000000000000000d)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 10000000000000d)
			SetState (State.ScaleLayer8);
		transform.position = new Vector3 ((float)relativePosition.x/1000000000000, (float)relativePosition.y/1000000000000, (float)relativePosition.z/1000000000000);
	}

	void ScaleLayer10() {
		if(currentDistance >= 10000000000000000d)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 100000000000000d)
			SetState (State.ScaleLayer9);
	}

	void ScaleLayer11() {
		if(currentDistance >= 100000000000000000d)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1000000000000000d)
			SetState (State.ScaleLayer10);
	}

	void ScaleLayer12() {
		if(currentDistance >= 1000000000000000000d)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 10000000000000000d)
			SetState (State.ScaleLayer11);
	}

	void ScaleLayer13() {
		if(currentDistance >= 10000000000000000000d)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 100000000000000000d)
			SetState (State.ScaleLayer12);
	}

	void ScaleLayer14() {
		if(currentDistance >= 100000000000000000000d)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1000000000000000000d)
			SetState (State.ScaleLayer13);
	}

	void ScaleLayer15() {
		if(currentDistance >= 1000000000000000000000d)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 10000000000000000000d)
			SetState (State.ScaleLayer14);
	}

	void ScaleLayer16() {
		if(currentDistance >= 10000000000000000000000d)
			SetState (State.ScaleLayer17);
		else if(currentDistance < 100000000000000000000d)
			SetState (State.ScaleLayer15);
	}

	void ScaleLayer17() {
		if(currentDistance >= 100000000000000000000000d)
			SetState (State.ScaleLayer18);
		else if(currentDistance < 1000000000000000000000d)
			SetState (State.ScaleLayer16);
	}

	void ScaleLayer18() {
		if(currentDistance < 10000000000000000000000d)
			SetState (State.ScaleLayer17);
	}
}
