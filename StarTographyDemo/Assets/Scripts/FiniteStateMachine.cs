using UnityEngine;
using System.Collections;

public class FiniteStateMachine : MonoBehaviour {
	private delegate void FState ();				// Create the delegate
	private FState stateMethod;						// Create a holder for the delegate
	
	#region UnityEngine methods
	void Start () {									/// Sets up the controller and puts the object into the new mode.
		stateMethod = new FState (EnterStateA);
	}
	
	void FixedUpdate () {							/// Executes in every frame once. Only calls the current state method.
		stateMethod ();
	}
	#endregion
	
	#region State A
	private void EnterStateA () {					/// Sets up State A.
		// Things to prepare State A come here.
		stateMethod = new FState (StateA);
		// The next line will ensure that the first run of State A will occur in the same frame. Commenting this line out will delay execution to the next frame.
		stateMethod();
	}
	
	private void StateA () {						/// The actual state that is being repeated every frame.
		// Code goes here ...
		//if (someConditionApplies ()) {				// Conditions to switch into another state go here
			stateMethod = new FState (EnterStateB);
			ExitStateA ();
		//}
	}
	
	private void ExitStateA () {					/// Clean up method for state A.
		// All the stuff that needs to be cleaned up comes here.
	}
	#endregion
	
	#region State B
	private void EnterStateB () {					/// Sets up State B.
		// Things to prepare State B come here.
		stateMethod = new FState (StateB);
		// The next line will ensure that the first run of State B will occur in the same frame. Commenting this line out will delay execution to the next frame.
		stateMethod();
	}
	
	private void StateB () {						/// The actual state that is being repeated every frame.
		// Code goes here ...
		//if (someConditionApplies ()) {				// Conditions to switch into another state go here
			stateMethod = new FState (EnterStateA);
			ExitStateB ();
		//}
	}
	
	private void ExitStateB () {					/// Clean up method for state B.
		// All the stuff that needs to be cleaned up comes here.
	}
	
	#endregion
}