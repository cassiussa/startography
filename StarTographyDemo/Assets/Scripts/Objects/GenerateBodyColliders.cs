using UnityEngine;
using System.Collections;

public class GenerateBodyColliders : Functions {

	ObjectData objectDataScript;
	/*
	 * This script creates the gameobject children for the colliders for any given body
	 */

	// Use this for initialization
	void Awake () {
		objectDataScript = GetComponent<ObjectData> ();
		if (!objectDataScript)
			Debug.LogError ("There needs to be an ObjectData script here", gameObject);

		GameObject localColliders = new GameObject();
		localColliders.name = "LocalColliders";
		localColliders.transform.SetParent(transform);
		localColliders.AddComponent<Rigidbody>();
		localColliders.rigidbody.isKinematic = true;
		localColliders.rigidbody.useGravity = false;
		localColliders.transform.localScale = new Vector3 (1f, 1f, 1f);
		localColliders.transform.localPosition = new Vector3 (0f,0f,0f);
		localColliders.transform.localRotation = new Quaternion (0f,0f,0f,0f);

		string[] inputs;		// Array of strings of distance types
		double[] measurements;		// Array of strings of distance types
		inputs = new string[] { "SM", "MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		measurements = new double[] { SM, MK, AU, LH, Ld, LY, PA, LD, LC, LM };
		for(int i=0;i<inputs.Length;i++) {
			GameObject tempObj = new GameObject();
			tempObj.name = inputs[i];
			tempObj.tag = inputs[i];
			tempObj.transform.SetParent (localColliders.transform);
			tempObj.transform.localScale = new Vector3 (1f, 1f, 1f);
			tempObj.transform.localPosition = new Vector3 (0f,0f,0f);
			tempObj.transform.localRotation = new Quaternion (0f,0f,0f,0f);
			tempObj.AddComponent<SphereCollider>();
			Vector3d thisRadius = S3dToV3d(objectDataScript.radius);
			//Debug.LogError ("radius = "+radius.x+", measurements = "+measurements[i]);
			if(i == 0)	// Make sure we give smallest scale at least a 2.0 radius
				tempObj.GetComponent<SphereCollider>().radius = 2.0f;
			else
				tempObj.GetComponent<SphereCollider>().radius = (float)(measurements[i]/(thisRadius.x*2));
			tempObj.GetComponent<SphereCollider>().isTrigger = true;


		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
