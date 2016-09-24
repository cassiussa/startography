using UnityEngine;
using System.Collections;

public class DistanceMarkerBorder : MonoBehaviour {
	//public Transform parentForScale;
	public int horizCirclePoints = 201;									// This should be static, regardless of the distance marker scale
	public double xradius;												// Calculated by using the distance marker's scale
	public double zradius;												// Calculated by using the distance marker's scale
	public LineRenderer lineRenderer;									// Get the LineRenderer component so we can apply the line positions
	public double lineRendererWidth;										// The size of the Start Width and End Width of the lines
	double _cachedLineRendererWidth;										// To hold onto the 'real' value (without scaling) of the line renderer
	
	void Awake () {
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.receiveShadows = false;							// Not a celestial body so receive any shadows
		lineRenderer.castShadows = false;								// Not a celestial body so no shadow casting
		lineRenderer.SetVertexCount (horizCirclePoints);				// Predefine the size of the array of positions
		lineRenderer.useWorldSpace = false;								// The Line Along Edge will need to move with the DistanceMarker

		double distanceMarkerSize = transform.parent.gameObject.GetComponent<DistanceMarkerStates> ().distanceMarkerSize;
		distanceMarkerSize *= 0.00001; // TODO: Temporary - until scales are functional.  See TODO in BuildDistanceMarkers.cs, BuildDistanceMarkerBorder.cs
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

		/*
		 * TODO: Fix this, with TODO in DistanceMarkerBorder.cs, BuildDistanceMarkers.cs
		 * The 0.00002005xxx number represents a static amount to be multiplied by whatever
		 * line edge.
		 */
		_cachedLineRendererWidth = (gameObject.transform.parent.GetComponent<DistanceMarkerStates> ().distanceMarkerSize) * 0.000000200537613669d;
		lineRendererWidth = _cachedLineRendererWidth;								// Cache the initial size of the lineRenderer, which is unscaled
		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// Set the initial size of the lineRenderer

	}

	void StateUpdate(double scaleMultiplier) {
		lineRendererWidth = scaleMultiplier * _cachedLineRendererWidth;
		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// Set the initial size of the lineRenderer
		if (transform.parent.name == "[STAR] Sun [MARKER] 1 AU") {
			print ("lineRendererWidth = "+lineRendererWidth);
		}
	}

}
