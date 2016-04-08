using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public double speed = 10.0;
	public SimulationSpeed simulationSpeed;
	public bool enableScroll = false;
	void Start() {
		simulationSpeed = GetComponent<SimulationSpeed> ();

	}

	void Update() {
		if (simulationSpeed.simulationSpeed < -1000000) {
			speed = 10000000;
		} else if (simulationSpeed.simulationSpeed < -100000) {
			speed = 1000000;
		} else if (simulationSpeed.simulationSpeed < -50000) {
			speed = 100000;
		} else if (simulationSpeed.simulationSpeed < -500) {
			speed = 10000;
		} else if (simulationSpeed.simulationSpeed < -10) {
			speed = 500;
		} else if (simulationSpeed.simulationSpeed < 50 && simulationSpeed.simulationSpeed > -50) {
			speed = 50;
		} else if (simulationSpeed.simulationSpeed < 500) {
			speed = 500;
		} else if (simulationSpeed.simulationSpeed < 10000) {
			speed = 10000;
		} else if (simulationSpeed.simulationSpeed < 100000) {
			speed = 100000;
		} else if (simulationSpeed.simulationSpeed < 1000000) {
			speed = 1000000;
		} else {
			speed = 10000000;
		}
		if (enableScroll) {
			double timeSpeed = Input.GetAxis ("Mouse ScrollWheel") * speed;
			timeSpeed *= Time.deltaTime;
			//simulationSpeed.simulationSpeed = Mathf.Clamp (simulationSpeed.simulationSpeed + timeSpeed, 1, 30000000);
			simulationSpeed.simulationSpeed = simulationSpeed.simulationSpeed + timeSpeed;
		}
	}
}