using UnityEngine;
using System.Collections;

public abstract class Trigger {
	
	public abstract bool IsTriggered();

	public virtual void Init() {
		// Optionally add code to be executed when the trigger is initialized
	}
	
}
