using UnityEngine;
using System.Collections;
using Functions;

public class BodyDistanceToCam : MonoBehaviour {

	public GameObject activeStar;						// This is the currently-active star.  Will end up being set by whatever is governing the Star States
	public Vector3d[] relativePositions;				// The relative position to the camera.  relativePosition is the difference between realPosition and the camera's realPosition
	public double[] distances;							// An array to hold the distances between each star system body and the camera
	public double lowest = 10e200;						// start with a crazy large number so we can eventually attain the smallest distance.
	int count = 0;										// Iterator count.  Always less than the number of items in the relativePositions and distances arrays

	void Awake() {
		activeStar = GameObject.Find ("[STAR] Sun");
		relativePositions = activeStar.GetComponent<DistanceArrays> ().relativePositions;
		// How many distances do we have to iterate through?
		distances = new double[relativePositions.Length];

	}
	
	void Update () {
		if (count == relativePositions.Length)			// Set up an iterator for the array so that we can perform one iteration per Update()
			count = 0;

		// Calculate the distance of this array item to the camera
		distances[count] = Vector3d.Distance(new Vector3d(0,0,0), relativePositions[count]);

		// Check to see if it's a smaller distance than all the previous distances
		if(distances[count] < lowest)
			lowest = distances[count];

		count++;										// Iterate to the next item on the array if there are any.  Otherwise, it'll start from the beginning again
	}
}
