using UnityEngine;
using System.Collections;

public class PlanetOrbitPathTrail : Functions {
	public LineRenderer lineRenderer;
	public int lengthOfLineRenderer = 200;
	public PositionProcessing positionProcessingScript;
	Vector3d curPosition;
	Vector3d cachedPosition;
	int i = 0;
	// Use this for initialization
	void Start () {
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		curPosition = positionProcessingScript.position;
		cachedPosition = new Vector3d(
			positionProcessingScript.position.x,
			positionProcessingScript.position.y,
			positionProcessingScript.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		positionProcessingScript.position.x += 100;					// **** FOR TESTING ONLY *** //
		curPosition = positionProcessingScript.position;			// This is a literal assignment.  Both are now the same item, not just equal to the same values
		// Check to see if the object has gone farther than 20000k.  If so, update the LineRenderer
		if (System.Math.Abs (v3dDistance(curPosition, cachedPosition)) >= 20000d) {
			//Debug.LogError ("farther than 20k");

			// We have the real position in space so lets get the localized Vector3 of where it should be
			// CalculatePosition(double, Vector3d, Vector3d (camera));


			while (i < lengthOfLineRenderer) {
				lineRenderer.SetPosition(i, V3dToV3(curPosition));
				Debug.Log ("Set a position at "+V3dToV3(curPosition));
				i++;
			}
			i=0;

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
