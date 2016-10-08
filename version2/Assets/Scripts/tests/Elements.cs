using UnityEngine;
using System;
using System.Collections;
using CustomMath;

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

		// Constructors
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

		public Element(Element element)
		{
			this.Short = element.Short;
			this.Name = element.Name;
			this.Value = element.Value;
			this.Measurement = element.Measurement;
			this.Uncertainty = element.Uncertainty;
			this.Reference = element.Reference;
			this.System = element.System;
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
		double ConvertToMeters(double value, string measurement)
		{
			if(measurement == "meter")
				value *= Maths.meter;
			else if(measurement == "kilometer")
				value *= Maths.kilometer;
			else if(measurement == "megameter")
				value *= Maths.megameter;
			else if(measurement == "gigameter")
				value *= Maths.gigameter;
			else if(measurement == "terameter")
				value *= Maths.terameter;
			else if(measurement == "petameter")
				value *= Maths.petameter;
			else if(measurement == "exameter")
				value *= Maths.exameter;
			else if(measurement == "zetameter")
				value *= Maths.zetameter;
			else if(measurement == "yottameter")
				value *= Maths.yottameter;

			//this.Measurement = "meter";
			return value;
		}
		/*
		 * The following functions will take the current value in
		 * (Star|Planet|Moon).Distance.Value and convert it into the requested
		 * Element.Value.  For example, it would convert 10000 kilometers
		 * into 10 megameters if ToMM() was used and the original value
		 * was in kilometers.
		 * 
		 * SETS A VALUE
		 */

		public virtual void ToM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.meter;
			this.Measurement = "meter";
		}
		public virtual void ToKM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.kilometer;
			this.Measurement = "kilometer";
		}
		public virtual void ToMM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.megameter;
			this.Measurement = "megameter";
		}
		public virtual void ToGM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.gigameter;
			this.Measurement = "gigameter";
		}
		public virtual void ToTM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.terameter;
			this.Measurement = "terameter";
		}
		public virtual void ToPM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.petameter;
			this.Measurement = "petameter";
		}
		public virtual void ToEM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.exameter;
			this.Measurement = "exameter";
		}
		public virtual void ToZM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.zetameter;
			this.Measurement = "zetameter";
		}
		public virtual void ToYM()
		{
			this.Value = ConvertToMeters (this.Value, this.Measurement) / Maths.yottameter;
			this.Measurement = "yottameter";
		}

	}
	

}
