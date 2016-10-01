using UnityEngine;
using System;
using System.Collections;
using Constants;

namespace Orbit
{

	[System.Serializable] // Show it in the Inspector
	public class Orbit
	{

		// Fields of various types
		public Constant a;    	  // Semi-major axis = size
		public Constant e;        // Eccentricity = shape
		public Constant i;        // inclination = tilt
		public Constant ω;        // argument of perigee = twist
		public Constant Ω;        // longitude of the ascending node = pin
		public Constant v;        // mean anomaly = angle now

		public Orbit()
		{
			OrbitTest ();
		}
		// Constructor
		public Orbit(Constant a, Constant e, Constant i, Constant ω, Constant Ω, Constant v)
		{
			this.a = a;
			this.e = e;
			this.i = i;
			this.ω = ω;
			this.Ω = Ω;
			this.v = v;
			OrbitTest ();
			
		}
		
		// Make sure we don't have any values that are null
		private void OrbitTest()
		{
			Debug.Log ("figure out how to do value checking of Type Constant");
			//if (double.IsNaN ((double)a) || double.IsNaN ((double)e) || double.IsNaN ((double)i) || double.IsNaN ((double)ω) || double.IsNaN ((double)Ω) || double.IsNaN ((double)v))
			//	Debug.LogError ("A value is empty or null in one of the following:... double: " + a + ", double: " + e + ", double: " + i + ", double: " + ω + ", double: " + Ω + ", double: " + v);
		}
		
		// Outputs the Items in the object to screen and/or String variable
		public virtual String Items()
		{
			Debug.Log ("Values for a: "+a+", e: "+e+", i: "+i+", ω: "+ω+", Ω: "+Ω+", v: "+v);
			return "Values for a: "+a+", e: "+e+", i: "+i+", ω: "+ω+", Ω: "+Ω+", v: "+v;
		}
	}
}
