using UnityEngine;
using System.Collections;
using Globals;

public class BuildGalaxy : MonoBehaviour {

	/*
	 * The scale states are based on 1 Unity3D Unit per 1,000kms.
	 * So the 10,000 maximum distance that can be traveled within
	 * 3D space before a layer change represents 10,000,000km.
	 */
	void Awake () {
		GameObject galaxy = new GameObject ("Galaxy");					// Create the parent Galaxy gameObject
		GameObject scaleStates = new GameObject ("Scale Layers");		// Create a child of the Galaxy gameObject that we use to contain the scale states
		scaleStates.transform.parent = galaxy.transform;				// Assign the parent/child relationship
		for(int i=1;i<=Global.layerCount-7;i++) {
			GameObject scale = new GameObject("Scale Layer "+i);		// Create this specific scale's gameObject
			scale.transform.parent = scaleStates.transform;				// Set the parent/child relationship
			scale.gameObject.layer = i+7;								// Assigns this Scale the appropriate layer
		}

		//Destroy (this);
	}

}
