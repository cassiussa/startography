using UnityEngine;
using System.Collections;

public class CameraSpeedStates : Functions {

	public enum State { 
		Initialize, 
		SM,
		MK,
		AU,
		LH,
		Ld,
		LY,
		PA,
		LD,
		LC,
		LM
	}
	
	public State state = State.Initialize;
	State _prevState;
	State _cacheState;
	
	/* 
	 * Check to see what type of item this script is attached to.  For example,
	 * if it's a camera, we'll update the clipping upon state change.  If it's
	 * something else, like a planet or star, we'll change the scale and position
	 * to the appropriate location and size.
	 */

	Positioning positionScript;
	
	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	void Awake() {
		positionScript = GetComponent<Positioning> ();
		if (!positionScript)
			Debug.LogError ("There is no Positioning script attached to this gameObject.  It is required", gameObject);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
				switch (state) {
				case State.Initialize:
					break;
				case State.SM:
					SM ();
					break;
				case State.MK:
					MK ();
					break;
				case State.AU:
					AU ();
					break;
				case State.LH:
					LH ();
					break;
				case State.Ld:
					Ld ();
					break;
				case State.LY:
					LY ();
					break;
				case State.PA:
					PA ();
					break;
				case State.LD:
					LD ();
					break;
				case State.LC:
					LC ();
					break;
				case State.LM:
					LM ();
					break;
				}
			}
			yield return null;
		}
	}

	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	
	
	void SM() {								// This State is heavily commented as each other state uses same conditions		
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 30f;
		positionScript.holdTimeMax = 300f;
		_cacheState = state;
	}

	void MK() {
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 300f;
		positionScript.holdTimeMax = 3000f;
		_cacheState = state;
	}

	void AU() {	
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 3000f;
		positionScript.holdTimeMax = 30000f;
		_cacheState = state;
	}

	void LH() {	
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 30000f;
		positionScript.holdTimeMax = 300000f;
		_cacheState = state;
	}

	void Ld() {	
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 300000f;
		positionScript.holdTimeMax = 3000000f;
		_cacheState = state;
	}

	void LY() {	
		//Debug.LogError ("Entered state " + state);
		positionScript.holdTimeMin = 3000000f;
		positionScript.holdTimeMax = 30000000f;
		_cacheState = state;
	}

	void PA() {	
		positionScript.holdTimeMin = 30000000f;
		positionScript.holdTimeMax = 300000000f;
		_cacheState = state;
	}

	void LD() {	
		positionScript.holdTimeMin = 300000000f;
		positionScript.holdTimeMax = 3000000000f;
		_cacheState = state;
	}

	void LC() {	
		positionScript.holdTimeMin = 3000000000f;
		positionScript.holdTimeMax = 30000000000f;
		_cacheState = state;
	}

	void LM() {	
		positionScript.holdTimeMin = 3000000000f;
		positionScript.holdTimeMax = 30000000000f;
		_cacheState = state;
	}

}
