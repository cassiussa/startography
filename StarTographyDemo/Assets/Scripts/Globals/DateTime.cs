using UnityEngine;
using System.Collections;

public class DateTime : MonoBehaviour {
	public System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
	public System.DateTime currentDate;
	public int timestamp = 0;
	public System.DateTime dtDateTime;
	public string[] months;

	public SimulationSpeed simulationSpeed;
	double timestampUpdate = 0;

	// Use this for initialization
	void Start () {
		timestamp = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;		// Create the unix timestamp so we can update date/time based on seconds
		//Debug.Log ("timestamp = " + timestamp);
		currentDate = epochStart.AddSeconds( timestamp ).ToLocalTime();				// Convert unix timestamp to date/time format
		//Debug.Log ("dtDateTime = " + months[int.Parse(currentDate.ToString("MM"))-1]+" "+currentDate.ToString("dd, yyyy"));

		simulationSpeed = GetComponent<SimulationSpeed> ();
	}
	
	// Update is called once per frame
	void Update () { // 10 x speed, 0.25 delta = 2.5s in frame
		timestampUpdate += simulationSpeed.simulationSpeed * Time.deltaTime;
		int _timestamp = timestamp + (int)timestampUpdate;
		currentDate = epochStart.AddSeconds( _timestamp ).ToLocalTime();				// Convert unix timestamp to date/time format
		//Debug.Log ("dtDateTime = " + months[int.Parse(currentDate.ToString("MM"))-1]+" "+currentDate.ToString("dd, yyyy"));
	}
	
}
