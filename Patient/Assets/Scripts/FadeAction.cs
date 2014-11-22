using UnityEngine;
using System.Collections;

public class FadeAction : ActionRunner {

	//Camera blackCamera;
	Camera mainCamera;
	bool isFinished;
	bool fadeToBlack;
	SceneFadeInOut fade;

	public FadeAction(bool fadeToBlack) {
		this.fadeToBlack = fadeToBlack;
		isFinished = false;
		this.fade = GameObject.Find ("ScreenFader").GetComponent<SceneFadeInOut> ();
	}
	
	// Start the action
	public override void Start() {
		if (this.fadeToBlack) {
			fade.FadeToBlack();
		} 
		else {
			fade.FadeToClear();
		}
	}
	
	// Message prints immediately, so there's nothing to interrupt
	public override void Interrupt() {
		if (this.fadeToBlack) {
			fade.FadeToClear();
		} 
		else {
			fade.FadeToBlack();
		}
		isFinished = true;
	}
	
	// We're finished as soon as we start
	public override bool IsFinished() {
		return isFinished;
	}

}
