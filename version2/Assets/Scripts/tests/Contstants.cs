using UnityEngine;
using System.Collections;

public class Contstants : MonoBehaviour {

	/*
	 * We statically assign the size of each scale so that we can
	 * quickly access the different scale sizes
	 */

	public const double meter		= 1d;
	public const double kilometer	= 1000d;
	public const double megameter	= 1000000d;
	public const double gigameter	= 1000000000d;
	public const double terameter	= 1000000000000d;
	public const double petameter	= 1000000000000000d;
	public const double exameter	= 1000000000000000000d;
	public const double zetameter	= 1000000000000000000000d;
	public const double yottameter	= 1000000000000000000000000d;

	/*
	 * Create the astronomical constants in SI units
	 * https://github.com/astropy/astropy/blob/master/astropy/constants/si.py
	 */

	public const double au = 1.49597870700e11d;	// meters, IAU 2012 Resolution B2
	//public const double parsec = // Can be derived from au
	// public const double kiloparsec = // Can be derived from au

	public const double pi = 3.14159265358979323846d;

	public const double luminosityOfSun 	= 3.846e26d;	// watts		Allen's Astrophysical Quantities 4th Ed.
	public const double massOfSun 			= 1.9891e30d;	// kilograms	Allen's Astrophysical Quantities 4th Ed.
	public const double radiusOfSun 		= 6.95508e8d;	// meters		Allen's Astrophysical Quantities 4th Ed.
	public const double massOfJupiter 		= 1.8987e27d;	// kilograms	Allen's Astrophysical Quantities 4th Ed.
	public const double radiusOfJupiter 	= 7.1492e7d;	// meters		Allen's Astrophysical Quantities 4th Ed.
	public const double massOfEarth 		= 5.9742e24d;	// kilograms	Allen's Astrophysical Quantities 4th Ed.
	public const double radiusOfEarth 		= 6.378136e6d;	// meters		Allen's Astrophysical Quantities 4th Ed.
}
