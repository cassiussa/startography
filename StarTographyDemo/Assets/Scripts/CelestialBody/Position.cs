﻿using UnityEngine;
using System.Collections;
using Functions;

public class Position : MonoBehaviour {

	/*
	 * the realPosition Vector3d variable holds on to this celestial
	 * body's real position in 3D space.  It is likely to always be
	 * a very large set of numbers.
	 * 
	 * relativePosition, on the other hand, holds positional data based
	 * on the difference in position from the camera.  So if realPosition
	 * is at 10,0,0 and camera is at 2,0,0, then relativePosition would be
	 * at 8,0,0.
	 * 
	 * realPosition and relativePosition have the same values (though not
	 * literally the same variable reference) at the start of the simulation
	 */
	public Vector3d realPosition;
	public Vector3d relativePosition;

	void Awake() {
		/*
		 * Set the initial value of the relativePostion variable to be the same values
		 * so that we can immediately calculate the closest planet or moon, if that
		 * applies
		 */
		relativePosition = new Vector3d(realPosition);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}