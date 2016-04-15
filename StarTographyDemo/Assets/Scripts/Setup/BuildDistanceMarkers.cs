using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Functions;

public class BuildDistanceMarkers : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		/*
		 * Distance markers are to identify arbitrary, but known sizes so that
		 * the user can get some kind of idea of scale.  For example, we can 
		 * make it known when we've reached an AU or a Light Year in distance
		 * from a given star
		 */
		// Create a Dictionary of the different Distance Markers and their distances
		Dictionary<string, double> markerDistance = new Dictionary<string, double>();
		markerDistance.Add (gameObject.name + " [MARKER] AU", 149597870.7d);
		markerDistance.Add (gameObject.name + " [MARKER] Light Hour", 1079252848.8d);
		markerDistance.Add (gameObject.name + " [MARKER] Light Day", 25902068371.2d);
		markerDistance.Add (gameObject.name + " [MARKER] Light Year", 9460730472600d);

		foreach (KeyValuePair<string, double> marker in markerDistance) {
			GameObject mark = GameObject.CreatePrimitive(PrimitiveType.Plane);				// Create a plane primitive
			mark.name = marker.Key;															// Create the name for the Distance Marker's gameObject
			mark.transform.parent = transform;												// Assign the parent transform
			Destroy (mark.collider);														// Remove the collider that is automatically added when we create the primitive
			Quaternion newRot = new Quaternion();											// Set up a temporary Quaternion to build the new rotation
			newRot.eulerAngles = new Vector3(0,0,0);										// Reset the rotation as this was from Blender
			mark.transform.localRotation = newRot;											// Set the rotation of the star
			Vector3d scale = new Vector3d(marker.Value, 1, marker.Value);					// Scale the Distance Marker based on its expected scale size

			mark.transform.localScale = new Vector3(2,1,2);									// This is temporary assignment of scale
		}

		Destroy (this);
	}

}
