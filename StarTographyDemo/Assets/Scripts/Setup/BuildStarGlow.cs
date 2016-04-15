using UnityEngine;
using System.Collections;
using Functions;

public class BuildStarGlow : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		/*
		 * Each Star has a glow to it as well as a specific
		 * colour and luminository.  Here we specify some of this
		 * information
		 */
		GameObject starGlow = new GameObject ("Star Glow");
		starGlow.transform.parent = transform;
		GameObject starGlowMain = new GameObject ("Main Star Glow");
		starGlowMain.transform.parent = starGlow.transform;

		Destroy (this);
	}
}
