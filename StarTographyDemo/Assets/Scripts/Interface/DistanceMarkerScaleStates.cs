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
		LightParsecs
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
		lineSizes.Add (0d,Size.Initialize);
		lineSizes.Add (AU,Size.AstronomicalUnits);
		lineSizes.Add (LH,Size.LightHours);
		lineSizes.Add (Ld,Size.LightDays);
		lineSizes.Add (LY,Size.LightYears);
		lineSizes.Add (PA,Size.LightParsecs);

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
					case Size.LightParsecs: LightParsecs(); break;
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
		//Debug.Log ("In AstronomicalUnits");
		ScaleAndPosition(AU);
	}

	void LightHours() {
		//Debug.Log ("In LightHours");
		ScaleAndPosition(LH);
	}
	
	void LightDays() {
		//Debug.Log ("In LightDays");
		ScaleAndPosition(Ld);
	}
	
	void LightYears() {
		//Debug.Log ("In LightYears");
		ScaleAndPosition(LY);
	}
	
	void LightParsecs() {
		//Debug.Log ("In LightDecades");
		ScaleAndPosition(LD);
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




	// Let empty because we don't use it for anything, but we do need to keep it
	void SubMillion() {
		_cacheScale = scale;
	}
	
	void MillionKilometers() {
		//Debug.LogError (System.Enum.GetValues(typeof(Size)).Length);
		_cacheScale = scale;
	}

	void AstronomicalUnit() {
		CalculateLineRenderSizes (0d, AU, LH);
		_cacheScale = scale;
	}
	
	void LightHour() {
		CalculateLineRenderSizes (AU, LH, Ld);
		_cacheScale = scale;
	}
	
	void LightDay() {
		CalculateLineRenderSizes (LH, Ld, LY);
		_cacheScale = scale;
	}
	
	void LightYear() {
		CalculateLineRenderSizes (Ld, LY, PA);
		_cacheScale = scale;
	}
	
	void Parsec() {
		CalculateLineRenderSizes (LY, PA, 0d);
		_cacheScale = scale;
	}
	
	void LightDecade() {
		_cacheScale = scale;
	}
	
	void LightCentury() {
		_cacheScale = scale;
	}
	
	void LightMillenium() {
		_cacheScale = scale;
	}


	/*
	 * This function uses a Dictionary variable and will
	 * check to see he current Size State being passed 
	 * by the dictionary variable is the same as the Size 
	 * State that this gameObject is set to.  It uses the
	 * Constants values as Indexes on the variable to
	 * run the conditional.  It then uses those same values
	 * again, but to calculate the LineRenderer's Start and End
	 * sizes.
	 */
	void CalculateLineRenderSizes(double beforeD, double currentD, double afterD) {
		float lineWidth = 0f;
		if (size == lineSizes [beforeD]) {
			lineWidth = (float)((beforeD / currentD) * baseLineWidth);
		} else if (size == lineSizes [currentD]) {
			lineWidth = (float)baseLineWidth;
		} else if (size == lineSizes [afterD]) {
			lineWidth = (float)((afterD / currentD) * baseLineWidth);
		}
		scaleCircleLines.SetWidth (lineWidth, lineWidth);
	}



}