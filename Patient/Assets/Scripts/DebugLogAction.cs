using UnityEngine;
using System.Collections;

public class DebugLogAction : Action {

	private string message;
	
	public DebugLogAction(string message) {
		this.message = message;
	}
	
	// Start the action.
	public override void Start() {
		Debug.Log(message);
	}
	
	// Message prints immediately, so there's nothing to interrupt
	public override void Interrupt() {
		
	}
	
	// We're finished as soon as we start
	public override bool IsFinished() {
		return true;
	}
}