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

	float xSpeed = 0f;			//Don't touch this
	float zSpeed = 0f;			//Don't touch this
	float MaxSpeed = 60000f;		//This is the maximum speed that the object will achieve 
	float Acceleration = 100000000000f;	//How fast will object reach a maximum speed 
	float Deceleration = 75000f;	//How fast will object reach a speed of 0

	Vector3d thisPosition = new Vector3d (0d, 0d, 0d);

	void Update () {
		float horizontal = Input.GetAxis ("Horizontal")*MaxSpeed*-1;
		float vertical = Input.GetAxis ("Vertical")*MaxSpeed;

		thisPosition = new Vector3d (thisPosition.x + horizontal * Time.deltaTime,
		                             thisPosition.y, 
		                             thisPosition.z + vertical * Time.deltaTime);
		camPosition = new Vector3d (thisPosition.x, thisPosition.y, thisPosition.z);


		/*
		if ((Input.GetKey(KeyCode.LeftArrow)) && (xSpeed < MaxSpeed)) 
			xSpeed = xSpeed - Acceleration * Time.deltaTime;
		else if ((Input.GetKey(KeyCode.RightArrow)) && (xSpeed > -MaxSpeed))
			xSpeed = xSpeed + Acceleration * Time.deltaTime;
		else if ((Input.GetKey(KeyCode.DownArrow)) && (zSpeed < MaxSpeed)) 
			zSpeed = zSpeed - Acceleration * Time.deltaTime;
		else if ((Input.GetKey(KeyCode.UpArrow)) && (zSpeed > -MaxSpeed))
			zSpeed = zSpeed + Acceleration * Time.deltaTime;
		else {
			if (xSpeed > Deceleration) 
				xSpeed = xSpeed - Deceleration * Time.deltaTime;
			else if (xSpeed < -Deceleration) 
				xSpeed = xSpeed + Deceleration * Time.deltaTime;
			else xSpeed = 0;
			if (zSpeed > Deceleration) 
				zSpeed = zSpeed - Deceleration * Time.deltaTime;
			else if (zSpeed < -Deceleration) 
				zSpeed = zSpeed + Deceleration * Time.deltaTime;
			else zSpeed = 0;
		}
		/*thisPosition = new Vector3d (thisPosition.x + xSpeed * Time.deltaTime,
		                             thisPosition.y, 
		                             thisPosition.z + zSpeed * Time.deltaTime);
		camPosition = new Vector3d (thisPosition.x * -1, thisPosition.y * -1, thisPosition.z * -1);
		*/

	}
}
