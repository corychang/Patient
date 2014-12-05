using UnityEngine;
using System.Collections;

public class SetVariableAction : ActionRunner {

	private GameStateManager manager;
	private string name;
	private object value;

	public SetVariableAction(GameStateManager manager, string name, object value){
		this.manager = manager;
		this.name = name;
		this.value = value;
	}

	public override void Start () {
		manager.SetVariable(name, value);
	}
	
	// Nothing to interrupt	
	public override void Interrupt () {
		
	}
}
