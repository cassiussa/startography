using UnityEngine;
using System.Collections;

public class GenerateDistanceVisuals : MonoBehaviour {
	public GameObject astronomicalUnit;
	public GameObject lightYear;

	void Start () {
		GameObject interfaceObject = new GameObject();
		interfaceObject.name = "Interface Objects";
		interfaceObject.transform.parent = transform;
		interfaceObject.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);
		interfaceObject.transform.localScale = new Vector3 (1f, 1f, 1f);

		astronomicalUnit = Instantiate(Resources.Load ("Prefabs/1 AU (0.149B km)")) as GameObject;
		astronomicalUnit.transform.parent = interfaceObject.transform;
		astronomicalUnit.transform.localRotation = new Quaternion (0f, 0f, 0f, 0f);
		astronomicalUnit.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
