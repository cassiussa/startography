using UnityEngine;
using System.Collections;

public class DistanceMarkerSmallCollider : MonoBehaviour {
	
	public FadeDistanceMarker fadeDistanceMarkerScript;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;

	void Awake() {
		/*
		 * We need to assign the parent gameObject to this
		 * as that's where the FadeDistanceMarker script is
		 * located.  It is the parent Distance Marker for
		 * this object
		 */
		fadeDistanceMarkerScript = transform.parent.gameObject.GetComponent<FadeDistanceMarker> ();
	}

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

