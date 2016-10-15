using UnityEngine;
using System;
using System.Collections;
using CustomMath;
using System.Linq;

namespace Elements
{

	[System.Serializable] // Show it in the Inspector
	public class RightAscension {
		public double Hours;
		public double Minutes;
		public double Seconds;
		
		// Constructors
		public RightAscension(double Hours, double Minutes, double Seconds) {
			this.Hours = Hours;
			this.Minutes = Minutes;
			this.Seconds = Seconds;
		}
		public RightAscension(string Hours, string Minutes, string Seconds) {
			double _hours = 0d;
			double _minutes = 0d;
			double _seconds = 0d;
			this.Hours = _hours;
			this.Minutes = _minutes;
			this.Seconds = _seconds;
		}
		public RightAscension(string HourMinSec) {
			/*
			 * Possible format options
			 * 
			 * '00h42m30s'
			 * '00h42.5m'
			 * '00:42.5'
			 * '00 42 30'
		 	 */
			string[] _words = new string[] { "" };
			double[] _values = new double[4];

			if (HourMinSec.Split (new char[] { 'h', 'm', 's' }).Length == 4) {
				_words = HourMinSec.Split (new char[] { 'h', 'm', 's' });
			} else if(HourMinSec.Split (new char[] { 'h', '.', 'm' }).Length == 4) {
				_words = HourMinSec.Split (new char[] { 'h', '.', 'm' });
				_words[2] = ((Convert.ToDouble(_words[2])/10) * 60).ToString();  // Convert fraction of minute to seconds
			} else if(HourMinSec.Split (new char[] { ':', '.' }).Length == 3) {
				_words = HourMinSec.Split (new char[] { ':', '.' });
				_words[2] = ((Convert.ToDouble(_words[2])/10) * 60).ToString();  // Convert fraction of minute to seconds
			} else if(HourMinSec.Split (new char[] { ' ' }).Length == 3) {
				_words = HourMinSec.Split (new char[] { ' ' });
			}

			for (int i=0; i<_words.Length; i++) {
				if(_words[i] != "")
					_values [i] = Convert.ToDouble (_words [i]);
			}

			this.Hours = _values[0];
			this.Minutes = _values[1];
			this.Seconds = _values[2];
		}
	}
	
	public class Declination {
		public double Degrees;
		public double DegreeMinutes;
		public double DegreeSeconds;
		
		// Constructors
		public Declination(double Degrees, double DegreeMinutes, double DegreeSeconds) {
			this.Degrees = Degrees;
			this.DegreeMinutes = DegreeMinutes;
			this.DegreeSeconds = DegreeSeconds;
		}
		public Declination(string Degrees, string DegreeMinutes, string DegreeSeconds) {
			double _degrees = 0d;
			double _degreeMinutes = 0d;
			double _degreeSeconds = 0d;
			this.Degrees = _degrees;
			this.DegreeMinutes = _degreeMinutes;
			this.DegreeSeconds = _degreeSeconds;
		}
		public Declination(string Deg_DegMin_DegSec) {
			/*
			 * Possible format options
			 * 
			 * '+41d12m00s'
			 * '+41d12m'
			 * '+41:12'
			 * '+41 12 00'
		 	 */
			string[] _words = new string[] { "" };
			double[] _values = new double[3];
			
			if (Deg_DegMin_DegSec.Split (new char[] {'+','d','m','s'} ).Length == 5) {
				_words = Deg_DegMin_DegSec.Split (new char[] {'+','d','m','s'} );
			} else if(Deg_DegMin_DegSec.Split (new char[] {'+','d','m'} ).Length == 4) {
				_words = Deg_DegMin_DegSec.Split (new char[] {'+','d','m'} );
			} else if(Deg_DegMin_DegSec.Split (new char[] {'+',':'} ).Length == 3) {
				_words = Deg_DegMin_DegSec.Split (new char[] {'+',':'} );
			} else if(Deg_DegMin_DegSec.Split (new char[] {'+',' '} ).Length == 4) {
				_words = Deg_DegMin_DegSec.Split (new char[] {'+',' '} );
			}

			int b = 0;
			for (int i=0; i<_words.Length; i++) {
				if(_words[i] != "" && _words[i] != " " && _words[i] != null) {  // Check that it's not an empty value
					_values[b] = Convert.ToDouble (_words [i]);                 // Convert to a double and add to the array
					b++;
				}
			}
			
			this.Degrees = _values[0];
			this.DegreeMinutes = _values[1];
			this.DegreeSeconds = _values[2];
		}
	}

	[System.Serializable] // Show it in the Inspector
	public class Element {
		/*
		 * Class Specification (Value Type)
		 * Purpose: To contain a value and its related data, such as measurement, name, reference, etc
		 * Has Receiver: Yes
		 * Inputs:  1. A name, value, unit measurement type, the uncertainty of the value, a reference for
		 *          where the values come from, a system type, and the date the value was last updated
		 *          2. An Element Type
		 * Outputs: A name, value, unit measurement type, the uncertainty of the value, a reference for
		 *          where the values come from, a system type, and the date the value was last updated
		 * Side Effects: none
		 * Error Case 1: values cannot be of the wrong Type
		 * Error Case 2: must not contain any null or empty values
		 */

		// Fields of various types
		public string Name;         // Ex: Mass of Jupiter
		public double Value;        // Ex: 1.8987e27d
		public string Measurement;  // Ex: kilogram
		public double Uncertainty;  // Ex: 0.00005e27d
		public string System;       // Ex: si
		public string Reference;    // Ex: Allen's Astrophysical Quantities 4th Edition
		public string LastUpdate;	// TODO: Convert to DateTime

		// Constructors
		public Element(string Name, double Value, string Measurement, double Uncertainty, string System, string Reference, string LastUpdate) {
			this.LastUpdate = LastUpdate;
			this.Name = Name;
			this.Value = Value;
			this.Measurement = Measurement;
			this.Uncertainty = Uncertainty;
			this.Reference = Reference;
			this.System = System;
			ElementTest ();

		}

		public Element(Element element) {
			this.Name = element.Name;
			this.Value = element.Value;
			this.Measurement = element.Measurement;
			this.Uncertainty = element.Uncertainty;
			this.Reference = element.Reference;
			this.System = element.System;
			this.LastUpdate = element.LastUpdate;
			ElementTest ();
			
		}

		// Make sure we don't have any values that are null
		private void ElementTest() {
			if (Name == null || Name == "" || double.IsNaN ((double)Value) || Measurement == null || Measurement == "" || double.IsNaN ((double)Uncertainty) || System == null || System == "" || Reference == null || Reference == "" || LastUpdate == null || LastUpdate == "")
				Debug.LogError ("A value is empty or null in one of the following:... string: " + Name + ", double: " + Value + ", string: " + Measurement + ", double: " + Uncertainty + ", string: " + System + ", string: " + Reference + ", string: " + LastUpdate);
		}

		// Outputs the Elements in the object to screen and/or String variable
		public virtual String Elements() {
			Debug.Log ("Values for Name: "+Name+", Value: "+Value+", Measurement: "+Measurement+", Uncertainty: "+Uncertainty+", System: "+System+", Reference: "+Reference+", LastUpdate: "+LastUpdate);
			return "Values for Name: "+Name+", Value: "+Value+", Measurement: "+Measurement+", Uncertainty: "+Uncertainty+", System: "+System+", Reference: "+Reference+", LastUpdate: "+LastUpdate;
		}




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
		double SetMeasurementTo(double value, string measurement) {
			/* Distance Conversion */
			if (Maths.distanceArray.Contains (measurement)) {
				if (measurement == "meter")
					value *= Maths.meter;
				else if (measurement == "kilometer")
					value *= Maths.kilometer;
				else if (measurement == "megameter")
					value *= Maths.megameter;
				else if (measurement == "gigameter")
					value *= Maths.gigameter;
				else if (measurement == "terameter")
					value *= Maths.terameter;
				else if (measurement == "petameter")
					value *= Maths.petameter;
				else if (measurement == "exameter")
					value *= Maths.exameter;
				else if (measurement == "zetameter")
					value *= Maths.zetameter;
				else if (measurement == "yottameter")
					value *= Maths.yottameter;
			/* Time Conversion */
			} else if (Maths.timeArray.Contains (measurement)) {
				if (measurement == "millisecond")
					value *= Maths.millisecond;
				else if (measurement == "centisecond")
					value *= Maths.centisecond;
				else if (measurement == "second")
					value *= Maths.second;
				else if (measurement == "minute")
					value *= Maths.minute;
				else if (measurement == "sidrealMinute")
					value *= Maths.siderealMinute;
				else if (measurement == "hour")
					value *= Maths.hour;
				else if (measurement == "day")
					value *= Maths.day;
				else if (measurement == "sidrealDay")
					value *= Maths.siderealDay;
				else if (measurement == "year")
					value *= Maths.year;
				else if (measurement == "decade")
					value *= Maths.decade;
				else if (measurement == "century")
					value *= Maths.century;
				else if (measurement == "millennium")
					value *= Maths.millennium;
			}
			return value;
		}

		public virtual void ToM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.meter;
			this.Measurement = "meter";
		}
		public virtual void ToKM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.kilometer;
			this.Measurement = "kilometer";
		}
		public virtual void ToMM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.megameter;
			this.Measurement = "megameter";
		}
		public virtual void ToGM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.gigameter;
			this.Measurement = "gigameter";
		}
		public virtual void ToTM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.terameter;
			this.Measurement = "terameter";
		}
		public virtual void ToPM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.petameter;
			this.Measurement = "petameter";
		}
		public virtual void ToEM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.exameter;
			this.Measurement = "exameter";
		}
		public virtual void ToZM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.zetameter;
			this.Measurement = "zetameter";
		}
		public virtual void ToYM() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.yottameter;
			this.Measurement = "yottameter";
		}
		/* Time Conversions */
		public virtual void ToSecond() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.second;
		}
		public virtual void ToMinute() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.minute;
		}
		public virtual void ToSidrealMinute() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.siderealMinute;
		}
		public virtual void ToHour() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.hour;
		}
		public virtual void ToDay() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.day;
		}
		public virtual void ToSidrealDay() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.siderealDay;
		}
		public virtual void ToYear() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.year;
		}
		public virtual void ToDecade() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.decade;
		}
		public virtual void ToCentury() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.century;
		}
		public virtual void ToMillennium() {
			this.Value = SetMeasurementTo (this.Value, this.Measurement) / Maths.millennium;
		}

	}

	/*
	 * Create a new Type.  Vector3d double.
	 * 
	 * Parameters
	 * ----------
	 * x : x coordinate double
	 * y : y coordinate double
	 * z : z coordinate double
	 * 
	 * Returns
	 * -------
	 * Coordinates : x,y,z - all doubles
	 * Stores a Vector that contains doubles instead of floats for higher accuracy
	 * Type: Reference
	 * 
	*/
	[System.Serializable]
	public class Vector3d {
		public double x;
		public double y;
		public double z;

		public Vector3d(double x, double y, double z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		// Constructors

		// Reset the Vector3d reference variable to zeros
		public virtual void Clear() {
			this.x = 0;
			this.y = 0;
			this.z = 0;
		}

		public Vector3d() {
			Clear();
		}
		
		public Vector3d(Vector3d vector3d) {
			this.x = vector3d.x;
			this.y = vector3d.y;
			this.z = vector3d.z;
		}

		// Vector3d Addition
		public static Vector3d operator + (Vector3d first, Vector3d second) {
			return new Vector3d(first.x + second.x, first.y + second.y, first.z + second.z);
		}
		public static Vector3d operator ++ (Vector3d first) {
			return new Vector3d(first.x+1d, first.y+1d, first.z+1d);
		}

		// Vector3d Subtraction
		public static Vector3d operator - (Vector3d first, Vector3d second) {
			return new Vector3d(first.x - second.x, first.y - second.y, first.z - second.z);
		}
		public static Vector3d operator -- (Vector3d first) {
			return new Vector3d(first.x-1d, first.y-1d, first.z-1d);
		}
		
		// Vector3d multiplication
		public static Vector3d operator * (Vector3d first, Vector3d second) {
			return new Vector3d(first.x * second.x, first.y * second.y, first.z * second.z);
		}
		
		// Scalar multiplication
		public static Vector3d operator * (double scalar, Vector3d vector3d) {
			return new Vector3d(scalar * vector3d.x, scalar * vector3d.y, scalar * vector3d.z);
		}
		public static Vector3d operator * (Vector3d vector3d, double scalar) {
			return new Vector3d(scalar * vector3d.x, scalar * vector3d.y, scalar * vector3d.z);
		}
		
		// Vector3d division
		public static Vector3d operator / (Vector3d first, Vector3d second) {
			return new Vector3d(first.x / second.x, first.y / second.y, first.z / second.z);
		}
		
		// Scalar division
		public static Vector3d operator / (double scalar, Vector3d vector3d) {
			return new Vector3d(scalar / vector3d.x, scalar / vector3d.y, scalar / vector3d.z);
		}
		public static Vector3d operator / (Vector3d vector3d, double scalar) {
			return new Vector3d(vector3d.x / scalar, vector3d.y / scalar, vector3d.z / scalar);
		}
		
		// Distance between two Vector3d variables
		public static double Distance(Vector3d first, Vector3d second) {
			Vector3d difference = new Vector3d(second - first);
			Vector3d squared = new Vector3d (difference * difference);
			return (double)System.Math.Sqrt(squared.x+squared.y+squared.z);
		}

		// Comparison of two Vector3d variables. Checks their values instead of checking if it's the same reference
		public static bool operator == (Vector3d first, Vector3d second) {
			return (first.x == second.x && first.y == second.y && first.z == second.z);
		}
		
		// Comparison of two Vector3d variables.  Checks their values instead of checking if it's the same reference
		public static bool operator !=(Vector3d first, Vector3d second) {
			return (first.x != second.x || first.y != second.y || first.z != second.z);
		}

		// Calculate the length of the Vector3d variable (from 0,0,0)
		public double Length() {
			return System.Math.Sqrt(this.x*this.x + this.y*this.y + this.z*this.z);
		}
		
		// Allow direct casting of Type.  Example vector3(Vector3)vector3d
		public static implicit operator Vector3(Vector3d convert) {
			return new Vector3( (float)convert.x, (float)convert.y, (float)convert.z );
		}
		
		// Scales the Vector3d reference
		public void ScaledRef(double factor) {
			this.x *= factor;
			this.y *= factor;
			this.z *= factor;
		}
		
		// Returns the scaled variable value of the Vector3d as a new Vector3d
		public static Vector3d ScaledVal(double factor) {
			Vector3d _vector3d = new Vector3d (factor, factor, factor);
			return new Vector3d (_vector3d.x*factor, _vector3d.y*factor, _vector3d.z*factor);
		}
		
		// Interpolate to..from over amount.  The parameter 'amount' is clamped to the range [0, 1].
		// Ex: if amount is 0.5 then we get the Vector3d position that's half-way between first and second
		public static Vector3d Lerp(Vector3d first, Vector3d second, double amount) {
			return new Vector3d(first.x * (1.0 - amount) + second.x * amount,
			                    first.y * (1.0 - amount) + second.y * amount,
			                    first.z * (1.0 - amount) + second.z * amount);
		}
		
	}
	

}
