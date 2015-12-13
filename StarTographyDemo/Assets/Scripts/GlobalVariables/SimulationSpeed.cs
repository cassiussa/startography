using UnityEngine;
using System.Collections;

public class SimulationSpeed : MonoBehaviour {
	// 525949 = 1 earth year per 60 seconds
	public double simulationSpeed = 0d;						// The overall simulation speed
	double cachedSpeed = 0d;

	void Update() {
		if (cachedSpeed != simulationSpeed) {
			cachedSpeed = simulationSpeed;
		}

	}
	
}
