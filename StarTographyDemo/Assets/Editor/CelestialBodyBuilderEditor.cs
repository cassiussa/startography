using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CelestialBodyBuilder)), CanEditMultipleObjects]
public class CelestialBodyBuilderEditor : Editor {

	// create a list of new serialized items.  These can be named anything, but have been named same as variables in ObjectData.cs for simplicity
	public SerializedProperty 
		celestialBodyType,
		bodyName,

		rightAscension,
		declination,
		distance,
		opticalMagnitude,
		temperature,
		stellarMass,
		stellarRadius,
		dateLastUpdate,
		star,
		planet,
		bodies,
		planets,
		moons,

		numPlanetsInSystem,
		orbitalPeriod,
		semiMajorAxis,
		eccentricity,
		inclination,
		planetMass,
		planetRadius,

		numMoonsInSystem,
		moonMass,
		moonRadius,

		coordinates,
		radius,
		luminosity
		;
	
	void OnEnable () {

		// Setup the SerializedProperties
		celestialBodyType = serializedObject.FindProperty("celestialBodyType");	// Assign the celestialBodyType variable into this variable by finding it in the ObjectData.cs script
		bodyName = serializedObject.FindProperty ("bodyName");
		// Star variable serialization
		rightAscension = serializedObject.FindProperty ("rightAscension");
		declination = serializedObject.FindProperty ("declination");
		distance = serializedObject.FindProperty ("distance");
		opticalMagnitude = serializedObject.FindProperty ("opticalMagnitude");
		temperature = serializedObject.FindProperty ("temperature");
		stellarMass = serializedObject.FindProperty ("stellarMass");
		stellarRadius = serializedObject.FindProperty ("stellarRadius");
		dateLastUpdate = serializedObject.FindProperty ("dateLastUpdate");
		star = serializedObject.FindProperty ("star");
		planet = serializedObject.FindProperty ("planet");
		bodies = serializedObject.FindProperty ("bodies");
		planets = serializedObject.FindProperty ("planets");
		moons = serializedObject.FindProperty ("moons");

		// Planet variable serialization
		numPlanetsInSystem = serializedObject.FindProperty ("numPlanetsInSystem");
		orbitalPeriod = serializedObject.FindProperty ("orbitalPeriod");
		semiMajorAxis = serializedObject.FindProperty ("semiMajorAxis");
		eccentricity = serializedObject.FindProperty ("eccentricity");
		inclination = serializedObject.FindProperty ("inclination");
		planetMass = serializedObject.FindProperty ("planetMass");
		planetRadius = serializedObject.FindProperty ("planetRadius");

		// Moon variable serialization
		numMoonsInSystem = serializedObject.FindProperty ("numMoonsInSystem");
		moonMass = serializedObject.FindProperty ("moonMass");
		moonRadius = serializedObject.FindProperty ("moonRadius");

		coordinates = serializedObject.FindProperty ("coordinates");
		radius = serializedObject.FindProperty ("radius");
		luminosity = serializedObject.FindProperty ("luminosity");

	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update ();
		EditorGUILayout.PropertyField(celestialBodyType);
		CelestialBodyBuilder.CelestialBodyType states = (CelestialBodyBuilder.CelestialBodyType)celestialBodyType.enumValueIndex;
		
		switch(states) {
		case CelestialBodyBuilder.CelestialBodyType.Star:
			EditorGUILayout.PropertyField(bodyName, new GUIContent("bodyName"), true);
			EditorGUILayout.PropertyField(rightAscension, new GUIContent("rightAscension"), true);		// True boolean is to allow array
			EditorGUILayout.PropertyField(declination, new GUIContent("declination"), true);
			EditorGUILayout.PropertyField(distance, new GUIContent("distance"), true);
			EditorGUILayout.PropertyField(opticalMagnitude, new GUIContent("opticalMagnitude"), true);
			EditorGUILayout.PropertyField(temperature, new GUIContent("temperature"), true);
			EditorGUILayout.PropertyField(stellarMass, new GUIContent("stellarMass"), true);
			EditorGUILayout.PropertyField(stellarRadius, new GUIContent("stellarRadius"), true);
			EditorGUILayout.PropertyField(dateLastUpdate, new GUIContent("dateLastUpdate"), true);

			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);
			EditorGUILayout.PropertyField(luminosity, new GUIContent("luminosity"), true);

			EditorGUILayout.PropertyField(bodies, new GUIContent("bodies"), true);
			EditorGUILayout.PropertyField(planets, new GUIContent("planets"), true);
			EditorGUILayout.PropertyField(moons, new GUIContent("moons"), true);
			break;

		case CelestialBodyBuilder.CelestialBodyType.Planet:
			EditorGUILayout.PropertyField(bodyName, new GUIContent("bodyName"), true);
			EditorGUILayout.PropertyField(numPlanetsInSystem, new GUIContent("numPlanetsInSystem"), true);
			EditorGUILayout.PropertyField(orbitalPeriod, new GUIContent("orbitalPeriod"), true);
			EditorGUILayout.PropertyField(semiMajorAxis, new GUIContent("semiMajorAxis"), true);
			EditorGUILayout.PropertyField(eccentricity, new GUIContent("eccentricity"), true);
			EditorGUILayout.PropertyField(inclination, new GUIContent("inclination"), true);
			EditorGUILayout.PropertyField(planetMass, new GUIContent("planetMass"), true);
			EditorGUILayout.PropertyField(planetRadius, new GUIContent("planetRadius"), true);

			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);

			EditorGUILayout.PropertyField(star, new GUIContent("star"), true);
			EditorGUILayout.PropertyField(moons, new GUIContent("moons"), true);
			break;

		case CelestialBodyBuilder.CelestialBodyType.Moon:
			EditorGUILayout.PropertyField(bodyName, new GUIContent("bodyName"), true);
			EditorGUILayout.PropertyField(numMoonsInSystem, new GUIContent("numMoonsInSystem"), true);
			EditorGUILayout.PropertyField(orbitalPeriod, new GUIContent("orbitalPeriod"), true);
			EditorGUILayout.PropertyField(semiMajorAxis, new GUIContent("semiMajorAxis"), true);
			EditorGUILayout.PropertyField(eccentricity, new GUIContent("eccentricity"), true);
			EditorGUILayout.PropertyField(inclination, new GUIContent("inclination"), true);
			EditorGUILayout.PropertyField(moonMass, new GUIContent("moonMass"), true);
			EditorGUILayout.PropertyField(moonRadius, new GUIContent("moonRadius"), true);

			EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);
			EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);

			EditorGUILayout.PropertyField(star, new GUIContent("star"), true);
			EditorGUILayout.PropertyField(planet, new GUIContent("planet"), true);

			//EditorGUILayout.PropertyField(coordinates, new GUIContent("coordinates"), true);			// True boolean is to allow array
			//EditorGUILayout.PropertyField(radius, new GUIContent("radius"), true);
			break;
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}
















public class PrimativeSpawner : MonoBehaviour {
	
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
[CustomEditor(typeof(PrimativeSpawner))]
public class PrimativeSpawnerInspector : Editor {
	// Keep track of our current object being  inspected
	public PrimativeSpawner spawner;
	
	// Replace the given inspector gui
	public override void OnInspectorGUI() {
		// Draw the given inspector
		this.DrawDefaultInspector();
		
		// Check that it's been initialized
		if (spawner == null)
			spawner = this.serializedObject.targetObject as PrimativeSpawner;
		
		// Simple Button
		if (GUILayout.Button("Spawn Primative"))
			spawner.CreateChosenPrimative();
	}
}