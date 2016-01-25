using UnityEngine;
using System.Collections;

public class GenerateBodyColliders : Functions {

	ObjectData objectDataScript;
	ScaleStates scaleStatesScript;
	/*
	 * This script creates the gameobject children for the colliders for any given body.
	 * It is automatically added to Planets and Stars by the ScaleStates script.
	 */
	void Awake () {
		objectDataScript = GetComponent<ObjectData> ();
		if (!objectDataScript)
			Debug.LogError ("There needs to be an ObjectData script here", gameObject);
		scaleStatesScript = GetComponent<ScaleStates> ();

		GameObject localColliders = new GameObject();											// Instantiate a new GameObject
		localColliders.name = "LocalColliders";													// Set the name for the LocalColliders GameObject
		localColliders.transform.SetParent(transform);											// Set the LocalColliders GameObject as a child of the body
		localColliders.AddComponent<Rigidbody>();												// Add a rigidbody component to the LocalColliders GameObject
		localColliders.rigidbody.isKinematic = true;											// Set isKinematic on the rigidbody on LocalColliders GameObject
		localColliders.rigidbody.useGravity = false;											// Disable gravity on the rigidbody on LocalColliders GameObject
		localColliders.transform.localScale = new Vector3 (1f, 1f, 1f);							// Reset the scale of the LocalColliders GameObject
		localColliders.transform.localPosition = new Vector3 (0f,0f,0f);						// Reset the position of the LocalColliders GameObject
		localColliders.transform.localRotation = new Quaternion (0f,0f,0f,0f);					// Reset the rotation of the LocalColliders GameObject

		string[] inputs;																		// Array of strings of distance types
		double[] measurements;																	// Array of doubles of distances taken from Constants.cs file
		
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Planet) {
			inputs = new string[] { "A","B","C","D"};											// Planets only get 4 sphere colliders
		} else if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
			inputs = new string[] { "A","B","C","D","E","F","G","H","I","J","K","L","M", "N" };	// Create an array of the names (string) of each scale state
		} else {
			inputs = new string[] { "A","B" };													// Create an array of the names (string) of each scale state
		}
		measurements = new double[] { 
			10000,
			100000,
			1000000,
			10000000,
			100000000,
			1000000000,
			10000000000,
			100000000000,
			1000000000000,
			10000000000000,
			100000000000000,
			1000000000000000,
			10000000000000000,
			100000000000000000};					// Create an array of the size (double) of each scale state using values from Constants.cs

		for(int i=0;i<inputs.Length;i++) {														// Iterate through each of the scale state sizes
			GameObject tempObj = new GameObject();												// Create a new GameObject for this specific scale state
			tempObj.name = inputs[i];															// Assign the appropriate name to the scale state's GameObject
			tempObj.tag = inputs[i];															// Assign the appropriate tag to the scale state's GameObject
			tempObj.transform.SetParent (localColliders.transform);								// Assign the scale state's GameObject as a child of the LocalColliders GameObject
			tempObj.transform.localScale = new Vector3 (1f, 1f, 1f);							// Reset the scale of the scale state's GameObject
			tempObj.transform.localPosition = new Vector3 (0f,0f,0f);							// Reset the position of the scale state's GameObject
			tempObj.transform.localRotation = new Quaternion (0f,0f,0f,0f);						// Reset the rotation of the scale state's GameObject
			tempObj.AddComponent<SphereCollider>();												// Add a SphereCollider to the child GameObject
			Vector3d thisRadius = S3dToV3d(objectDataScript.radius);							// Get the assign radius from ObjectData and convert it to a Vector3d
			tempObj.GetComponent<SphereCollider>().radius = (float)(measurements[i]/(thisRadius.x*2));	// Dynamically apply the radius scale to the scale state's GameObject
			tempObj.GetComponent<SphereCollider>().isTrigger = true;							// Set the scale state's collider to report on triggers

			tempObj.AddComponent<BodyColliderTest01>();											// Add the OnTrigger(Enter|Exit) script
			tempObj.GetComponent<BodyColliderTest01>().thisScale = measurements[i];				// Add the scale, in double, of this GameObject's scale
			tempObj.GetComponent<BodyColliderTest01>().thisLocalScale = transform.localScale.x;	// Add the scale, in double, of this GameObject's scale
			if(objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Planet)	 {
				/*if(i == inputs.Length-1) {
					tempObj.GetComponent<BodyColliderTest01>().lastCollider = true;						// Add the scale, in double, of this GameObject's scale
				}*/
				tempObj.GetComponent<BodyColliderTest01>().mesh = scaleStatesScript.meshes;
			}
		}
	}

}
