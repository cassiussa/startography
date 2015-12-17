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
