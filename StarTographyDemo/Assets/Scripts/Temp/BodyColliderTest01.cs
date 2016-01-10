using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class BodyColliderTest01 : MonoBehaviour {

	public CameraSpeedStates cameraSpeedStates;
	public double thisScale;
	
	void Start() {
		cameraSpeedStates = GameObject.Find ("/Cameras").GetComponent<CameraSpeedStates> ();
	} 


	void OnTriggerEnter(Collider other) {
		if (other.tag == "MainCamera") {
			if (!cameraSpeedStates.currentCollisions.ContainsKey(gameObject.collider)) {
				cameraSpeedStates.currentCollisions.Add(gameObject.collider,thisScale);
				cameraSpeedStates.OnScaleCollision();
			}
		}
	}
	
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "MainCamera") {
			if (cameraSpeedStates.currentCollisions.ContainsKey(gameObject.collider)) {
				cameraSpeedStates.currentCollisions.Remove(gameObject.collider);
				cameraSpeedStates.OnScaleCollision();
			}
		}
	}
	
}
