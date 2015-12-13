using UnityEngine;
using System.Collections;

/*
 * This script is used to adjust the distance of the camera to whatever solar system it is moving
 * away from.  It should calculate the distance and direction vector from the camera to the solar
 * system in question.
 * */

public class AdjustDistance : MonoBehaviour {

	private Vector3 direction;
	public bool useMainCamera = true;	// Use the camera tagged MainCamera
	public Camera cameraToUse;			// Only use this if useMainCamera is false
	Camera cam;

	// Use this for initialization
	void Start () {
		if (useMainCamera)
			cam = Camera.main;
		else
			cam = cameraToUse;
	}

	// Update is called once per frame
	void Update () {
		//CalculateDirection (4f);
		DebugOurStuff ();
	}

	public void CalculateFarDirection(float scale) {
		if(cam != null)
			direction = CalculateFarVector(cam.transform.position, this.transform.position, scale);
	}
	
	Vector3 CalculateFarVector(Vector3 first, Vector3 second, float scale) {
		Vector3 midPoint = new Vector3 (
			((second.x - first.x) / scale)+first.x,
			((second.y - first.y) / scale)+first.y,
			((second.z - first.z) / scale)+first.z);
		// Reposition the solar system so that it's a distance from the camera 
		// that equals its scale during the last frame pre-State change
		transform.position = midPoint;
		return midPoint;
	}
	//(first.x - second.x) / 1/scale) + second.x) * -1

	public void CalculateLocalDirection(float scale) {
		if(cam != null)
			direction = CalculateLocalVector(cam.transform.position, this.transform.position, scale);
	}
	
	Vector3 CalculateLocalVector(Vector3 first, Vector3 second, float scale) {
		Vector3 midPoint = new Vector3 (
			((first.x - second.x) / (1/scale) ) + second.x * -1,
			((first.y - second.y) / (1/scale) ) + second.y * -1,
			((first.z - second.z) / (1/scale) ) + second.z * -1);
		// Reposition the solar system so that it's a distance from the camera 
		// that equals its scale during the last frame pre-State change
		transform.position = midPoint;
		return midPoint;
	}

	public void DebugOurStuff() {
		Debug.DrawLine(Vector3.zero, cam.transform.position, Color.blue);
		Debug.DrawLine(Vector3.zero, this.transform.position, Color.cyan);
		Debug.DrawLine(Vector3.zero, direction, Color.green);
	}
}
