using UnityEngine;
using System.Collections;

public class RestartGameAction : ActionRunner {

	public override void Start () {
		Application.LoadLevel(0);
	}
	
	// This is done as soon as it starts, so there's no way to interrupt it.
	public override void Interrupt () {}
}
