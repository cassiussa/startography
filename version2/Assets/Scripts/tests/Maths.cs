using UnityEngine;
using System;
using System.Collections;
using Elements;
using BodyElements;
using System.Linq;

namespace CustomMath
{
	
	[System.Serializable] // Show it in the Inspector
	public class Maths
	{
		
		/************************************************************
		 * 
		 * Methods()
		 * 
		 * The following functions will take the current value in
		 * Element.Value and convert it into the requested
		 * Element.Measurement type.  For example, it would convert
		 * 10000 kilometers into 10 megameters if InMM() was used
		 * and the original value was in kilometers.  Another example
		 * would be converting 86400 seconds into 1 day if InDays()
		 * was used and the original value was in seconds;
		 * 
		 * These functions: SET A VALUE
		 * 
		 ************************************************************/

		/*
		 * Conversions
		 */
		public static Element GetMeasurementIn(Element element) {
			Element _element = new Element (element);
			string _measurement = "";

			/* Distance Conversion */
			if (distanceArray.Contains (_element.Measurement)) {
				if (_element.Measurement == "meter") {
					_element.Value *= meter;
					_measurement = "meter";
				} else if (_element.Measurement == "kilometer") {
					_element.Value *= kilometer;
					_measurement = "kilometer";
				} else if (_element.Measurement == "megameter") {
					_element.Value *= megameter;
					_measurement = "megameter";
				} else if (_element.Measurement == "gigameter") {
					_element.Value *= gigameter;
					_measurement = "gigameter";
				} else if (_element.Measurement == "terameter") {
					_element.Value *= terameter;
					_measurement = "terameter";
				} else if (_element.Measurement == "petameter") {
					_element.Value *= petameter;
					_measurement = "petameter";
				} else if (_element.Measurement == "exameter") {
					_element.Value *= exameter;
					_measurement = "exameter";
				} else if (_element.Measurement == "zetameter") {
					_element.Value *= zetameter;
					_measurement = "zetameter";
				} else if (_element.Measurement == "yottameter") {
					_element.Value *= yottameter;
					_measurement = "yottameter";
				}

				/* Time Conversion */
			} else if (timeArray.Contains (_element.Measurement)) {
				if (_element.Measurement == "millisecond") {
					_element.Value *= millisecond;
					_measurement = "millisecond";
				} else if (_element.Measurement == "centisecond") {
					_element.Value *= centisecond;
					_measurement = "centisecond";
				} else if (_element.Measurement == "second") {
					_element.Value *= second;
					_measurement = "second";
				} else if (_element.Measurement == "minute") {
					_element.Value *= minute;
					_measurement = "minute";
				} else if (_element.Measurement == "hour") {
					_element.Value *= hour;
					_measurement = "hour";
				} else if (_element.Measurement == "day") {
					_element.Value *= day;
					_measurement = "day";
				} else if (_element.Measurement == "year") {
					_element.Value *= year;
					_measurement = "year";
				} else if (_element.Measurement == "decade") {
					_element.Value *= decade;
					_measurement = "decade";
				} else if (_element.Measurement == "century") {
					_element.Value *= century;
					_measurement = "century";
				} else if (_element.Measurement == "millennium") {
					_element.Value *= millennium;
					_measurement = "millennium";
				}
			} else if (othersArray.Contains (_element.Measurement)) {
				if (_element.Measurement == "stellarRadius") {
					_element.Value *= radiusOfSun;
					_measurement = "meter";
				} else if (_element.Measurement == "stellarMass") {
					_element.Value *= massOfSun;
					_measurement = "kilogram";
				} else if (_element.Measurement == "luminosityOfSun") {
					_element.Value *= luminosityOfSun;
					_measurement = "luminosityOfSun";
				} else if (_element.Measurement == "jupiterRadius") {
					_element.Value *= radiusOfJupiter;
					_measurement = "meter";
				} else if (_element.Measurement == "jupiterMass") {
					_element.Value *= massOfJupiter;
					_measurement = "kilogram";
				} else if (_element.Measurement == "earthRadius") {
					_element.Value *= radiusOfEarth;
					_measurement = "meter";
				} else if (_element.Measurement == "earthMass") {
					_element.Value *= massOfEarth;
					_measurement = "kilogram";
				}
			}
			_element.Measurement = _measurement;
			return _element;
		}

		/* Distance, Size, & Radius Measurement Conversions */
		public static double InM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / meter;
		}
		public static double InKM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / kilometer;
		}
		public static double InMM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / megameter;
		}
		public static double InGM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / gigameter;
		}
		public static double InTM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / terameter;
		}
		public static double InPM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / petameter;
		}
		public static double InEM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / exameter;
		}
		public static double InZM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / zetameter;
		}
		public static double InYM(Element element) {
			return new Element(GetMeasurementIn(element)).Value / yottameter;
		}

		/* Time Measurement Conversions */
		public static double InMillisecond(Element element) {
			return new Element(GetMeasurementIn(element)).Value / millisecond;
		}
		public static double InCentisecond(Element element) {
			return new Element(GetMeasurementIn(element)).Value / centisecond;
		}
		public static double InSeconds(Element element) {
			return new Element(GetMeasurementIn(element)).Value / second;
		}
		public static double InMinutes(Element element) {
			return new Element(GetMeasurementIn(element)).Value / minute;
		}
		public static double InSidrealMinutes(Element element) {
			return new Element(GetMeasurementIn(element)).Value / siderealMinute;
		}
		public static double InHours(Element element) {
			return new Element(GetMeasurementIn(element)).Value / hour;
		}
		public static double InDays(Element element) {
			return new Element(GetMeasurementIn(element)).Value / day;
		}
		public static double InSidrealDays(Element element) {
			return new Element(GetMeasurementIn(element)).Value / siderealDay;
		}
		public static double InYears(Element element) {
			return new Element(GetMeasurementIn(element)).Value / year;
		}
		public static double InDecade(Element element) {
			return new Element(GetMeasurementIn(element)).Value / decade;
		}
		public static double InCentury(Element element) {
			return new Element(GetMeasurementIn(element)).Value / century;
		}
		public static double InMillennium(Element element) {
			return new Element(GetMeasurementIn(element)).Value / millennium;
		}



		/*
		 * TODO: 
		 * These values need to be taken out of here and put into a more appropriate place
		 * at a later time.
		 */


		// http://www.stellar-database.com/
		// Some definitions on what fields mean
		// http://www.stellar-database.com/fields.html
		
		/*
		 * We statically assign the size of each scale so that we can
		 * quickly access the different scale sizes
		 */
		public static string[] distanceArray = new string[]{"meter", "kilometer", "megameter", "gigameter", "terameter", "petameter", "exameter", "zetameter", "yottameter"};
		public const double meter        = 1d;
		public const double kilometer    = 1000d;
		public const double megameter    = 1000000d;
		public const double gigameter    = 1000000000d;
		public const double terameter    = 1000000000000d;
		public const double petameter    = 1000000000000000d;
		public const double exameter     = 1000000000000000000d;
		public const double zetameter    = 1000000000000000000000d;
		public const double yottameter   = 1000000000000000000000000d;


		/*
		 * TODO: Come back and put in real values for these
		 * and possibly expand on the types available
		 */
		public static string[] timeArray   = new string[]{"millisecond", "centisecond", "second", "minute", "siderealMinute", "hour", "day", "siderealDay", "year", "decade", "century", "millennium"};
		public const double millisecond    = 0.001d;
		public const double centisecond    = 0.01d;
		public const double second         = 1d;
		public const double minute         = 60d;
		public const double siderealMinute = 60d;
		public const double hour           = 3600d;
		public const double day            = 86400d;
		public const double siderealDay    = 86164.090530833d;  // https://en.wikipedia.org/wiki/Sidereal_time: Aoki, S., B. Guinot, G. H. Kaplan, H. Kinoshita, D. D. McCarthy and P. K. Seidelmann: "The new definition of Universal Time". Astronomy and Astrophysics 105(2), 361, 1982, see equation 19.
		public const double year           = 31556925d;
		public const double decade         = 315569250d;
		public const double century        = 3155692500d;
		public const double millennium     = 31556925000d;


		/*
		 * Create the astronomical constants in SI units
		 * https://github.com/astropy/astropy/blob/master/astropy/constants/si.py
		 */
		
		public const double au = 1.49597870700e11d;	// meters, IAU 2012 Resolution B2
		// public const double parsec = // Can be derived from au
		// public const double kiloparsec = // Can be derived from au
		
		public const double pi = 3.14159265358979323846d;   // Pi to a high degree of accuracy
		public const double Deg2Rad = pi/180d;              // Convert from Degrees to Radians, just like Mathf.Deg2Rad
		public const double Rad2Deg = 180d/pi;              // Convert from Radians to Degrees, just like Mathf.Rad2Deg

		public static string[] othersArray = new string[]{"stellarRadius", "stellarMass", "luminosityOfSun", "jupiterRadius", "jupiterMass", "earthRadius", "earthMass"};
		public const double luminosityOfSun = 3.846e26d;	// watts        Allen's Astrophysical Quantities 4th Ed.
		public const double massOfSun       = 1.9891e30d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfSun     = 6.95508e8d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfJupiter   = 1.8987e27d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfJupiter = 7.1492e7d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfEarth     = 5.9742e24d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfEarth   = 6.378136e6d;	// meters       Allen's Astrophysical Quantities 4th Ed.

		public const double speedOfLight    = 299792458d;   // meters per second


		public static double RightAscensionToDegrees(RightAscension rightAscension) {
			return (rightAscension.Hours * 15d) + (rightAscension.Minutes * 0.25d) + (rightAscension.Seconds * 0.004166d);
		}
		public static double DeclinationToDegrees(Declination declination) {
			return (Math.Abs (declination.Degrees) + (declination.DegreeMinutes / 60d) + (declination.DegreeSeconds / 3600d)) * Math.Sign (declination.Degrees);
		}

		// Convert from Right Ascension & Declination to XYZ Cartesian coordinates
		// Verification: http://keisan.casio.com/exec/system/1359534351
		public static Vector3d SphericalToCartesianCoords(double distance, RightAscension rightAscension, Declination declination) {
			// Converting to degree (double) values
			double _rightAscensionDegrees = RightAscensionToDegrees(rightAscension);
			double _declinationDegrees = DeclinationToDegrees(declination);
			// Convert to cartesian values
			double _x = distance * Math.Sin(Maths.Deg2Rad * _declinationDegrees) * Math.Cos(Maths.Deg2Rad * _rightAscensionDegrees);
			double _y = distance * Math.Sin(Maths.Deg2Rad * _declinationDegrees) * Math.Sin(Maths.Deg2Rad * _rightAscensionDegrees);
			double _z = distance * Math.Cos(Maths.Deg2Rad * _declinationDegrees);
			return new Vector3d (_x, _y, _z );
		}


	}
	
	
}
