using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class BodyColliderTest01 : MonoBehaviour {

	public CameraSpeedStates cameraSpeedStates;
	public double thisScale;
	public double thisLocalScale;
	//public bool lastCollider = false;
	public GameObject mesh;
	
	void Start() {
		cameraSpeedStates = GameObject.Find ("/Cameras").GetComponent<CameraSpeedStates> ();
	} 


	void OnTriggerEnter(Collider other) {
		if (other.tag == "MainCamera") {
			if (!cameraSpeedStates.currentCollisions.ContainsKey(gameObject.collider)) {	// Check to see that we haven't already added this collider to the currentCollisions Dictionary
				cameraSpeedStates.currentCollisions.Add(gameObject.collider,thisScale);		// Add this SphereCollider to the currentCollisions Dictionary
				cameraSpeedStates.OnScaleCollision();										// Find the smallest scale found in the currentCollisions Dictionary
				/*if(lastCollider == true) {
					mesh.transform.localScale = new Vector3(1,1,1);
				}*/
			}
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "MainCamera") {
			if (cameraSpeedStates.currentCollisions.ContainsKey(gameObject.collider)) {		// Make sure the current collider exists in the currentCollisions Dictionary before trying to remove it
				cameraSpeedStates.currentCollisions.Remove(gameObject.collider);			// Remove this SphereCollider to the currentCollisions Dictionary
				cameraSpeedStates.OnScaleCollision();										// Find the smallest scale found in the currentCollisions Dictionary
				/*if(lastCollider == true) {
					mesh.transform.localScale = new Vector3(100,100,100);
				}*/
			}
		}
	}
	
}
