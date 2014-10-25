using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequentialAction : Action {

	private IList<Action> actions;
	private int currentActionIndex;
	
	// Run each action, waiting for each to be finished before the next one

	public SequentialAction(IList<Action> actions) {
		this.actions = actions;
	}
	
	public override void Start() {
		currentActionIndex = 0;
	}
	
	public override bool IsFinished() {
		return currentActionIndex >= actions.Count;
	}
	
	public override void Update() {
		if (IsFinished())
			return;
		
		var currentAction = actions[currentActionIndex];
		currentAction.Update();
		if (currentAction.IsFinished())
			currentActionIndex += 1;
	}
	
	public override void Interrupt() {
		if (IsFinished())
			return;
			
		actions[currentActionIndex].Interrupt();
	}
}
