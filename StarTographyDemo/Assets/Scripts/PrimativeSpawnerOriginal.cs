using UnityEngine;
using System.Collections;
using UnityEditor;

public class PrimativeSpawnerOriginal : MonoBehaviour {
	
	// Active primative choice
	public UnityEngine.PrimitiveType primativeChoice;
	public UnityEngine.GameObject someGO;

	
	// Spawn a primative based on the enum choice
	public void CreateChosenPrimative() {
		// Check what's currently active
		// Commented out the original code
		//GameObject newPrimative = GameObject.CreatePrimitive(primativeChoice);
		GameObject newPrimative = new GameObject ("testing");
	}
}

// We can use a custom  inspector  for this
[CustomEditor(typeof(PrimativeSpawnerOriginal))]
public class PrimativeSpawnerInspectorOriginal : Editor {
	// Keep track of our current object being  inspected
	public PrimativeSpawnerOriginal spawner;
	
	// Replace the given inspector gui
	public override void OnInspectorGUI() {
		// Draw the given inspector
		this.DrawDefaultInspector();
		
		// Check that it's been initialized
		if (spawner == null)
			spawner = this.serializedObject.targetObject as PrimativeSpawnerOriginal;
		
		// Simple Button
		if (GUILayout.Button("Spawn Primative"))
			spawner.CreateChosenPrimative();
	}
}