using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Elements;

namespace BodyElements
{

	[System.Serializable] // Show it in the Inspector
	public class CelestialBody
	{
		public string Name;
		
		// Constructors
		public CelestialBody() { }
		public CelestialBody (
			string Name)
		{
			this.Name = Name;
		}
	}


	[System.Serializable] // Show it in the Inspector
	public class Star : BodyElement
		
	{
		public string RightAscension;
		public string Declination;
		public Element Distance;
		public Element Luminosity;
		public Element Temperature;

		// Constructors
		public Star() { }
		public Star (
			string RightAscension,
			string Declination,
			Element Distance,
			Element Luminosity,
			Element Temperature
			)
		{
			this.RightAscension = RightAscension;
			this.Declination = Declination;
			this.Distance = Distance;
			this.Luminosity = Luminosity;
			this.Temperature = Temperature;
		}
	}




	[System.Serializable] // Show it in the Inspector
	public class BodyElement : CelestialBody
	{
		public Element Mass;
		public Element Radius;
		public string DateLastUpdated;
		
		// Constructors
		public BodyElement() { }
		public BodyElement (
			Element Mass,
			Element Radius,
			string DateLastUpdated)
		{
			this.Mass = Mass;
			this.Radius = Radius;
			this.DateLastUpdated = DateLastUpdated;
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class OrbitElement : BodyElement
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

		// Constructors
		public OrbitElement() { }
		public OrbitElement(
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

	[System.Serializable] // Show it in the Inspector
	public class Planet : OrbitElement
		
	{
		// Constructors
		public Planet() { }
		
	}

	[System.Serializable] // Show it in the Inspector
	public class Moon : OrbitElement
		
	{
		// Constructors
		public Moon() { }
		
	}


}
