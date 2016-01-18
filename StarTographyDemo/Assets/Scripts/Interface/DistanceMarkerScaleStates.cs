using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceMarkerScaleStates : Functions {

	public enum Size {
		Initialize,
		AstronomicalUnits,
		LightHours,
		LightDays,
		LightYears,
		//LightDecades,
		//LightCenturies
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

	public double baseLineWidth = 21d;					// This is the base Start and End width of the LineRenderer (21 is same as AU)
	Dictionary<double, Size> lineSizes = new Dictionary<double, Size>();

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
		scaleCirclesScript.horizCirclePoints = 200;
		scaleCircleLines.SetVertexCount (scaleCirclesScript.horizCirclePoints + 1);
		scaleCircleLines.useWorldSpace = false;
		// These are measured in Units
		scaleCirclesScript.xradius = 10000f;
		scaleCirclesScript.yradius = 10000f;
		scaleCircleLines.SetWidth(3, 3);
		scaleCirclesScript.CreatePoints (scaleCircleLines);

		/*
		 * Add a Dictionary of the Double scale sizes that we can then
		 * use as indexes to get the States.  This is done because we
		 * need to use a sliding scale of adjustments for the Start
		 * and End sizes of the LineRenderers.  The farther out we go,
		 * the larger the baseLineWidth would therefore become.
		 */
		lineSizes.Add (AU,Size.AstronomicalUnits);
		lineSizes.Add (LH,Size.LightHours);
		lineSizes.Add (Ld,Size.LightDays);
		lineSizes.Add (LY,Size.LightYears);
		//lineSizes.Add (LD,Size.LightDecades);
		//lineSizes.Add (LC,Size.LightCenturies);

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
					//case Size.LightDecades: LightDecades(); break;
					//case Size.LightCenturies: LightCenturies(); break;
				}
			}
			if(_cacheScale != scale) {
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
		//Debug.LogError (System.Enum.GetValues(typeof(Size)).Length);
		float lineWidth = (float)((AU / MK) * baseLineWidth);
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		//Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;
	}
	
	void AstronomicalUnit() {
		//Debug.LogError (System.Enum.GetValues(typeof(Size)).Length);
		float lineWidth;
		if(size == Size.AstronomicalUnits) {
			lineWidth = (float)((AU / AU) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightHours) {
			lineWidth = (float)((LH / AU) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightDays) {
			lineWidth = (float)((Ld / AU) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightYears) {
			lineWidth = (float)((LY / AU) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		}
		_cacheScale = scale;
	}
	
	void LightHour() {
		float lineWidth;
		if(size == Size.AstronomicalUnits) {
			lineWidth = (float)((AU / LH) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightHours) {
			lineWidth = (float)((LH / LH) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightDays) {
			lineWidth = (float)((Ld / LH) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightYears) {
			lineWidth = (float)((LY / LH) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		}
		_cacheScale = scale;
		/*
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;*/
	}
	
	void LightDay() {
		float lineWidth;
		if(size == Size.AstronomicalUnits) {
			lineWidth = (float)((AU / Ld) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightHours) {
			lineWidth = (float)((LH / Ld) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightDays) {
			lineWidth = (float)((Ld / Ld) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightYears) {
			lineWidth = (float)((LY / Ld) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		/*} else if(size == Size.LightDecades) {
			lineWidth = (float)((LD / Ld) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);*/
		}
		_cacheScale = scale;
	}
	
	void LightYear() {
		float lineWidth;
		if(size == Size.LightHours) {
			lineWidth = (float)((LH / LY) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightDays) {
			lineWidth = (float)((Ld / LY) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightYears) {
			lineWidth = (float)((LY / LY) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		/*} else if(size == Size.LightDecades) {
			lineWidth = (float)((LD / LY) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);
		} else if(size == Size.LightCenturies) {
			lineWidth = (float)((LC / LY) * baseLineWidth);
			scaleCircleLines.SetWidth(lineWidth,lineWidth);*/
		}
		_cacheScale = scale;
	}
	
	void Parsec() {
		float lineWidth = (float)((AU / PA) * baseLineWidth);
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;
	}
	
	void LightDecade() {
		float lineWidth = (float)((AU / LD) * baseLineWidth);
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;
	}
	
	void LightCentury() {
		float lineWidth = (float)((AU / LC) * baseLineWidth);
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;
	}
	
	void LightMillenium() {
		float lineWidth = (float)((AU / LM) * baseLineWidth);
		scaleCircleLines.SetWidth(lineWidth,lineWidth);
		Debug.LogError ("Width is " + lineWidth, gameObject);
		_cacheScale = scale;
	}

}