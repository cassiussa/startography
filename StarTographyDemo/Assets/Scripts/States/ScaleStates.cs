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
	public Vector3d thisPosition = new Vector3d (0, 0, 0);

	public double currentDistance;
	public double scaleFactor;									// This is the scale multiplier, which is updated on every State update


	void Awake() {
		positionScript = gameObject.GetComponent<Position> ();

		realPosition = positionScript.realPosition;

		if(currentDistance < 1e+7)
			SetState (State.ScaleLayer1);
		else if(currentDistance < 1e+8)
			SetState (State.ScaleLayer2);
		else if(currentDistance < 1e+9)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+10)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+11)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+12)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+13)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+14)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+15)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+16)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+17)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+18)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+19)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+20)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+21)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+22)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+23)
			SetState (State.ScaleLayer17);
		else
			SetState (State.ScaleLayer18);

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
		if (relativePosition.x == 0d || relativePosition.y == 0d || relativePosition.z == 0d) {
			relativePosition.x = 1;
			relativePosition.y = 1;
			relativePosition.z = 1;
		}
		currentDistance = Vector3d.Distance(new Vector3d(0,0,0), relativePosition);

		/*
		 * The distance values below are multiples of 1,000km per Unity.
		 * In other words, the first distance (10,000,000km) represents
		 * 10,000 Units in Unity.
		 */

	}

	/*
	 * Scales should be checked every Update(), so these functions
	 * are executed every frame.
	 */
	void ScaleLayer1() {
		if(currentDistance >= 1e+7)
			SetState (State.ScaleLayer2);
		gameObject.layer = 8;
		ScaleFactor (1e+4);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer2() {
		if(currentDistance >= 1e+8)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+6)
			SetState (State.ScaleLayer1);
		gameObject.layer = 9;
		ScaleFactor (1e+5);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer3() {
		if(currentDistance >= 1e+9)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+7)
			SetState (State.ScaleLayer2);
		gameObject.layer = 10;
		ScaleFactor (1e+6);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer4() {
		if(currentDistance >= 1e+10)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+8)
			SetState (State.ScaleLayer3);
		gameObject.layer = 11;
		ScaleFactor (1e+7);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer5() {
		if(currentDistance >= 1e+11)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+9)
			SetState (State.ScaleLayer4);
		gameObject.layer = 12;
		ScaleFactor (1e+8);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer6() {
		if(currentDistance >= 1e+12)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+10)
			SetState (State.ScaleLayer5);
		gameObject.layer = 13;
		ScaleFactor (1e+9);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer7() {
		if(currentDistance >= 1e+13)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+11)
			SetState (State.ScaleLayer6);
		gameObject.layer = 14;
		ScaleFactor (1e+10);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer8() {
		if(currentDistance >= 1e+14)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+12)
			SetState (State.ScaleLayer7);
		gameObject.layer = 15;
		ScaleFactor (1e+11);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer9() {
		if(currentDistance >= 1e+15)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+13)
			SetState (State.ScaleLayer8);
		gameObject.layer = 16;
		ScaleFactor (1e+12);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer10() {
		if(currentDistance >= 1e+16)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+14)
			SetState (State.ScaleLayer9);
		gameObject.layer = 17;
		ScaleFactor (1e+13);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer11() {
		if(currentDistance >= 1e+17)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+15)
			SetState (State.ScaleLayer10);
		ScaleFactor (1e+14);
		thisPosition = relativePosition / scaleFactor;
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer12() {
		if(currentDistance >= 1e+18)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+16)
			SetState (State.ScaleLayer11);
		ScaleFactor (1e+15);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer13() {
		if(currentDistance >= 1e+19)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+17)
			SetState (State.ScaleLayer12);
		ScaleFactor (1e+16);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer14() {
		if(currentDistance >= 1e+20)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+18)
			SetState (State.ScaleLayer13);
		ScaleFactor (1e+17);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer15() {
		if(currentDistance >= 1e+21)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+19)
			SetState (State.ScaleLayer14);
		ScaleFactor (1e+18);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer16() {
		if(currentDistance >= 1e+22)
			SetState (State.ScaleLayer17);
		else if(currentDistance < 1e+20)
			SetState (State.ScaleLayer15);
		ScaleFactor (1e+19);

	}

	void ScaleLayer17() {
		if(currentDistance >= 1e+23)
			SetState (State.ScaleLayer18);
		else if(currentDistance < 1e+21)
			SetState (State.ScaleLayer16);
		ScaleFactor (1e+20);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer18() {
		if(currentDistance < 1e+24)
			SetState (State.ScaleLayer17);
		ScaleFactor (1e+21);
		transform.position = Vector3d.toV3(thisPosition);
	}


	void ScaleFactor(double sFactor) {
		if (_cacheState != state) {
			scaleFactor = sFactor;
			_cacheState = state;
		}
	}
}
