using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star {
	public string name;
	public ObjectData objectDataScript;
	public ScaleStates scaleStatesScript;

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
	public string name;
	public GameObject parentStar;
	public ObjectData objectDataScript;
	public ScaleStates scaleStatesScript;
	public PlanetOrbitPathTrail planetOrbitPathTrailScript;
	
	// Constructor
	public Planet() {
		name = null;
		parentStar = null;
		objectDataScript = null;
		scaleStatesScript = null;
		planetOrbitPathTrailScript = null;
	}
	
	public Planet(string xname, ObjectData xobjectDataScript, ScaleStates xscaleStatesScript, GameObject xparentStar, PlanetOrbitPathTrail xplanetOrbitPathTrailScript) {
		name = xname;
		objectDataScript = xobjectDataScript;
		scaleStatesScript = xscaleStatesScript;
		parentStar = xparentStar;
		planetOrbitPathTrailScript = xplanetOrbitPathTrailScript;
	}
}

public class Moon {
	public string name;
	public GameObject parentStar;
	public ObjectData objectDataScript;
	public ScaleStates scaleStatesScript;
	//public PlanetOrbitPathTrail planetOrbitPathTrailScript;
	
	// Constructor
	public Moon() {
		name = null;
		parentStar = null;
		objectDataScript = null;
		scaleStatesScript = null;
		//planetOrbitPathTrailScript = null;
	}
	
	public Moon(string xname, ObjectData xobjectDataScript, ScaleStates xscaleStatesScript, GameObject xparentStar) {
		name = xname;
		objectDataScript = xobjectDataScript;
		scaleStatesScript = xscaleStatesScript;
		parentStar = xparentStar;
		//planetOrbitPathTrailScript = xplanetOrbitPathTrailScript;
	}
}

public class Items : MonoBehaviour {

	public List<Star> stars = new List<Star>();
	public List<Planet> planets = new List<Planet>();
	public List<Moon> moons = new List<Moon>();

	// Use this for initialization
	void Start () {
		int i = 0;
		for (i=0; i<stars.Count; i++) {
			Debug.Log ("Star name is " + stars [i].name);
		}
		for (i=0; i<planets.Count; i++) {
			Debug.LogError ("Planet name is " + planets [i].name+" and my star's name is "+planets[i].parentStar.name+", path trail: "+planets[i].planetOrbitPathTrailScript);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
