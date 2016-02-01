using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.IO;  

public class StarTemperatureColour : Functions {

	string file = "Assets/TextFiles/TemperatureToRGB.txt";
	StreamReader theReader;
	public float[,] temperatureColours;
	public GameObject meshes;
	public Color starColour;
	public Transform starGlow;
	//public float temperature;
	public ObjectData objectDataScript;

	void Awake() {
		objectDataScript = GetComponent<ObjectData> ();
		theReader = new StreamReader(file, Encoding.Default);
		//Load ();

		/*for (int i=0; i<temperatureColours.Length; i++) {

		}*/

	}

	public bool Load() {
		// Handle any problems that might arise when reading the text
		try {
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(file, Encoding.Default);

			string _file = new StreamReader(file).ReadToEnd();					// Get the text and count read to the end of the file
			string[] _lines = _file.Split(new char[] {'\n'});						// Split each line using '\n' and put them into the _lines array
			int count = _lines.Length;												// Get the length of the array
			temperatureColours = new float[count,4];								// Create a 2D array

			int _i = 0;
			for(int i=0;i<count;i++) {												// Iterate through the length of the file in line numbers
				string[] _thisLine = _lines[i].Split(new char[] {','});				// Create an array of this line using the comma as separator
				for(int j=0;j<4;j++) {												// Iterate through each array item on this line
					temperatureColours[i,j] = float.Parse(_thisLine[j]);			// Assign the value into this array index
				}

				if(objectDataScript.temperature >= temperatureColours[i,0]) {
					_i = i;
					// Now assign the colour that the star would be, based on what we've looked up in the pre-canned text file
					starColour = new Color (temperatureColours[_i,1]/255,
					                        temperatureColours[_i,2]/255,
					                        temperatureColours[_i,3]/255);
				}
			}
			
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader) {

				// While there's lines left in the text file, do this:
				do {
					line = theReader.ReadLine();
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e) {
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}
}
