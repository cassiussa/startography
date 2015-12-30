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

	// This is the ongoing real coordinate for the camera origin.  While
	// the camera will always remain at 0,0,0, this value will represent
	// where position 0,0,0 is corresponds from in space.

	Vector3d camCoords;

	void Start () {
		//coordinates = new Vector3d (coordX, coordY, coordZ);
		//coordinates = new Vector3d (149597870.7d, 0d, 0d);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
