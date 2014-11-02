using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequentialAction : Action {

	IEnumerator<Action> actions;
	Action next;
	Action current;
	
	// Run each action, waiting for each to be finished before the next one
	public SequentialAction(IList<Action> actions) {
		this.actions = actions.GetEnumerator();
		this.actions.MoveNext ();
	}

	public override void Start() {
		current = actions.Current;
		current.register (currentDone);
		current.Start ();
		bool hasNext = actions.MoveNext ();
		if(hasNext){
			next = actions.Current;
		}
	}

	//Called when curreng subAction finishes
	//Either start next action, or if no more actions left, call done
	protected void currentDone(){
		if(next != null) {
			next.register (currentDone);
			next.Start ();

			current = next;
			bool hasNext = actions.MoveNext ();
			if (hasNext) {
				next = actions.Current;
			} 
			else next = null;
		}
		else{
			done();
		}
	}

	public override void Interrupt(){
		current.Interrupt ();
	}
	
}
