/*
 * The Light components and gameObjects they're attached to
 * are all enabled when they're within Scale Layers 1-4, meaning
 * they only cast light when the camera is within 9.9999~ billion
 * kilometres from the parent Star.
 * 
 * The parent Star is responsible for controlling whether or not
 * these lights are on or off, as well as their positions.
 */

using UnityEngine;
using System.Collections;

public class BuildStarLight : MonoBehaviour {
	GameObject[] lightGameObjects = new GameObject[5];

	void Awake () {
		float lightRange = 2e+07f;												// Scale Layer 1 needs 20,000,000 range to cast light 9.99999~ billion kms in any direction
		for (int i=0; i<4; i++) {												// Iterate 4 times to generate the 4 Light componets on 4 Scale Layers 1-4
			lightGameObjects[i] = new GameObject (gameObject.name+" [LIGHT] Layer "+(i+1));	// Give it an appropriate name
			lightGameObjects[i].transform.parent = GameObject.Find ("/Galaxy/Scale Layers/Scale Layer "+(i+1)).transform;	// Assign the appropriate parent
			lightGameObjects[i].SetActive(false);								// Deactive the gameObject for now so that it doesn't use any cpu
			lightGameObjects[i].layer = 8+i;									// Assign the layer of this Light component's gameObject
			Light lightComponent = lightGameObjects[i].AddComponent<Light>();	// Add the Light component
			lightComponent.range = lightRange;									// Assign the range value of the Light component
			lightRange /= 10;													// Reduce the distance required for this light to be cast by 10-fold
			lightComponent.cullingMask &= 1 << i+8;								// Assign only the visible layers
			lightComponent.shadows = LightShadows.Hard;							// Assign shadows.  TODO: This causes an error, but it's ok for now.  Later I should fix it to show shadows correctly
		}
	}

}
