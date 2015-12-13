using UnityEngine;
using System.Collections;

public class SecondsToRotate : MonoBehaviour {

	public double secToRotate = 0;										// The number of seconds it takes for the body to rotate around its axis
	public GameObject globalVariables;								// Set the globalVariables gameObject manually in inspecto
	public GameObject barycenter;
	public double barycenterYearInSeconds = 0;							// The number of seconds it takes for the body to rotate around the barycenter

	//public float speed = 0f;										// Calculate speed for distance in radians since last frame
	public float distanceToBarycenter = 0f;							// The distance from body to the barycenter

	public double angle = 0f;
	SimulationSpeed simulationSpeed;
	void Start () {
		simulationSpeed = globalVariables.GetComponent<SimulationSpeed>();

		Barycenter _barycenter = barycenter.GetComponent<Barycenter> ();
		barycenterYearInSeconds = _barycenter.secToRotate;			// Get the amount of time to rotate around the Barycenter from the Barycenter.cs script
		distanceToBarycenter = Vector3.Distance(barycenter.transform.position,transform.position);	// Calculate the radius from body to barycenter
		if (barycenter.transform.position.z > transform.position.z)
			distanceToBarycenter = distanceToBarycenter * -1;		// Do this, otherwise both bodies rotate with each other around barycenter instead of opposite sides
	}
	
	// Update is called once per frame
	void Update () {
		double rotationSpeed = 0;

		if (secToRotate > 0) {										// Only calculate rotation time if the body is set to rotate
			rotationSpeed = Time.deltaTime * simulationSpeed.simulationSpeed * 360;		// Calculate how much the body will have rotated since last frame
			rotationSpeed = rotationSpeed / secToRotate * -1;		// Calculate how long each rotation should take
			transform.Rotate(0, (float)rotationSpeed, 0, Space.Self);		// Perform the day rotation of the body
		}

		if (barycenterYearInSeconds > 0) {
			angle += (Time.deltaTime/ barycenterYearInSeconds) * simulationSpeed.simulationSpeed * (Mathf.Deg2Rad*360);	// Specify the new angle so that we can calculate new XZ coordinates
			float pX = distanceToBarycenter * Mathf.Cos((float)angle);		// Calculate the new X coordinate
			float pZ = distanceToBarycenter * Mathf.Sin((float)angle);		// Calculate the new Z coordinate
			transform.localPosition = new Vector3 (pX, transform.localPosition.y, pZ);	// Calculate the new XZ coordinates	
		}
	}
	
}
