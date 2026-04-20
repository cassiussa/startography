using System.IO;
using SimpleJSON;
using UnityEngine;


namespace ImportData
{

	public class Data {

		private static readonly string FileName = Path.Combine(Application.dataPath, "Scripts/data.json");
		public static JSONNode importedData;

		[HideInInspector]
		public string JSONData;    // Holds the data.json file data
		public int numberOfStars;

		public static string LoadJson() {
			return File.ReadAllText(FileName);
		}


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
