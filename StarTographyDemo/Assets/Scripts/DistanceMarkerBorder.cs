using UnityEngine;
using System.Collections;

public class DistanceMarkerBorder : MonoBehaviour
{
	public Transform parentForScale;
	public int horizCirclePoints;
	public float xradius;
	public float yradius;
	LineRenderer line;
	
	void Awake () {
		line = gameObject.GetComponent<LineRenderer>();
	}
	
	
	public void CreatePoints (LineRenderer line) {
		if (gameObject.activeSelf == false) return;
		float x;
		float y;
		float z = 0f;
		
		float angle = 20f;
		
		for (int i=0; i<(horizCirclePoints+1); i++) {
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
			y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
			
			line.SetPosition(i,new Vector3(x,y,z) );
			
			angle += (360f / horizCirclePoints);
		}
	}
}
