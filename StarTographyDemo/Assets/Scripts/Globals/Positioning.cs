using UnityEngine;
using System.Collections;

/*
 * This script contains the ongoing accurate positioning data that is used
 * by the cameras and also by all other objects in space.  They should use
 * the vectors contained here as an additive to their own positions within
 * their individual positioning scripts.
 * 
 * This way, the camera is always at the origin (0,0,0) and the universe
 * moves around it.
 */

public class Positioning : Functions {

	/* 
	 * This is the ongoing real coordinate for the camera origin.  While
	 * the camera will always remain at 0,0,0, this value will represent
	 * where position 0,0,0 corresponds from in space.  Numbers must 
	 * be opposite values.  Ex: 10,20,55 is where the camera would be,
	 * however it's position is 0,0,0 making adjustment -10,-20,-55
	 */

	float xSpeed = 0f;							// Don't touch this
	float zSpeed = 0f;							// Don't touch this
	public float maxSpeed = 0f;				// This is the maximum speed that the object will achieve 
	float acceleration = 0f	;				// How fast will object reach a maximum speed 
	float Deceleration = 75000f;				// How fast will object reach a speed of 0

	Vector3d thisPosition = new Vector3d (0d, 0d, 0d);

	float holdTime = 0;
	bool timeSet = false;
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal")*-1;
		float vertical = Input.GetAxis ("Vertical");

		camPosition = new Vector3d (camPosition.x + horizontal * Time.deltaTime,
		                             camPosition.y, 
		                            camPosition.z + acceleration);

		if (vertical != 0) {
			holdTime += (holdTime*Time.deltaTime)+Time.deltaTime;
			acceleration = (holdTime*vertical);
			if(acceleration < 0)
				acceleration = Mathf.Clamp (acceleration, -20000000000000000f, -200f);
			else if (acceleration > 0)
				acceleration = Mathf.Clamp (acceleration, 200f, 20000000000000000f);

			Debug.Log ("holdTime = "+holdTime+",  acceleration = "+acceleration);
		} else {
			holdTime = 0f;
			acceleration = 0;
		}
	}
}
