using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;


namespace ImportData
{

	public class Data {

		static string fileName = "Assets/Scripts/data.json";
		public static StreamReader data = File.OpenText(fileName);
		public static JSONNode importedData;

		[HideInInspector]
		public string JSONData;    // Holds the data.json file data
		public int numberOfStars;


		/*void Awake () {
			StreamReader data = File.OpenText(fileName);
			JSONData = data.ReadToEnd ();
			data.Close ();

			importedData = JSON.Parse(JSONData);

			// Sample of iterating over the Star array of stars
			for (int i=0; i<importedData["star"].Count; i++) {
				string starName = importedData["star"][i]["name"] as string;
				int starNumOfPlanets = importedData["star"][i]["planets"].Count;
				Debug.Log (starName);
				Debug.Log (starNumOfPlanets);
			}
			numberOfStars = importedData ["star"].Count;

		}*/

	}
}