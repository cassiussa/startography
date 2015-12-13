using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	public double secToOrbit = 0;									// The amount of seconds it takes for the bodies to orbit around their barycenter
	public GameObject globalVariables;							// Set the globalVariables gameObject manually in inspector
	public double localSpeed = 0f;								// The variable that will hold the simulation speed, from globalVariables
	// Use this for initialization
	void Start () {
		SimulationSpeed simulationSpeed = globalVariables.GetComponent<SimulationSpeed>();
		localSpeed = simulationSpeed.simulationSpeed;			// Get the simulation's speed from the globalVariables gameObject and assign to this body
	}
	
	// Update is called once per frame
	void Update () {
		//float rotationSpeed = 0f;
		//rotationSpeed = Time.deltaTime * 360 * -1 * localSpeed / secToOrbit;	// Calculate how long each rotation should take
		//transform.Rotate(0, rotationSpeed, 0, Space.Self);
	}
}
