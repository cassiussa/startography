using UnityEngine;
using System.Collections;

namespace Constants
{

	[System.Serializable]	// Show it in the Inspector
	public class Constant
	{
		/*
		 * Class Specification (Value Type)
		 * Purpose: To output data for a specified predefined astronomical constant
		 * Has Receiver: Yes
		 * Inputs:	A short name, full name, value, value unit type, the uncertainty of the value, a reference for
		 * 			where the values come from, and a system type
		 * Outputs: A short name, full name, value, value unit type, the uncertainty of the value, a reference for
		 * 			where the values come from, and a system type
		 * Side Effects: none
		 * Error Case 1: values cannot be of the wrong Type
		 * Error Case 2: must not contain any null or empty values
		 */

		// Fields of various types
		public string ShortName;	// Ex: m_jup <-- mass of Jupiter
		public string Name;			// Ex: Mass of Jupiter
		public string Unit;			// Ex: kg
		public string Reference;	// Ex: Allen's Astrophysical Quantities 4th Edition
		public string System;		// Ex: si
		public double Value;		// Ex: 1.8987e27d (Nullable)
		public double Uncertainty;	// Ex: 0.00005e27d (Nullable)

		public Constant() {
			ConstantTest ();
		}
		// Constructor
		public Constant(string ShortName, string Name, double Value, string Unit, double Uncertainty, string Reference, string System)
		{
			this.ShortName = ShortName;
			this.Name = Name;
			this.Value = Value;
			this.Unit = Unit;
			this.Uncertainty = Uncertainty;
			this.Reference = Reference;
			this.System = System;
			ConstantTest ();

		}

		private void ConstantTest()
		{
			/*
			 * Debugging the Constant Type
			 */
			
			// Make sure we don't have any values that are null
			if (ShortName == null
				|| ShortName == "" 
				|| Name == null
				|| Name == "" 
				|| double.IsNaN ((double)Value)
				|| Unit == null
				|| Unit == "" 
				|| double.IsNaN ((double)Uncertainty)
				|| Reference == null
				|| Reference == "" 
				|| System == null
				|| System == "")
				Debug.LogError ("A value is empty or null in one of the following:... string: " + ShortName + ", string: " + Name + ", double: " + Value + ", string: " + Unit + ", double: " + Uncertainty + ", string: " + Reference + ", string: " + System);
		}

		/*
		 * TODO: 
		 * These values need to be taen out of here and put into a more appropriate place
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
