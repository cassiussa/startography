using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

public class StarDataFunctions : MonoBehaviour {

	protected void dmsToDeg(string declination) {
		string[] splitDeclination = declination.Split(new string[] { "d", "m", "s" }, StringSplitOptions.None);
		float decDegrees = float.Parse(splitDeclination[0]) + (float.Parse(splitDeclination[1])/60) + (float.Parse(splitDeclination[2])/360);
		Debug.Log(" "+decDegrees + " degrees Declination");
	}

	protected void hmsToDeg(string rightAscension) {
		string[] splitMessage = rightAscension.Split(new string[] { "h", "m", "s" }, StringSplitOptions.None);
		float raDegrees = 15 * (float.Parse(splitMessage[0]) + (float.Parse(splitMessage[1])/60) + (float.Parse(splitMessage[2])/360));
		Debug.Log(" "+raDegrees+" degrees Right Ascension");
	}

	protected double parsecToLightYear(float parsec) {
		double lightYears = parsec * 3.26156d;
		return lightYears;
	}

	protected double JLYtoMKM(double julianLightYears) {
		double mkm = julianLightYears * 9460730472600d / 1000000;
		return mkm;
	}
}
