using UnityEngine;
using System.Collections;

public class DistanceMarkerScaleStates : Functions {

	public enum Size {
		AstronomicalUnits,
		LightHours,
		LightDays,
		LightYears,
		LightDecades,
		LightCenturies
	}

	public enum State { 
		Initialize, 
		SubMillion, 
		MillionKilometers, 
		AstronomicalUnit, 
		LightHour, 
		LightDay, 
		LightYear, 
		Parsec, 
		LightDecade, 
		LightCentury, 
		LightMillenium
	}

	public Size size = Size.AstronomicalUnits;										// The variable to hold what size of Distance Marker this is
	public State state = State.Initialize;											// The variable to hold what scale we're currently within
	State _prevState;
	State _cacheState;

	Circle scaleCirclesScript;
	LineRenderer scaleCircleLines;

	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}
	#endregion
	
	void Awake() {
		scaleCirclesScript = GetComponentInChildren<Circle> ();
		scaleCircleLines = scaleCirclesScript.gameObject.GetComponent<LineRenderer> ();

		scaleCirclesScript.horizCirclePoints = (int)Mathf.Round(scaleCirclesScript.xradius / 10);
		scaleCircleLines.SetVertexCount (scaleCirclesScript.horizCirclePoints + 1);
		scaleCircleLines.useWorldSpace = false;
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			switch(size) {
				case Size.AstronomicalUnits: AstronomicalUnits(); break;
				case Size.LightHours: LightHours(); break;
				case Size.LightDays: LightDays(); break;
				case Size.LightYears: LightYears(); break;
				case Size.LightDecades: LightDecades(); break;
				case Size.LightCenturies: LightCenturies(); break;
			}
			switch (state) {
				case State.Initialize: break;
				case State.SubMillion: SubMillion (); break;
				case State.MillionKilometers: MillionKilometers (); break;
				case State.AstronomicalUnit: AstronomicalUnit (); break;
				case State.LightHour: LightHour (); break;
				case State.LightDay: LightDay (); break;
				case State.LightYear: LightYear (); break;
				case State.Parsec: Parsec (); break;
				case State.LightDecade: LightDecade (); break;
				case State.LightCentury: LightCentury (); break;
			case State.LightMillenium: LightMillenium (); break;
			}
			yield return null;
		}
	}
	
	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}
	

	/*
	 * The states of Size that this Distace Marker exists as
	 */
	void AstronomicalUnits() {
		Debug.Log ("In AstronomicalUnits");

		scaleCirclesScript.xradius = 1081f;
		scaleCirclesScript.yradius = 1081f;

		scaleCircleLines.SetWidth(scaleCirclesScript.xradius/540f, scaleCirclesScript.xradius/540f);
		scaleCirclesScript.CreatePoints (scaleCircleLines);


	}
	
	void LightHours() {
		Debug.Log ("In LightHours");
	}
	
	void LightDays() {
		Debug.Log ("In LightDays");
	}
	
	void LightYears() {
		Debug.Log ("In LightYears");
	}
	
	void LightDecades() {
		Debug.Log ("In LightDecades");
	}
	
	void LightCenturies() {
		Debug.Log ("In LightCenturies");
	}



	void SubMillion() {	
		_cacheState = state;
	}
	
	void MillionKilometers() {
		_cacheState = state;
	}
	
	void AstronomicalUnit() {	
		_cacheState = state;
	}
	
	void LightHour() {	
		_cacheState = state;
	}
	
	void LightDay() {	
		_cacheState = state;
	}
	
	void LightYear() {	
		_cacheState = state;
	}
	
	void Parsec() {
		_cacheState = state;
	}
	
	void LightDecade() {	
		_cacheState = state;
	}
	
	void LightCentury() {	
		_cacheState = state;
	}
	
	void LightMillenium() {	
		_cacheState = state;
	}
	


}