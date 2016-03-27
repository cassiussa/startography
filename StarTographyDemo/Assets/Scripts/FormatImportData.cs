using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using JSON;

public class FormatImportData : MonoBehaviour
{
	[SerializeField] private Star star = null;
	private void Start () {
		string json = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts/config.json"));
		this.star = (Star)JSONSerialize.Deserialize(typeof(Star), json);
		//Debug.Log (star);
	}
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			JSONSerialize.Serialize(Path.Combine(Application.dataPath, "configNew.json"), this.star);
		}
	}
}

[System.Serializable]
public class Star {
	[JSONItem("id",typeof(int))]
	public int id = 0;

	[JSONItem("hostStarName", typeof(string))]
	public string hostStarName;

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


	[JSONArray("planet", typeof(Planet))]
	public Planet[] planet;
}

[System.Serializable]
public class Planet {
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