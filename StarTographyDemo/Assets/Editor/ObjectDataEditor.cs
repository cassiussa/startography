using UnityEditor;
using UnityEngine;

/*[CustomEditor(typeof(ObjectData)), CanEditMultipleObjects]
public class ObjectDataEditor : Editor {

	// create a list of new serialized items.  These can be named anything, but have been named same as variables in ObjectData.cs for simplicity
	public SerializedProperty 
		celestialBodyType,
		coordinates,
		radius,
		mass,
		tilt,
		orbitalPeriod,
		parentMass,
		temperature,
		luminosity,
		parentObject,
		solarSystemSphere;
	
	void OnEnable () {
		// Setup the SerializedProperties
		celestialBodyType = serializedObject.FindProperty("celestialBodyType");	// Assign the celestialBodyType variable into this variable by finding it in the ObjectData.cs script
		coordinates = serializedObject.FindProperty ("coordinates");
		radius = serializedObject.FindProperty ("radius");
		mass = serializedObject.FindProperty ("mass");
		tilt = serializedObject.FindProperty ("tilt");
		orbitalPeriod = serializedObject.FindProperty ("orbitalPeriod");
		parentMass = serializedObject.FindProperty ("parentMass");
		temperature = serializedObject.FindProperty ("temperature");   
		luminosity = serializedObject.FindProperty ("luminosity");
		parentObject = serializedObject.FindProperty ("parentObject");
		solarSystemSphere = serializedObject.FindProperty ("solarSystemSphere");
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		EditorGUILayout.PropertyField(celestialBodyType);
		ObjectData.CelestialBodyType states = (ObjectData.CelestialBodyType)celestialBodyType.enumValueIndex;
		
		switch(states) {
		case ObjectData.CelestialBodyType.Planet:
			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);			// True boolean is to allow array
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);
			EditorGUILayout.PropertyField(mass, new GUIContent("mass"));
			EditorGUILayout.PropertyField(tilt, new GUIContent("tilt"));
			EditorGUILayout.PropertyField(orbitalPeriod, new GUIContent("orbitalPeriod"));
			EditorGUILayout.PropertyField(parentMass, new GUIContent("parentMass"));
			EditorGUILayout.PropertyField(parentObject, new GUIContent("parentObject") );
			break;
			
		case ObjectData.CelestialBodyType.Star:            
			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);
			EditorGUILayout.PropertyField(mass, new GUIContent("mass"));
			EditorGUILayout.PropertyField(temperature, new GUIContent("temperature") );
			EditorGUILayout.PropertyField(luminosity, new GUIContent("luminosity") );
			EditorGUILayout.PropertyField(solarSystemSphere, new GUIContent("solarSystemSphere") );
			break;

		case ObjectData.CelestialBodyType.SolarSystemSphere:
			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			EditorGUILayout.PropertyField(parentObject, new GUIContent("parentObject") );
			break;

		case ObjectData.CelestialBodyType.StarLight:
			//EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			//EditorGUILayout.PropertyField(parentObject, new GUIContent("parentObject") );
			break;

		case ObjectData.CelestialBodyType.Moon:
			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);			// True boolean is to allow array
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);
			EditorGUILayout.PropertyField(mass, new GUIContent("mass"));
			EditorGUILayout.PropertyField(tilt, new GUIContent("tilt"));
			EditorGUILayout.PropertyField(orbitalPeriod, new GUIContent("orbitalPeriod"));
			EditorGUILayout.PropertyField(parentMass, new GUIContent("parentMass"));
			EditorGUILayout.PropertyField(parentObject, new GUIContent("parentObject") );
			break;
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}*/