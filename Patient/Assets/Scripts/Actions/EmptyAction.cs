using UnityEngine;
using System.Collections;

public class EmptyAction : ActionRunner {
	
	public override void Start () {
		done ();
	}

	public override void Interrupt () {}
}
