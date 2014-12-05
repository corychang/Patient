using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SequentialAction : ActionRunner {

	IEnumerator<ActionRunner> actions;
	ActionRunner next;
	ActionRunner current;
	
	public SequentialAction(params ActionRunner[] list) : this(list.ToList()) {
	
	}
		
	// Run each action, waiting for each to be finished before the next one
	public SequentialAction(IList<ActionRunner> actions) {
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


	public override bool IsFinished(){
				return next == null;
		}
}
