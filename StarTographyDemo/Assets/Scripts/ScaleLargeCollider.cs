using UnityEngine;
using System.Collections;

public class ScaleLargeCollider : MonoBehaviour {

	public FadeMaterial fadeMaterial;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
		//	Debug.LogError ("camera is inside");
			fadeMaterial.fadeIn = true;
			fadeMaterial.largeEntered = true;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			Debug.LogError ("large exited");
			fadeMaterial.fadeIn = false;
			fadeMaterial.largeEntered = false;
		}
	}
}

