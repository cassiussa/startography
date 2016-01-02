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

	float curVertSpeed = 0.0f;
	float curHorizSpeed = 0.0f;
	float acceleration = 1000f;	//How fast will object reach a maximum speed 
	//float deceleration = 75000f;	//How fast will object reach a speed of 0
	public float maxSpeed = 100000000000f;		//This is the maximum speed that the object will achieve 


	Vector3d thisPosition = new Vector3d (0d, 0d, 0d);
	float cachedHorizontal;
	float cachedVertical;

	void Awake() {
		maxSpeed = 100000000000f;
	}

	public float zSlowTime = 1f;
	private bool zSlowing = false;		// Are we currently slowing? we use this to prevent multiple instance of SlowVertical starting.
	
	IEnumerator SlowVertical() {
		zSlowing = true;
		var time = 0f;
		//var turnIncrement = new Vector3( 0 , 0 , turnSpeed * Time.deltaTime);
		//Turn towards the side.
		while ( time < zSlowTime ) {
			time += Time.deltaTime;
			//transform.rotation.eulerAngles += turnIncrement;
			yield return null;
		}
	}
	
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		curVertSpeed += acceleration*vertical;
		curHorizSpeed += acceleration*horizontal;

		curVertSpeed = Mathf.Clamp(curVertSpeed,-maxSpeed,maxSpeed);	// Clamps curSpeed
		curHorizSpeed = Mathf.Clamp(curHorizSpeed,-maxSpeed,maxSpeed);	// Clamps curSpeed

		if (vertical == 0 && !zSlowing) {
			SlowVertical();
			if (curVertSpeed > acceleration)
				curVertSpeed -= acceleration * 5; 
			if (curVertSpeed < -acceleration)
				curVertSpeed += acceleration * 5;
		} else {
			curVertSpeed = curVertSpeed * 1.01f;
		}


		if (horizontal == 0) {
			if (curHorizSpeed > acceleration)
				curHorizSpeed -= acceleration * 5; 
			if (curHorizSpeed < -acceleration)
				curHorizSpeed += acceleration * 5;
		} else {
			curHorizSpeed = curHorizSpeed * 1.01f;
		}


		//float horizontal = Input.GetAxis ("Horizontal")*maxSpeed*-1;
		//float vertical = Input.GetAxis ("Vertical")*maxSpeed;

		/*if (horizontal > 0 && cachedHorizontal > 0)
			maxSpeed = maxSpeed + 10000f;
		else if (horizontal < 0 && cachedHorizontal > 0)
			maxSpeed = maxSpeed - 10000f;

		if (vertical > 0 && cachedVertical > 0)
			maxSpeed = maxSpeed + 10000f;
		else if (vertical < 0 && cachedVertical > 0)
			maxSpeed = maxSpeed - 10000f;*/

		thisPosition = new Vector3d (thisPosition.x + curHorizSpeed * Time.deltaTime,
		                             thisPosition.y, 
		                             thisPosition.z + curVertSpeed * Time.deltaTime);
		camPosition = new Vector3d (thisPosition.x, thisPosition.y, thisPosition.z);


		/*
		thisPosition = new Vector3d (thisPosition.x + horizontal * Time.deltaTime,
		                             thisPosition.y, 
		                             thisPosition.z + vertical * Time.deltaTime);
		camPosition = new Vector3d (thisPosition.x, thisPosition.y, thisPosition.z);
		*/

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

		//cachedHorizontal = horizontal;
		//cachedVertical = vertical;
	}
}
