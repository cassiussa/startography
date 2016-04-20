using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Globals;

/*
 * This script contains a series of functions to allow for processes such as
 * measurement conversions, positional data and conversion, getting angles,
 * and other things
 */
namespace Functions {

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

	// See about changing this to a class so that we using Reference Types instead of Value Types??
	[System.Serializable]
	public class Vector3d : System.Object {
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


		public Vector3d(double xx, double yy, double zz) {
			x = xx;
			y = yy;
			z = zz;
		}

		// Constructor
		public Vector3d() {
			x = 0;
			y = 0;
			z = 0;
		}

		public Vector3d(Vector3d input) {
			x = input.x;
			y = input.y;
			z = input.z;
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

		public static Vector3d Set(Vector3d first, Vector3d second) {
			first.x = second.x;
			first.y = second.y;
			first.z = second.z;
			return first;
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

		public static GameObject MakeSphereMesh(string name, Transform parent, bool hasCollider) {
			GameObject mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);				// Create a sphere primitive
			if(!hasCollider)																// Check if this gameObject should have a collider or not
				Destroy (mesh.collider);													// Remove the collider that is automatically added when we create the primitive
			mesh.name = name;																// Name the gameObject
			mesh.transform.parent = parent;													// Assign the parent of this GameObject
			Mesh meshSphere = (Mesh)Resources.Load("Mesh/Planet-10000-kms",typeof(Mesh));	// Get the pre-made mesh
			mesh.GetComponent<MeshFilter>().mesh = meshSphere;								// Assign the mesh from Resources to the gameObject
			Quaternion newRot = new Quaternion();											// Set up a temporary Quaternion to build the new rotation
			newRot.eulerAngles = new Vector3(-90,0,0);										// Reset the rotation as this was from Blender
			mesh.transform.localRotation = newRot;											// Set the rotation of the star
			return mesh.gameObject;															// Send the gameObject return
		}



		public static GameObject MakeSphereCollider(string name, Transform parent, float radius, bool isTrigger) {
			GameObject go = new GameObject(name);
			go.transform.parent = parent;
			go.AddComponent<SphereCollider>();
			go.collider.isTrigger = isTrigger;
			go.GetComponent<SphereCollider>().radius = radius;
			return go.gameObject;
		}




	}
}
