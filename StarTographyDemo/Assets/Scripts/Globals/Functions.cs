using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Globals;

/*
 * This script contains a series of functions to allow for processes such as
 * measurement conversions, positional data and conversion, getting angles,
 * and other things
 */
namespace Functions {

	[System.Serializable]
	public struct Vector3d {
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
		
		public Vector3d(Vector3d v3d) {
			x = v3d.x;
			y = v3d.y;
			z = v3d.z;
		}
		
		public static Vector3d operator + (Vector3d first, Vector3d second) {
			return new Vector3d(first.x + second.x, first.y + second.y, first.z + second.z);
		}

		public static Vector3d operator - (Vector3d first, Vector3d second) {
			return new Vector3d(first.x - second.x, first.y - second.y, first.z - second.z);
		}

		// Multiply by a Vector3d
		public static Vector3d operator * (Vector3d first, Vector3d second) {
			return new Vector3d(first.x * second.x, first.y * second.y, first.z * second.z);
		}
		
		// Multiply by a scalar
		public static Vector3d operator * (double scalar, Vector3d vector3d) {
			return new Vector3d(scalar * vector3d.x, scalar * vector3d.y, scalar * vector3d.z);
		}
		
		// Multiply by a scalar
		public static Vector3d operator *(Vector3d vector3d, double scalar) {
			return new Vector3d(scalar * vector3d.x, scalar * vector3d.y, scalar * vector3d.z);
		}

		// Divide by a Vector3d
		public static Vector3d operator / (Vector3d first, Vector3d second) {
			return new Vector3d(first.x / second.x, first.y / second.y, first.z / second.z);
		}
		
		// Divide by a scalar
		public static Vector3d operator / (double scalar, Vector3d vector3d) {
			return new Vector3d(scalar / vector3d.x, scalar / vector3d.y, scalar / vector3d.z);
		}
		
		// Divide by a scalar
		public static Vector3d operator / (Vector3d vector3d, double scalar) {
			return new Vector3d(scalar / vector3d.x, scalar / vector3d.y, scalar / vector3d.z);
		}

		// Distance between two Vector3d variables
		public static double Distance(Vector3d first, Vector3d second) {
			Vector3d difference = new Vector3d(second - first);
			Vector3d squared = new Vector3d (difference * difference);
			double result = System.Math.Sqrt(squared.x+squared.y+squared.z);
			return result;
		}

		// Comparison of two Vector3d variables (this checks their values instead of checking if it's literally the same variable/item
		public static bool operator == (Vector3d first, Vector3d second) {
			return (first.x == second.x && first.y == second.y && first.z == second.z);
		}

		// Comparison of two Vector3d variables (this checks their values instead of checking if it's literally the same variable/item
		public static bool operator !=(Vector3d first, Vector3d second) {
			return (first.x != second.x || first.y != second.y || first.z != second.z);
		}


		// Reset the Vector3d variable to zeros
		public static Vector3d Empty() {
			return new Vector3d(0,0,0);
		}

		// Reset the Vector3d variable to zeros
		public static Vector3d Zero() {
			return new Vector3d(0,0,0);
		}

		// Calculate the length of the Vector3d variable (from 0,0,0)
		public double Length() {
			return System.Math.Sqrt(x*x + y*y + z*z);
		}

		// Convert the value of the Vector3d variable into a regular Vector3
		public Vector3 toV3() {
			Vector3 result = new Vector3( (float)x, (float)y, (float)z );
			return result;
		}

		// Scales the Vector3d it's applied to - ex at 10x0.5: 5, 2.5, 1.25, 0.625, etc
		public Vector3d Scale(double factor) {
			x *= factor;
			y *= factor;
			z *= factor;
			return new Vector3d(x,y,z);
		}

		// Returns the scaled value of the Vector3d - ex at 10x0.5: 5, 5, 5, 5, etc
		public Vector3d Scaled(double factor) {
			return new Vector3d (x*factor, y*factor, z*factor);
		}

		// Normalize the Vector3d
		public void Normalize() {
			double length = this.Length();
			if (length != 0) {
				x /= length;
				y /= length;
				z /= length;
			}
		}

		//Get the dot product of the Vector3d variable
		public static double Dot(Vector3d first, Vector3d second) {
			return first.x * second.x + first.y * second.y + first.z * second.z;
		}

		// Interpolate to..from over amount.  The parameter 'amount' is clamped to the range [0, 1].
		public static Vector3d Lerp(Vector3d first, Vector3d second, double amount) {
			return new Vector3d(first.x * (1.0 - amount) + second.x * amount, first.y * (1.0 - amount) + second.y * amount, first.z * (1.0 - amount) + second.z * amount);
		}

		public static Vector3d Midpoint(Vector3d first, Vector3d second) {
			//Vector3d halved = new Vector3d((first.x * 0.5) + (second.x * 0.5), (first.y * 0.5) + (second.y * 0.5), (first.z * 0.5) + (second.z * 0.5));
			Vector3d halved = new Vector3d((first.x + second.x) / 2, (first.y + second.y) / 2, (first.z + second.z) / 2);
			halved.Normalize();
			return halved;
		}

		// The parameter 'amount' is clamped to the range [0, 1].
		public static Vector3d Slerp(Vector3d first, Vector3d second, double amount) {
			double dot = Dot(first, second);
			while (dot < .98) {
				Vector3d middle = Midpoint(first, second);
				if (amount > 0.5) {
					first = middle;
					amount -= 0.5;
					amount *= 2;
				} else {
					second = middle;
					amount *= 2;
				}
				dot = Dot(first, second);
			}
			
			Vector3d cur = Lerp(first, second, amount);
			cur.Normalize();
			return cur;
		}




	}


		
	public class Function : MonoBehaviour {

		void Start() {
			ConvertDistanceStart();
		}

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
		


		Dictionary<string, double> measurements = new Dictionary<string, double>();							// Set up a new dictionary of all the distances
		void ConvertDistanceStart() {
			measurements.Add ("SM", Global.SM); measurements.Add ("MK", Global.MK); measurements.Add ("AU", Global.AU);			// Add the string as a name we can use in order to get the value of the doubles
			measurements.Add ("LH", Global.LH); measurements.Add ("Ld", Global.Ld); measurements.Add ("LY", Global.LY);
			measurements.Add ("PA", Global.PA); measurements.Add ("LD", Global.LD); measurements.Add ("LC", Global.LC);
			measurements.Add ("LM", Global.LM);
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
			double result = value * (measurements[from]/measurements[to]);									// We have the "from" and "to" values, so calculate them to get the ratio
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
			double result = value * (measurements[to]/measurements[from]) * Global.maxUnits;						// 10,000 is the maximum distance for the camera clip
			return result;																					// The near clipping plane for the camera
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
			double longitude = (ra1 - ra2) * Global.PI / 180d;
			double lattitude = (dec1 - dec2) * Global.PI / 180d;
			// Haversine formula
			double dist = 2.0d*System.Math.Asin( System.Math.Sqrt( System.Math.Pow(System.Math.Sin(lattitude/2.0d), 2d) + System.Math.Cos(dec1*Global.PI/180d)*System.Math.Cos(dec2*Global.PI/180d)*System.Math.Pow(System.Math.Sin(longitude/2.0d),2d) ) );
			
			double result = dist/Global.PI*180d;
			return result;
		}
		
		protected Vector3d V3ToV3d(Vector3 vector) {
			Vector3d result = new Vector3d( vector.x, vector.y, vector.z );
			return result;
		}

		protected Vector3d S3dToV3d(String3d vector) {
			Vector3d result = new Vector3d (double.Parse(vector.x), double.Parse(vector.y), double.Parse(vector.z));
			return result;
		}

		protected String3d V3dToS3d(Vector3d vector) {
			String3d result = new String3d (vector.x.ToString(), vector.y.ToString(), vector.z.ToString());
			return result;
		}

		protected float RadToSunRad(float radius) {							// Takes in a star's radius and retuns the amount in solar portions
			float result = radius / (float)Global.radiusConstantSolar;
			return result;
		}

		protected float TempToSunTemp(float temperature) {					// Takes in a star's temperature and retuns the amount in solar portions
			float result = temperature / (float)Global.radiusTemperatureSolar;
			return result;
		}

		protected float Luminosity(float radius, float temperature) {		// Calculates luminosity in solar terms.  Input is radius compared to Sun, temp compared to Sun
			float result = Mathf.Pow(radius, 2) * Mathf.Pow(temperature, 4);
			return result;
		}

		protected double SolsToKilos(double solarUnits) {					// Calculates the amount of kilograms a star is based on its solar unit measurement
			double result = Global.massConstantSolar * solarUnits;
			return result;
		}

		protected double JulianYearToSeconds(double years) {				// Convert input years into Julian Year (in seconds)
			double result = Global.julianYear * years;
			return result;
		}

		protected double NormalizedDegrees(double degrees) {
			double result = degrees - System.Math.Floor(degrees/360.0)*360.0;		// Convert an angle to wrap within 360 degrees.  Ex: 370 degrees = 10 degrees
			return result;
		}

		// Calculates the number of days since the J2000.0
		protected double DayNumber(int yyyy, int mm, int dd, int hrs, int mins, int sec, float ms) {	// ms = microseconds (7 digits)
			float fullDays = 367 * yyyy - (7 * ( yyyy + (int)((mm + 9)/12) ) )/4 + (275 * mm)/9 + dd - 730530;
			double days = fullDays+((hrs+mins+sec+ms)/24);
			return days;
		}


		/*
		 * Get the Eccentric Anomoly
		 * 
		 * Parameters
		 * ----------
		 * M : Mean Anomaly 
		 * e : Eccentricity 0 < e < 1
		 * 
		 * Returns
		 * -------
		 * double : an eccentric anomoly based on multiple interations
		 * note: This function is used by the TrueAnomaly(), EccentricAnomaly(), and possibley other functions 
		 * 
		 */
		private double Anomaly(double M, double e) {
			double K = Global.PI / 180.0;
			int maxIterations = 10;												// 10 iterations should be enough
			int i = 0;
			
			double E;
			double F;
			M = M / 360.0;														// Get the propotion of full orbit
			
			M = 2.0 * Global.PI * (M - Math.Floor (M));
			if (e < 0.8)														// Shortcut if eccentricity is small
				E = M;
			else
				E = Global.PI;
			
			F = E - e * Math.Sin (M) - M;
			while ((Math.Abs(F) > 0.000001) && (i<maxIterations)) {				// Only iterate while we're outside of the accuracy threshold
				E = E - F / (1.0 - e * Math.Cos (E));
				F = E - e * Math.Sin (E) - M;
				i++;
			}
			if (i == maxIterations)
				Debug.LogError ("Hit the maximum number of iterations.  May want to consider revising this", gameObject);

			return E;
		}

		/*
		 * Get the Heliocentric rectangular coordinates along the orbital plane
		 * 
		 * Parameters
		 * ----------
		 * 
		 * r : radius - heliocentric distance
		 * v : true anomaly
		 * N : Longitude of Ascending Node
		 * w : Angle of ascending node to the perihelion along the orbit
		 * i : inclination
		 * 
		 * Returns
		 * -------
		 * Vector3d : the x,y,z rectangular coordinates of the position in orbit
		 */
		protected Vector3d HelioRectCoords(double r, double v, double N, double w, double i) {
			Vector3d result;
			double vw = Deg2Rad (v + w);
			N = Deg2Rad (N);
			i = Deg2Rad (i);
			double cosN = Math.Cos (N);
			double sinN = Math.Sin (N);
			double sinVW = Math.Sin (vw);
			double cosVW = Math.Cos (vw);
			double cosI = Math.Cos (i);
			double xhelio = r * ( (cosN * cosVW) - (sinN * cosI * sinVW) );
			double yhelio = r * (sinN * cosVW + cosN * sinVW * cosI);
			double zhelio = r * sinVW * Math.Sin (i);
			result = new Vector3d (yhelio, zhelio, xhelio);	// These have purposefully been re-arranged due to Unity3d's strange xyz assignments
			return result;
		}

		protected double Deg2Rad(double angle) {
			return Global.PI * angle / 180.0;
		}

		/*
		 * a : mean distance (usually from star)
		 */
		protected double OrbitDistance(double M, double e, double a) {
			double K = Global.PI/180.0;
			double E = Anomaly (M, e);
			double x = a * (Math.Cos (E) - e);
			double y = a * Math.Sqrt(1 - e*e) * Math.Sin(E);
			double r = Math.Sqrt ( (x*x) + (y*y) );
			return r;
		}

		/*
		 * M : Mean Anomaly
		 * e : Eccentricity 0 < e < 1
		 */
		protected double TrueAnomaly(double M, double e) {
			double K = Global.PI/180.0;
			double E = Anomaly (M, e);
			double v = NormalizedDegrees(Math.Atan2(Math.Sqrt(1.0 - e * e) * Math.Sin(E), Math.Cos(E) - e) / K);
			return v;
		}
		
		/*
		 * M : Mean Anomaly 
		 * e : Eccentricity 0 < e < 1
		 */
		protected double EccentricAnomaly(double M, double e) {
			double K = Global.PI/180.0;
			double E = Anomaly (M, e);

			double result = E / K;
			return result;
			/*
			double E0 = M + (180/PI) * e * Math.Sin(M) * (1 + e * Math.Cos(M));
			Debug.Log ("E0 = " + E0);
			double E1 = 0d;
			for (int i=0; i<10; i++) {
				E1 = E0 - (E0 - (180/PI) * e * Math.Sin(E0) - M) / (1 - e * Math.Cos(E0)) ;
				Debug.Log ("iteartion # "+i);
				Debug.Log ("E0 = "+E0+", E1 = "+E1);
				/*if(Math.Abs(E0 - E1) <= 0.05 ) {
					E0 = E1;
					break;
				} else {
					E0 = E1;
				}

			}
			return E1;
			*/
		}

		protected double AvgOrbitRadius(double m, double t) {
			/*
			 * Get the average orbit radius
			 * 
			 * Parameters
			 * ----------
			 * m(ass) : in kilograms.  If you need in solar units, multiple by 'massConstantSolar' first
			 * t(ime) : in seconds.  If you nead in years, multiple by earth years
			 * 
			 * Returns
			 * -------
			 * double : the average distance from host star in meters
			*/
			double result = System.Math.Pow( ((Global.G * m * (t*t)) / (4d*(Global.PI*Global.PI)) ), 1d/3d );
			return result;
		}
		


		// Make these variables, when public, available in the inspector
		[System.Serializable]
		public class String3d : System.Object {
			/*
			 * Create a new Type.  StringVector3d string.
			 * 
			 * Parameters
			 * ----------
			 * x : x coordinate string
			 * y : y coordinate string
			 * z : z coordinate string
			 * 
			 * Returns
			 * -------
			 * Coordinates : x,y,z - all strings, which should be converted to doubles in code,
			 * such as S3dToV3d(String3d) for example.
			 * Stores a Vector that contains string instead of input field doubles, for higher accuracy
			*/
			public string x;
			public string y;
			public string z;
			
			public String3d(string a, string b, string c) {
				x = a;
				y = b;
				z = c;
			}
			
			// Constructor
			public String3d() { x = "0"; y = "0"; z = "0"; }
		}


		public Vector3 ScalePosDiff(double value, Vector3d position) {
			/*
			 * Calculate the position in real Vector3 space, based on the ScaleState supplied in
			 * the 'value' variable.
			 * 
			 * Parameters
			 * ----------
			 * value : The distance value of the scale State (ex: 100000 for MK)
			 *		- supplied by the PlanetOrbitPathTrail.cs script
			 * firstPosition : A Vector3d value of the real position of any point in space based on the scale (value)
			 * 
			 * Actions
			 * -------
			 * Returns a Vector3 coordinate position of where the point in space would be within the 
			 * supplied Scalestate (value).
			*/
			
			float _x = (float)( (position.x / value) * Global.maxUnits );
			float _y = (float)( (position.y / value) * Global.maxUnits );
			float _z = (float)( (position.z / value) * Global.maxUnits );
			
			Vector3 localizedPosition = new Vector3 (_x, _y, _z);
			return localizedPosition;
		}

		public Vector3 CalculatePosition(double value, Vector3d position, Vector3d camPos) {
			/*
			 * Calculate the ratio of real position to fit within 10k unit limit
			 * 
			 * Parameters
			 * ----------
			 * value : The distance value of the scale State (ex: 100000 for MK)
			 *		- supplied by the ScaleStates.cs script
			 *		- supplied by the PlanetOrbitPathTrail.cs script
			 * position : A Vector3d value of the real position of the object
			 * 
			 * Actions
			 * -------
			 * Assigns the position to the gameObject that the calling ScaleStates.cs script is attached to
			*/

			float _x = (float)(((position.x + camPos.x) / value) * Global.maxUnits);
			float _y = (float)(((position.y + camPos.y) / value) * Global.maxUnits);
			float _z = (float)(((position.z + camPos.z) / value) * Global.maxUnits);

			Vector3 newPosition = new Vector3 (_x, _y, _z);
			return newPosition;
		}

		public static Vector3d camPosition = new Vector3d(0d,0d,0d);

		

		public IEnumerator StarGlowResize(Transform glow, float endSize, float time) {
			/*
			 * Calculate the scale of the Vector3 in a coroutine
			 * 
			 * Parameters
			 * ----------
			 * glow : The transform of the StarGlow gameObject - we will be rescaling this
			 * endSize : The end size scale of the StarGlow transform
			 * time : The amount of time that the scaling transition takes
			 * 
			 * Actions
			 * -------
			 * Assigns the Vector3 scale of the StarGlow gameObject as it trasitions between one ScaleState size and the next
			 */
			float startSize = glow.localScale.x;
			float elapsedTime = 0;
			float currentScale = startSize;
			glow.localScale = new Vector3(startSize, startSize, startSize);

			while (elapsedTime < time) {
				currentScale = Mathf.Lerp (startSize, endSize, (elapsedTime/time));
				glow.localScale = new Vector3(currentScale,currentScale,currentScale);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			glow.localScale = new Vector3(endSize, endSize, endSize);
		}

	}
}
