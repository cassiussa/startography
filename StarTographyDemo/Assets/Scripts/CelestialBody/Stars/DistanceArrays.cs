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
	public Vector3d[] realPositions;										// Don't think I'll need this in here, but just in case
	public Vector3d[] relativePositions;

	void Awake() {
		// Literal assignations
		bodies = gameObject.GetComponent<CelestialBodyBuilder> ().bodies;
		celestialBodyBuilderScripts = gameObject.GetComponent<CelestialBodyBuilder> ().celestialBodyBuilderScripts;
		positionScripts = gameObject.GetComponent<CelestialBodyBuilder> ().positionScripts;
		realPositions = gameObject.GetComponent<CelestialBodyBuilder> ().realPositions;
		relativePositions = gameObject.GetComponent<CelestialBodyBuilder> ().relativePositions;

		for(int i=0;i<positionScripts.Length;i++) {
			positionScripts[i].realPosition = realPositions[i];				// Assign them into the same variable reference
			positionScripts[i].relativePosition = relativePositions[i];		// Assign them into the same variable reference
			Debug.Log ("It is "+ReferenceEquals(positionScripts[i].relativePosition,relativePositions[i])+" that we're using references instead of values");
		}

	}
}
