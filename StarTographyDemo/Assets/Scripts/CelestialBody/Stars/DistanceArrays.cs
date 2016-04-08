using UnityEngine;
using System.Collections;
using Functions;

public class DistanceArrays : MonoBehaviour {

	public GameObject[] bodies;
	public Position[] positionScripts;
	public Vector3d[] bodyPositions;

	void Awake() {
		bodies = gameObject.GetComponent<CelestialBodyBuilder> ().bodies;
		positionScripts = new Position[bodies.Length];
		bodyPositions = new Vector3d[bodies.Length];
		for (int i = 0; i<bodies.Length; i++) {
			positionScripts[i] = bodies[i].GetComponent<Position>();
			bodyPositions[i] = positionScripts[i].realPosition;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
