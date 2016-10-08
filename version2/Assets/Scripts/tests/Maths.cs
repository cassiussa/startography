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
		
		// Methods
		public static Element ConvertToMeters(Element element)
		{
			Element el = new Element (element);

			if(el.Measurement == "meter")
				el.Value *= meter;
			else if(el.Measurement == "kilometer")
				el.Value *= kilometer;
			else if(el.Measurement == "megameter")
				el.Value *= megameter;
			else if(el.Measurement == "gigameter")
				el.Value *= gigameter;
			else if(el.Measurement == "terameter")
				el.Value *= terameter;
			else if(el.Measurement == "petameter")
				el.Value *= petameter;
			else if(el.Measurement == "exameter")
				el.Value *= exameter;
			else if(el.Measurement == "zetameter")
				el.Value *= zetameter;
			else if(el.Measurement == "yottameter")
				el.Value *= yottameter;
			
			el.Measurement = "meter";
			return el;
		}

		/*
		 * The following functions will take the current value in
		 * (Star|Planet|Moon).Distance.Value and return the same
		 * value within the requested measurement type.  For
		 * example, it would take 10000 kilometers and return
		 * 10 megameters if InMM() was used and the value submitted
		 * was in kilometers.
		 * 
		 * GETS A VALUE
		 */
		public static double InM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / meter;
			return value;
		}
		public static double InKM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / kilometer;
			return value;
		}
		public static double InMM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / megameter;
			return value;
		}
		public static double InGM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / gigameter;
			return value;
		}
		public static double InTM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / terameter;
			return value;
		}
		public static double InPM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / petameter;
			return value;
		}
		public static double InEM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / exameter;
			return value;
		}
		public static double InZM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / zetameter;
			return value;
		}
		public static double InYM(Element element) {
			Element el = ConvertToMeters (element);
			double value = el.Value / yottameter;
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
		
		public const double meter       = 1d;
		public const double kilometer   = 1000d;
		public const double megameter   = 1000000d;
		public const double gigameter   = 1000000000d;
		public const double terameter   = 1000000000000d;
		public const double petameter   = 1000000000000000d;
		public const double exameter    = 1000000000000000000d;
		public const double zetameter   = 1000000000000000000000d;
		public const double yottameter  = 1000000000000000000000000d;
		
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
