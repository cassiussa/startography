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
	public Vector3d thisLocalScale = new Vector3d(1,1,1);
	public Vector3d originalLocalScale;

	public Transform[] allChildren;
	public Transform bodyMesh;

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

		foreach (Transform child in allChildren) {
			if(child.gameObject.name == "CelestialBodyMesh") {
				bodyMesh = child;
				bodyMesh.localScale = new Vector3((float)(realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
			}
		}


		if(currentDistance < 1e+6)
			SetState (State.ScaleLayer1);
		else if(currentDistance < 1e+7)
			SetState (State.ScaleLayer2);
		else if(currentDistance < 1e+8)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+9)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+10)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+11)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+12)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+13)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+14)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+15)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+16)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+17)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+18)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+19)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+20)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+21)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+22)
			SetState (State.ScaleLayer17);
		else
			SetState (State.ScaleLayer18);


		Debug.Log("149000000 = " + 149000000 + " = " + 1.49e+8);
		Debug.Log ("10,000,000 = " + 10000000 + " = " + 1e+7);
		Debug.Log ("149,000,000,000 = "+149000000000+" = "+1.49e+11);
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
		 * The distance values below are multiples of 1,000km per Unit.
		 * In other words, the first distance (10,000,000km) represents
		 * 10,000 Units in Unity.
		 */

	}

	/*
	 * Scales should be checked every Update(), so these functions
	 * are executed every frame.
	 */

	/*
	 * Something that is 100 units in size within ScaleLayer1 should be
	 * scale of 10 units when in the ScaleLayer2.  If it's position is
	 * at Vector3(5000,2000,1000) in ScaleLayer1, that is the same as being
	 * at Vector3(500,200,100) when in ScaleLayer2.
	 * 
	 */


	void ScaleLayer1() {
		// Check if the distance is less than or equal to 10M Km away
		if(currentDistance >= 1e+7)
			SetState (State.ScaleLayer2);
		SetLayer(8);
		CalculateLocalScale (1e+3, 1e+2);
		transform.position = Vector3d.toV3(thisPosition);
		//transform.localScale = new Vector3(0.01f,0.01f,0.01f);		// TODO: Check this out again.  Start at 0.01 instead of 1.0 as 10,000 is 1000 times smaller than 10,000,000
	}

	void ScaleLayer2() {
		if(currentDistance >= 1e+8)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+7)
			SetState (State.ScaleLayer1);
		SetLayer (9);
		CalculateLocalScale (1e+4, 1e+3);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer3() {
		if(currentDistance >= 1e+9)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+8)
			SetState (State.ScaleLayer2);
		SetLayer (10);
		CalculateLocalScale (1e+5,1e+4);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer4() {
		if(currentDistance >= 1e+10)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+9)
			SetState (State.ScaleLayer3);
		SetLayer (11);
		CalculateLocalScale (1e+6,1e+5);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer5() {
		if(currentDistance >= 1e+11)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+10)
			SetState (State.ScaleLayer4);
		SetLayer (12);
		CalculateLocalScale (1e+7,1e+6);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer6() {
		if(currentDistance >= 1e+12)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+11)
			SetState (State.ScaleLayer5);
		SetLayer (13);
		CalculateLocalScale (1e+8,1e+12);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer7() {
		if(currentDistance >= 1e+13)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+12)
			SetState (State.ScaleLayer6);
		SetLayer (14);
		CalculateLocalScale (1e+9, 1e+13);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer8() {
		if(currentDistance >= 1e+14)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+13)
			SetState (State.ScaleLayer7);
		SetLayer (15);
		CalculateLocalScale (1e+10, 1e+13);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer9() {
		if(currentDistance >= 1e+15)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+14)
			SetState (State.ScaleLayer8);
		SetLayer (16);
		CalculateLocalScale (1e+11, 1e+14);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer10() {
		if(currentDistance >= 1e+16)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+15)
			SetState (State.ScaleLayer9);
		SetLayer (17);
		CalculateLocalScale (1e+12, 1e+15);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer11() {
		if(currentDistance >= 1e+17)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+16)
			SetState (State.ScaleLayer10);
		SetLayer (18);
		CalculateLocalScale (1e+13, 1e+16);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer12() {
		if(currentDistance >= 1e+18)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+17)
			SetState (State.ScaleLayer11);
		SetLayer (19);
		CalculateLocalScale (1e+14, 1e+17);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer13() {
		if(currentDistance >= 1e+19)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+18)
			SetState (State.ScaleLayer12);
		SetLayer (20);
		CalculateLocalScale (1e+15, 1e+18);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer14() {
		if(currentDistance >= 1e+20)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+19)
			SetState (State.ScaleLayer13);
		SetLayer (21);
		CalculateLocalScale (1e+16, 1e+19);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer15() {
		if(currentDistance >= 1e+21)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+20)
			SetState (State.ScaleLayer14);
		SetLayer (22);
		CalculateLocalScale (1e+17, 1e+20);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer16() {
		if(currentDistance >= 1e+22)
			SetState (State.ScaleLayer17);
		else if(currentDistance < 1e+21)
			SetState (State.ScaleLayer15);
		SetLayer (23);
		CalculateLocalScale (1e+18, 1e+21);
		transform.position = Vector3d.toV3(thisPosition);

	}

	void ScaleLayer17() {
		if(currentDistance >= 1e+23)
			SetState (State.ScaleLayer18);
		else if(currentDistance < 1e+22)
			SetState (State.ScaleLayer16);
		SetLayer (24);
		CalculateLocalScale (1e+19, 1e+22);
		transform.position = Vector3d.toV3(thisPosition);
	}

	void ScaleLayer18() {
		if(currentDistance < 1e+24)
			SetState (State.ScaleLayer17);
		SetLayer (25);
		CalculateLocalScale (1e+20,1e+23);
		transform.position = Vector3d.toV3(thisPosition);
	}


	/*
	 * Here we take the original scale of the object and modify it so that
	 * perspectively, it will look the appropriate size in any given scale
	 * state layer.  In other words if the object goes beyond the bounds of
	 * any one layer (ex ScaleLayer1) and into the next (ex ScaleLayer2), then
	 * the object will readjust its position to be closer to the camera (to 
	 * ensure we never go beyond the 10,000 unit limit) and the object will be
	 * resized (shrunk in this case) to account for the perspective difference,
	 * making it appear that nothing has happened.
	 */

	private void CalculateLocalScale(double pFactor, double sFactor) {
		positionFactor = pFactor;
		Vector3d newLocalScale = new Vector3d (
			(thisLocalScale.x / sFactor) * Global.kmPerUnit,
			(thisLocalScale.y / sFactor) * Global.kmPerUnit,
			(thisLocalScale.z / sFactor) * Global.kmPerUnit);

		gameObject.transform.localScale = new Vector3(
			(float)newLocalScale.x, 
			(float)newLocalScale.y, 
			(float)newLocalScale.z);

		thisPosition = realPosition / positionFactor;
	}



	void SetLayer(int newLayer) {
		gameObject.layer = newLayer;
		foreach(Transform child in allChildren) {            
			child.gameObject.layer = newLayer;
		}
	}

}

