using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Elements;

namespace BodyElements
{
	[System.Serializable] // Show it in the Inspector
	public class Details
	{
		public Element Name;
		public Element Mass;
		public Element Radius;
		public Element LastUpdated;
		public Element Temperature;
		public Element OpticalMagnitude;
		public Element RightAscension;
		public Element Declination;
		public List<Element> Moons;
		//public List<Orbit> celestialBodies = new List<Orbit>();

		public Details() { }

		// Constructor
		public Details (
			Element Name,
			Element Mass,
			Element Radius,
			Element LastUpdated,
			Element Temperature,
			Element OpticalMagnitude,
			Element RightAscension,
			Element Declination,
			List<Element> Moons)
		{
			this.Name = Name;
			this.Mass = Mass;
			this.Radius = Radius;
			this.LastUpdated = LastUpdated;
			this.Temperature = Temperature;
			this.OpticalMagnitude = OpticalMagnitude;
			this.RightAscension = RightAscension;
			this.Declination = Declination;
			this.Moons = Moons;
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class Orbit
	{

		// Fields of various types
		public Element OrbitalPeriod;        // Length of time it takes to orbit
		public Element SemiMajorAxis;    	  // Semi-major axis = size
		public Element Eccentricity;         // Eccentricity = shape
		public Element EccentricAnomaly;     // The Eccentric Anomaly
		public Element Inclination;          // inclination = tilt
		public Element Perigee;              // argument of perigee = twist
		public Element RightAscension;       // longitude of the ascending node = pin
		public Element MeanAnomaly;          // mean anomaly = angle now
		public Element TrueAnomaly;          // the True anomaly

		public Orbit() { }

		// Constructor
		public Orbit(
			Element OrbitalPeriod,
			Element SemiMajorAxis,
			Element Eccentricity,
			Element EccentricAnomaly,
			Element Inclination,
			Element Perigee,
			Element RightAscension,
			Element MeanAnomaly,
			Element TrueAnomaly)
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
