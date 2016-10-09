using UnityEngine;
using System;
using System.Collections;
using CustomMath;
using System.Linq;

namespace Elements
{

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
		public double Value;        // Ex: 1.8987e27d (Nullable)
		public string Measurement;  // Ex: kilogram
		public double Uncertainty;  // Ex: 0.00005e27d (Nullable)
		public string System;       // Ex: si
		public string Reference;    // Ex: Allen's Astrophysical Quantities 4th Edition
		public string LastUpdate;	// TODO: Convert to DateTime
		
		/*public Constant()
		{
			ConstantTest ();
		}*/

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
	

}
