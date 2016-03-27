using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using JSON;

public class FormatImportData : MonoBehaviour
{
	[SerializeField] private Stars stars = null;
	private void Start () {
		string json = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts/config.json"));
		this.stars = (Stars)JSONSerialize.Deserialize(typeof(Stars), json);
		//Debug.Log (star);

	}
}

[System.Serializable]
public class Stars {
	
	[JSONArray("star", typeof(Star))]
	public Star[] star;
}

[System.Serializable]
public class Star {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name;

	[JSONItem("id",typeof(int))]
	public int id = 0;
	
	[JSONItem("rightAscension", typeof(string))]
	public string rightAscension;
	
	[JSONItem("declination", typeof(string))]
	public string declination;
	
	[JSONItem("distance", typeof(float))]
	public float distance;
	
	[JSONItem("opticalMagnitude", typeof(float))]
	public float opticalMagnitude;
	
	[JSONItem("temperature", typeof(float))]
	public float temperature;
	
	[JSONItem("stellarMass", typeof(float))]
	public float stellarMass;
	
	[JSONItem("stellarRadius", typeof(float))]
	public float stellarRadius;
	
	[JSONItem("dateLastUpdate", typeof(string))]
	public string dateLastUpdate;
	
	
	[JSONArray("planets", typeof(Planet))]
	public Planet[] planets;
}

[System.Serializable]
public class Planet {
	[HideInInspector]
	[JSONItem("name", typeof(string))]
	public string name = null;

	[JSONItem("status", typeof(bool))]
	public bool status = true;

	[JSONItem("numPlanetsInSystem", typeof(int))]
	public int numPlanetsInSystem;

	[JSONItem("orbitalPeriod", typeof(float))]
	public float orbitalPeriod;

	[JSONItem("semiMajorAxis", typeof(float))]
	public float semiMajorAxis;

	[JSONItem("eccentricity", typeof(float))]
	public float eccentricity;

	[JSONItem("inclination", typeof(float))]
	public float inclination;

	[JSONItem("planetMass", typeof(float))]
	public float planetMass;

	[JSONItem("planetRadius", typeof(float))]
	public float planetRadius;

}