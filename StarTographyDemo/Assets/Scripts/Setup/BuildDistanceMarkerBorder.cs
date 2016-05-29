using UnityEngine;
using System.Collections;

public class BuildDistanceMarkerBorder : MonoBehaviour {

	LineRenderer lineRenderer;
	public Material distanceMarkerFlat;										// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs

	void Awake () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		distanceMarkerFlat = new Material(Resources.Load("Material/DistanceMarkerFlat") as Material);
		distanceMarkerFlat.name = "Distance Marker Line Renderer Material";
		lineRenderer.material = distanceMarkerFlat;							// This is used by the fadeDistanceMarkerScript.colour variable in BuildDistanceMarkers.cs

		// Now that we've set everything up, lets add the DistanceMarkerBorder script
		DistanceMarkerBorder distanceMarkerBorderScript = gameObject.AddComponent<DistanceMarkerBorder>();
		distanceMarkerBorderScript.lineRenderer = lineRenderer;

	}



}
