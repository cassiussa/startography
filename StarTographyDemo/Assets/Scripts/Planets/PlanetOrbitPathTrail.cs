using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public int lengthOfLineRenderer = 1;
	public PositionProcessing positionProcessingScript;
	public Positioning positioningScript;

	Vector3d curPosition;
	Vector3d cachedPosition;
	List<Vector3> lineArray = new List<Vector3>();

	// Use this for initialization
	void Start () {
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		curPosition = positionProcessingScript.position;
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);

		lineRenderer.SetPosition(0, CalculatePosition(LH, curPosition, positioningScript.camPosition) );
	}
	
	// Update is called once per frame
	void Update () {
		positionProcessingScript.position.x += 100;					// **** FOR TESTING ONLY *** //
		curPosition = positionProcessingScript.position;			// This is a literal assignment.  Both are now the same item, not just equal to the same values
		// Check to see if the object has gone farther than 20000k.  If so, update the LineRenderer
		if (System.Math.Abs (v3dDistance(curPosition, cachedPosition)) >= 20000d) {

			lineArray.Insert(0,CalculatePosition(LH, curPosition, positioningScript.camPosition));
			// We have the real position in space so lets get the localized Vector3 of where it should be
			// CalculatePosition(double, Vector3d, Vector3d (camera));

			// Iterate over all the items in the List so that we can assign them to the LineRenderer's array of positions
			for(int i=0;i<lineArray.Count;i++) {
				if(lengthOfLineRenderer <= 200) {							// No point in processing more if it's greater than 200 items
					if(lineArray.Count > lengthOfLineRenderer) {			// Check if
						lengthOfLineRenderer = lineArray.Count;
						lineRenderer.SetVertexCount(lengthOfLineRenderer);
					}
				} else {													// Remove the last item from the list, insert the current into the front

				}
				lineRenderer.SetPosition(i, lineArray[i]);
			}

			// Set the cachedPosition to the current position so we can start the conditional again
			cachedPosition = new Vector3d(
				positionProcessingScript.position.x,
				positionProcessingScript.position.y,
				positionProcessingScript.position.z);				// Get the real position from the planet's PositionProcessing script
		}
		/*
		float t = Time.time;
		int i = 0;
		while (i < lengthOfLineRenderer) {
			Vector3 pos = new Vector3(i * 0.5F, Mathf.Sin(i + t), 0);
			lineRenderer.SetPosition(i, pos);
			i++;
		}*/
	}
}
