using UnityEngine;
using System;
using System.Collections;
using Constants;

public class tester : MonoBehaviour {
	
	public Constant sun = new Constant("m_sun","Mass of the Sun",100.05d,"kg",0.0000034d,"From my mind","si");
	// Use this for initialization
	void Start ()
	{
		Debug.Log (sun.Name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
