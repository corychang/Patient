using UnityEngine;
using System.Collections;

public class IfVariableAction : ActionRunner {
	
	private string variableName;
	private ActionRunner actionIfTrue;
	private ActionRunner actionIfFalse;
	private ActionRunner action;

	public IfVariableAction(string variableName, ActionRunner actionIfTrue, ActionRunner actionIfFalse){
		this.variableName = variableName;
		this.actionIfTrue = actionIfTrue;
		this.actionIfFalse = actionIfFalse;
	}

	public override void Start () {
		if ((bool)GameStateManager.Instance.GetVariable(variableName))
			action = actionIfTrue;
		else
			action = actionIfFalse;
		
		action.Start();
	}
	
	// Nothing to interrupt	
	public override void Interrupt () {
		action.Interrupt();
	}
}
