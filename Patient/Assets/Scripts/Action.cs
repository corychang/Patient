using UnityEngine;
using System.Collections;

public abstract class Action {

	// Start the action.
	public abstract void Start();
	
	// Stop the action in-progress.
	// Should be idempotent (interrupting after already interrupted does nothing).
	// After Interrupt is called, IsFinished should return true.
	public abstract void Interrupt();
	
	// Is the action finished running?
	public abstract bool IsFinished();
	
	// Run this on every update to progress the action.
	public abstract void Update();
}
