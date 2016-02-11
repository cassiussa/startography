﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public PositionProcessing positionProcessingScript;
	public Positioning positioningScript;
	public Transform planetTransform;
	public ScaleStates scaleStatesScript;

	int lineSegments = 50;
	Vector3 scaledPosition;
	Vector3d cachedPosition;
	Vector3d originPosition;
	List<Vector3> lineArray = new List<Vector3>();
	int layerMask = 8;
	Dictionary<ScaleStates.State, double> scales = new Dictionary<ScaleStates.State, double>();	// Allows us to convert string as variable names

	// Use this for initialization
	void Start () {
		//lineRenderer.SetVertexCount(lineSegments);
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		// Note that below, 'camPosition' is a global variable
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

		//Debug.Log ("lineArray.Count = " + lineArray.Count);

		originPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		//Debug.Log ("originPosition = " + V3dToV3 (originPosition));
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
		if (System.Math.Abs (v3dDistance(positionProcessingScript.position, cachedPosition)) >= 10000d) {
			// We have the real position in space so lets get the localized Vector3 of where it should be
			// CalculatePosition(double, Vector3d, Vector3d (camera));
			scaledPosition = CalculatePosition(thisScaleSize, positionProcessingScript.position, camPosition);

			// Insert scaledPosition into index 0 then insert the transform's position
			lineArray.Insert(0, planetTransform.position);

			//Debug.Log ("lineArray.Count = "+lineArray.Count+", lineSegments = "+lineSegments);
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
			lineRenderer.SetPosition(i, lineArray[i]);
		}

		//Debug.Log ("camPosition = " + V3dToV3(camPosition));
		lineRenderer.SetPosition (0, planetTransform.position);
	}
}