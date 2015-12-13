using UnityEngine;
using System.Collections;

public class BodyData : MonoBehaviour {
	public Sun year;
	public SecondsToRotate day;
	GUIText text;
	// Use this for initialization
	void Start () {
		text = GetComponent<GUIText> ();
		int days = (int)Mathf.Round((float)year.secToOrbit/(24*60*60));
		int hours = (int)Mathf.Round((float)day.secToRotate / 3600);
		text.text += "\nYear:"+days+"d, Day:"+hours+"h";
	}
	
	// Update is called once per frame
	void Update () {

	}
}
