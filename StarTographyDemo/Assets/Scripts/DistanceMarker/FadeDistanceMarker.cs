using UnityEngine;
using System.Collections;

public class FadeDistanceMarker : MonoBehaviour {
	public Material mat;
	public GameObject largeCollider;
	public GameObject smallCollider;
	public GameObject label;
	Material labelMaterial;
	public Renderer[] childMaterials;
	
	public float fadeTime = 3.0f;
	public bool fadeIn = false;
	private float alphaCounter = 0f;
	public Color colour;
	private Color colourInvisible;
	private Color colourVisible;
	
	public bool smallEntered = false;
	public bool largeEntered = false;
	
	// Use this for initialization
	void Awake () {

		/* 
		 * First we need to determine how large the array of children
		 * with Renderers is going to be so that we can update the
		 * childMaterials arrays size
		 */
		int rendererCount = 0;
		foreach (Transform transformChild in transform) {
			if(transformChild.gameObject.GetComponent<Renderer>()) {
				rendererCount++;
			}
		}

		childMaterials = new Renderer[rendererCount];							// Set the new array size
		int rendererIterator = 0;												// Set up an iterator count so we can add renderers into array and set up material Colors
		foreach (Transform transformChild in transform) {						// Iterate through all the child transforms of this Distance Marker
			if(transformChild.gameObject.GetComponent<Renderer>()) {			// Check to see if this child has a renderer component
				Renderer thisRenderer = transformChild.gameObject.GetComponent<Renderer>();	// Cache this iteration's Renderer
				childMaterials[rendererIterator] = thisRenderer;				// Add it into the array
				Color col = thisRenderer.material.color;						// Cache the Color
				var newMat = new Color(col.r, col.g, col.b, colour.a);			// Create a new Color reference
				thisRenderer.material.color = newMat;							// Set the renderer material's colour to the newMat variable reference
				rendererIterator++;												// Increment the iteration counter
			}
		}
		
		labelMaterial = label.GetComponent<GUIText> ().material;
		colour = mat.color;
		colourInvisible = new Color(colour.r, colour.g, colour.b , 0f);
		colourVisible = new Color(colour.r, colour.g, colour.b , 1f);
	}
	
	// Update is called once per frame
	void Update () {
		ChangeAlpha();
	}
	
	void ChangeAlpha(){
		if (gameObject.activeSelf == false) return;
		// First check that we didn't entered both the ScaleSmallCollder and ScaleLargeCollider at the same time
		if (smallEntered == true && largeEntered == true) {
			largeEntered = false;
			fadeIn = false;
			return;
		}
		if(fadeIn) alphaCounter += Time.deltaTime / fadeTime;
		else alphaCounter -= Time.deltaTime / fadeTime;
		if (alphaCounter != 0 && alphaCounter != 1) {
			alphaCounter = Mathf.Clamp01 (alphaCounter);
			colour = Color.Lerp (colourInvisible, colourVisible, alphaCounter);
			
			foreach (Renderer childMaterial in childMaterials) {
				var newMat = new Color (childMaterial.material.color.r, childMaterial.material.color.g, childMaterial.material.color.b, colour.a);
				childMaterial.material.color = newMat;								// Assign a standardized material to the circles
			}
			
			labelMaterial.color = new Color (labelMaterial.color.r, labelMaterial.color.g, labelMaterial.color.b, colour.a);
		}
	}
	
}
