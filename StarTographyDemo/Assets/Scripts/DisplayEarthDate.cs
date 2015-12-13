using UnityEngine;
using System.Collections;


public class DisplayEarthDate : MonoBehaviour {
	public DateTime dateTime;
	public GUIText dataDisplay;
	string bodyName;
	// Use this for initialization
	void Start () {
		dataDisplay = GetComponent<GUIText>();
		bodyName = guiText.text;
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = bodyName+" | "+dateTime.months [int.Parse (dateTime.currentDate.ToString ("MM")) - 1] + " " + dateTime.currentDate.ToString ("dd, yyyy - hh:mm:ss tt");
		//Debug.Log (dateTime.months [int.Parse (dateTime.currentDate.ToString ("MM")) - 1] + " " + dateTime.currentDate.ToString ("dd, yyyy - hh:mm:ss tt"));
	}
}
