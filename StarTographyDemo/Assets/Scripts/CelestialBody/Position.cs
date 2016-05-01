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
	public Vector3d realPosition = new Vector3d(0,0,0);				// These must absolutely be set or we end up with a null variable that we try passing later but cant
	public Vector3d relativePosition = new Vector3d(0,0,0);
	Vector3d cachedRelativePosition;

	void Awake() {
		// Lets generate some random positions for now so that we can use them to iterate over
		// and put together the code for determining the closest
		realPosition = Vector3d.Set (realPosition, new Vector3d((double)Random.Range (-100000000000000.0f, 100000000000000.0f),(double)Random.Range (-100000000000000.0f, 100000000000000.0f),(double)Random.Range (-100000000000000.0f, 100000000000000.0f)));

		/*
		 * Set the initial value of the relativePostion variable to be the same values
		 * so that we can immediately calculate the closest planet or moon, if that
		 * applies
		 */
		relativePosition = new Vector3d (realPosition.x, realPosition.y, realPosition.z);		// If we don't have this here than the relativePosition variables will be empty for Star types
		cachedRelativePosition = new Vector3d (relativePosition);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
