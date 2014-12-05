using UnityEngine;
using System.Collections;

public class DialogAction : ActionRunner {
	
	private string message;
	private float duration;
	private DialogManager dialog;
	
	public DialogAction(string message, float duration, DialogManager dialog) {
		this.message = message;
		this.duration = duration;
		this.dialog = dialog;
	}
	
	// Start the action.
	public override void Start() {
		dialog.updateText(message, duration, done);
	}
	
	// Message prints immediately, so there's nothing to interrupt
	public override void Interrupt() {
		
	}
	
	// We're finished as soon as we start
	public override bool IsFinished() {
		return true;
	}
}
