using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Run all given actions simultaneously, waiting for all of them to finish.
public class ParallelAction : Action {
	
	private IList<Action> actions;
	
	public ParallelAction(IList<Action> actions) {
		this.actions = actions;
	}
	
	public override void Start() {
		foreach (Action action in actions) {
			action.Start();
		}
	}
	
	public override bool IsFinished() {
		foreach (Action action in actions) {
			if (!action.IsFinished())
				return false;
		}
		return true;
	}
	
	public override void Update() {
		foreach (Action action in actions) {
			action.Update();
		}
	}
	
	public override void Interrupt() {
		foreach (Action action in actions) {
			action.Interrupt();
		}
	}
}
