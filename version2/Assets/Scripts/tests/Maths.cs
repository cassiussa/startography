using UnityEngine;
using System;
using System.Collections;
using Elements;
using BodyElements;

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
		 * Distance Conversions
		 */
		public static Element ConvertToMeters(Element element) {
			Element _element = new Element (element);

			if(_element.Measurement == "meter")
				_element.Value *= meter;
			else if(_element.Measurement == "kilometer")
				_element.Value *= kilometer;
			else if(_element.Measurement == "megameter")
				_element.Value *= megameter;
			else if(_element.Measurement == "gigameter")
				_element.Value *= gigameter;
			else if(_element.Measurement == "terameter")
				_element.Value *= terameter;
			else if(_element.Measurement == "petameter")
				_element.Value *= petameter;
			else if(_element.Measurement == "exameter")
				_element.Value *= exameter;
			else if(_element.Measurement == "zetameter")
				_element.Value *= zetameter;
			else if(_element.Measurement == "yottameter")
				_element.Value *= yottameter;
			
			_element.Measurement = "meter";
			return _element;
		}

		public static double InM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / meter;
			return value;
		}
		public static double InKM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / kilometer;
			return value;
		}
		public static double InMM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / megameter;
			return value;
		}
		public static double InGM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / gigameter;
			return value;
		}
		public static double InTM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / terameter;
			return value;
		}
		public static double InPM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / petameter;
			return value;
		}
		public static double InEM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / exameter;
			return value;
		}
		public static double InZM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / zetameter;
			return value;
		}
		public static double InYM(Element element) {
			Element _element = ConvertToMeters (element);
			double value = _element.Value / yottameter;
			return value;
		}


		public static Element ConvertToSeconds(Element element) {
			Element _element = new Element (element);
			if(_element.Measurement == "second")
				_element.Value *= second;
			else if(_element.Measurement == "minute")
				_element.Value *= minute;
			else if(_element.Measurement == "hour")
				_element.Value *= hour;
			else if(_element.Measurement == "day")
				_element.Value *= day;
			else if(_element.Measurement == "year")
				_element.Value *= year;
			else if(_element.Measurement == "decade")
				_element.Value *= decade;
			else if(_element.Measurement == "century")
				_element.Value *= century;
			else if(_element.Measurement == "millennium")
				_element.Value *= millennium;
			_element.Measurement = "second";
			return _element;
		}

		public static double InSeconds(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / second;
			return value;
		}

		public static double InMinutes(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / minute;
			return value;
		}

		public static double InHours(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / hour;
			return value;
		}

		public static double InDays(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / day;
			return value;
		}

		public static double InYears(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / year;
			return value;
		}

		public static double InDecade(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / decade;
			return value;
		}

		public static double InCentury(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / century;
			return value;
		}

		public static double InMillennium(Element element) {
			Element _element = ConvertToSeconds (element);
			double value = _element.Value / millennium;
			return value;
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
		public const double second       = 1d;
		public const double minute       = 60d;
		public const double hour         = 3600d;
		public const double day          = 86400d;
		public const double siDay        = 86164.09164d;
		public const double ephemerisDay = 0d;
		public const double year         = 31556925d;
		public const double decade       = 315569250d;
		public const double century      = 3155692500d;
		public const double millennium   = 31556925000d;
		
		/*
		 * Create the astronomical constants in SI units
		 * https://github.com/astropy/astropy/blob/master/astropy/constants/si.py
		 */
		
		public const double au = 1.49597870700e11d;	// meters, IAU 2012 Resolution B2
		// public const double parsec = // Can be derived from au
		// public const double kiloparsec = // Can be derived from au
		
		public const double pi = 3.14159265358979323846d;
		
		public const double luminosityOfSun = 3.846e26d;	// watts        Allen's Astrophysical Quantities 4th Ed.
		public const double massOfSun       = 1.9891e30d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfSun     = 6.95508e8d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfJupiter   = 1.8987e27d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfJupiter = 7.1492e7d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		public const double massOfEarth     = 5.9742e24d;	// kilograms    Allen's Astrophysical Quantities 4th Ed.
		public const double radiusOfEarth   = 6.378136e6d;	// meters       Allen's Astrophysical Quantities 4th Ed.
		
	}
	
	
}
