using UnityEngine;
using System.Collections;

public class DistanceMarkerLargeCollider : MonoBehaviour {
	
	public FadeDistanceMarker fadeDistanceMarkerScript;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeDistanceMarkerScript.fadeIn = false;
			fadeDistanceMarkerScript.smallEntered = true;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeDistanceMarkerScript.fadeIn = true;
			fadeDistanceMarkerScript.smallEntered = false;
		}
	}
}

