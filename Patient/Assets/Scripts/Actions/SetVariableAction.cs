using UnityEngine;
using System.Collections;

public class SetVariableAction : ActionRunner {

	private string name;
	private object value;

	public SetVariableAction(string name, object value){
		this.name = name;
		this.value = value;
	}

	public override void Start () {
		GameStateManager.Instance.SetVariable(name, value);
	}
	
	// Nothing to interrupt	
	public override void Interrupt () {}
}
