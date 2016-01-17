using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
	public Transform parentForScale;
	public int horizCirclePoints;
	public float xradius;
	public float yradius;
	LineRenderer line;
	
	void Start ()
	{
		line = gameObject.GetComponent<LineRenderer>();
		
		//line.SetVertexCount (horizCirclePoints + 1);
		//line.useWorldSpace = false;
		// multiply the size of the parent to the scale so that the border lines aren't too thick for smaller measurements
		//line.Start = line.width * parentForScale.scale.x;
		//line.SetWidth(parentForScale.localScale.x*2, parentForScale.localScale.x*2);
		//CreatePoints ();
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
