using UnityEngine;
using System.Collections;

public class SolarSystemCollider : MonoBehaviour {
	SolarSystemStates solarSystemState;

	void Start() {
		solarSystemState = GetComponent<SolarSystemStates>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			solarSystemState.SetState(SolarSystemStates.State.FadeIn);
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "MainCamera") {
			solarSystemState.SetState(SolarSystemStates.State.FadeOut);
		}
	}
}
