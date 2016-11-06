using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elements;
using SimpleJSON;
using BodyElements;
using ImportData;
using System.IO;
using CustomMath;

public class CreateCameras : MonoBehaviour {

	int camDepth = 20;
	void Awake () {
		for (int i=1; i<19; i++) {
			Camera cam = new GameObject().AddComponent<Camera>();
			cam.depth = camDepth - i;
			cam.transform.parent = transform;
			cam.transform.position = new Vector3(0f,0f,0f);
			cam.name = "Camera "+i;
			cam.farClipPlane = 20000f;
			cam.nearClipPlane = 0.01f;
			if(i != 18)
				cam.clearFlags = CameraClearFlags.Depth;    // Add as a camera layer
			else
				cam.clearFlags = CameraClearFlags.Nothing;  // Background layer
			cam.camera.cullingMask = 1 << i+7;
			cam.gameObject.layer = i+7;
			cam.tag = "MainCamera";
			Rigidbody _cameraRigidbody = cam.gameObject.AddComponent<Rigidbody>();
			_cameraRigidbody.useGravity = false;
			SphereCollider _cameraCollider = cam.gameObject.AddComponent<SphereCollider>();
			_cameraCollider.isTrigger = true;
			//_cameraCollider.radius = 0.0144f;

			Debug.Log (cam.depth);
		}
	}

}
