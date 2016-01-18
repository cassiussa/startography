using UnityEngine;
using System.Collections;

public class DistanceMarkerScaleStates : Functions {

	public enum Size {
		Initialize,
		AstronomicalUnits,
		LightHours,
		LightDays,
		LightYears,
		LightDecades,
		LightCenturies
	}

	public enum Scale { 
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


	Size _prevSize;
	Size _cacheSize;

	public GameObject star;								// Attained by the ScaleStates.cs script assigning it.  Forget why I thought I needed this here.
	public Size size = Size.Initialize;					// The initial state as it needs to switch on Start/Awake to perform needed operations

	public Scale scale = Scale.Initialize;
	Scale _prevScale;
	Scale _cacheScale;

	Circle scaleCirclesScript;
	LineRenderer scaleCircleLines;

	double ratio = 0d;

	#region Basic Getters/Setters
	public Size CurrentSize {
		get { return size; }
	}
	public Size PrevSize {
		get { return _prevSize; }
	}

	public Scale CurrentScale {
		get { return scale; }
	}
	public Scale PrevScale {
		get { return _prevScale; }
	}
	#endregion

	void Awake() {
		scaleCirclesScript = GetComponentInChildren<Circle> ();
		scaleCircleLines = scaleCirclesScript.gameObject.GetComponent<LineRenderer> ();
		scaleCirclesScript.horizCirclePoints = (100);
		scaleCircleLines.SetVertexCount (scaleCirclesScript.horizCirclePoints + 1);
		scaleCircleLines.useWorldSpace = false;
		// These are measured in Units
		scaleCirclesScript.xradius = 10000f;
		scaleCirclesScript.yradius = 10000f;
		scaleCircleLines.SetWidth(scaleCirclesScript.xradius / 1000, scaleCirclesScript.xradius / 1000);
		scaleCirclesScript.CreatePoints (scaleCircleLines);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheSize != size) {
				switch(size) {
					case Size.AstronomicalUnits: AstronomicalUnits(); break;
					case Size.LightHours: LightHours(); break;
					case Size.LightDays: LightDays(); break;
					case Size.LightYears: LightYears(); break;
					case Size.LightDecades: LightDecades(); break;
					case Size.LightCenturies: LightCenturies(); break;
				}

				switch (scale) {
					case Scale.Initialize: break;
					case Scale.SubMillion: SubMillion (); break;
					case Scale.MillionKilometers: MillionKilometers (); break;
					case Scale.AstronomicalUnit: AstronomicalUnit (); break;
					case Scale.LightHour: LightHour (); break;
					case Scale.LightDay: LightDay (); break;
					case Scale.LightYear: LightYear (); break;
					case Scale.Parsec: Parsec (); break;
					case Scale.LightDecade: LightDecade (); break;
					case Scale.LightCentury: LightCentury (); break;
					case Scale.LightMillenium: LightMillenium (); break;
				}
			}
			yield return null;
		}
	}

	public void SetSize(Size newSize) {
		_prevSize = size;
		size = newSize;
	}

	/*
	 * The states of Size that this Distace Marker exists as
	 */
	void AstronomicalUnits() {
		Debug.Log ("In AstronomicalUnits");
		ScaleAndPosition(AU);
	}

	void LightHours() {
		Debug.Log ("In LightHours");
		ScaleAndPosition(LH);
	}
	
	void LightDays() {
		Debug.Log ("In LightDays");
		ScaleAndPosition(Ld);
	}
	
	void LightYears() {
		Debug.Log ("In LightYears");
		ScaleAndPosition(LY);
	}
	
	void LightDecades() {
		Debug.Log ("In LightDecades");
		ScaleAndPosition(LD);
	}
	
	void LightCenturies() {
		Debug.Log ("In LightCenturies");
		ScaleAndPosition(LC);
	}

	int reRun = 0;
	void ScaleAndPosition(double scale) {
		reRun++;
		if (reRun < 2) {
			return;
		} else {
			ratio = scale / AU;
			transform.position = star.transform.position;
			double scaling = ratio / (double)star.transform.localScale.x;
			transform.parent = star.transform;
			transform.localScale = new Vector3 ((float)(scaling), (float)(scaling), (float)(scaling));
			_cacheSize = size;
		}
	}





	void SubMillion() {																				// This State is heavily commented as each other state uses same conditions
		/*CalculatePosition (SM, positionProcessingScript.position, positioningScript.camPosition);	// Calculate the relative position based on real position and scale of this State
		layerMask = 8;
		if (_cacheState != state) {																	// Without this we get crazy bugs.  Don't know why.  It needs to be here for code efficiency anyways!
			StateFunction(layerMask, SM, "SM", 1f, "", "SM", "MK", 0d, SM, MK);
			
			_cacheState = state;
			
			if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {			// Check if this gameObject is, or contains, a light
				Lights(true, "MK", MK);																	// Activate or deactivate the lights, depending on state
				DistanceMarkerScaleUpdate();
			}
		}*/
	}
	
	void MillionKilometers() {
	}
	
	void AstronomicalUnit() {
	}
	
	void LightHour() {
	}
	
	void LightDay() {
	}
	
	void LightYear() {
	}
	
	void Parsec() {
	}
	
	void LightDecade() {
	}
	
	void LightCentury() {
	}
	
	
	void LightMillenium() {
	}

}