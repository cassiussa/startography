using UnityEngine;
using System.Collections;

public class ScaleLargeCollider : MonoBehaviour {

	public FadeMaterial fadeMaterial;
	bool started = false;

	//bool fadeIn = false;
	//private float alphaCounter = 0f;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (started == false) {
			started = true;
			GetComponent<Collider>().enabled = true;
		}
	}

	
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeMaterial.fadeIn = true;
			fadeMaterial.largeEntered = true;
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeMaterial.fadeIn = false;
			fadeMaterial.largeEntered = false;
		}
	}
}






