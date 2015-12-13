using UnityEngine;
using System.Collections;

public class BodyTriggers : MonoBehaviour {
	public GUIText label;
	bool started = false;

	float fadeTime = 3.0f;
	bool fadeIn = false;
	private float alphaCounter = 0f;
	Color colour;
	private Color colourInvisible;
	private Color colourVisible;


	// Use this for initialization
	void Start () {
		GetComponent<Collider>().enabled = false;

		colour = label.material.color;
		colourInvisible = new Color(colour.r, colour.g, colour.b , 0f);
		colourVisible = new Color(colour.r, colour.g, colour.b , 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (started == false) {
			started = true;
			GetComponent<Collider>().enabled = true;
		}

		ChangeAlpha();

	}

	void ChangeAlpha(){
		if (gameObject.activeSelf == false) return;
		if(fadeIn) alphaCounter += Time.deltaTime / fadeTime;
		else alphaCounter -= Time.deltaTime / fadeTime;
		alphaCounter = Mathf.Clamp01(alphaCounter);
		label.material.color = Color.Lerp(colourInvisible, colourVisible, alphaCounter); 
		
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeIn = true;

		}
	}


	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			fadeIn = false;
		}
	}
}
