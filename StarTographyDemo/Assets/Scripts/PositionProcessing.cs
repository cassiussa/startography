using UnityEngine;
using System.Collections;

public class PositionProcessing : Positioning {

	public double coordX;
	public double coordY;
	public double coordZ;

	public Vector3d position;// = new Vector3d(0d,0d,0d);

	float random;
	void Awake() {
		position = new Vector3d(coordX,coordY,coordZ);
		Debug.Log ("Position = "+transform.position);
		Debug.Log ("Vector3d = (" + position.x + "," + position.y + "," + position.z + ")");
		random = Random.Range (-20.0F, 20.0F);
	}

	// Update is called once per frame
	void LateUpdate () {
		position = new Vector3d (position.x, position.y, position.z+10d);
		//transform.position = V3dToV3 (position);	// Convert from Vector3d double to native Vector3 float and move the gameObject into position
		//transform.Rotate(Vector3.up * Time.deltaTime*random, Space.World);
	}
}
