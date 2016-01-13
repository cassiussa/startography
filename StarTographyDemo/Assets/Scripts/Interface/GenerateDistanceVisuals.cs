using UnityEngine;
using System.Collections;

public class GenerateDistanceVisuals : Functions {
	public GameObject astronomicalUnit;
	public GameObject lightYear;

	string[] distanceMarkersName;
	int[] distanceMarkersLayer;
	double[] distanceMarkersRadius;

	void Start () {
		GameObject interfaceObject = new GameObject();
		interfaceObject.name = "Interface Objects";
		interfaceObject.transform.parent = transform;
		interfaceObject.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);
		interfaceObject.transform.localScale = new Vector3 (1f, 1f, 1f);

		// Assign the values to the array variables so that we can iterate through them and generate the visuals
		distanceMarkersName = new string[] { "1AU Distance Marker", "1LH Distance Marker" };
		distanceMarkersLayer = new int[] { 10, 11 };
		distanceMarkersRadius = new double[] { AU, LH };

		for (int i=0; i<distanceMarkersName.Length; i++) {
			astronomicalUnit = Instantiate (Resources.Load ("Prefabs/DistanceMarker")) as GameObject;
			astronomicalUnit.name = distanceMarkersName[i];
			astronomicalUnit.layer = distanceMarkersLayer[i];
			astronomicalUnit.GetComponent<DistanceMarkerData>().radius = V3dToS3d(new Vector3d(distanceMarkersRadius[i],distanceMarkersRadius[i],distanceMarkersRadius[i]));
			astronomicalUnit.transform.parent = interfaceObject.transform;
			astronomicalUnit.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);
			astronomicalUnit.transform.localScale = new Vector3 (1f, 1f, 1f);
		}
	}
}
