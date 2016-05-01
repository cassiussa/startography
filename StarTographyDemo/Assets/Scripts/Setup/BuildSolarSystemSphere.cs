using UnityEngine;
using System.Collections;
using Functions;

public class BuildSolarSystemSphere : MonoBehaviour {
/*
* The solar system sphere is a visual representation of the
* size and position of any given solar system when it is close
* enough that it may be of interest to the user.  It is the 
* spherical blue-lined object that we see when moving around.
*/

	void Awake () {
		GameObject solarSystemSphere = new GameObject ("Solar System Sphere");
		solarSystemSphere.transform.parent = transform;

		for(int solSphere=0;solSphere<4;solSphere++) {
			GameObject thisSolarSystemSphere = Function.MakeSphereMesh("Solar System Sphere Outer", solarSystemSphere.transform, false);
			Material celestialSphereMaterial = Resources.Load("Material/CelestialSphere") as Material;	// Get the CelestialSphere material from the 'Resources' folder
			thisSolarSystemSphere.renderer.material = new Material(celestialSphereMaterial);			// Assign the material to the Material variable
			thisSolarSystemSphere.transform.localScale = new Vector3(10000000000,10000000000,10000000000); // TODO: Fix this later
			if(solSphere == 1 || solSphere == 3) {														// Check if this is the 2nd or 4th sphere so we can reverse its normals
				thisSolarSystemSphere.name = "Solar System Sphere Inner";								// Name the GameObject
				thisSolarSystemSphere.AddComponent<ReverseNormals>();									// Reverse the normals to point inwards
			}
		}

		Destroy (this);
	}
}
