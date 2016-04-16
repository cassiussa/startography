using UnityEngine;
using System.Collections;

public class BuildDistanceMarkerSmallCollider : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
		sphereCollider.radius = 5000;

		gameObject.AddComponent<DistanceMarkerSmallCollider> ();

		//Destroy (this);
	}

}
