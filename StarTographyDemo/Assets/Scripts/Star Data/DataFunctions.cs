using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

/*
 * This script contains a series of functions to allow for processes such as
 * measurement conversions, positional data and conversion, getting angles,
 * and other things
 */

public class DataFunctions : MonoBehaviour {
	double PI = 3.14159265358979323846d;

	// Predefine these as it may cache or keep them in memory instead of assigning on each function call
	[HideInInspector]
	public double MK = 1000000d;				// Million Kilometers
	[HideInInspector]
	public double AU = 149597870.7d;			// Astronomical Units
	[HideInInspector]
	public double LH = 1079252848.8d;			// Light Hours
	[HideInInspector]
	public double Ld = 25902068371d;			// Light Days
	[HideInInspector]
	public double LY = 9460730472600d;			// Light Years (Julian)
	[HideInInspector]
	public double PA = 30856740080213.256d;		// Parsecs
	[HideInInspector]
	public double LD = 94607304725808d;			// Light Decades (Julian)
	[HideInInspector]
	public double LC = 946073047258080d;		// Light Centuries (Julian)
	[HideInInspector]
	public double LM = 9460730472580800d;		// Light Millenia (Julian)

	protected double dmsToDeg(string declination) {
		/*
		 * Convert degree, arcminute, arcsecond input into degrees
		 * 
		 * Parameters
		 * ----------
		 * d : Degrees - float
		 * m : Arcminutes (0-60) - float
		 * s : Arcseconds (0-60) - float
		 * 
		 * Returns
		 * -------
		 * Angle : float
		 * The corresponding angle in degrees
		*/

		string[] splitDMS = declination.Split (new string[] { "d", "m", "s" }, StringSplitOptions.None);
		float degrees = float.Parse (splitDMS [0]);
		float minutes = float.Parse (splitDMS [1]);
		float seconds = float.Parse (splitDMS [2]);
		double sign = 1.0f;	// The default sign is positive

		if (minutes < 0.0 || minutes >= 60.0)
			Debug.LogError ("Minute (" + minutes + ") field is out of range (0 <= minutes < 60).  Please specify a value between 0 and 60");
		if (seconds < 0.0 || seconds >= 60.0)
			Debug.LogError ("Second (" + seconds + ") field is out of range (0 <= minutes < 60).  Please specify a value between 0 and 60");
		if (degrees < 0.0)
			sign = -1.0d;	// Sign is negative to apply negative degrees

		double result = degrees + sign * (minutes / 60.0d) + sign * (seconds / 3600.0d);
		return result;
	}

	protected double hmsToDeg(string rightAscension) {
		/*
		 * Convert hour, minute, second input into degrees
		 * 
		 * Parameters
		 * ----------
		 * h : Hours (0-24) - float
		 * m : Minutes (time, 0-60) - float
		 * s : Seconds (time, 0-60) - float
		 * 
		 * Returns
		 * -------
		 * Angle : float
		 * The corresponding angle in degrees
		*/

		string[] splitHMS = rightAscension.Split(new string[] { "h", "m", "s" }, StringSplitOptions.None);
		float hour = float.Parse (splitHMS [0]);
		float minutes = float.Parse (splitHMS [1]);
		float seconds = float.Parse (splitHMS [2]);

		if (hour < 0.0 || hour >= 24.0)
			Debug.LogError ("Hour (" + hour + ") field is out of range (0 <= hour < 24).  Please specify a value between 0 and 24.");
		if (minutes < 0.0 || minutes >= 60.0)
			Debug.LogError ("Minute (" + minutes + ") field is out of range (0 <= minutes < 60).  Please specify a value between 0 and 60.");
		if (seconds < 0.0 || seconds >= 60.0)
			Debug.LogError ("Second (" + seconds + ") field is out of range (0 <= minutes < 60).  Please specify a value between 0 and 60.");
		
		double result = hour*15.0d + minutes*0.25d + seconds*(0.25d/60.0d);
		return result;
	}
	

	protected double conDist(double value, string from, string to) {
		/*
		 * Convert Distance : Convert any one type of measurement to another
		 * 
		 * Parameters
		 * ----------
		 * value : the distance of the measurement to convert - double
		 * from : the origin measurement type - string
		 * to : the destination measurement type - string
		 * 
		 * Returns
		 * -------
		 * distance : double
		 * The corresponding distance in the new measurement type
		*/
		double ratio = 0d;
		// Set up an array of all the possible measurement type shortforms
		string[] inputs = new string[] {"MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		// Set up an array of all the possible measurement variables
		double[] measurements = new double[] {MK, AU, LH, Ld, LY, PA, LD, LC, LM };
		// Iterate through each of the types of measurements looking for the one stored in the "from" variable
		for (int i=0; i<measurements.Length; i++) {
			if(inputs[i] == from) {
				// Iterate through each type of measurement again, now looking for the one stored in the "to" variable
				for (int a=0;a<inputs.Length;a++) {
					if(inputs[a] == to) {
						// We have the "from" and "to" values, so calculate them to get the ratio
						ratio = measurements[i]/measurements[a];
						break;
					}
				}
				break;	// We've already iterated to where we need, so break the loop
			}
		}

		double result = value * ratio;
		return result;
	}

	protected double ConCamClip(double value, string from, string to) {
		/*
		 * Convert Camera Clip : Convert the camera's near clip plane when
		 * transitioning from one measurement type to the next
		 * 
		 * Parameters
		 * ----------
		 * value : the distance of the measurement to convert - double
		 * from : the origin measurement type - string
		 * to : the destination measurement type - string
		 * 
		 * Returns
		 * -------
		 * distance : double
		 * The corresponding distance in the new measurement type
		*/
		double ratio = 0d;
		// Set up an array of all the possible measurement type shortforms
		string[] inputs = new string[] {"MK", "AU", "LH", "Ld", "LY", "PA", "LD", "LC", "LM" };
		// Set up an array of all the possible measurement variables
		double[] measurements = new double[] {MK, AU, LH, Ld, LY, PA, LD, LC, LM };
		// Iterate through each of the types of measurements looking for the one stored in the "from" variable
		for (int i=0; i<measurements.Length; i++) {
			if(inputs[i] == from) {
				// Iterate through each type of measurement again, now looking for the one stored in the "to" variable
				for (int a=0;a<inputs.Length;a++) {
					if(inputs[a] == to) {
						// We have the "from" and "to" values, so calculate them to get the ratio
						ratio = measurements[a]/measurements[i];
						break;
					}
				}
				break;	// We've already iterated to where we need, so break the loop
			}
		}
		
		double result = value * ratio * 10000d;		// 10,000 is the maximum distance for the camera clip
		return result;	// The near clipping plane for the camera
	}


	protected double getAngDis(double ra1, double dec1, double ra2, double dec2) {
		/*
		 * Calculate the angular distance between two spacial coordinates
		 * 
		 * Parameters
		 * ----------
		 * ra1 : Right ascension of the first object in degrees.
		 * dec1 : Declination of the first object in degrees
		 * ra2 : Right ascension of the second object in degrees
		 * dec2 : Declination of the second object in degrees
		 * 
		 * Returns
		 * -------
		 * Angle : float
		 * The angular distance in degrees between first and second coordinates
		*/

		double longitude = (ra1 - ra2) * PI / 180d;
		double lattitude = (dec1 - dec2) * PI / 180d;
		// Haversine formula
		double dist = 2.0d*System.Math.Asin( System.Math.Sqrt( System.Math.Pow(System.Math.Sin(lattitude/2.0d), 2d) + System.Math.Cos(dec1*PI/180d)*System.Math.Cos(dec2*PI/180d)*System.Math.Pow(System.Math.Sin(longitude/2.0d),2d) ) );

		double result = dist/PI*180d;
		return result;
	}

	protected Vector3 V3dToV3(Vector3d vector) {
		Vector3 result = new Vector3( (float)vector.x, (float)vector.y, (float)vector.z );
		return result;
	}

	public class Vector3d {
		/*
		 * Create a new Type.  Vector3 double.
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
		*/

		public double x;
		public double y;
		public double z;

		public Vector3d(double xc, double yc, double zc) {
			x = xc;
			y = yc;
			z = zc;
		}

		// Constructor
		public Vector3d() { x = 0; y = 0; z = 0; }
	}
}
