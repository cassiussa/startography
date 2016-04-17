using UnityEngine;
using System.Collections;

public class DistanceMarkerBorder : MonoBehaviour {
	//public Transform parentForScale;
	public int horizCirclePoints = 201;									// This should be static, regardless of the distance marker scale
	public double xradius;												// Calculated by using the distance marker's scale
	public double zradius;												// Calculated by using the distance marker's scale
	public LineRenderer lineRenderer;									// Get the LineRenderer component so we can apply the line positions
	
	void Awake () {
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.receiveShadows = false;							// Not a celestial body so receive any shadows
		lineRenderer.castShadows = false;								// Not a celestial body so no shadow casting
		lineRenderer.SetVertexCount (horizCirclePoints);				// Predefine the size of the array of positions
		lineRenderer.useWorldSpace = false;								// The Line Along Edge will need to move with the DistanceMarker

		double distanceMarkerSize = transform.parent.gameObject.GetComponent<DistanceMarkerStates> ().distanceMarkerSize;
		distanceMarkerSize *= 0.00001; // Temporary - until scales are functional
		xradius = distanceMarkerSize;
		zradius = distanceMarkerSize;


		/*
		 * Create the circle to form the LineRenderer edge
		 * of the Distance Marker.
		 */
		if (gameObject.activeSelf == false) return;
		float x;
		float y = 0f;
		float z;
		float angle = 20f;
		for (int i=0; i<horizCirclePoints; i++) {
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * (float)xradius;
			z = Mathf.Cos(Mathf.Deg2Rad * angle) * (float)zradius;
			lineRenderer.SetPosition(i,new Vector3(x,y,z) );
			angle += (360f / (horizCirclePoints-1));				// Because we have 1 less segment than joints, subtract 1
		}
	}

}
