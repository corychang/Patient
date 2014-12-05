using UnityEngine;
using System.Collections;

public class LightPulseAction : ActionRunner {
	private bool finished;
	private LightPulse lightPulse;

	public LightPulseAction() {
		finished = false;
		lightPulse = GameObject.Find ("SurrealLight").GetComponent<LightPulse> ();
	}

	public override void Start() {
		lightPulse.ShouldPulse = true;
	}

	public override void Interrupt() {
		lightPulse.ShouldPulse = false;
		finished = true;
	}

	public override bool IsFinished() {
		return finished;
	}
}
