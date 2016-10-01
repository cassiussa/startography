using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class ImportData : MonoBehaviour {

	string fileName = "Assets/Scripts/data.json";
	StreamReader reader;
	public string JSONData = "";
	public int numberOfStars;


	// Use this for initialization
	void Start () {
		StreamReader reader = File.OpenText(fileName);
		JSONData = reader.ReadToEnd ();

		var importedData = JSON.Parse(JSONData);

		// Sample of iterating over the Star array of stars
		for (int i=0; i<importedData["star"].Count; i++) {
			string starName = importedData["star"][i]["name"] as string;
			int starNumOfPlanets = importedData["star"][i]["planets"].Count;
			Debug.LogError (starName);
			Debug.LogError (starNumOfPlanets);
		}
		numberOfStars = importedData["star"].Count;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
