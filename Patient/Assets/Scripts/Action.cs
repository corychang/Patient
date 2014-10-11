using UnityEngine;
using System.Collections;

public class Action {

	public Action() {
	
	}
	
	public void Start() {
		// Start the action.
	}
	
	public void Interrupt() {
		// Stop the action in-progress.
	}
	
	// TODO: maybe convert to abstract class?
	public bool IsFinished() {
		return false;
	}
}
