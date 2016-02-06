using UnityEngine;
using System.Collections;

public class StarColour : MonoBehaviour {

	public StarTemperatureColour starTemperatureColourScript;
	public float[,] temperatureColours;
	public SgtProminence sgtProminenceScript;
	public Material starGlowPlane;
	public SgtCorona sgtCoronaScript;
	public GameObject meshes;
	public Color starColour;
	public Transform starGlow;
	public Material[] starRenderer;
	public ObjectData objectDataScript;
	
	void Start () {
		objectDataScript = GetComponent<ObjectData> ();
		starTemperatureColourScript = GameObject.Find ("Globals").GetComponent<StarTemperatureColour> ();
		sgtProminenceScript = meshes.GetComponent<SgtProminence> ();
		starGlowPlane = GameObject.Find ("StarMainGlow").GetComponent<Renderer>().material;

		temperatureColours = starTemperatureColourScript.temperatureColours;
		for (int i=0; i<temperatureColours.GetLength(0); i++) {
			if (objectDataScript.temperature >= temperatureColours [i, 0] && temperatureColours [i, 0] > 0) {
				// Now assign the colour that the star would be, based on what we've looked up in the pre-canned text file
				starColour = new Color (temperatureColours [i, 1] / 255,
			                        temperatureColours [i, 2] / 255,
			                        temperatureColours [i, 3] / 255);

			}
		}
		starRenderer[0].color = starColour;
		sgtProminenceScript.Color = starColour;
		float starGlowPlaneA = starGlowPlane.color.a;	// Cache the pre-defined alpha value of the material
		starGlowPlane.color = new Color(starColour.r, starColour.g, starColour.b, starGlowPlaneA); // Revert to the pre-defined alpha value of the material

		// Here we're creating a gradient of the colour, so that the edge of the stars look a little bit better
		// Gradients use an array, with the last float being the "time" between 0.0 and 1.0 (and anywhere in between)
		GradientColorKey[] defaultAtmosphereColor = new GradientColorKey[2];
		defaultAtmosphereColor[0] = new GradientColorKey(new Color(starColour.r,starColour.g,starColour.b), 0.0f);
		defaultAtmosphereColor[1] = new GradientColorKey(new Color(starColour.r-0.3f, starColour.g-0.3f, starColour.b-0.3f), 1.0f);
		sgtCoronaScript.DensityColor.colorKeys = defaultAtmosphereColor;

		//Debug.Log ("starColour = " + starColour);
	}

}
