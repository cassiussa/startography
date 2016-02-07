using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public int lengthOfLineRenderer = 1;
	public PositionProcessing positionProcessingScript;
	public Transform planetTransform;
	public ScaleStates scaleStatesScript;
	//public State state;

	int lineSegments = 300;
	Vector3d startPosition;
	Vector3d curPosition;
	Vector3d cachedPosition;
	Vector3d positionRelativeToOrigin;										// Updated every frame, holds the position difference since start
	Vector3 scaledPosToOrigin;
	List<Vector3> lineArray = new List<Vector3>();
	int layerMask = 8;
	Dictionary<ScaleStates.State, double> scales = new Dictionary<ScaleStates.State, double>();	// Allows us to convert string as variable names

	// Use this for initialization
	void Start () {
		lineRenderer.SetVertexCount(lineSegments);
		curPosition = positionProcessingScript.position;							// Literally equal to each other - same Object
		startPosition = new Vector3d(curPosition.x, curPosition.y, curPosition.z);
		cachedPosition = new Vector3d(curPosition.x, curPosition.y, curPosition.z);

		// Note that below, 'camPosition' is a global variable
		//lineRenderer.SetPosition(0, CalculatePosition(LH, startPosition, camPosition) );
		lineRenderer.SetPosition(0, new Vector3(0f,0f,0f) );

		scales.Add (ScaleStates.State.Initialize, SM);
		scales.Add (ScaleStates.State.SubMillion, SM);
		scales.Add (ScaleStates.State.MillionKilometers, SM);
		scales.Add (ScaleStates.State.AstronomicalUnit, MK);
		scales.Add (ScaleStates.State.LightHour, AU);
		scales.Add (ScaleStates.State.LightDay, LH);
		scales.Add (ScaleStates.State.LightYear, Ld);
		scales.Add (ScaleStates.State.Parsec, LY);
		scales.Add (ScaleStates.State.LightDecade, PA);
		scales.Add (ScaleStates.State.LightCentury, LD);
		scales.Add (ScaleStates.State.LightMillenium, LC);

		for(int i=0;i<lineSegments;i++) {
			lineArray.Add (new Vector3(0,0,0));
			lineRenderer.SetPosition(i,lineArray[i]);
		}
		Debug.Log ("lineArray.Count = " + lineArray.Count);
	}
	
	// Update is called once per frame
	void Update () {
		double thisScaleSize = scales[scaleStatesScript.state];	// Get the double value from the Dictionary based on using the ScaleStates state

		/*
		 * Take the difference between the two full-scale positions
		 * and then convert that over to local space position (which
		 * is a global position).  Convert that global position data
		 * into localPosition data
		 */
		positionRelativeToOrigin = new Vector3d(
			curPosition.x-startPosition.x+camPosition.x,
			curPosition.y-startPosition.y+camPosition.y,
			curPosition.z-startPosition.z+camPosition.z);
		scaledPosToOrigin = ScalePosDiff (scales[scaleStatesScript.state], positionRelativeToOrigin);

		//positionProcessingScript.position.x += 100;					// **** FOR TESTING ONLY *** //
		// Check to see if the object has gone farther than 20000k.  If so, update the LineRenderer
		if (System.Math.Abs (v3dDistance(curPosition, cachedPosition)) >= 10000d) {
			// We have the real position in space so lets get the localized Vector3 of where it should be
			// CalculatePosition(double, Vector3d, Vector3d (camera));

			//lineArray.Insert(0,CalculatePosition(thisScaleSize, curPosition, positioningScript.camPosition));
			//lineArray.Insert(0,ScalePosDiff(scales[scaleStatesScript.state],curPosition));
			//lineArray.Add(ScalePosDiff(LH,curPosition));
			lineArray.Insert(0,scaledPosToOrigin + planetTransform.localPosition);

			// Iterate over all the items in the List so that we can assign them to the LineRenderer's array of positions
			if(lengthOfLineRenderer >= lineSegments) {							// Remove the last item from the list, insert the current into the front
				lineArray.RemoveAt(lineSegments);								// Remove at 200 (not 201) because we've gone over 200 items (0 is first)
				//lineArray.RemoveAt(0);											// Remove at 0
			}
			if(lineArray.Count > lengthOfLineRenderer) {						// Check if we have yet to fill up the array
				lengthOfLineRenderer = lineArray.Count;							
				lineRenderer.SetVertexCount(lengthOfLineRenderer);
			}

			// Set the cachedPosition to the current position so we can start the conditional again
			cachedPosition = new Vector3d(curPosition.x, curPosition.y, curPosition.z);	// Get the real position from the planet's PositionProcessing script
		}



		/*
		 * Calculate the current position in space compared to where
		 * this planet started at the beginning.
		 */
		for(int i=0;i<lineArray.Count;i++) {
			if(i == 0) {
				lineRenderer.SetPosition(i, new Vector3(0,0,0));
			} else {
				//Debug.Log ("i = "+i);
				lineRenderer.SetPosition(i, lineArray[i] - planetTransform.localPosition - scaledPosToOrigin);
			}
		}
	}
}
