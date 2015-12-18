using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

public class StarDataFunctions : MonoBehaviour {
	double PI = 3.14159265358979323846d;

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


	// Predefine these as it may cache or keep them in memory instead of assigning on each function call
	double MK = 1000000d;
	double AU = 149597870.7d;
	double LH = 1079252848.8d;
	double Ld = 25902068371d;
	double LY = 9460730472600d;
	double PA = 30856740080213.256d;
	double LD = 94607304725808d;
	double LC = 946073047258080d;
	double LM = 9460730472580800d;

	protected double conDis(double value, string from, string to) {
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
					}
				}
			}
		}
		/*
		if (from == "MK") {
			if(to == "AU") ratio = MK/AU;
			else if(to == "LH") ratio = MK/LH;
			else if(to == "Ld") ratio = MK/Ld;
			else if(to == "LY") ratio = MK/LY;
			else if(to == "PA") ratio = MK/PA;
			else if(to == "LD") ratio = MK/LD;
			else if(to == "LC") ratio = MK/LC;
			else if(to == "LM") ratio = MK/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "AU") {
			if(to == "MK") ratio = MK/MK;
			else if(to == "LH") ratio = AU/LH;
			else if(to == "Ld") ratio = AU/Ld;
			else if(to == "LY") ratio = AU/LY;
			else if(to == "PA") ratio = AU/PA;
			else if(to == "LD") ratio = AU/LD;
			else if(to == "LC") ratio = AU/LC;
			else if(to == "LM") ratio = AU/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "LH") {
			if(to == "MK") ratio = LH/MK;
			else if(to == "AU") ratio = LH/AU;
			else if(to == "Ld") ratio = LH/Ld;
			else if(to == "LY") ratio = LH/LY;
			else if(to == "PA") ratio = LH/PA;
			else if(to == "LD") ratio = LH/LD;
			else if(to == "LC") ratio = LH/LC;
			else if(to == "LM") ratio = LH/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "Ld") {
			if(to == "MK") ratio = Ld/MK;
			else if(to == "AU") ratio = Ld/AU;
			else if(to == "LH") ratio = Ld/LH;
			else if(to == "LY") ratio = Ld/LY;
			else if(to == "PA") ratio = Ld/PA;
			else if(to == "LD") ratio = Ld/LD;
			else if(to == "LC") ratio = Ld/LC;
			else if(to == "LM") ratio = Ld/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "LY") {
			if(to == "MK") ratio = LY/MK;
			else if(to == "AU") ratio = LY/AU;
			else if(to == "LH") ratio = LY/LH;
			else if(to == "Ld") ratio = LY/Ld;
			else if(to == "LD") ratio = LY/LD;
			else if(to == "PA") ratio = LY/PA;
			else if(to == "LC") ratio = LY/LC;
			else if(to == "LM") ratio = LY/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "PA") {
			if(to == "MK") ratio = PA/MK;
			else if(to == "AU") ratio = PA/AU;
			else if(to == "LH") ratio = PA/LH;
			else if(to == "LY") ratio = PA/LY;
			else if(to == "Ld") ratio = PA/Ld;
			else if(to == "LD") ratio = PA/LD;
			else if(to == "LC") ratio = PA/LC;
			else if(to == "LM") ratio = PA/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "LD") {
			if(to == "MK") ratio = LD/MK;
			else if(to == "AU") ratio = LD/AU;
			else if(to == "LH") ratio = LD/LH;
			else if(to == "Ld") ratio = LD/Ld;
			else if(to == "LY") ratio = LD/LY;
			else if(to == "PA") ratio = LD/PA;
			else if(to == "LC") ratio = LD/LC;
			else if(to == "LM") ratio = LD/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "LC") {
			if(to == "MK") ratio = LC/MK;
			else if(to == "AU") ratio = LC/AU;
			else if(to == "LH") ratio = LC/LH;
			else if(to == "Ld") ratio = LC/Ld;
			else if(to == "LY") ratio = LC/LY;
			else if(to == "PA") ratio = LC/PA;
			else if(to == "LD") ratio = LC/LD;
			else if(to == "LM") ratio = LC/LM;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		} else if (from == "LM") {
			if(to == "MK") ratio = LM/MK;
			else if(to == "AU") ratio = LM/AU;
			else if(to == "LH") ratio = LM/LH;
			else if(to == "Ld") ratio = LM/Ld;
			else if(to == "LY") ratio = LM/LY;
			else if(to == "PA") ratio = LM/PA;
			else if(to == "LD") ratio = LM/LD;
			else if(to == "LC") ratio = LM/LC;
			else Debug.LogError("You have passed an invalid 'to' string in your conDis function call");
		}
		*/
		double result = value * ratio;
		return result;
	}

	protected double parsecToLightYear(float parsec) {
		double lightYears = parsec * 3.26156d;
		return lightYears;
	}

	protected double jlyToMkm(double julianLightYears) {
		double result = julianLightYears * 9460730472600d / 1000000;
		return result;
	}

	protected double jlyToKms(double julianLightYears) {
		double result = julianLightYears * 9460730472600d;
		return result;
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
}
