using UnityEngine;
using System.Collections;

public class SetAnimVarAction : ActionRunner {
	
	private string gameObjectName;
	private string variableName;
	private bool variableValue;
	
	public SetAnimVarAction(string gameObjectName, string variableName, bool variableValue){
		this.gameObjectName = gameObjectName;
		this.variableName = variableName;
		this.variableValue = variableValue;
	}
	
	public override void Start () {
		var go = GameObject.Find(gameObjectName);
		var anim = go.GetComponent<Animator>();
		anim.SetBool(variableName, variableValue);
		done ();
	}
	
	// Nothing to interrupt	
	public override void Interrupt () {}
}

