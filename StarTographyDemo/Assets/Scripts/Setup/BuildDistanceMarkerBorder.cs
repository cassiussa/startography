using UnityEngine;
using System.Collections;

public class BuildDistanceMarkerBorder : MonoBehaviour {

	LineRenderer lineRenderer;
	public Material distanceMarkerFlat;										// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs
	public double lineRendererWidth;										// The size of the Start Width and End Width of the lines
	double _cachedLineRendererWidth;										// To hold onto the 'real' value (without scaling) of the line renderer

	void Awake () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();


		//lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// These sizes depend the DistanceMarker scale
		distanceMarkerFlat = new Material(Resources.Load("Material/DistanceMarkerFlat") as Material);
		distanceMarkerFlat.name = "Distance Marker Line Renderer Material";
		lineRenderer.material = distanceMarkerFlat;							// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs

		// Now that we've set everything up, lets add the DistanceMarkerBorder script
		DistanceMarkerBorder distanceMarkerBorderScript = gameObject.AddComponent<DistanceMarkerBorder>();
		distanceMarkerBorderScript.lineRenderer = lineRenderer;

		/*
		 * TODO: Fix this, with TODO in DistanceMarkerBorder.cs, BuildDistanceMarkers.cs
		 * The 0.00002005xxx number represents a static amount to be multiplied by whatever
		 * line edge.
		 */
		_cachedLineRendererWidth = (gameObject.transform.parent.GetComponent<DistanceMarkerStates> ().distanceMarkerSize) * 0.0000000000200537613669d;
		lineRendererWidth = _cachedLineRendererWidth;								// Cache the initial size of the lineRenderer, which is unscaled
		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// Set the initial size of the lineRenderer
	}

	void StateUpdate(double scaleMultiplier) {
		lineRendererWidth = _cachedLineRendererWidth / scaleMultiplier;
		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// Set the initial size of the lineRenderer
		print ("scaleMultiplier = " + scaleMultiplier);
	}

}
