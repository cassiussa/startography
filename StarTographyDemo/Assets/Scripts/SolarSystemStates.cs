using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;	// For iterating through all the transforms

public class SolarSystemStates : MonoBehaviour {
	public enum State { Initialize, SetupState, FadeIn, Visible, FadeOut, Invisible, SolarLocal, SolarFar, SolarGone }
	
	public State state = State.Initialize;
	State _prevState;
	State _cacheState;
	
	#region Basic Getters/Setters
	public State CurrentState {
		get { return state; }
	}
	
	public State PrevState {
		get { return _prevState; }
	}

	#endregion

	public List<GameObject> gameObjects;
	public List<TrailRenderer> lineRenderers;
	public List<GUIText> guiTexts;
	public List<Renderer> spheres;
	public List<GameObject> sphereObjects;
	
	public float fadeTime = 3.0f;
	public bool fadeIn = false;
	private float alphaCounter = 0f;
	public Color colour;
	private Color colourInvisible;
	private Color colourVisible;
	private SphereCollider col;

	public AdjustDistance adjustDistance;
	
	void Awake() {
		// Do any general system initialization stuff here.
		SetState(State.SetupState);
		adjustDistance = GetComponent<AdjustDistance> ();	// Get the AdjustDistance script so that we can pass to the CalculateDirection() function
		col = GetComponent<SphereCollider> ();
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheState != state) {
			//Debug.Log("_prevState = "+_prevState+", state = "+state+", alphaCounter = "+alphaCounter);
			switch (state) {
				case State.Initialize:
					break;
				case State.SetupState:
					Debug.Log("SetupState");
					SetupState ();
					break;
				case State.FadeIn:
					//Debug.Log("FadeIn");
					FadeIn ();
					break;
				case State.Visible:
					Debug.Log("Visible");
					Visible ();
					break;
				case State.FadeOut:
					Debug.Log("FadeOut");
					FadeOut ();
					break;
				case State.Invisible:
					Debug.Log("Invisible");
					Invisible ();
					break;
				case State.SolarLocal:
					Debug.Log("SolarLocal");
					SolarLocal ();
					break;
				case State.SolarFar:
					SolarFar ();
					break;
				case State.SolarGone:
					SolarGone ();
					break;
				}
			}
			yield return null;
		}
	}
	
	void Update() {
		ChangeAlpha();
	}

	
	public void SetState(State newState) {
		_prevState = state;
		state = newState;
	}

	
	void SetupState() {
		// Get all the child gameObjects so that we can disable them when the camera is not within view of the solar system
		foreach (Transform child in transform) {
			if (child.gameObject.name != "Sphere") {
				gameObjects.Add (child.gameObject);
				child.gameObject.SetActive (false);
			}
		}

		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true)) {

			if(child.gameObject.GetComponent<TrailRenderer>() && child.gameObject.name != "Sphere") {
				lineRenderers.Add(child.gameObject.GetComponent<TrailRenderer>());
				/*if(child.gameObject.GetComponent<TrailRenderer>().material) {
					var newTrailRenderer = new Color(child.gameObject.GetComponent<TrailRenderer>().material.color.r,
				                       child.gameObject.GetComponent<TrailRenderer>().material.color.g,
				                       child.gameObject.GetComponent<TrailRenderer>().material.color.b,
				                       colour.a);
					child.gameObject.GetComponent<TrailRenderer>().material.color = newTrailRenderer;
				}*/
			}

			if(child.gameObject.GetComponent<GUIText>() && child.gameObject.name != "Sphere") {
				guiTexts.Add(child.gameObject.GetComponent<GUIText>());
				var newMaterial = new Color(child.gameObject.GetComponent<GUIText>().material.color.r,
				                            child.gameObject.GetComponent<GUIText>().material.color.g,
				                            child.gameObject.GetComponent<GUIText>().material.color.b,
				                            colour.a);
				child.gameObject.GetComponent<GUIText>().material.color = newMaterial;
			}

			if(child.gameObject.GetComponent<Renderer>() && child.gameObject.name == "Sphere") {
				spheres.Add(child.gameObject.GetComponent<Renderer>());
				sphereObjects.Add(child.gameObject);
				var newSphereColour = new Color(child.gameObject.GetComponent<Renderer>().material.color.r,
				                            child.gameObject.GetComponent<Renderer>().material.color.g,
				                            child.gameObject.GetComponent<Renderer>().material.color.b,
				                            1);
				child.gameObject.GetComponent<Renderer>().material.color = newSphereColour;
			}

			colourInvisible = new Color(colour.r, colour.g, colour.b, 0f);
			colourVisible = new Color(colour.r, colour.g, colour.b, 1f);

		}

		SetState(State.Invisible);
	}

	void FadeIn() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
		fadeIn = true;
		if(alphaCounter == 1) SetState(State.Visible);
	}

	void Visible() {
		_cacheState = state;
		SetState(State.SolarLocal);
	}

	void FadeOut() {
		fadeIn = false;
		if(alphaCounter == 0) SetState(State.Invisible);
	}

	void Invisible() {
		foreach (Transform child in transform) {
			if(child.gameObject.name != "Sphere")
				child.gameObject.SetActive(false);
		}
		_cacheState = state;
		SetState(State.SolarFar);
	}

	void SolarLocal() {
		float scaling = 1f;
		float newScale = scaling / transform.localScale.x;
		//adjustDistance.CalculateLocalDirection(newScale);
		//transform.localScale = new Vector3 (scaling, scaling, scaling);
		foreach (GameObject child in gameObjects) {
			child.SetActive (true);
		}
		_cacheState = state;
	}

	void SolarFar() {
		float scaling = 0.25f;
		//adjustDistance.CalculateFarDirection(1/scaling);
		//transform.localScale = new Vector3 (scaling, scaling, scaling);
		//col.radius = col.radius*(1/0.25f);
		_cacheState = state;
	}

	void SolarGone() {
		_cacheState = state;
	}


	void ChangeAlpha(){
		if (gameObject.activeSelf == false) return;

		if(fadeIn) alphaCounter += Time.deltaTime / fadeTime;
		else alphaCounter -= Time.deltaTime / fadeTime;
		if (alphaCounter != 0 && alphaCounter != 1) {
			alphaCounter = Mathf.Clamp01 (alphaCounter);
			colour = Color.Lerp (colourInvisible, colourVisible, alphaCounter);

			foreach (Renderer render in lineRenderers) {
				//var newTrailRenderer = new Color (render.material.color.r, render.material.color.g, render.material.color.b, colour.a);
				//render.material.color = newTrailRenderer;								// Assign a standardized material to the circles
			}

			foreach(GUIText guiText in guiTexts) {
				var newMaterial = new Color (guiText.material.color.r, guiText.material.color.g, guiText.material.color.b, colour.a);
				guiText.material.color = newMaterial;								// Assign a standardized material to the circles
			}
		}

		//labelMaterial.color = new Color (labelMaterial.color.r, labelMaterial.color.g, labelMaterial.color.b, colour.a);

	}
}