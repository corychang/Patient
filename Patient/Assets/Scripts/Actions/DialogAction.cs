using UnityEngine;
using System.Collections;

public class DialogAction : ActionRunner {
	
	private string message;
	private float duration;
	
	// http://www.wolframalpha.com/input/?i=average+reading+speed
	const float WordsPerSecond = 4.0f; // go a little bit under
	const float ReadingExtraTime = 1.0f; // an extra fixed amount to read
	
	public DialogAction(string message) {
		int numWords = message.Split(' ').Length;
		float timeToRead = numWords / WordsPerSecond + ReadingExtraTime;
		this.message = message;
		this.duration = timeToRead;
	}
	
	
	public DialogAction(string message, float duration) {
		this.message = message;
		this.duration = duration;
	}
	
	// Start the action.
	public override void Start() {
		DialogManager.Instance.updateText(message, duration, done);
	}
	
	// Message prints immediately, so there's nothing to interrupt
	public override void Interrupt() {
		
	}
}
