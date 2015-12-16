using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarTypeData : MonoBehaviour {

	public enum StarClass { Initialize, NotSet, WhiteDwarf, BrownDwarf, MainSequence, Giant, SuperGiant }
	public enum StarType { Initialize, NotSet, D, O, B, A, F, G, K, M }
	
	public StarClass starClass = StarClass.Initialize;
	public StarType starType = StarType.Initialize;
	StarClass _prevStarClass;
	StarType _prevStarType;
	StarClass _cacheStarClass;
	StarType _cacheStarType;
	
	#region Basic Getters/Setters
	public StarClass CurrentStarClass { get { return starClass; } }
	public StarType CurrentStarType { get { return starType; } }
	
	public StarClass PrevStarClass { get { return _prevStarClass; } }
	public StarType PrevStarType { get { return _prevStarType; } }

	public int StarNumeric;
	
	#endregion
	
	void Awake() {
		if(starClass == StarClass.Initialize)
			SetStarClass(StarClass.NotSet);
		if(starType == StarType.Initialize)
			SetStarType(StarType.NotSet);
	}
	
	// NOTE: Async version of Start.
	IEnumerator Start() {
		while (true) {
			if (_cacheStarClass != starClass) {
				switch (starClass) {
				case StarClass.Initialize:
					Debug.Log("Initialize");
					break;
				case StarClass.NotSet:
					Debug.LogError ("The Star Type has not bee set", gameObject);
					break;
				case StarClass.WhiteDwarf:
					break;
				case StarClass.BrownDwarf:
					Debug.Log("BrownDwarf");
					BrownDwarf ();
					break;
				case StarClass.MainSequence:
					//Debug.Log("MainSequence");
					MainSequence ();
					break;
				case StarClass.Giant:
					Debug.Log("Giant");
					Giant ();
					break;
				case StarClass.SuperGiant:
					Debug.Log("SuperGiant");
					SuperGiant ();
					break;
				}
			}
			yield return null;
		}
	}
	
	public void SetStarClass(StarClass newStarClass) { _prevStarClass = starClass; starClass = newStarClass; }
	public void SetStarType(StarType newStarType) { _prevStarType = starType; starType = newStarType; }


	void BrownDwarf() {
		_cacheStarClass = starClass;
	}
	
	void MainSequence() {
		_cacheStarClass = starClass;
	}
	
	void Giant() {
		_cacheStarClass = starClass;
	}
	
	void SuperGiant() {
		_cacheStarClass = starClass;
	}

}
