using UnityEngine;
using System.Collections;
using Functions;
using Globals;

public class ScaleStates : MonoBehaviour {

	public enum State { ScaleNull, ScaleLayer1, ScaleLayer2, ScaleLayer3, ScaleLayer4, ScaleLayer5, ScaleLayer6, ScaleLayer7, ScaleLayer8, ScaleLayer9, ScaleLayer10, ScaleLayer11, ScaleLayer12, ScaleLayer13, ScaleLayer14, ScaleLayer15, ScaleLayer16, ScaleLayer17, ScaleLayer18 }

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

		currentDistance = Vector3d.Distance(new Vector3d(0,0,0), realPosition);

		allChildren = gameObject.GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if(child.gameObject.name == "CelestialBodyMesh") {
				bodyMesh = child;
				bodyMesh.localScale = new Vector3((float)(realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
			}
		}


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


		/*Debug.Log("149000000 = " + 149000000 + " = " + 1.49e+8);
		Debug.Log ("10,000,000 = " + 10000000 + " = " + 1e+7);
		Debug.Log ("149,000,000,000 = "+149000000000+" = "+1.49e+11);*/

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
		currentDistance = Vector3d.Distance(new Vector3d(0,0,0), realPosition);

		/*
		 * The distance values below are multiples of 1,000km per Unit.
		 * In other words, the first distance (10,000,000km) represents
		 * 10,000 Units in Unity.
		 */

		realPosition.z -= 100000000;
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

	// double positionFactor, int layer, double moveScaleUp, double moveScaleDown, State higherState, State lowerState
	private void CalculateStateValues(int layer, double moveScaleUp, double moveScaleDown, State higherState, State lowerState) {
		/* ScaleLayer2 example
		if(currentDistance >= 1e+8)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+7)
			SetState (State.ScaleLayer1);
		 */

		State tempState = state;
		if (gameObject.name == "[STAR] Sun") {


			if (higherState != State.ScaleNull) {
				if (currentDistance >= moveScaleUp) {
					tempState = higherState;
				}
			}
			if (lowerState != State.ScaleNull) {
				if (currentDistance < moveScaleDown) {
					tempState = lowerState;
				}
			}

			// Have we received a state change this Update()?
			if(tempState != state) {
				SetLayer (layer);
				SetState (tempState);
			}

		}


	}



	public Vector3 CalculatePosition(double stateScale, Vector3d camPos) {
		/*
		 * Calculate the ratio of real position to fit within 10k unit limit
		 * 
		 * Parameters
		 * ----------
		 * stateScale : The distance value of the scale State (ex: 100000 for MK)
		 *		- supplied by the ScaleStates.cs script
		 *		- supplied by the PlanetOrbitPathTrail.cs script
		 * position : A Vector3d value of the real position of the object
		 * 
		 * Actions
		 * -------
		 * Assigns the position to the gameObject that the calling ScaleStates.cs script is attached to
		*/

		Vector3d tempV3 = new Vector3d(((realPosition + camPos) / stateScale) * Global.kmPerUnit);
		/*float _x = (float)(((realPosition.x + camPos.x) / stateScale) * Global.maxUnits);
		float _y = (float)(((realPosition.y + camPos.y) / stateScale) * Global.maxUnits);
		float _z = (float)(((realPosition.z + camPos.z) / stateScale) * Global.maxUnits);*/
		
		return new Vector3 ((float)tempV3.x, (float)tempV3.y, (float)tempV3.z);
	}




	/*
	 * Scales should be checked every Update(), so these functions
	 * are executed every frame.
	 * 
	 * Something that is 100 units in size within ScaleLayer1 should be
	 * scale of 10 units when in the ScaleLayer2.  If it's position is
	 * at Vector3(5000,2000,1000) in ScaleLayer1, that is the same as being
	 * at Vector3(500,200,100) when in ScaleLayer2.
	 * 
	 */

	// Check if the distance is more than 9,999,999.9999999km away
	void ScaleLayer1() {
		if (currentDistance >= 1e+7)
			SetState (State.ScaleLayer2);
		transform.position = CalculatePosition (1e+6, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (8, 1e+7, 0, State.ScaleLayer2, State.ScaleNull);

		// Things to do only once per state change
		if (_cacheState != state) {
			SetLayer (8);
			_cacheState = state;

			bodyMesh.localScale = new Vector3(
				(float)(1e+2 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
			    (float)(1e+2 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
			    (float)(1e+2 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer2() {
		if (currentDistance >= 1e+8) {
			SetState (State.ScaleLayer3);
		} else if (currentDistance < 1e+7) {
			SetState (State.ScaleLayer1);
		}
		transform.position = CalculatePosition (1e+7, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (9, 1e+8, 1e+7, State.ScaleLayer3, State.ScaleLayer1);
		if (_cacheState != state) {
			SetLayer (9);
			bodyMesh.localScale = new Vector3(
				(float)(1e+1 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e+1 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e+1 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer3() {
		if (currentDistance >= 1e+9) {
			SetState (State.ScaleLayer4);
		} else if (currentDistance < 1e+8) {
			SetState (State.ScaleLayer2);
		}
		transform.position = CalculatePosition (1e+8, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (10, 1e+9, 1e+8, State.ScaleLayer4, State.ScaleLayer2);
		if (_cacheState != state) {
			SetLayer (10);
			bodyMesh.localScale = new Vector3(
				(float)(1 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer4() {
		if (currentDistance >= 1e+10) {
			SetState (State.ScaleLayer5);
		} else if (currentDistance < 1e+9) {
			SetState (State.ScaleLayer3);
		}
		transform.position = CalculatePosition (1e+9, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (11, 1e+10, 1e+9, State.ScaleLayer5, State.ScaleLayer3);
		if (_cacheState != state) {
			SetLayer (11);
			bodyMesh.localScale = new Vector3(
				(float)(1e-1 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-1 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-1 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer5() {
		if (currentDistance >= 1e+11) {
			SetState (State.ScaleLayer6);
		} else if (currentDistance < 1e+10) {
			SetState (State.ScaleLayer4);
		}
		transform.position = CalculatePosition (1e+10, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (12, 1e+11, 1e+10, State.ScaleLayer6, State.ScaleLayer4);
		if (_cacheState != state) {
			SetLayer (12);
			bodyMesh.localScale = new Vector3(
				(float)(1e-2 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-2 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-2 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer6() {
		if (currentDistance >= 1e+12) {
			SetState (State.ScaleLayer7);
		} else if (currentDistance < 1e+11) {
			SetState (State.ScaleLayer5);
		}
		transform.position = CalculatePosition (1e+11, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (13, 1e+12, 1e+11, State.ScaleLayer7, State.ScaleLayer5);
		if (_cacheState != state) {
			SetLayer (13);
			bodyMesh.localScale = new Vector3(
				(float)(1e-3 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-3 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-3 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer7() {
		if (currentDistance >= 1e+13) {
			SetState (State.ScaleLayer8);
		} else if (currentDistance < 1e+12) {
			SetState (State.ScaleLayer6);
		}
		transform.position = CalculatePosition (1e+12, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (14, 1e+13, 1e+12, State.ScaleLayer8, State.ScaleLayer6);
		if (_cacheState != state) {
			SetLayer (14);
			bodyMesh.localScale = new Vector3(
				(float)(1e-4 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-4 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-4 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer8() {
		if (currentDistance >= 1e+14) {
			SetState (State.ScaleLayer9);
		} else if (currentDistance < 1e+13) {
			SetState (State.ScaleLayer7);
		}
		transform.position = CalculatePosition (1e+13, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (15, 1e+14, 1e+13, State.ScaleLayer9, State.ScaleLayer7);
		if (_cacheState != state) {
			SetLayer (15);
			bodyMesh.localScale = new Vector3(
				(float)(1e-5 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-5 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-5 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer9() {
		if (currentDistance >= 1e+15) {
			SetState (State.ScaleLayer10);
		} else if (currentDistance < 1e+14) {
			SetState (State.ScaleLayer8);
		}
		transform.position = CalculatePosition (1e+14, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (16, 1e+15, 1e+14, State.ScaleLayer10, State.ScaleLayer8);
		if (_cacheState != state) {
			SetLayer (16);
			bodyMesh.localScale = new Vector3(
				(float)(1e-6 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-6 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-6 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer10() {
		if (currentDistance >= 1e+16) {
			SetState (State.ScaleLayer11);
		} else if (currentDistance < 1e+15) {
			SetState (State.ScaleLayer9);
		}
		transform.position = CalculatePosition (1e+15, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (17, 1e+16, 1e+15, State.ScaleLayer11, State.ScaleLayer9);
		if (_cacheState != state) {
			SetLayer (17);
			bodyMesh.localScale = new Vector3(
				(float)(1e-7 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-7 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-7 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer11() {
		if (currentDistance >= 1e+17) {
			SetState (State.ScaleLayer12);
		} else if (currentDistance < 1e+16) {
			SetState (State.ScaleLayer10);
		}
		transform.position = CalculatePosition (1e+16, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (18, 1e+17, 1e+16, State.ScaleLayer12, State.ScaleLayer10);
		if (_cacheState != state) {
			SetLayer (18);
			bodyMesh.localScale = new Vector3(
				(float)(1e-8 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-8 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-8 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer12() {
		if (currentDistance >= 1e+18) {
			SetState (State.ScaleLayer13);
		} else if (currentDistance < 1e+17) {
			SetState (State.ScaleLayer11);
		}
		transform.position = CalculatePosition (1e+17, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (19, 1e+18, 1e+17, State.ScaleLayer13, State.ScaleLayer11);
		if (_cacheState != state) {
			SetLayer (19);
			bodyMesh.localScale = new Vector3(
				(float)(1e-9 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-9 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-9 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer13() {
		if (currentDistance >= 1e+19) {
			SetState (State.ScaleLayer14);
		} else if (currentDistance < 1e+18) {
			SetState (State.ScaleLayer12);
		}
		transform.position = CalculatePosition (1e+18, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (20, 1e+19, 1e+18, State.ScaleLayer14, State.ScaleLayer12);
		if (_cacheState != state) {
			SetLayer (20);
			bodyMesh.localScale = new Vector3(
				(float)(1e-10 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-10 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-10 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer14() {
		if (currentDistance >= 1e+20) {
			SetState (State.ScaleLayer15);
		} else if (currentDistance < 1e+19) {
			SetState (State.ScaleLayer13);
		}
		transform.position = CalculatePosition (1e+19, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (21, 1e+20, 1e+19, State.ScaleLayer15, State.ScaleLayer13);
		if (_cacheState != state) {
			SetLayer (21);
			bodyMesh.localScale = new Vector3(
				(float)(1e+11 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e+11 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e+11 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer15() {
		if (currentDistance >= 1e+21) {
			SetState (State.ScaleLayer16);
		} else if (currentDistance < 1e+20) {
			SetState (State.ScaleLayer14);
		}
		transform.position = CalculatePosition (1e+20, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (22, 1e+21, 1e+20, State.ScaleLayer16, State.ScaleLayer14);
		if (_cacheState != state) {
			SetLayer (22);
			bodyMesh.localScale = new Vector3(
				(float)(1e-12 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-12 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-12 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer16() {
		if (currentDistance >= 1e+22) {
			SetState (State.ScaleLayer17);
		} else if (currentDistance < 1e+21) {
			SetState (State.ScaleLayer15);
		}
		transform.position = CalculatePosition (1e+21, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (23, 1e+22, 1e+21, State.ScaleLayer17, State.ScaleLayer15);
		if (_cacheState != state) {
			SetLayer (23);
			bodyMesh.localScale = new Vector3(
				(float)(1e-13 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-13 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-13 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer17() {
		if (currentDistance >= 1e+23) {
			SetState (State.ScaleLayer18);
		} else if (currentDistance < 1e+22) {
			SetState (State.ScaleLayer16);
		}
		transform.position = CalculatePosition (1e+22, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (23, 1e+23, 1e+22, State.ScaleLayer18, State.ScaleLayer16);
		if (_cacheState != state) {
			SetLayer (24);
			bodyMesh.localScale = new Vector3(
				(float)(1e-14 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-14 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-14 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}

	void ScaleLayer18() {
		if (currentDistance < 1e+23) {
			SetState (State.ScaleLayer17);
		}
		transform.position = CalculatePosition (1e+23, new Vector3d (0d, 0d, 0d));
		//CalculateStateValues (23, 1e+24, 0, State.ScaleNull, State.ScaleLayer17);
		if (_cacheState != state) {
			SetLayer (25);
			bodyMesh.localScale = new Vector3(
				(float)(1e-15 * realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-15 * realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				(float)(1e-15 * realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
		}
	}



	void SetLayer(int newLayer) {
		gameObject.layer = newLayer;
		foreach(Transform child in allChildren) {            
			child.gameObject.layer = newLayer;
		}
	}

}

