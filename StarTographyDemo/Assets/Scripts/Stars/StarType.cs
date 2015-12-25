using UnityEngine;
using System.Collections;

public class StarType : MonoBehaviour {

	public StarData starDataScript;		// So that we can calculate the star's class and type to then assign the States


	public enum StarTemperatureSequence { Initialize, SetTemperatureSequence, O, B, A, F, G, K, M, R, N, S }	// R, N, S may not be needed
	// I indicates a supergiant star; II indicates a bright giant; III indicates a giant; IV indicates a subgiant star; V indicates a main sequence star
	public enum StarLuminosityClass { Initialize, SetLuminosityClass, I, II, III, IV, V }
	
	public StarTemperatureSequence starTemperatureSequence = StarTemperatureSequence.Initialize;
	public int starTemperatureSubtype;		// The SubType of the star temperature.  0-9, 9 being hottest within any Temperature Sequence type
	public StarLuminosityClass starLuminosityClass = StarLuminosityClass.Initialize;
	StarTemperatureSequence _prevStarTemperatureSequence;
	StarLuminosityClass _prevLuminosityClass;
	StarTemperatureSequence _cacheStarTemperatureSequence;
	StarLuminosityClass _cacheLuminosityClass;
	
	#region Basic Getters/Setters
	public StarTemperatureSequence CurrentStarTemperatureSequence { get { return starTemperatureSequence; } }
	public StarLuminosityClass CurrentLuminosityClass { get { return starLuminosityClass; } }
	
	public StarTemperatureSequence PrevStarTemperatureSequence { get { return _prevStarTemperatureSequence; } }
	public StarLuminosityClass PrevLuminosityClass { get { return _prevLuminosityClass; } }

	
	#endregion
	
	void Awake() {
		starDataScript = GetComponent<StarData> ();		// Assign the StarData script to the starDataScript variable
		if (!starDataScript)
			Debug.LogError ("There doesn't appear to be a StarData script attached to this star", gameObject);

		SetStarTemperatureSequence(StarTemperatureSequence.SetTemperatureSequence);
		SetStarLuminosityClass(StarLuminosityClass.SetLuminosityClass);

		if (starDataScript.effectiveTemperature > 30000) {
			SetStarTemperatureSequence (StarTemperatureSequence.O);
			starTemperatureSubtype = CalculateTemperatureSubtype (30000, 50000, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature >= 10000) {
			SetStarTemperatureSequence (StarTemperatureSequence.B);
			starTemperatureSubtype = CalculateTemperatureSubtype (10000, 29999, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature >= 7500) {
			SetStarTemperatureSequence (StarTemperatureSequence.A);
			starTemperatureSubtype = CalculateTemperatureSubtype (7500, 9999, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature >= 6000) {
			SetStarTemperatureSequence (StarTemperatureSequence.F);
			starTemperatureSubtype = CalculateTemperatureSubtype (6000, 7499, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature >= 5000) {
			SetStarTemperatureSequence (StarTemperatureSequence.G);
			starTemperatureSubtype = CalculateTemperatureSubtype (5000, 5999, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature >= 3500) { 
			SetStarTemperatureSequence (StarTemperatureSequence.K);
			starTemperatureSubtype = CalculateTemperatureSubtype (3500, 4999, starDataScript.effectiveTemperature);
		} else if (starDataScript.effectiveTemperature < 3500) {
			SetStarTemperatureSequence (StarTemperatureSequence.M);
			starTemperatureSubtype = CalculateTemperatureSubtype (0, 3500, starDataScript.effectiveTemperature);
		}

	}

	// Calculate the value between 0-9 for the star's temperature subtype
	public int CalculateTemperatureSubtype(int min, int max, float value) {
		int range = max-min;
		// The following calculation is assumed.  May need to revise if I find actual calculation for values
		float remainder = Mathf.Round(((range-(value-min))/range)*10);
		return (int)remainder;
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheStarTemperatureSequence != starTemperatureSequence) {
				switch (starTemperatureSequence) {
				case StarTemperatureSequence.Initialize:
					Debug.Log("Initialize");
					break;
				case StarTemperatureSequence.SetTemperatureSequence:
					Debug.Log ("The Star Type has not been set", gameObject);
					break;
				case StarTemperatureSequence.O:
					Debug.Log("O");
					break;
				case StarTemperatureSequence.B:
					Debug.Log("B");
					B ();
					break;
				case StarTemperatureSequence.A:
					Debug.Log("A");
					A ();
					break;
				case StarTemperatureSequence.F:
					Debug.Log("F");
					F ();
					break;
				case StarTemperatureSequence.G:
					Debug.Log("G");
					G ();
					break;
				}
			}
			yield return null;
		}
	}
	
	public void SetStarTemperatureSequence(StarTemperatureSequence newStarTemperatureSequence) { _prevStarTemperatureSequence = starTemperatureSequence; starTemperatureSequence = newStarTemperatureSequence; }
	public void SetStarLuminosityClass(StarLuminosityClass newLuminosityClass) { _prevLuminosityClass = starLuminosityClass; starLuminosityClass = newLuminosityClass; }
	
	
	void B() {
		_cacheStarTemperatureSequence = starTemperatureSequence;
	}
	
	void A() {
		_cacheStarTemperatureSequence = starTemperatureSequence;
	}
	
	void F() {
		_cacheStarTemperatureSequence = starTemperatureSequence;
	}
	
	void G() {
		_cacheStarTemperatureSequence = starTemperatureSequence;
	}
}
