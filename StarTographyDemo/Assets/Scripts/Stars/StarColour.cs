using UnityEngine;
using System.Collections;

public class StarColour : MonoBehaviour {

	public StarTemperatureColour starTemperatureColourScript;
	public float[,] temperatureColours;
	public SgtProminence sgtProminenceScript;
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

		temperatureColours = starTemperatureColourScript.temperatureColours;
		Debug.Log ("StarColour.cs Start()"+temperatureColours[0,0]);
		//Debug.LogError ("temperatureColours = " + temperatureColours [0, 0]);
		//sgtProminenceScript.Color = starColour;

		//int _i = 1;
		for (int i=0; i<temperatureColours.GetLength(0); i++) {
			if (objectDataScript.temperature >= temperatureColours [i, 0] && temperatureColours [i, 0] > 0) {
				Debug.Log ("temperatureColours [i, 0] = "+temperatureColours [i, 0]);
				// Now assign the colour that the star would be, based on what we've looked up in the pre-canned text file
				starColour = new Color (temperatureColours [i, 1] / 255,
			                        temperatureColours [i, 2] / 255,
			                        temperatureColours [i, 3] / 255);

			}
		}
		starRenderer[0].color = starColour;
		sgtProminenceScript.Color = starColour;
		GradientColorKey[] defaultAtmosphereColor = new GradientColorKey[] { new GradientColorKey (starColour, 0.5f) };
		sgtCoronaScript.DensityColor.colorKeys = defaultAtmosphereColor;

		Debug.Log ("starColour = " + starColour);
	}

}
