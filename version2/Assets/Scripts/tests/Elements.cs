using UnityEngine;
using System;
using System.Collections;

namespace Elements
{

	[System.Serializable] // Show it in the Inspector
	public class Element
	{
		/*
		 * Class Specification (Value Type)
		 * Purpose: To output data for a specified predefined astronomical constant
		 * Has Receiver: Yes
		 * Inputs:  A short name, full name, value, value unit type, the uncertainty of the value, a reference for
		 *          where the values come from, and a system type
		 * Outputs: A short name, full name, value, value unit type, the uncertainty of the value, a reference for
		 *          where the values come from, and a system type
		 * Side Effects: none
		 * Error Case 1: values cannot be of the wrong Type
		 * Error Case 2: must not contain any null or empty values
		 */

		// Fields of various types
		public string Short;        // Ex: m_jup <-- mass of Jupiter
		public string Name;         // Ex: Mass of Jupiter
		public double Value;        // Ex: 1.8987e27d (Nullable)
		public string Measurement;  // Ex: kg
		public double Uncertainty;  // Ex: 0.00005e27d (Nullable)
		public string System;       // Ex: si
		public string Reference;    // Ex: Allen's Astrophysical Quantities 4th Edition
		
		/*public Constant()
		{
			ConstantTest ();
		}*/

		// Constructor
		public Element(string Short, string Name, double Value, string Measurement, double Uncertainty, string System, string Reference)
		{
			this.Short = Short;
			this.Name = Name;
			this.Value = Value;
			this.Measurement = Measurement;
			this.Uncertainty = Uncertainty;
			this.Reference = Reference;
			this.System = System;
			ElementTest ();

		}

		// Make sure we don't have any values that are null
		private void ElementTest()
		{
			if (Short == null || Short == "" || Name == null || Name == "" || double.IsNaN ((double)Value) || Measurement == null || Measurement == "" || double.IsNaN ((double)Uncertainty) || System == null || System == "" || Reference == null || Reference == "")
				Debug.LogError ("A value is empty or null in one of the following:... string: " + Short + ", string: " + Name + ", double: " + Value + ", string: " + Measurement + ", double: " + Uncertainty + ", string: " + System + ", string: " + Reference);
		}

		// Outputs the Elements in the object to screen and/or String variable
		public virtual String Elements()
		{
			Debug.Log ("Values for Short: "+Short+", Name: "+Name+", Value: "+Value+", Measurement: "+Measurement+", Uncertainty: "+Uncertainty+", System: "+System+", Reference: "+Reference);
			return "Values for Short: "+Short+", Name: "+Name+", Value: "+Value+", Measurement: "+Measurement+", Uncertainty: "+Uncertainty+", System: "+System+", Reference: "+Reference;
		}





		// Methods
		public void ToM()
		{
			if(this.Measurement == "kilometer")
				this.Value *= kilometer;
			else if(this.Measurement == "megameter")
				this.Value *= megameter;
			else if(this.Measurement == "gigameter")
				this.Value *= gigameter;
			else if(this.Measurement == "terameter")
				this.Value *= terameter;
			else if(this.Measurement == "petameter")
				this.Value *= petameter;
			else if(this.Measurement == "exameter")
				this.Value *= exameter;
			else if(this.Measurement == "zetameter")
				this.Value *= zetameter;
			else if(this.Measurement == "yottameter")
				this.Value *= yottameter;

			this.Measurement = "meter";
		}

		public void ToKM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= kilometer;
			this.Measurement = "kilometer";
		}
		public void ToMM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= megameter;
			this.Measurement = "megameter";
		}
		public void ToGM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= gigameter;
			this.Measurement = "gigameter";
		}
		public void ToTM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= terameter;
			this.Measurement = "terameter";
		}
		public void ToPM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= petameter;
			this.Measurement = "petameter";
		}
		public void ToEM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= exameter;
			this.Measurement = "exameter";
		}
		public void ToZM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= zetameter;
			this.Measurement = "zetameter";
		}
		public void ToYM()
		{
			string Measurement = this.Measurement;
			ToM ();
			this.Value /= yottameter;
			this.Measurement = "yottameter";
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
