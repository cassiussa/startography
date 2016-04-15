using UnityEngine;
using System.Collections;

public class BuildGalaxy : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GameObject galaxy = new GameObject ("Galaxy");					// Create the parent Galaxy gameObject
		GameObject scaleStates = new GameObject ("Scale States");		// Create a child of the Galaxy gameObject that we use to contain the scale states
		scaleStates.transform.parent = galaxy.transform;				// Assign the parent/child relationship
		for(int i=1;i<=10;i++) {
			GameObject scale = new GameObject("Scale Layer "+i);				// Create this specific scale's gameObject
			scale.transform.parent = scaleStates.transform;				// Set the parent/child relationship
			scale.gameObject.layer = i+7;								// Assigns this Scale the appropriate layer
		}

		Destroy (this);
	}

}
