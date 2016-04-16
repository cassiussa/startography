using UnityEngine;
using System.Collections;

public class BuildDistanceMarkerLargeCollider : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
		sphereCollider.radius = 60000;


		Destroy (this);
	}

}
