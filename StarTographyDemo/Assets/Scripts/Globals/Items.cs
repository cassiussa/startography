using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star {
	public string name;// { get; set; }
	public ObjectData objectDataScript;// { get; set; }
	public ScaleStates scaleStatesScript;// { get; set; }

	// Constructor
	public Star() {
		name = null;
		objectDataScript = null;
		scaleStatesScript = null;
	}

	public Star(string xname, ObjectData xobjectDataScript, ScaleStates xscaleStatesScript) {
		name = xname;
		objectDataScript = xobjectDataScript;
		scaleStatesScript = xscaleStatesScript;
	}
}

public class Planet {
	public string name;// { get; set; }
	//public GameObject parentStar { get; set; }
	public ObjectData objectDataScript;// { get; set; }
	public ScaleStates scaleStatesScript;// { get; set; }
	//public PlanetOrbitPathTrail planetOrbitPathTrailScript { get; set; }

	// Constructor
	public Planet() {
		name = null;
		objectDataScript = null;
		scaleStatesScript = null;
	}
	
	public Planet(string xname, ObjectData xobjectDataScript, ScaleStates xscaleStatesScript) {
		name = xname;
		objectDataScript = xobjectDataScript;
		scaleStatesScript = xscaleStatesScript;
	}
}

public class Items : MonoBehaviour {

	public List<Star> stars = new List<Star>();
	public List<Planet> planets = new List<Planet>();
	//public List<Moon> moons = new List<Moon>();

	// Use this for initialization
	void Start () {
		int i = 0;
		for (i=0; i<stars.Count; i++) {
			Debug.Log ("stars = " + stars [i].scaleStatesScript);
		}
		for (i=0; i<planets.Count; i++) {
			Debug.Log ("planets = " + planets [i].scaleStatesScript);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
