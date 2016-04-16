using UnityEngine;
using System.Collections;

public class FadeDistanceMarker : MonoBehaviour {
	public Material mat;
	public GameObject largeCollider;
	public GameObject smallCollider;
	public GameObject labelSize;
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
		foreach (Renderer childMaterial in childMaterials) {
			var newMat = new Color(childMaterial.material.color.r, childMaterial.material.color.g,childMaterial.material.color.b, colour.a);
			childMaterial.material.color = newMat;
		}
		
		labelMaterial = labelSize.GetComponent<GUIText> ().material;
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
