using UnityEngine;
using System;
using System.Collections;
using CustomMath;

public class SystemScaleState : MonoBehaviour {

	public enum State { ScaleNull, ScaleLayer1, ScaleLayer2, ScaleLayer3, ScaleLayer4, ScaleLayer5, ScaleLayer6, ScaleLayer7, ScaleLayer8, ScaleLayer9, ScaleLayer10, ScaleLayer11, ScaleLayer12, ScaleLayer13, ScaleLayer14, ScaleLayer15, ScaleLayer16, ScaleLayer17, ScaleLayer18 }

	public State state = State.ScaleNull;
	State _prevState = State.ScaleNull;
	public State _cacheState = State.ScaleNull;

	public StarList starList;
	double scaleFactor = 1d;
	double stateMin;  // The minimum distance before we need to switch state downward
	double stateMax;  // The maximum distance before we need to switch state upward


	// This method is called by the CreateSolarSystems class in the Awake method 
	public void Begin () {
		// Find the largest vector size to see if we're out of the scale's max position
		double _largestVector = Math.Abs (starList.positionInSpace.x);
		if (Math.Abs (starList.positionInSpace.y) > _largestVector)
			_largestVector = Math.Abs (starList.positionInSpace.y);
		if (Math.Abs (starList.positionInSpace.z) > _largestVector)
			_largestVector = Math.Abs (starList.positionInSpace.z);

		_largestVector /= 1e11d;

		if(_largestVector < Maths.meter) {
			SetState (State.ScaleLayer1);
		} else if(_largestVector < Maths.kilometer) {
			SetState (State.ScaleLayer2);
		} else if(_largestVector < Maths.megameter) {
			SetState (State.ScaleLayer3);
		} else if(_largestVector < Maths.gigameter) {
			SetState (State.ScaleLayer4);
		} else if(_largestVector < Maths.terameter) {
			SetState (State.ScaleLayer5);
		} else if(_largestVector < Maths.petameter) {
			SetState (State.ScaleLayer6);
		} else if(_largestVector < Maths.exameter) {
			SetState (State.ScaleLayer7);
		} else if(_largestVector < Maths.zetameter) {
			SetState (State.ScaleLayer8);
		}

	}

	IEnumerator Start() {
		while (true) {
			switch (state) {
			case State.ScaleNull:
				break;
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
		// never gets to here
		if(gameObject.name == "[STAR] Sun") print ("At the end of Start(), _cacheState = " + _cacheState + ", state = " + state+" _prevState = "+_prevState);
	}

	public void SetState(State newState) {
		_prevState = state;
		_cacheState = State.ScaleNull;
		state = newState;
	}

	void ScaleLayer1() {
		stateMin = Maths.meter;
		stateMax = Maths.kilometer;
		gameObject.layer = 8;  // Update the layer of the star system
		scaleFactor = Maths.meter;
	}

	void ScaleLayer2() {
		stateMin = Maths.kilometer;
		stateMax = Maths.megameter;
		gameObject.layer = 9;  // Update the layer of the star system
		scaleFactor = Maths.kilometer;
	}

	void ScaleLayer3() {
		stateMin = Maths.megameter;
		stateMax = Maths.gigameter;
		gameObject.layer = 10;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer4() {
		stateMin = Maths.gigameter;
		stateMax = Maths.terameter;
		gameObject.layer = 11;  // Update the layer of the star system
		scaleFactor = Maths.gigameter;
	}

	void ScaleLayer5() {
		stateMin = Maths.terameter;
		stateMax = Maths.petameter;
		gameObject.layer = 12;  // Update the layer of the star system
		scaleFactor = Maths.terameter;
	}

	void ScaleLayer6() {
		stateMin = Maths.petameter;
		stateMax = Maths.exameter;
		gameObject.layer = 13;  // Update the layer of the star system
		scaleFactor = Maths.petameter;
	}

	void ScaleLayer7() {
		stateMin = Maths.exameter;
		stateMax = Maths.zetameter;
		gameObject.layer = 14;  // Update the layer of the star system
		scaleFactor = Maths.exameter;
	}

	void ScaleLayer8() {
		stateMin = Maths.zetameter;
		stateMax = Maths.yottameter;
		gameObject.layer = 15;  // Update the layer of the star system
		scaleFactor = Maths.zetameter;
	}

	void ScaleLayer9() {
		gameObject.layer = 16;  // Update the layer of the star system
		scaleFactor = Maths.yottameter;
	}

	void ScaleLayer10() {
		gameObject.layer = 17;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer11() {
		gameObject.layer = 18;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer12() {
		gameObject.layer = 19;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer13() {
		gameObject.layer = 20;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer14() {
		gameObject.layer = 21;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer15() {
		gameObject.layer = 22;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer16() {
		gameObject.layer = 23;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer17() {
		gameObject.layer = 24;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void ScaleLayer18() {
		gameObject.layer = 25;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void Update() {
		transform.position = (Vector3)(starList.positionInSpace / scaleFactor);
	}
	
}
