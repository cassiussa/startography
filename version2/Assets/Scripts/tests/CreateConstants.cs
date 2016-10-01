﻿using UnityEngine;
using System.Collections;
using Constants;

public class CreateConstants : MonoBehaviour
{

	/*
	 * Earth Solar System Values
	 */

	// *** SUN ***
	// Mass of the Sun
	Constant massOfSun =
		new Constant("m_sun", "Mass of the Sun", 1.9891e30d, "kilograms",
		             0.00005e30d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// Equatorial radius of the Sun
	Constant radiusOfSun =
		new Constant("r_sun", "Radius of the Sun", 6.95508e8d, "meters",
		             0.00026e8d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// Luminosity of the Sun
	Constant luminosityOfSun =
		new Constant("l_sun", "Luminosity of the Sun", 3.846e26d, "watts",
		             0.0005e26d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// *** JUPITER ***
	// Mass of Jupiter
	Constant massOfJupiter =
		new Constant("m_jup", "Mass of Jupiter", 1.8987e27d, "kilograms",
		             0.00005e27d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// Equatorial radius of Jupiter
	Constant radiusOfJupiter =
		new Constant("r_jup", "Jupiter equatorial radius", 7.1492e7d, "meters",
		             0.00005e7d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// *** EARTH ***
	// Mass of the Earth
	Constant massOfEarth =
		new Constant("m_earth", "Mass of the Earth", 5.9742e24d, "kilograms",
		             0.00005e24d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// Equatorial radius of the Earth
	Constant radiusOfEarth =
		new Constant("r_earth", "Equatorial radius of the Earth", 6.378136e6d, "meters",
		             0.0000005e6d, "si", "Allen's Astrophysical Quantities 4th Edition");

	// Mars orbital period
	Constant orbitalPeriodOfMars =
		new Constant("op_earth", "Orbital period of the Mars", 516.228d, "days",
		             0.0d, "si", "StarTography version 1");

	// Mars semi-major axis
	Constant semiMajorAxisOfMars =
		new Constant("sma_earth", "Semi-Major axis the Mars", 1.54d, "AU (change later)",
		             0.0d, "si", "StarTography version 1");

	// Eccentricity of Mars orbit
	Constant eccentricityOfMars =
		new Constant("e_mars", "Orbital eccentricity of Mars", 0.08d, "e",
		             0.0d, "si", "StarTography version 1");

	// Inclination of Mars orbit
	Constant inclinationOfMars =
		new Constant("i_mars", "Orbital inclination of Mars", 0.1d, "degrees",
		             0.0d, "si", "StarTography version 1");
	

	void Start ()
	{
		Debug.Log ("The " + massOfSun.Name + " is " + massOfSun.Value +" "+ massOfSun.Unit+" according to " + massOfSun.Reference);
		Debug.Log ("The " + radiusOfSun.Name + " is " + radiusOfSun.Value +" "+ radiusOfSun.Unit+" according to " + radiusOfSun.Reference);
		Debug.Log ("The " + luminosityOfSun.Name + " is " + luminosityOfSun.Value +" "+ luminosityOfSun.Unit+" according to " + luminosityOfSun.Reference);
		Debug.Log ("The " + massOfEarth.Name + " is " + massOfEarth.Value +" "+ massOfEarth.Unit+" according to " + massOfEarth.Reference);
		Debug.Log ("The " + orbitalPeriodOfMars.Name + " is " + orbitalPeriodOfMars.Value +" "+ orbitalPeriodOfMars.Unit+" according to " + orbitalPeriodOfMars.Reference);
		Debug.Log ("The " + semiMajorAxisOfMars.Name + " is " + semiMajorAxisOfMars.Value +" "+ semiMajorAxisOfMars.Unit+" according to " + semiMajorAxisOfMars.Reference);
		Debug.Log ("The " + eccentricityOfMars.Name + " is " + eccentricityOfMars.Value +" "+ eccentricityOfMars.Unit+" according to " + eccentricityOfMars.Reference);
		Debug.Log ("The " + inclinationOfMars.Name + " is " + inclinationOfMars.Value +" "+ inclinationOfMars.Unit+" according to " + inclinationOfMars.Reference);
	}
	

	void Update ()
	{
	 
	}
}
