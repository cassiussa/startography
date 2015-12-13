using UnityEngine;
using System.Collections;

public class TrailRenderControl : MonoBehaviour {

	public TrailRenderer trailRend;
	public GameObject globalVariables;
	public SimulationSpeed simulationSpeed;
	public Sun sun;
	public double sunSecToOrbit;

	double cachedSimulationSpeed;
	// Use this for initialization
	void Start () {
		globalVariables = GameObject.Find ("/GlobalVariables");
		simulationSpeed = globalVariables.GetComponent<SimulationSpeed>();
		cachedSimulationSpeed = simulationSpeed.simulationSpeed;
		trailRend = GetComponent<TrailRenderer>();
		trailRend.startWidth = 2;
		trailRend.endWidth = 0;

		sunSecToOrbit = sun.secToOrbit;
		trailRend.time = (float)(sunSecToOrbit/simulationSpeed.simulationSpeed*0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (simulationSpeed.simulationSpeed != cachedSimulationSpeed) {
			cachedSimulationSpeed = simulationSpeed.simulationSpeed;
			trailRend.time = (float)sunSecToOrbit/Mathf.Abs((float)simulationSpeed.simulationSpeed)*((float)0.5);
		}
	}
	
}
