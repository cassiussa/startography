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
		 * 10000 kilometers into 10 megameters if ToMM() was used
		 * and the original value was in kilometers.  Another example
		 * would be converting 86400 seconds into 1 day if ToDay()
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
			if(distanceArray.Contains(_element.Measurement)) {
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
			} else if(timeArray.Contains(_element.Measurement)) {
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
			}

			_element.Measurement = _measurement;
			return _element;
		}

		/* Distance Measurement Conversions */
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
		
		public const double luminosityOfSun = 3.846e26d;	// watts        Allen's Astrophysical Quantities 4th Ed.
		public const double massOfSun       = 1.9891e30d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfSun     = 6.95508e8d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfJupiter   = 1.8987e27d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfJupiter = 7.1492e7d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfEarth     = 5.9742e24d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfEarth   = 6.378136e6d;	// meters       Allen's Astrophysical Quantities 4th Ed.

		public const double speedOfLight    = 299792458d;   // meters per second


		// Convert from RA and Dec to XYZ cartesian coordinates
		public static Vector3 SphericalToCartesianCoords(
			double distance,
			double rightAscensionHours,
			double rightAscensionMinutes,
			double rightAscensionSeconds,
			double declinationDegrees,
			double declinationMinutes,
			double declinationSeconds) {

			// Verification: http://keisan.casio.com/exec/system/1359534351
			// Converting to decimal (double) values
			double rightAscension = (rightAscensionHours * 15d) + (rightAscensionMinutes * 0.25d) + (rightAscensionSeconds * 0.004166d);
			double declination = (Math.Abs (declinationDegrees) + (declinationMinutes / 60d) + (declinationSeconds / 3600d)) * Math.Sign (declinationDegrees);
			Debug.Log ("In double: " + rightAscension + ", " + declination + ", " + distance);

			// Convert to cartesian values
			double X = distance * Math.Sin(Maths.Deg2Rad * declination) * Math.Cos(Maths.Deg2Rad * rightAscension);
			double Y = distance * Math.Sin(Maths.Deg2Rad * declination) * Math.Sin(Maths.Deg2Rad * rightAscension);
			double Z = distance * Math.Cos(Maths.Deg2Rad * declination);
			Debug.Log ("In double: " + X + ", " + Y + ", " + Z);
			Vector3 coord = new Vector3 ( (float)X, (float)Y, (float)Z );

			return coord;
		}


	}
	
	
}
