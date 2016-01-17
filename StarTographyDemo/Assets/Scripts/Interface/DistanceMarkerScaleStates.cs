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
	

	public GameObject star;	// Attained by the ScaleStates.cs script assigning it.  Forget why I thought I needed this here.

	public Size size = Size.AstronomicalUnits;										// The variable to hold what size of Distance Marker this is


	Circle scaleCirclesScript;
	LineRenderer scaleCircleLines;

	double ratio = 0d;
	

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
			switch(size) {
				case Size.AstronomicalUnits: AstronomicalUnits(); break;
				case Size.LightHours: LightHours(); break;
				case Size.LightDays: LightDays(); break;
				case Size.LightYears: LightYears(); break;
				case Size.LightDecades: LightDecades(); break;
				case Size.LightCenturies: LightCenturies(); break;
			}

			yield return null;
		}
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


	void ScaleAndPosition(double scale) {
		ratio = scale / AU;
		transform.position = star.transform.position;
		double scaling = ratio/(double)star.transform.localScale.x;
		transform.parent = star.transform;
		transform.localScale = new Vector3 ( (float)(scaling),(float)(scaling),(float)(scaling) );
	}



}