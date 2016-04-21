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
	public Radius radiusScript;
	public Vector3d realPosition;
	public Vector3d relativePosition;
	public Vector3d thisPosition = new Vector3d (100, 100, 100);

	public Vector3d realRadius;
	public Vector3d relativeRadius;
	public Vector3d thisRadius = new Vector3d (100, 100, 100);

	public double currentDistance;
	public double positionFactor;									// This is the scale multiplier, which is updated on every State update
	public double radiusFactor;

	public Transform[] allChildren;


	void Awake() {

		if (thisPosition.x == 0d || thisPosition.y == 0d || thisPosition.z == 0d) {
			thisPosition.x = 1;
			thisPosition.y = 1;
			thisPosition.z = 1;
		}
		if (thisRadius.x == 0d || thisRadius.y == 0d || thisRadius.z == 0d) {
			thisRadius.x = 1;
			thisRadius.y = 1;
			thisRadius.z = 1;
		}
		positionScript = gameObject.GetComponent<Position> ();
		radiusScript = gameObject.GetComponent<Radius> ();

		realPosition = positionScript.realPosition;
		relativePosition = positionScript.relativePosition;
		realRadius = radiusScript.realRadius;
		relativeRadius = radiusScript.relativeRadius;

		currentDistance = Vector3d.Distance(new Vector3d(0,0,0), relativePosition);

		allChildren = gameObject.GetComponentsInChildren<Transform>();


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
		if (thisPosition.x == 0d || thisPosition.y == 0d || thisPosition.z == 0d) {
			thisPosition.x = 1;
			thisPosition.y = 1;
			thisPosition.z = 1;
		}
		if (thisRadius.x == 0d || thisRadius.y == 0d || thisRadius.z == 0d) {
			thisRadius.x = 1;
			thisRadius.y = 1;
			thisRadius.z = 1;
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

		SetLayer(8);

		ScaleFactor (1e+3, 1);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer2() {
		if(currentDistance >= 1e+8)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+6)
			SetState (State.ScaleLayer1);
		SetLayer (9);
		ScaleFactor (1e+4, 10);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer3() {
		if(currentDistance >= 1e+9)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+7)
			SetState (State.ScaleLayer2);
		SetLayer (10);
		ScaleFactor (1e+5, 100);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer4() {
		if(currentDistance >= 1e+10)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+8)
			SetState (State.ScaleLayer3);
		SetLayer (11);
		ScaleFactor (1e+6, 1000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer5() {
		if(currentDistance >= 1e+11)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+9)
			SetState (State.ScaleLayer4);
		SetLayer (12);
		ScaleFactor (1e+7, 10000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer6() {
		if(currentDistance >= 1e+12)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+10)
			SetState (State.ScaleLayer5);
		SetLayer (13);
		ScaleFactor (1e+8, 100000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer7() {
		if(currentDistance >= 1e+13)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+11)
			SetState (State.ScaleLayer6);
		SetLayer (14);
		ScaleFactor (1e+9, 1000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer8() {
		if(currentDistance >= 1e+14)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+12)
			SetState (State.ScaleLayer7);
		SetLayer (15);
		ScaleFactor (1e+10, 10000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer9() {
		if(currentDistance >= 1e+15)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+13)
			SetState (State.ScaleLayer8);
		SetLayer (16);
		ScaleFactor (1e+11, 100000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer10() {
		if(currentDistance >= 1e+16)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+14)
			SetState (State.ScaleLayer9);
		SetLayer (17);
		ScaleFactor (1e+12, 1000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer11() {
		if(currentDistance >= 1e+17)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+15)
			SetState (State.ScaleLayer10);
		SetLayer (18);
		ScaleFactor (1e+13, 10000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer12() {
		if(currentDistance >= 1e+18)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+16)
			SetState (State.ScaleLayer11);
		SetLayer (19);
		ScaleFactor (1e+14, 100000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer13() {
		if(currentDistance >= 1e+19)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+17)
			SetState (State.ScaleLayer12);
		SetLayer (20);
		ScaleFactor (1e+15, 1000000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer14() {
		if(currentDistance >= 1e+20)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+18)
			SetState (State.ScaleLayer13);
		SetLayer (21);
		ScaleFactor (1e+16, 10000000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer15() {
		if(currentDistance >= 1e+21)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+19)
			SetState (State.ScaleLayer14);
		SetLayer (22);
		ScaleFactor (1e+17, 100000000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer16() {
		if(currentDistance >= 1e+22)
			SetState (State.ScaleLayer17);
		else if(currentDistance < 1e+20)
			SetState (State.ScaleLayer15);
		SetLayer (23);
		ScaleFactor (1e+18, 1000000000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);

	}

	void ScaleLayer17() {
		if(currentDistance >= 1e+23)
			SetState (State.ScaleLayer18);
		else if(currentDistance < 1e+21)
			SetState (State.ScaleLayer16);
		SetLayer (24);
		ScaleFactor (1e+19, 10000000000000000);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}

	void ScaleLayer18() {
		if(currentDistance < 1e+24)
			SetState (State.ScaleLayer17);
		ScaleFactor (1e+20, 100000000000000000);
		SetLayer (25);
		thisPosition = relativePosition / positionFactor;
		thisRadius = relativeRadius / radiusFactor;
		transform.position = Vector3d.toV3(thisPosition);
		transform.localScale = Vector3d.toV3(thisRadius);
	}


	void ScaleFactor(double pFactor, double rFactor) {
		if (_cacheState != state) {
			//Vector3d.toV3(thisPosition);
			positionFactor = pFactor;
			radiusFactor = rFactor;
			_cacheState = state;
		}
	}

	void SetLayer(int newLayer) {
		gameObject.layer = newLayer;
		foreach(Transform child in allChildren) {            
			child.gameObject.layer = newLayer;
		}
	}

}

