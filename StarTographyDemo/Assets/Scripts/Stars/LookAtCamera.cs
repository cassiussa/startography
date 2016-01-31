using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	
	void Update() {
		// Rotate the camera every frame so it keeps looking at the target 
		transform.LookAt(Vector3.zero);
	}

}
