﻿using UnityEngine;
using System.Collections;
using Functions;
using Globals;

public class ScaleStates : MonoBehaviour {

	public enum State { ScaleNull, ScaleLayer1, ScaleLayer2, ScaleLayer3, ScaleLayer4, ScaleLayer5, ScaleLayer6, ScaleLayer7, ScaleLayer8, ScaleLayer9, ScaleLayer10, ScaleLayer11, ScaleLayer12, ScaleLayer13, ScaleLayer14, ScaleLayer15, ScaleLayer16, ScaleLayer17, ScaleLayer18 }

	public State state = State.ScaleNull;
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

		//realPosition.z -= 100000001;
		if (gameObject.name == "[STAR] Sun")
			print("Update()");
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
	
	void ScaleLayer1() {
		StateChecks (1e+7d,							// The minimum value of the state above this state
		             State.ScaleLayer2,				// The state above this State
		             double.MinValue,				// The minimum value we can use for this state
		             State.ScaleNull,				// Otherwise, drop down to the next lowest State
		             1e+6d,							// Scale State size.  1,000,000 in this case (makes this max: 9,999,999.999999999~)
		             new Vector3d (0d, 0d, 0d),		// The relative position of the camera
		             8,								// The Layer that this State occupies
		             1e-2d);						// The scale multiplier
	}

	void ScaleLayer2() {
		StateChecks (1e+8d, State.ScaleLayer3, 1e+7d, State.ScaleLayer1, 1e+7d, new Vector3d (0d, 0d, 0d), 9, 1e-3d);
	}

	void ScaleLayer3() {
		StateChecks (1e+9d, State.ScaleLayer4, 1e+8d, State.ScaleLayer2, 1e+8d, new Vector3d (0d, 0d, 0d), 10, 1e-4d);
	}

	void ScaleLayer4() {
		StateChecks (1e+10d, State.ScaleLayer5, 1e+9d, State.ScaleLayer3, 1e+9d, new Vector3d (0d, 0d, 0d), 11, 1e-5d);
	}

	void ScaleLayer5() {
		StateChecks (1e+11d, State.ScaleLayer6, 1e+10d, State.ScaleLayer4, 1e+10d, new Vector3d (0d, 0d, 0d), 12, 1e-6d);
	}

	void ScaleLayer6() {
		StateChecks (1e+12d, State.ScaleLayer7, 1e+11d, State.ScaleLayer5, 1e+11d, new Vector3d (0d, 0d, 0d), 13, 1e-7d);
	}

	void ScaleLayer7() {
		StateChecks (1e+13d, State.ScaleLayer8, 1e+12d, State.ScaleLayer6, 1e+12d, new Vector3d (0d, 0d, 0d), 14, 1e-8d);
	}

	void ScaleLayer8() {
		StateChecks (1e+14d, State.ScaleLayer9, 1e+13d, State.ScaleLayer7, 1e+13d, new Vector3d (0d, 0d, 0d), 15, 1e-9d);
	}

	void ScaleLayer9() {
		StateChecks (1e+15d, State.ScaleLayer10, 1e+14d, State.ScaleLayer8, 1e+14d, new Vector3d (0d, 0d, 0d), 16, 1e-10d);
	}

	void ScaleLayer10() {
		StateChecks (1e+16d, State.ScaleLayer11, 1e+15d, State.ScaleLayer9, 1e+15d, new Vector3d (0d, 0d, 0d), 17, 1e-11d);
	}

	void ScaleLayer11() {
		StateChecks (1e+17d, State.ScaleLayer12, 1e+16d, State.ScaleLayer10, 1e+16d, new Vector3d (0d, 0d, 0d), 18, 1e-12d);
	}

	void ScaleLayer12() {
		StateChecks (1e+18d, State.ScaleLayer13, 1e+17d, State.ScaleLayer11, 1e+17d, new Vector3d (0d, 0d, 0d), 19, 1e-13d);
	}

	void ScaleLayer13() {
		StateChecks (1e+19d, State.ScaleLayer14, 1e+18d, State.ScaleLayer12, 1e+18d, new Vector3d (0d, 0d, 0d), 20, 1e-14d);
	}

	void ScaleLayer14() {
		StateChecks (1e+20d, State.ScaleLayer15, 1e+19d, State.ScaleLayer13, 1e+19d, new Vector3d (0d, 0d, 0d), 21, 1e-15d);
	}

	void ScaleLayer15() {
		StateChecks (1e+21d, State.ScaleLayer16, 1e+20d, State.ScaleLayer14, 1e+20d, new Vector3d (0d, 0d, 0d), 22, 1e-16d);
	}

	void ScaleLayer16() {
		StateChecks (1e+22d, State.ScaleLayer17, 1e+21d, State.ScaleLayer15, 1e+21d, new Vector3d (0d, 0d, 0d), 23, 1e-17d);
	}

	void ScaleLayer17() {
		StateChecks (1e+23d, State.ScaleLayer18, 1e+22d, State.ScaleLayer16, 1e+22d, new Vector3d (0d, 0d, 0d), 24, 1e-18d);
	}

	void ScaleLayer18() {
		StateChecks (double.MaxValue, State.ScaleNull, 1e+23d, State.ScaleLayer17, 1e+23d, new Vector3d (0d, 0d, 0d), 25, 1e-19d);
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




	private void StateChecks(double greaterThan, State nextState, double lessThan, State previousState, double stateScaleSize, Vector3d cameraPosition, int layer, double scaleMultiplier) {
		/*
		 * Perform the operations that the specified state requires
		 * 
		 * Parameters
		 * ----------
		 * greaterThan : The minimum value of the state above this state
		 * nextState : The state above this State
		 * lessThan : The minimum value we can use for this state
		 * previousState : The next lowest State below the current state being checked
		 * stateScaleSize : Scale State size.  1,000,000 for example (makes a max of 9,999,999.999999999~)
		 * cameraPosition : The relative position of the camera
		 * layer : The Layer that this State occupies
		 * scaleMultiplier : The scale multiplier
		 */
		             
		if (currentDistance >= greaterThan) {
			SetState (nextState);
		} else if (currentDistance < lessThan) {
			SetState (previousState);
		} else {
			/* 
			 * We update the position every frame.  If one of the above two conditions is true, we
			 * continue to check in other States until finally this condition is reached within
			 * the same Update()
			 */
			transform.position = CalculatePosition (stateScaleSize, cameraPosition);
			
			/*
			 * If the current state is not the same as the state that we were in in the last Update(),
			 * then readjust the scale of this celestial body, set the cache value, and update the
			 * Layer
			 */
			if (_cacheState != state) {
				SetLayer (layer);
				Vector3d tempV3 = new Vector3d(scaleMultiplier * realRadius * Global.radiusConstantSolar / Global.kmPerUnit); //TODO: come back to this and in Awake do a check to see what type of celestial body.  Use a variable to hold the "radiusConstantSolar" (or radiusConstantX) value.
				bodyMesh.localScale = new Vector3 ((float)tempV3.x, (float)tempV3.y, (float)tempV3.z);
				_cacheState = state;
			}
		}
	}


	public Vector3 CalculatePosition(double stateScale, Vector3d cameraPosition) {
		/*
		 * Calculate the ratio of real position to fit within 10k unit limit
		 * 
		 * Parameters
		 * ----------
		 * stateScale : The distance value of the scale State (ex: 1,000,000)
		 * cameraPosition : A Vector3d value of the real position of the object
		 * 
		 * Actions
		 * -------
		 * Assigns the position to the gameObject that the calling ScaleStates.cs script is attached to
		*/
		
		Vector3d tempV3d = new Vector3d(((realPosition + cameraPosition) / stateScale) * Global.kmPerUnit);
		return new Vector3 ((float)tempV3d.x, (float)tempV3d.y, (float)tempV3d.z);
	}


	void SetLayer(int newLayer) {
		/*
		 * Iterate through the list of all the child transforms and assign the
		 * appropriate layer to the transform's gameObject
		 */
		gameObject.layer = newLayer;
		foreach(Transform child in allChildren) {            
			child.gameObject.layer = newLayer;
		}
	}

}

