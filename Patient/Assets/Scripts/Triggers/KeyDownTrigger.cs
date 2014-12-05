using UnityEngine;
using System.Collections;

public class KeyDownTrigger : Trigger {

	private KeyCode keyCode;
	
	public KeyDownTrigger(KeyCode keyCode) {
		this.keyCode = keyCode;
	}

	public override bool IsTriggered() {
		return Input.GetKeyDown(keyCode);
	}
}
