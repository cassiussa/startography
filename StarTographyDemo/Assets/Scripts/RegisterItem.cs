using UnityEngine;
using System.Collections;

/*
 * This script will add the current item into the Items.cs script, which we can
 * consider to be a Registry.  This script should be the last Component appended
 * to whatever object it is attached to as we will need to account for any other
 * scripts that have been added to this object during the Awake() or Start()
 * time.
 */

public class RegisterItem : Functions {

	public ObjectData objectDataScript;
	public ScaleStates scaleStatesScript;
	Items itemsScript;

	// Register this item's details into the applicable List item in the Items.cs global script
	void Awake () {
		objectDataScript = GetComponent<ObjectData> ();
		scaleStatesScript = GetComponent<ScaleStates> ();

		// Send required items to the Item script attached to the Global GameObject
		itemsScript = GameObject.Find ("/Globals").GetComponent<Items> ();
		if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Star) {
//			itemsScript.stars.Add (new Star (gameObject.name, objectDataScript, scaleStatesScript));
		} else if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Planet) {
			//itemsScript.planets.Add (new Planet (gameObject.name, objectDataScript, scaleStatesScript, objectDataScript.parentObject, scaleStatesScript.planetOrbitPathTrailScript));
		} else if (objectDataScript.celestialBodyType == ObjectData.CelestialBodyType.Moon) {
			itemsScript.moons.Add (new Moon (gameObject.name, objectDataScript, scaleStatesScript, objectDataScript.parentObject));
		}
	}
}
