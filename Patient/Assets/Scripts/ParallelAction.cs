using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallelAction : Action  {

	IList<Action> actions;
	int numDone;

	// Start all actions, without waiting for action.IsFinished()
	public ParallelAction(IList<Action> actions) {
		this.actions = actions;
	}

	public override void Start (){
		foreach (Action action in actions) {
			action.register(subActionFin);
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

	//increment everytime subAction finishes
	//if all subactions have finished, call done on overall action
	protected void subActionFin(){
		numDone ++;
		if(numDone == actions.Count){
			done();
		}
	}



}







