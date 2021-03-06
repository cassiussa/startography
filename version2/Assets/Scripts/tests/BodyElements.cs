﻿using UnityEngine;
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
		public string DateLastUpdated;
		public Element Mass;
		public Element Radius;
		
		// Constructors
		public CelestialBody() { }
		public CelestialBody (string Name,string DateLastUpdated,Element Mass,Element Radius)
		{
			this.Name = Name;
			this.DateLastUpdated = DateLastUpdated;
			this.Mass = Mass;
			this.Radius = Radius;
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class OrbitElement : CelestialBody
	{

		// Fields of various types
		// Classic orbital elements
		public Element SemiMajorAxis;        // Semi-major axis = size
		public Element Eccentricity;         // Eccentricity = shape
		public Element Inclination;          // inclination = tilt
		public Element ArgumentOfPariapsis;  // argument of pariapsis = twist
		public Element Longitude;            // longitude of the ascending node = pin
		public Element MeanAnomaly;          // mean anomaly = angle now

		public Element Period;               // Length of time it takes to orbit
		public Element EccentricAnomaly;     // The Eccentric Anomaly
		public Element TrueAnomaly;          // the True anomaly

		// Constructors
		public OrbitElement() { }
		public OrbitElement(Element SemiMajorAxis,Element Eccentricity,Element Inclination,Element ArgumentOfPariapsis,Element Longitude,Element MeanAnomaly,Element Period,Element EccentricAnomaly,Element TrueAnomaly)
		{
			this.SemiMajorAxis = SemiMajorAxis;
			this.Eccentricity = Eccentricity;
			this.Inclination = Inclination;
			this.ArgumentOfPariapsis = ArgumentOfPariapsis;
			this.Longitude = Longitude;
			this.MeanAnomaly = MeanAnomaly;
			this.Period = Period;
			this.EccentricAnomaly = EccentricAnomaly;
			this.TrueAnomaly = TrueAnomaly;	
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class Star : CelestialBody
		
	{
		public string RightAscension;
		public string Declination;
		public Element Distance;
		public Element Luminosity;
		public Element Temperature;
		public List<GameObject> ChildPlanets = new List<GameObject>();
		
		// Constructors
		public Star() { }
		public Star (string RightAscension,string Declination,Element Distance,Element Luminosity,Element Temperature,List<GameObject> ChildPlanets)
		{
			this.RightAscension = RightAscension;
			this.Declination = Declination;
			this.Distance = Distance;
			this.Luminosity = Luminosity;
			this.Temperature = Temperature;
			this.ChildPlanets = ChildPlanets;
		}
	}
	
	[System.Serializable] // Show it in the Inspector
	public class Planet : OrbitElement
	{
		public GameObject ParentStar;
		public List<GameObject> ChildMoons = new List<GameObject>();

		// Constructors
		public Planet() { }
		public Planet(GameObject ParentStar) {
			this.ParentStar = ParentStar;
		}
		public Planet(GameObject ParentStar, List<GameObject> ChildMoons) {
			this.ParentStar = ParentStar;
			this.ChildMoons = ChildMoons;
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class Moon : OrbitElement
	{
		public GameObject ParentPlanet;
		
		// Constructors
		public Moon() { }
		public Moon(GameObject ParentPlanet)
		{
			this.ParentPlanet = ParentPlanet;
		}
	}
	
}
