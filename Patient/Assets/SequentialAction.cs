using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequentialAction : Action {

	IList<Action> actions;
	
	// Run each action, waiting for each to be finished before the next one

	public SequentialAction(IList<Action> actions) {
		this.actions = actions;
	}
	
	public void Start() {
		// TODO: implement
	}
	
}
