using UnityEngine;
using System.Collections;

public class Scaling : Functions {

	/*
	 * This script is to monitor the left digital pad and performing smooth
	 * scaling of the mesh gameObject we're attached to.
	 */

	int thisScale = 1;
	float largerTime = 0f;
	float smallerTime = 0f;
	public float maxScale = 0f;
	
	// Update is called once per frame
	void Update () {

		/*if (Input.GetButton ("Larger")) {
			largerTime += Time.deltaTime;
			smallerTime = 0f;
			UpdateScale(largerTime);
		} else if (Input.GetButton ("Smaller")) {
			smallerTime += Time.deltaTime;
			largerTime = 0f;
			UpdateScale(-smallerTime);
		} else {
			largerTime = 0f;
			smallerTime = 0f;
		}*/

	}

	void UpdateScale(float scale) {
		transform.localScale = new Vector3(
			Mathf.Clamp (transform.localScale.x+scale,1,maxScale),
			Mathf.Clamp (transform.localScale.y+scale,1,maxScale),
			Mathf.Clamp (transform.localScale.z+scale,1,maxScale));
		//Debug.Log ("localscale = " + transform.localScale);
	}

}