using UnityEngine;
using System.Collections;
using Constants;

public class CreateConstants : MonoBehaviour {

	public Constant sun = new Constant("m_sun","Mass of the Sun",6.95508e8d,"kg",0.00026e8d,"Allen's Astrophysical Quantities 4th Edition","si");
	public Constant jupiter = new Constant("m_jup","Mass of Jupiter",1.8987e27d,"kg",0.00005e27d,"Allen's Astrophysical Quantities 4th Edition","si");
	public Constant earth = new Constant("m_ear","Mass of the Earth",5.9742e24d,"kg",0.00005e24d,"Allen's Astrophysical Quantities 4th Edition","si");

	// Use this for initialization
	void Start () {
		Debug.Log ("The " + sun.Name + " is " + sun.Value + sun.Unit+" according to " + sun.Reference);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
