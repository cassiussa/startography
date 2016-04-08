using UnityEngine;
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
	public Vector3d realPosition = new Vector3d(1,0,0);
	public Vector3d relativePosition = new Vector3d(2,0,0);
	Vector3d cachedRelativePosition = new Vector3d(3,0,0);

	void Awake() {
		/*
		 * Set the initial value of the relativePostion variable to be the same values
		 * so that we can immediately calculate the closest planet or moon, if that
		 * applies
		 */
		//relativePosition = realPosition;
		//relativePosition = new Vector3d(realPosition);
		//cachedRelativePosition = new Vector3d (relativePosition);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
