using UnityEngine;
using System.Collections;

public class FadeDistanceMarker : MonoBehaviour {
	//public Material mat;
	//public GameObject largeCollider;
	//public GameObject smallCollider;
	//public GameObject label;
	public Material labelMaterial;
	public Renderer[] childRenderers;
	
	public float fadeTime = 3.0f;
	public bool fadeIn = false;
	private float alphaCounter = 0f;
	public Color colour;
	public Color guiColour;
	public Color colourInvisible;
	public Color colourVisible;

	public GUIText distanceLabel;
	
	public bool smallEntered = false;
	public bool largeEntered = false;
	
	// Use this for initialization
	void Awake () {

		/* 
		 * First we need to determine how large the array of children
		 * with Renderers is going to be so that we can update the
		 * childRenderers arrays size
		 */
		int rendererCount = 0;
		foreach (Transform transformChild in transform) {
			if(transformChild.gameObject.GetComponent<Renderer>()) {
				rendererCount++;
			}
		}

		childRenderers = new Renderer[rendererCount];							// Set the new array size
		int rendererIterator = 0;												// Set up an iterator count so we can add renderers into array and set up material Colors
		foreach (Transform transformChild in transform) {						// Iterate through all the child transforms of this Distance Marker
			if(transformChild.gameObject.GetComponent<Renderer>()) {			// Check to see if this child has a renderer component
				Renderer thisRenderer = transformChild.gameObject.GetComponent<Renderer>();	// Cache this iteration's Renderer
				childRenderers[rendererIterator] = thisRenderer;				// Add it into the array
				Color col = thisRenderer.material.color;						// Cache the Color
				var newMat = new Color(col.r, col.g, col.b, colour.a);			// Create a new Color reference
				thisRenderer.material.color = newMat;							// Set the renderer material's colour to the newMat variable reference
				rendererIterator++;												// Increment the iteration counter
			}
		}
	}

	void Update () {
		ChangeAlpha();
	}
	
	void ChangeAlpha(){
		if (gameObject.activeSelf == false) return;

		/*
		 * First check that we didn't entered both the 
		 * ScaleSmallCollder and ScaleLargeCollider at the same time.
		 * This can happen when we instantiate objects at the
		 * beginning and the colliders are within each other.
		 */
		if (smallEntered == true && largeEntered == true) {
			largeEntered = false;
			fadeIn = false;
			return;
		}

		/*
		 * Because we're always either adding to the alphaCounter during and after
		 * fading in, or subtracting from alphaCounter during or after fading out,
		 * this means it's always processing when active.  Therefore we should
		 * probably deactivate this script when certain conditions are met.
		 */
		if(fadeIn) alphaCounter += Time.deltaTime / fadeTime;						// Either we're adding time to the variable
		else alphaCounter -= Time.deltaTime / fadeTime;								// or we're subtracting time


		if (alphaCounter < 0 || alphaCounter > 1) {
			alphaCounter = Mathf.Clamp01 (alphaCounter);
		} else if (alphaCounter != 0 && alphaCounter != 1) {
			alphaCounter = Mathf.Clamp01 (alphaCounter);							// Clamp the range
			colour = Color.Lerp (colourInvisible, colourVisible, alphaCounter);		// Lerp between the invisible colour and the fully opaque colour
			
			foreach (Renderer childMaterial in childRenderers) {
				Color newMat = new Color (childMaterial.material.color.r, childMaterial.material.color.g, childMaterial.material.color.b, colour.a);
				childMaterial.material.color = newMat;								// Assign a standardized material to the circles
			}
			guiColour = new Color(labelMaterial.color.r, labelMaterial.color.g, labelMaterial.color.b, colour.a);
			labelMaterial.color = guiColour;
			distanceLabel.color = guiColour;										// This is needed to take care of fading the GUIText on the label

		}
	}
	
}
