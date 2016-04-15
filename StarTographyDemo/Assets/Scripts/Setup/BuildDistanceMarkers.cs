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
			GameObject markerParent = new GameObject(marker.Key);							// Create the name for the Distance Marker's gameObject
			markerParent.transform.parent = transform;
			double markerValue = marker.Value;												// Cache the size of the distance marker
			Vector3d scale = new Vector3d(markerValue, 1, markerValue);						// Scale the Distance Marker based on its expected scale size
			DistanceMarkerStates distanceMarkerStatesScript = markerParent.AddComponent<DistanceMarkerStates>();
			distanceMarkerStatesScript.distanceMarkerSize = markerValue;						// Assign the value of the distance marker into a variable in the relevant States script



			GameObject mark = GameObject.CreatePrimitive(PrimitiveType.Cube);				// Create a plane primitive
			mark.name = "Mesh";																// Create the name for the Distance Marker's gameObject
			mark.transform.parent = markerParent.transform;									// Assign the parent transform
			Destroy (mark.collider);														// Remove the collider that is automatically added when we create the primitive
			Quaternion newRot = new Quaternion();											// Set up a temporary Quaternion to build the new rotation
			newRot.eulerAngles = new Vector3(0,0,0);										// Reset the rotation as this was from Blender
			mark.transform.localRotation = newRot;											// Set the rotation of the star


			/*
			 * The below scale value should actually be sent to another script
			 * which can then look after it each Update() in the event that there's
			 * a scale state change
			 */


			mark.transform.localScale = new Vector3(2f,0.001f,2f);							// This is temporary assignment of scale



			/*
			 * We also need to build up the sphere colliders to determine when
			 * this distance marker will fade either in or out and when it will
			 * be active or inactive
			 */
			GameObject largeCollider = new GameObject("Large Collider");					// 
			GameObject smallCollider = new GameObject("Small Collider");					// 
			GameObject lineAlongEdge = new GameObject("Line Along Edge");					// 
			largeCollider.transform.parent = markerParent.transform;
			smallCollider.transform.parent = markerParent.transform;
			lineAlongEdge.transform.parent = markerParent.transform;
			// Now that I think of it, I probably don't need the below rigidbody
			//markerParent.AddComponent<Rigidbody>();											// Add the rigidbody to this collider parent
			//markerParent.rigidbody.useGravity = false;										// We don't want to use gravity
			//markerParent.rigidbody.isKinematic = true;										// Set it as Kinematic
		}

		Destroy (this);
	}

}
