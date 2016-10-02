﻿using UnityEngine;
using System;
using System.Collections;
using Constants;

namespace OrbitElements
{

	[System.Serializable] // Show it in the Inspector
	public class Orbit
	{

		// Fields of various types
		public Constant OrbitalPeriod;        // Length of time it takes to orbit
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
		}
		// Constructor
		public Orbit(
			Constant OrbitalPeriod,
			Constant SemiMajorAxis,
			Constant Eccentricity,
			Constant EccentricAnomaly,
			Constant Inclination,
			Constant Perigee,
			Constant RightAscension,
			Constant MeanAnomaly,
			Constant TrueAnomaly)
		{
			this.OrbitalPeriod = OrbitalPeriod;
			this.SemiMajorAxis = SemiMajorAxis;
			this.Eccentricity = Eccentricity;
			this.EccentricAnomaly = EccentricAnomaly;
			this.Inclination = Inclination;
			this.Perigee = Perigee;
			this.RightAscension = RightAscension;
			this.MeanAnomaly = MeanAnomaly;
			this.TrueAnomaly = TrueAnomaly;
			
		}

	}
}
