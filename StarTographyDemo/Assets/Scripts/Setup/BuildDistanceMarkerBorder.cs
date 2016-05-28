using UnityEngine;
using System.Collections;

public class BuildDistanceMarkerBorder : MonoBehaviour {

	LineRenderer lineRenderer;
	public Material distanceMarkerFlat;										// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs
	public double lineRendererWidth;										// The size of the Start Width and End Width of the lines

	void Awake () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();


		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// These sizes depend the DistanceMarker scale
		distanceMarkerFlat = new Material(Resources.Load("Material/DistanceMarkerFlat") as Material);
		distanceMarkerFlat.name = "Distance Marker Line Renderer Material";
		lineRenderer.material = distanceMarkerFlat;							// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs

		// Now that we've set everything up, lets add the DistanceMarkerBorder script
		DistanceMarkerBorder distanceMarkerBorderScript = gameObject.AddComponent<DistanceMarkerBorder>();
		distanceMarkerBorderScript.lineRenderer = lineRenderer;

		/*
		 * TODO: Fix this, with TODO in DistanceMarkerBorder.cs, BuildDistanceMarkers.cs
		 * The 0.002005xxx number represents a static amount to be multiplied by whatever
		 * line edge.  The 0.00001 is temporary until I have the scale states functioning,
		 * at which time a value will be placed in there which will depend upon the scale
		 * that this object is currently found in.
		 */
		lineRendererWidth = (gameObject.transform.parent.GetComponent<DistanceMarkerStates> ().distanceMarkerSize) * 0.00001 * 0.00200537613669;
		lineRenderer.SetWidth((float)lineRendererWidth, (float)lineRendererWidth);	// Set the initial size of the lineRenderer
	}

}
