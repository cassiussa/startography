using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public PositionProcessing positionProcessingScript;
	public Positioning positioningScript;
	public Transform planetTransform;
	public ScaleStates scaleStatesScript;
	[HideInInspector]
	public int lineSegments = 10000;
	public double segmentLength;											// This is received from the ParentStar.cs script

	Vector3 scaledPosition;
	Vector3d cachedPosition;
	Vector3d originPosition;
	List<Vector3d> lineArray = new List<Vector3d>();
	int layerMask = 8;
	Dictionary<ScaleStates.State, double> scales = new Dictionary<ScaleStates.State, double>();	// Allows us to convert string as variable names
	

	// Use this for initialization
	void Awake () {
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		lineRenderer.SetVertexCount(1);

		scales.Add (ScaleStates.State.Initialize, SM);
		scales.Add (ScaleStates.State.SubMillion, SM);
		scales.Add (ScaleStates.State.MillionKilometers, MK);
		scales.Add (ScaleStates.State.AstronomicalUnit, AU);
		scales.Add (ScaleStates.State.LightHour, LH);
		scales.Add (ScaleStates.State.LightDay, Ld);
		scales.Add (ScaleStates.State.LightYear, LY);
		scales.Add (ScaleStates.State.Parsec, PA);
		scales.Add (ScaleStates.State.LightDecade, LD);
		scales.Add (ScaleStates.State.LightCentury, LC);
		scales.Add (ScaleStates.State.LightMillenium, LM);

		originPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		lineArray.Insert(0, new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z));
		
		if(lineArray.Count >= lineSegments) lineArray.RemoveAt(lineArray.Count-1);					
		lineRenderer.SetVertexCount(lineArray.Count);
		
		// Set the cachedPosition to the current position so we can start the conditional again
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);	// Get the real position from the planet's PositionProcessing script
	}
	
	// Update is called once per frame
	void Update () {
		double thisScaleSize = scales[scaleStatesScript.state];	// Get the double value from the Dictionary based on using the ScaleStates state

		/*
		 * Take the full scale position and apply it to a Vector3d
		 * variable.  Then convert that into the local ScaleState
		 * position.  Once we have that, we can apply it to the
		 * LineRenderer with the WorldSpace option enabled.
		 * Check every X amount of time or every X amount of distance
		 * and once a threshold is breached update the LineRenderer
		 * positions array.
		 */
		if (planetTransform.gameObject.name == "Earth")
		//positionProcessingScript.position.x += 100;					// **** FOR TESTING ONLY *** //
		// Check to see if the object has gone farther than 20000k.  If so, update the LineRenderer
		if (System.Math.Abs (v3dDistance(positionProcessingScript.position, cachedPosition)) >= segmentLength) {
			/*
			 * Insert scaledPosition into index 0 then insert the transform's position.
			 * NOTE:  We MUST use a new Vector3d here because positionProcessingScript.position
			 * is an Object, not as value
			 */
			lineArray.Insert(0, new Vector3d(
				positionProcessingScript.position.x,
				positionProcessingScript.position.y,
				positionProcessingScript.position.z));

			if(lineArray.Count >= lineSegments) lineArray.RemoveAt(lineArray.Count-1);					
			lineRenderer.SetVertexCount(lineArray.Count);

			// Set the cachedPosition to the current position so we can start the conditional again
			cachedPosition = new Vector3d(
				positionProcessingScript.position.x,
				positionProcessingScript.position.y,
				positionProcessingScript.position.z);	// Get the real position from the planet's PositionProcessing script
		}



		/*
		 * Calculate the current position in space compared to where
		 * this planet started at the beginning.
		 */
		for(int i=0;i<lineArray.Count;i++) {
			if(i == 0) {
				scaledPosition = planetTransform.position;
			} else {
				scaledPosition = CalculatePosition(thisScaleSize, lineArray[i], camPosition);
			}
			lineRenderer.SetPosition(i, scaledPosition);
		}
	}
}
