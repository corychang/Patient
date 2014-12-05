using UnityEngine;
using System.Collections;

// A no-op. Do nothing. Nada. Zen, peace, etc.
public class NoAction : ActionRunner {

	public override void Start () {
		done ();
	}
	
	public override void Interrupt () {}
}
