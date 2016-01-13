using UnityEngine;
using System.Collections;

public class ScaleSmallCollider : MonoBehaviour {

	public FadeMaterial fadeMaterial;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeMaterial.fadeIn = false;
			fadeMaterial.smallEntered = true;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeMaterial.fadeIn = true;
			fadeMaterial.smallEntered = false;
		}
	}
}

