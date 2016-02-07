using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public int lengthOfLineRenderer = 1;
	public int lineSegments = 300;
	public PositionProcessing positionProcessingScript;
	public Positioning positioningScript;
	public ScaleStates scaleStatesScript;
	//public State state;

	Vector3d curPosition;
	Vector3d cachedPosition;
	List<Vector3> lineArray = new List<Vector3>();
	int layerMask = 8;
	Dictionary<ScaleStates.State, double> scales = new Dictionary<ScaleStates.State, double>();	// Allows us to convert string as variable names

	// Use this for initialization
	void Start () {
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		curPosition = positionProcessingScript.position;
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		lineRenderer.SetPosition(0, CalculatePosition(LH, curPosition, positioningScript.camPosition) );

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

	}
	
	// Update is called once per frame
	void Update () {
		//positionProcessingScript.position.x += 100;					// **** FOR TESTING ONLY *** //
		//curPosition = positionProcessingScript.position;			// This is a literal assignment.  Both are now the same item, not just equal to the same values
		// Check to see if the object has gone farther than 20000k.  If so, update the LineRenderer
		if (System.Math.Abs (v3dDistance(curPosition, cachedPosition)) >= 20000d) {
			// We have the real position in space so lets get the localized Vector3 of where it should be
			// CalculatePosition(double, Vector3d, Vector3d (camera));

			double thisScaleSize = scales[scaleStatesScript.state];	// Get the double value from the Dictionary based on using the ScaleStates state
			lineArray.Insert(0,CalculatePosition(thisScaleSize, curPosition, positioningScript.camPosition));

			// Iterate over all the items in the List so that we can assign them to the LineRenderer's array of positions
			if(lengthOfLineRenderer >= lineSegments) {							// Remove the last item from the list, insert the current into the front
				lineArray.RemoveAt(lineSegments);								// Remove at 200 (not 201) because we've gone over 200 items (0 is first)
			}
			if(lineArray.Count > lengthOfLineRenderer) {						// Check if we have yet to fill up the array
				lengthOfLineRenderer = lineArray.Count;							
				lineRenderer.SetVertexCount(lengthOfLineRenderer);
			}
			for(int i=0;i<lineArray.Count;i++) {
				lineRenderer.SetPosition(i, lineArray[i]);
			}

			// Set the cachedPosition to the current position so we can start the conditional again
			cachedPosition = new Vector3d(
				positionProcessingScript.position.x,
				positionProcessingScript.position.y,
				positionProcessingScript.position.z);				// Get the real position from the planet's PositionProcessing script
		}
	}
}
