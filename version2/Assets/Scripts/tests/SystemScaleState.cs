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

	// This method is called by the CreateSolarSystems class in the Awake method 
	public void Begin () {
		// Find the largest vector size to see if we're out of the scale's max position
		double _largestVector = Math.Abs(starList.positionInSpace.x);
		if(Math.Abs(starList.positionInSpace.y) > _largestVector)
			_largestVector = Math.Abs(starList.positionInSpace.y);
		if(Math.Abs(starList.positionInSpace.z) > _largestVector)
			_largestVector = Math.Abs(starList.positionInSpace.z);
		
		if(_largestVector < Maths.kilometer) {
			Debug.Log ("Position is inside of 10,000 : "+starList.name+"("+((Vector3)starList.positionInSpace)+")");
			SetState (State.ScaleLayer1);
			//gameObject.layer = 8;  // Update the layer of the star system
		} else if(_largestVector < Maths.megameter) {
			Debug.Log ("Position is outside of 10,000 : "+starList.name+"("+((Vector3)starList.positionInSpace)+")");
			SetState (State.ScaleLayer2);
			//gameObject.layer = 9;
		} else if(_largestVector < Maths.gigameter) {
			Debug.Log ("Position is WAY outside of 10,000 : "+starList.name+"("+((Vector3)starList.positionInSpace)+")");
			SetState (State.ScaleLayer3);
			//gameObject.layer = 10;
		}


		/*if(currentDistance < 1e+7d)
			SetState (State.ScaleLayer1);
		else if(currentDistance < 1e+8d)
			SetState (State.ScaleLayer2);*/
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
		gameObject.layer = 8;  // Update the layer of the star system
		scaleFactor = Maths.meter;
	}

	void ScaleLayer2() {
		gameObject.layer = 9;  // Update the layer of the star system
		scaleFactor = Maths.kilometer;
	}

	void ScaleLayer3() {
		gameObject.layer = 10;  // Update the layer of the star system
		scaleFactor = Maths.megameter;
	}

	void Update() {
		transform.position = (Vector3)(starList.positionInSpace / scaleFactor);
	}
	
}
