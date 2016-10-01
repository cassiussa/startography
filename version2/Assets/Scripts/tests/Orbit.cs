using UnityEngine;
using System;
using System.Collections;
using Constants;

namespace OrbitElements
{

	[System.Serializable] // Show it in the Inspector
	public class Orbit
	{

		// Fields of various types
		public Constant SemiMajorAxis;    	  // Semi-major axis = size
		public Constant Eccentricity;         // Eccentricity = shape
		public Constant EccentricAnomaly;     // The Eccentric Anomaly
		public Constant Inclination;          // inclination = tilt
		public Constant Perigee;              // argument of perigee = twist
		public Constant RightAscension;       // longitude of the ascending node = pin
		public Constant MeanAnomaly;          // mean anomaly = angle now
		public Constant TrueAnomaly;          // the True anomaly

		public Orbit()
		{
			OrbitTest ();
		}
		// Constructor
		public Orbit(Constant SemiMajorAxis, Constant Eccentricity, Constant EccentricAnomaly, Constant Inclination, Constant Perigee, Constant RightAscension, Constant MeanAnomaly, Constant TrueAnomaly)
		{
			this.SemiMajorAxis = SemiMajorAxis;
			this.Eccentricity = Eccentricity;
			this.EccentricAnomaly = EccentricAnomaly;
			this.Inclination = Inclination;
			this.Perigee = Perigee;
			this.RightAscension = RightAscension;
			this.MeanAnomaly = MeanAnomaly;
			this.TrueAnomaly = TrueAnomaly;
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
			Debug.Log ("Values for SemiMajorAxis: "+SemiMajorAxis+", Eccentricity: "+Eccentricity+", Inclination: "+Inclination+", Perigee: "+Perigee+", RightAscension: "+RightAscension+", MeanAnomaly: "+MeanAnomaly);
			return "Values for SemiMajorAxis: "+SemiMajorAxis+", Eccentricity: "+Eccentricity+", Inclination: "+Inclination+", Perigee: "+Perigee+", RightAscension: "+RightAscension+", MeanAnomaly: "+MeanAnomaly;
		}
	}
}
