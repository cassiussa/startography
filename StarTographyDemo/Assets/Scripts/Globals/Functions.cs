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








	}
}
