using UnityEngine;
using System.Collections;

public class GenerateDistanceVisuals : Functions {
	public GameObject distanceMarker;
	public GameObject lightYear;

	string[] inputs;		// Array of strings of distance types
	double[] measurements;	// Array of the measurements of the distance types

	string[] distanceMarkersName;
	int[] distanceMarkersLayer;
	double[] distanceMarkersRadius;

	PositionProcessing positionProcessingScript;

	void Start () {
		inputs = new string[] { "SM", "MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		measurements = new double[] { SM, MK, AU, LH, Ld, LY, PA, LD, LC, LM };

		// Assign the values to the array variables so that we can iterate through them and generate the visuals
		distanceMarkersName = new string[] { "1AU Distance Marker", "1LH Distance Marker" };					// A list of names of all the Distance Markers
		distanceMarkersLayer = new int[] { 10, 11 };															// A list of the layers that the Distance Markers will exist on
		distanceMarkersRadius = new double[] { AU, LH };														// A list of the sizes of that the Distance Markers will be - usually same as sizes found in Constants

		for (int i=0; i<distanceMarkersName.Length; i++) {														// Iterate over each scale size that we'll have a Distance Marker for

			// Generate the copies of the Distance Markers.  1 scale smaller, actual scale, and 1 scale larger
			for(int a=-1;a<=1;a++) {																			// So we can get a scale smaller, current, and larger
				int thisLayer = distanceMarkersLayer[i]+a;
				if(thisLayer >= 8 && thisLayer <= 17) {															// Ensure that we're not going below the smallest layer or above the highest layer


					/*
					* Somehow here I need to get the value just smaller than this layer
					* and just larger than this layer and put the gameObjects into the
					* appropriate parent and also assign the appropriate positions.
					*/

					int b = 0;	// Make the variable global within the iteration
					for(b=0;b<measurements.Length;b++) {																// Iterate through each scale value in the 'measurements' variable
						if(measurements[b] == distanceMarkersRadius[i]) {												// Check to see if we've found the match
							int thisScaleSize = i+a;																	// Do one above and one below the matched item
							if(thisScaleSize>=0 && thisScaleSize<measurements.Length) {									// Make sure it doesn't fall outside of the Index
								distanceMarker = Instantiate (Resources.Load ("Prefabs/DistanceMarker")) as GameObject;	// Locate the prefab that we'll be using for the Distance Marker generations
								distanceMarker.name = distanceMarkersName[i];											// Assign the gameObject's name based on the string values in distanceMarkersName
								foreach(Transform trans in distanceMarker.GetComponentsInChildren<Transform>(true)) {	// Iterate through this gameObject and it's children and...
									trans.gameObject.layer = thisLayer;													// ...assign the layer value based on the calculated layer
								}
								Debug.LogError ("bah "+measurements[thisScaleSize],distanceMarker);
								distanceMarker.GetComponent<DistanceMarkerData>().radius = V3dToS3d(new Vector3d(		// Assign the radius variable values based on the double value from the Constants script
                                                 distanceMarkersRadius[i],
                                                 distanceMarkersRadius[i],
                                                 distanceMarkersRadius[i]));
								//distanceMarker.GetComponent<DistanceMarkerData>().celestialBodyType = CelestialBodyType.UserInterface;
								distanceMarker.GetComponent<DistanceMarkerData>().coordinates = GetComponent<ObjectData>().coordinates;	// Assign the coordinates from the origin gameObject (star)
								distanceMarker.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);				// Reset the rotation of the newly-created gameObject
								distanceMarker.transform.localScale = new Vector3 (1f, 1f, 1f);							// Reset the scale of the newly-created gameObject
								positionProcessingScript = distanceMarker.GetComponent<PositionProcessing>();
								
								double thisMeasurement = distanceMarkersRadius [i];										// Cache the measurement for this iteration to save processing
								Vector3d thisPosition = new Vector3d (													// Set the initial position of the new light gameObjects
			                                      ((System.Math.Abs (positionProcessingScript.position.x) / thisMeasurement) * maxUnits),
			                                      ((System.Math.Abs (positionProcessingScript.position.y) / thisMeasurement) * maxUnits),
			                                      ((System.Math.Abs (positionProcessingScript.position.z) / thisMeasurement) * maxUnits));
								distanceMarker.transform.position = gameObject.transform.position;
							}
						}
					}
												// Set the position of the newly-created gameObject to same location as that of the gameObject (star) that generated it
				}
			}

		}
	}
/*
	lightGameObjects.Add (inputs [i], new GameObject ("Light - " + gameObject.name));	// Create empty gameObjects on the fly and reference them in the Dictionary
	GameObject lightGameObject = lightGameObjects [inputs [i]];						// Create a variable for this GameObject for faster processing
	lightGameObject.transform.parent = scaleStateParent [inputs [i]];					// Set this gameObject's parent to the appropriate scale's gameObject container
	double thisMeasurement = measurements [i];										// Cache the measurement for this iteration to save processing

	lightGameObject.transform.position = V3dToV3 (thisPosition);
	lightGameObject.layer = i + layerMask;											// Set the layer.  Note that 8 is the lowest layer we've made
	float calculatedRange = (float)((light.range / measurements [i]) * maxUnits);		// Range of the light depending on State
*/

}
