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

	//Vector3d thisPosition = new Vector3d (0d, 0d, 0d);
	[HideInInspector]
	public double holdTimeMin = 30d;
	[HideInInspector]
	public double holdTimeMax = 300d;

	double holdTime = 0;
	double upHoldTime = 0;
	double downHoldTime = 0;
	bool timeSet = false;

	GameObject cacheMovement;

	void Awake(){
		holdTimeMin = 300d;
		holdTimeMax = 3000d;
		cacheMovement = new GameObject();
		cacheMovement.name = "cacheMovement";
	}
	
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
		bool moveUp = Input.GetButton ("Up");														// Get true when the left digital pad's Up position is pressed
		bool moveDown = Input.GetButton ("Down");													// Get true when the left digital pad's Down position is pressed
		bool rotateRight = Input.GetButton ("Right");
		bool rotateLeft = Input.GetButton ("Left");
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

		if (moveUp == true) {
			downHoldTime = 0;
			upHoldTime += Time.deltaTime * upHoldTime;
			upHoldTime = Mathf.Clamp ((float)upHoldTime, (float)holdTimeMin, (float)holdTimeMax);
			cacheMovement.transform.Translate (Vector3.down * (float)upHoldTime);
		} else if (moveDown == true) {
			upHoldTime = 0;
			downHoldTime += Time.deltaTime * downHoldTime;
			downHoldTime = Mathf.Clamp ((float)downHoldTime, (float)holdTimeMin, (float)holdTimeMax);
			cacheMovement.transform.Translate (Vector3.up * (float)downHoldTime);
		} else if (rotateRight == true) {
			transform.Rotate(Vector3.back, Time.deltaTime * 20);
			upHoldTime = 0;
			downHoldTime = 0;
		} else if (rotateLeft == true) {
			transform.Rotate(Vector3.forward, Time.deltaTime * 20);
			upHoldTime = 0;
			downHoldTime = 0;
		} else {
			upHoldTime = 0;
			downHoldTime = 0;
		}

		camPosition.x += (cacheMovement.transform.position.x);										// Apply the distance that the caching gameObject moved to the camera
		camPosition.y += (cacheMovement.transform.position.y);
		camPosition.z += (cacheMovement.transform.position.z);

		cacheMovement.transform.position = new Vector3 (0, 0, 0);									// Reset position of caching gameObject
		cacheMovement.transform.rotation = new Quaternion (0, 0, 0, 0);								// Reset rotation of caching gameObject
	}
}
