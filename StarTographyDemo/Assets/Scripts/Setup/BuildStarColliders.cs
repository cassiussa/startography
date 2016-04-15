using UnityEngine;
using System.Collections;
using Functions;

public class BuildStarColliders : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject localColliders = new GameObject ("Local Colliders");						// Create the star's collider parent
		localColliders.transform.parent = transform;										// Assign the collider gameObject as a child of this gameObject
		localColliders.AddComponent<Rigidbody> ();											// Add the rigidbody to the collider parent
		localColliders.rigidbody.useGravity = false;										// We don't want to use gravity
		localColliders.rigidbody.isKinematic = true;										// Set it as Kinematic

		float colliderRadius = 1f;
		for(int localCols=0;localCols<15;localCols++) {
			GameObject go2 = Function.MakeSphereCollider("Local Collider "+localCols.ToString(), localColliders.transform, colliderRadius, true);
			colliderRadius *= 10;
		}

		Destroy (this);
	}
}
