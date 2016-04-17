using UnityEngine;
using System.Collections;

namespace Globals {

	public class Global : MonoBehaviour {
		public static double PI = 3.14159265358979323846d;
		public static double oneEightyOverPI = 57.29577951308232d;					// 180/PI - so it doesn't need to be calculated
		public static double radiusConstantSolar = 695508d;							// In kilometers
		public static double radiusTemperatureSolar = 5777d;						// The effective temperature of the sun, in Kelvin
		public static double radiusConstantEarth = 6371d;							// In kilometers
		public static double radiusConstantJupiter = 69911d;						// In kilometers
		public static double massConstantSolar = 1988500000000000000000000000000d;	// In kilograms
		public static double massConstantJupiter = 1898130000000000000000000000d;	// In kilograms
		public static double massConstantEarth = 5972190000000000000000000d;		// In Kilograms

		public static double G = 0.00000000006673889d;								// Gravitational Constant - meters, kilograms, seconds
		public static double julianYear = 31557600;//31558196.0153664				// Seconds in a Julian year (365.25 days) or 365.256898326 days

		public static double maxUnits = 10000d;				// The maximum number of distance, in units, that something can go

		// Predefine these as it may cache or keep them in memory instead of assigning on each function call
		public static double SM = 1d;						// SubMillion (the base size)
		public static double MK = 1000000d;					// Million Kilometers
		public static double AU = 149597870.7d;				// Astronomical Units
		public static double LightHour = 1079252848.8d;			// Light Hours
		public static double LightDay = 25902068371.2d;				// Light Days
		public static double LightYear = 9460730472600d;			// Light Years (Julian)
		public static double Parsec = 30856740080213.256d;		// Parsecs
		public static double LD = 94607304725808d;			// Light Decades (Julian)
		public static double LC = 946073047258080d;			// Light Centuries (Julian)
		public static double LM = 9460730472580800d;		// Light Millenia (Julian)
		public static double LDM = 94607304725808000d;		// Light DecaMillenia (Julian) [decamillenia is not a real name]

	}
}

