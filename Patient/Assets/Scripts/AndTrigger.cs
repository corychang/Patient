using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AndTrigger : Trigger {

	private IList<Trigger> triggers;

	public AndTrigger(IList<Trigger> triggers) {
		this.triggers = triggers;
	}

	public override bool IsTriggered() {
		foreach (Trigger trigger in triggers) {
			if (!trigger.IsTriggered())
				return false;
		}
		
		return true;
	}
}
