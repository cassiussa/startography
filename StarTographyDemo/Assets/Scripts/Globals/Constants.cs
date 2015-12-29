using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	protected double PI = 3.14159265358979323846d;
	protected double radiusConstantSolar = 695508d;							// In kilometers
	protected double radiusConstantEarth = 6371d;							// In kilometers
	protected double radiusConstantJupiter = 69911d;						// In kilometers
	protected double massConstantSolar = 1988500000000000000000000000000d;	// In kilograms
	protected double massConstantJupiter = 1898130000000000000000000000d;	// In kilograms
	protected double massConstantEarth = 5972190000000000000000000d;		// In Kilograms

	protected double maxUnits = 10000d;				// The maximum number of distance, in units, that something can go

	// Predefine these as it may cache or keep them in memory instead of assigning on each function call
	protected double SM = 1000d;					// SubMillion (the base size)
	protected double MK = 1000000d;					// Million Kilometers
	protected double AU = 149597870.7d;				// Astronomical Units
	protected double LH = 1079252848.8d;			// Light Hours
	protected double Ld = 25902068371d;				// Light Days
	protected double LY = 9460730472600d;			// Light Years (Julian)
	protected double PA = 30856740080213.256d;		// Parsecs
	protected double LD = 94607304725808d;			// Light Decades (Julian)
	protected double LC = 946073047258080d;			// Light Centuries (Julian)
	protected double LM = 9460730472580800d;		// Light Millenia (Julian)
	protected double LDM = 94607304725808000d;		// Light DecaMillenia (Julian) [decamillenia is not a real name]
}
