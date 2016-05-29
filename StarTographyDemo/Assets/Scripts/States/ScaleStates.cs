using UnityEngine;
using System.Collections;
using Functions;
using Globals;

public class ScaleStates : MonoBehaviour {

	public enum State { ScaleNull, ScaleLayer1, ScaleLayer2, ScaleLayer3, ScaleLayer4, ScaleLayer5, ScaleLayer6, ScaleLayer7, ScaleLayer8, ScaleLayer9, ScaleLayer10, ScaleLayer11, ScaleLayer12, ScaleLayer13, ScaleLayer14, ScaleLayer15, ScaleLayer16, ScaleLayer17, ScaleLayer18 }

	public State state = State.ScaleNull;
	State _prevState = State.ScaleNull;
	public State _cacheState = State.ScaleNull;

	#region Basic Getters/Setters
	public State CurrentState { get { return state; } }
	public State PrevState { get { return _prevState; } }
	#endregion

	public Position positionScript;
	public Radius radiusScript;
	public Vector3d realPosition;
	public Vector3d relativePosition;
	public Vector3d thisPosition = new Vector3d (100d, 100d, 100d);

	public Vector3d realRadius;
	public Vector3d thisRadius = new Vector3d (100d, 100d, 100d);

	public double currentDistance;
	public double radiusFactor;
	public Vector3d thisLocalScale = new Vector3d(1d,1d,1d);
	public Vector3d originalLocalScale;

	public Transform[] allChildren;
	public Transform bodyMesh;

	public Transform[] scaleLayers;

	public enum CelestialBodyType { Star, Planet, Moon }		// These should be the same as found in CelestialBodyBuilder.cs
	public CelestialBodyType celestialBodyType;					// Variable is set upon instantiation of this script by the FormatImportData.cs script
	public GameObject[] lightGameObjects = new GameObject[4];	// The lights the encompass Layers 1-4 for this if it's a Star type

	/*
	 * The following are set every update() by the States
	 */
	int layer = 1;												// Set each state update, used for controlling layer we're on
	double scaleMultiplier = 1d;								// Set each state update, used for scaling
	int parent = 1;												// Set each state update, used to assign the appropriate parent based on the new state
	double tooBig;												// Too big to remain in the current state, so enter the state above the current State
	State higherState;											// The state above the current State
	double tooSmall;											// Too small to remain in the current state, so it drop down one State
	State lowerState;											// The next lowest State below the current state being checked
	double stateScaleSize;										// Scale State size.  1,000,000 for example (makes a max of 9,999,999.999999999~)
	Vector3d cameraPosition;									// The relative position of the camera

	void Awake() {

		if (thisPosition.x == 0d || thisPosition.y == 0d || thisPosition.z == 0d) {
			thisPosition.x = 1d;
			thisPosition.y = 1d;
			thisPosition.z = 1d;
		}
		if (thisRadius.x == 0d || thisRadius.y == 0d || thisRadius.z == 0d) {
			thisRadius.x = 1d;
			thisRadius.y = 1d;
			thisRadius.z = 1d;
		}
		positionScript = gameObject.GetComponent<Position> ();
		radiusScript = gameObject.GetComponent<Radius> ();

		realPosition = positionScript.realPosition;
		relativePosition = positionScript.relativePosition;
		realRadius = radiusScript.realRadius;

		currentDistance = Vector3d.Distance(new Vector3d(0d,0d,0d), realPosition);

		allChildren = gameObject.GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if(child.gameObject.name == "CelestialBodyMesh") {
				bodyMesh = child;
				bodyMesh.localScale = new Vector3((float)(realRadius.x * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.y * Global.radiusConstantSolar/Global.kmPerUnit),
				                                  (float)(realRadius.z * Global.radiusConstantSolar/Global.kmPerUnit));
			}
		}
		scaleLayers = new Transform[18];
		for (int i=0; i<scaleLayers.Length; i++) {
			scaleLayers[i] = GameObject.Find ("/Galaxy/Scale Layers/Scale Layer "+(i+1)).GetComponent<Transform> ();
		}

		if(currentDistance < 1e+7d)
			SetState (State.ScaleLayer1);
		else if(currentDistance < 1e+8d)
			SetState (State.ScaleLayer2);
		else if(currentDistance < 1e+9d)
			SetState (State.ScaleLayer3);
		else if(currentDistance < 1e+10d)
			SetState (State.ScaleLayer4);
		else if(currentDistance < 1e+11d)
			SetState (State.ScaleLayer5);
		else if(currentDistance < 1e+12d)
			SetState (State.ScaleLayer6);
		else if(currentDistance < 1e+13d)
			SetState (State.ScaleLayer7);
		else if(currentDistance < 1e+14d)
			SetState (State.ScaleLayer8);
		else if(currentDistance < 1e+15d)
			SetState (State.ScaleLayer9);
		else if(currentDistance < 1e+16d)
			SetState (State.ScaleLayer10);
		else if(currentDistance < 1e+17d)
			SetState (State.ScaleLayer11);
		else if(currentDistance < 1e+18d)
			SetState (State.ScaleLayer12);
		else if(currentDistance < 1e+19d)
			SetState (State.ScaleLayer13);
		else if(currentDistance < 1e+20d)
			SetState (State.ScaleLayer14);
		else if(currentDistance < 1e+21d)
			SetState (State.ScaleLayer15);
		else if(currentDistance < 1e+22d)
			SetState (State.ScaleLayer16);
		else if(currentDistance < 1e+23d)
			SetState (State.ScaleLayer17);
		else
			SetState (State.ScaleLayer18);

		if(gameObject.name == "[STAR] Sun") print ("At the end of Awake(), _cacheState = " + _cacheState + ", state = " + state+" _prevState = "+_prevState);
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
		state = newState;
	}

	void Update() {
		if (thisPosition.x == 0d || thisPosition.y == 0d || thisPosition.z == 0d) {
			thisPosition.x = 1d;
			thisPosition.y = 1d;
			thisPosition.z = 1d;
		}
		if (thisRadius.x == 0d || thisRadius.y == 0d || thisRadius.z == 0d) {
			thisRadius.x = 1d;
			thisRadius.y = 1d;
			thisRadius.z = 1d;
		}

		currentDistance = Vector3d.Distance(new Vector3d(0d,0d,0d), realPosition);

		CheckDistance (tooBig, higherState, tooSmall, lowerState, stateScaleSize, cameraPosition);

		if (_cacheState != state) {
			StateChecks (layer, scaleMultiplier, parent);
			_cacheState = state;
		}
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
		layer = 8;
		scaleMultiplier = 1e-2d;
		parent = 1;							// Parent. The index is 1 as 0 contains the parent.  This works out nice for the index number matching layer number

		tooBig = 1e+7d;
		higherState = State.ScaleLayer2;
		tooSmall = double.MinValue;
		lowerState = State.ScaleNull;
		stateScaleSize = 1e+6d;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) UpdateLayer(layer);
	}

	void ScaleLayer2() {
		layer = 9;
		scaleMultiplier = 1e-3d;
		parent = 2;

		tooBig = 1e+8d;
		higherState = State.ScaleLayer3;
		tooSmall = 1e+7d;
		lowerState = State.ScaleLayer1;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}
	}

	void ScaleLayer3() {
		layer = 10;
		scaleMultiplier = 1e-4d;
		parent = 3;

		tooBig = 1e+9d;
		higherState = State.ScaleLayer4;
		tooSmall = 1e+8d;
		lowerState = State.ScaleLayer2;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer4() {
		layer = 11;
		scaleMultiplier = 1e-5d;
		parent = 4;

		tooBig = 1e+10d;
		higherState = State.ScaleLayer5;
		tooSmall = 1e+9d;
		lowerState = State.ScaleLayer3;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer5() {
		layer = 12;
		scaleMultiplier = 1e-6d;
		parent = 5;

		tooBig = 1e+11d;
		higherState = State.ScaleLayer6;
		tooSmall = 1e+10d;
		lowerState = State.ScaleLayer4;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer6() {
		layer = 13;
		scaleMultiplier = 1e-7d;
		parent = 6;

		tooBig = 1e+12d;
		higherState = State.ScaleLayer7;
		tooSmall = 1e+11d;
		lowerState = State.ScaleLayer5;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer7() {
		layer = 14;
		scaleMultiplier = 1e-8d;
		parent = 7;

		tooBig = 1e+13d;
		higherState = State.ScaleLayer8;
		tooSmall = 1e+12d;
		lowerState = State.ScaleLayer6;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer8() {
		layer = 15;
		scaleMultiplier = 1e-9d;
		parent = 8;

		tooBig = 1e+14d;
		higherState = State.ScaleLayer9;
		tooSmall = 1e+13d;
		lowerState = State.ScaleLayer7;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer9() {
		layer = 16;
		scaleMultiplier = 1e-10d;
		parent = 9;

		tooBig = 1e+15d;
		higherState = State.ScaleLayer10;
		tooSmall = 1e+14d;
		lowerState = State.ScaleLayer8;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer10() {
		layer = 17;
		scaleMultiplier = 1e-11d;
		parent = 10;

		tooBig = 1e+16d;
		higherState = State.ScaleLayer11;
		tooSmall = 1e+15d;
		lowerState = State.ScaleLayer9;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer11() {
		layer = 18;
		scaleMultiplier = 1e-12d;
		parent = 11;

		tooBig = 1e+17d;
		higherState = State.ScaleLayer12;
		tooSmall = 1e+16d;
		lowerState = State.ScaleLayer10;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer12() {
		layer = 19;
		scaleMultiplier = 1e-13d;
		parent = 12;

		tooBig = 1e+18d;
		higherState = State.ScaleLayer13;
		tooSmall = 1e+17d;
		lowerState = State.ScaleLayer11;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer13() {
		layer = 20;
		scaleMultiplier = 1e-14d;
		parent = 13;

		tooBig = 1e+19d;
		higherState = State.ScaleLayer14;
		tooSmall = 1e+18d;
		lowerState = State.ScaleLayer12;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer14() {
		layer = 21;
		scaleMultiplier = 1e-15d;
		parent = 14;

		tooBig = 1e+20d;
		higherState = State.ScaleLayer15;
		tooSmall = 1e+19d;
		lowerState = State.ScaleLayer13;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer15() {
		layer = 22;
		scaleMultiplier = 1e-16d;
		parent = 15;

		tooBig = 1e+21d;
		higherState = State.ScaleLayer16;
		tooSmall = 1e+20d;
		lowerState = State.ScaleLayer14;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer16() {
		layer = 23;
		scaleMultiplier = 1e-17d;
		parent = 16;

		tooBig = 1e+22d;
		higherState = State.ScaleLayer17;
		tooSmall = 1e+21d;
		lowerState = State.ScaleLayer15;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);


		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}

	void ScaleLayer17() {
		layer = 24;
		scaleMultiplier = 1e-18d;
		parent = 17;

		tooBig = 1e+23d;
		higherState = State.ScaleLayer18;
		tooSmall = 1e+22d;
		lowerState = State.ScaleLayer16;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);

		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

	}


	void ScaleLayer18() {
		layer = 25;
		scaleMultiplier = 1e-19d;
		parent = 18;

		tooBig = double.MaxValue;
		higherState = State.ScaleNull;
		tooSmall = 1e+23d;
		lowerState = State.ScaleLayer17;
		stateScaleSize = tooSmall;
		cameraPosition = new Vector3d (0d, 0d, 0d);


		if (gameObject.layer != layer) {
			UpdateLayer(layer);
		}

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


	private void UpdateLayer(int layer) {
		gameObject.layer = layer;
		foreach (Transform child in allChildren) {            
			child.gameObject.layer = layer;
		}
	}


	private void CheckDistance(double tooBig, State higherState, double tooSmall, State lowerState, double stateScaleSize, Vector3d cameraPosition) {
		/*
		 * tooBig : Too big to remain in this state, so enter the state above this State
		 * higherState : The state above this State
		 * tooSmall : Too small to remain in this state, so it drop down a State
		 * lowerState : The next lowest State below the current state being checked
		 * stateScaleSize : Scale State size.  1,000,000 for example (makes a max of 9,999,999.999999999~)
		 * cameraPosition : The relative position of the camera
		 */

		if (currentDistance >= tooBig) {
			SetState (higherState);
		} else if (currentDistance < tooSmall) {
			SetState (lowerState);
		} else {
			/* 
			 * No update to the state so only perform the below during this Update()
			 * 
			 * We update the position every frame.  If one of the above two conditions is true, we
			 * continue to check in other States until finally this condition is reached within
			 * the same Update()
			 */
			transform.position = CalculatePosition (stateScaleSize, cameraPosition);
			
		}
	}

	private void StateChecks(int layer, double scaleMultiplier, int parent) {
		/*
		 * Perform the operations that the specified state requires
		 * 
		 * Parameters
		 * ----------
		 * layer : The Layer that this State occupies
		 * scaleMultiplier : The scale multiplier
		 * parent : the parent's layer number by layer name
		 */

		if (celestialBodyType == CelestialBodyType.Star) {
			for (int i=0; i<lightGameObjects.Length; i++) {
				if((layer-7) <= 4)
					lightGameObjects [i].SetActive (true);
				else
					lightGameObjects [i].SetActive (false);
			}
		}

		Vector3d tempV3 = new Vector3d(scaleMultiplier * realRadius * Global.radiusConstantSolar / Global.kmPerUnit); //TODO: come back to this and in Awake do a check to see what type of celestial body.  Use a variable to hold the "radiusConstantSolar" (or radiusConstantX) value.
		bodyMesh.localScale = new Vector3 ((float)tempV3.x, (float)tempV3.y, (float)tempV3.z);

		// Make sure that we're not just re-assigning the same parent again every frame
		if (transform.parent != scaleLayers [parent - 1]) {
			Parent (parent);
		}
	}
	
	private void Parent(int index) {
		/*
		 * Sets the parent Scale Layer that the body should currently exist within.
		 * 
		 * Note that index 0 is actually the parent (/Galaxy/Scale Layer/") and not a child 
		 * (/Galaxy/Scale Layer/Scale Layer N") so it isn't used.
		 */
		transform.parent = scaleLayers[index-1];
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

}

