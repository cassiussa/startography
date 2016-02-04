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
		GradientColorKey[] defaultAtmosphereColor = new GradientColorKey[2];
		defaultAtmosphereColor[0] = new GradientColorKey(new Color(starColour.r,starColour.g,starColour.b), 0.0f);
		defaultAtmosphereColor[1] = new GradientColorKey(new Color(starColour.r-0.3f, starColour.g-0.3f, starColour.b-0.3f), 1.0f);
		sgtCoronaScript.DensityColor.colorKeys = defaultAtmosphereColor;

		/*sgtCoronaScript.DensityColor.colorKeys[0].color = starColour;
		sgtCoronaScript.DensityColor.colorKeys[0].time = 0.0f;
		sgtCoronaScript.DensityColor.colorKeys[1].color = new Color(starColour.r-20f,starColour.g-20f,starColour.b-20f);
		sgtCoronaScript.DensityColor.colorKeys[1].time = 1.0f;*/

		Debug.Log ("starColour = " + starColour);
	}

}
