/*
 * This script holds onto the positional data of all the objects
 * in the currently-active solar system.  That way we can quickly
 * iterate over each one [one per Update()] and do a much less
 * expensive distance calculation from the relativePosition to
 * the camera's position.  This is being done instead of having a
 * series of distance colliders on planets and moons  and also so
 * that we have a constant understanding of how close the camera
 * is to a planet or moon so we can scale the object appropriately.
 */
using UnityEngine;
using System.Collections;
using Functions;

public class DistanceArrays : MonoBehaviour {

	public GameObject[] bodies;
	public Position[] positionScripts;
	public CelestialBodyBuilder[] celestialBodyBuilderScripts;
	public Vector3d[] realPositions;					// Don't think I'll need this in here, but just in case
	public Vector3d[] relativePositions;
	public Vector3d some = new Vector3d(0,10,0);
	public Vector3d thing;

	void Awake() {
		// Literal assignations
		bodies = gameObject.GetComponent<CelestialBodyBuilder> ().bodies;
		celestialBodyBuilderScripts = gameObject.GetComponent<CelestialBodyBuilder> ().celestialBodyBuilderScripts;
		positionScripts = gameObject.GetComponent<CelestialBodyBuilder> ().positionScripts;
		/*
		 * NOTE: Significant problem is displayed here.
		 * The realPositions array of variables in the CelestialBodyBuilder script is still null at this point
		 */
		realPositions = gameObject.GetComponent<CelestialBodyBuilder> ().realPositions;
		relativePositions = gameObject.GetComponent<CelestialBodyBuilder> ().relativePositions;

		for(int i=0;i<positionScripts.Length;i++) {
			Debug.LogError (bodies[i],bodies[i]);
			Debug.LogError (celestialBodyBuilderScripts[i],celestialBodyBuilderScripts[i].gameObject);
			Debug.LogError (positionScripts[i],positionScripts[i].gameObject);
			Debug.LogError (realPositions[i]);
			Debug.LogError (relativePositions[i]);
			positionScripts[i].relativePosition = relativePositions[i];
		}

		thing = some;
		Debug.Log (ReferenceEquals(some,thing));

		/*
		 * The below is to test to make sure that the Vector3d variables
		 * we're using in here are literally the same variables as those
		 * found within the parent Star's array
		 */
		// works
		//relativePositions[0].x = 22;


	}
}
