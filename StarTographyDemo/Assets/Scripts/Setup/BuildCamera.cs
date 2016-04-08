using UnityEngine;
using System.Collections;

public class BuildCamera : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		GameObject cameras = new GameObject ("Cameras");
		cameras.AddComponent<AudioListener> ();
		for(int i=1;i<=10;i++) {
			GameObject camera = new GameObject("Camera Layer "+i);
			camera.transform.parent = cameras.transform;

			// Cache the Camera component since we have a few things to do with it
			Camera cam = camera.AddComponent<Camera>();
			cam.farClipPlane = 10000f;
			cam.nearClipPlane = 0.1f;
			cam.depth = i;

			// Set the clear flags to clear on all layers except for the farthest camera downt he layer list
			if(i == 10) {
				cam.clearFlags = CameraClearFlags.Nothing;
			} else {
				cam.clearFlags = CameraClearFlags.Depth;
			}

			camera.AddComponent("FlareLayer");
			camera.AddComponent<GUILayer>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
