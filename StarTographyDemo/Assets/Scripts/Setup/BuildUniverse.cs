using UnityEngine;
using System.Collections;
using Functions;

public class BuildUniverse : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject universe = Function.MakeSphereMesh("Universe", null, true);
		universe.transform.localPosition = new Vector3 (0, 0, 0);
		universe.transform.localScale = new Vector3 (19999f, 19999f, 19999f);
		universe.AddComponent<ReverseNormals> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
