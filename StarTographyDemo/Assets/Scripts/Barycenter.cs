using UnityEngine;
using System.Collections;

public class Barycenter : MonoBehaviour {
	public double secToRotate = 0;									// The amount of seconds it takes for the Barycenter to orbit around its star
	public GameObject globalVariables;							// Set the globalVariables gameObject manually in inspector
	public GameObject sun;
	double barycenterYearInSeconds = 0;						// The number of seconds it takes for the Barycenter to rotate around the local star

	double distanceToSun = 0;							// The distance from Barycenter to the local star
	double angle = 0;
	public bool rotateAroundBarycenter = true;						// So we can enable and distable the planet rotation (for testing)
	SimulationSpeed simulationSpeed;

	void Start () {
		simulationSpeed = globalVariables.GetComponent<SimulationSpeed>();

		Sun _sun = sun.GetComponent<Sun> ();
		barycenterYearInSeconds = _sun.secToOrbit;				// Get the amount of time to rotate around the star from the Sun.cs script
		distanceToSun = Vector3.Distance(sun.transform.position,transform.position);	// Calculate the radius from Barycenter to the star
		if (sun.transform.position.z > transform.position.z)
			distanceToSun = distanceToSun * -1;					// Do this, otherwise both bodies rotate with each other around star instead of opposite sides
	}

	void Update () {
			// This is for using the calculated position in the orbit
			// http://orbitsimulator.com/formulas/

		if (barycenterYearInSeconds > 0 && rotateAroundBarycenter == true) {
			angle += (Time.deltaTime/ barycenterYearInSeconds) * simulationSpeed.simulationSpeed * (Mathf.Deg2Rad*360);	// Specify the new angle so that we can calculate new XZ coordinates
			double pX = distanceToSun * Mathf.Cos((float)angle);		// Calculate the new X coordinate
			double pZ = distanceToSun * Mathf.Sin((float)angle);		// Calculate the new Z coordinate
			transform.localPosition = new Vector3 ((float)pX, transform.localPosition.y, (float)pZ);	// Calculate the new XZ coordinates	
		}
	}

}
