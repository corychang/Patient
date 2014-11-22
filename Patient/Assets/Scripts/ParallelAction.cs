using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallelAction : ActionRunner  {

	IList<ActionRunner> actions;
	int numDone;

	// Start all actions, without waiting for action.IsFinished()
	public ParallelAction(IList<ActionRunner> actions) {
		this.actions = actions;
	}

	public override void Start (){
		foreach (ActionRunner action in actions) {
			action.register(subActionFin);
			action.Start();
		}
	}

	public override bool IsFinished() {
		foreach (ActionRunner action in actions) {
			if (!action.IsFinished())
				return false;
		}
		return true;
	}
	
	public override void Interrupt() {
		foreach (ActionRunner action in actions) {
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







