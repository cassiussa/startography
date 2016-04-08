using UnityEngine;
using System.Collections;

public class BuildCamera : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		GameObject cameras = new GameObject ("Cameras");
		cameras.AddComponent<Rigidbody> ();							// Add the rigidbody to the Camera
		cameras.AddComponent<SphereCollider> ();					// Assign a SphereCollider
		cameras.AddComponent<AudioListener> ();
		cameras.tag = "MainCamera";
		for(int i=1;i<=10;i++) {
			GameObject camera = new GameObject("Camera Layer "+i);
			camera.transform.parent = cameras.transform;

			// Cache the Camera component since we have a few things to do with it
			Camera cam = camera.AddComponent<Camera>();
			cam.farClipPlane = 10000f;								// Set the camera's maximimum viewable distance
			cam.nearClipPlane = 0.1f;								// Set the camera's near clip plane
			cam.depth = 11-i;										// Set the camera's depth of field
			cam.fieldOfView = 60f;									// Set the camera's field of view
			cam.gameObject.layer = i+7;								// Assigns this camera the appropriate layer
			cam.tag = "MainCamera";

			// Set the clear flags to clear on all layers except for the farthest camera downt he layer list
			if(i == 10) {
				cam.clearFlags = CameraClearFlags.Skybox;			// this is the farthest camera so set the Skybox
				cam.backgroundColor = new Color(0,0,0);
			} else {
				cam.clearFlags = CameraClearFlags.Depth;			// Make sure we can see past this camera's maximum distance
			}

			// Add some default Components to the camera
			camera.AddComponent("FlareLayer");
			camera.AddComponent<GUILayer>();
		}
	}

}
