﻿using UnityEngine;
using System.Collections;

/*
 * This script contains the ongoing accurate positioning data that is used
 * by the cameras and also by all other objects in space.  They should use
 * the vectors contained here as an additive to their own positions within
 * their individual positioning scripts.
 * 
 * This way, the camera is always at the origin (0,0,0) and the universe
 * moves around it.
 * 
 * If you need to make changes to position data, such as orbit changes,
 * these should be done in the PositionProcessing.cs script instead.
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
	float factor = 0f;
	float zAcceleration = 0f;					// How fast will object reach a maximum speed 
	float xAcceleration = 0f;					// How fast will object reach a maximum speed 

	Vector3d thisPosition = new Vector3d (0d, 0d, 0d);
	[HideInInspector]
	public double holdTimeMin = 30d;
	[HideInInspector]
	public double holdTimeMax = 300d;

	double holdTime = 0;
	bool timeSet = false;

	void Awake(){
		holdTimeMin = 300d;
		holdTimeMax = 3000d;
		cacheMovement = new GameObject();
	}

	GameObject cacheMovement;
	void Update () {
		/*
		 * Controller Mapping for Left Analog Stick
		 * 
		 * Left	 +1
		 * Right -1
		 * Up	 -1
		 * Down	 +1
		 * 
		 */
		float horizontal = Input.GetAxis ("Horizontal")*-1;
		float vertical = Input.GetAxis ("Vertical");

		/*
		 * Controller Mapping for Right Analog Stick
		 * 
		 * Left	 -1
		 * Right +1
		 * Up	 -1
		 * Down	 +1
		 * 
		 */

		float turnVertical = Input.GetAxis ("TurnVertical");										// Get the up/down input from the right analog stick
		float turnHorizontal = Input.GetAxis ("TurnHorizontal");									// Get the left/right input from the right analog stick
		transform.Rotate(Vector3.up, turnHorizontal * Time.deltaTime * 20);							// Assign the up/down rotation to the camera itself
		transform.Rotate(Vector3.right, turnVertical * Time.deltaTime * 20);						// Assign the left/right rotation to the camera itself



		cacheMovement.transform.position = Vector3.zero;											// Reset the cached gameObject's Vector3 position to 0,0,0
		cacheMovement.transform.rotation = transform.rotation;										// Assign the rotation of the camera to the cached gameObject
		if (vertical != 0 || horizontal != 0) {
			holdTime += Time.deltaTime * holdTime;
			holdTime = Mathf.Clamp ((float)holdTime, (float)holdTimeMin, (float)holdTimeMax);		// Clamp the holdTime variable so that we don't get insane exceleration or speed on the camera

			zAcceleration = ((float)holdTime * vertical);											// Calculate the acceleration along the Z axis
			xAcceleration = ((float)holdTime * horizontal);											// Calculate the acceleration along the X axis
			cacheMovement.transform.Translate(Vector3.forward * zAcceleration);						// Translate the position of the cached Vector3
			cacheMovement.transform.Translate(Vector3.right * xAcceleration);						// Translate the position of the cached Vector3

		} else {
			xAcceleration = 0;																		// Reset the acceleration along the X axis
			zAcceleration = 0;																		// Reset the acceleration along the z axis
			holdTime = 0f;																			// reset the holdTime variable as no movement on the gamepad was detected
		}

		camPosition = new Vector3d (																// Create the new double position of where the camera would be if the universe didn't move around it
			camPosition.x + (cacheMovement.transform.position.x),
			camPosition.y + (cacheMovement.transform.position.y), 
			camPosition.z + (cacheMovement.transform.position.z));

	}
}
