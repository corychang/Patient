using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrTrigger : Trigger {

	private IList<Trigger> triggers;
	
	public OrTrigger(IList<Trigger> triggers) {
		this.triggers = triggers;
	}
	
	public override bool IsTriggered() {
		foreach (Trigger trigger in triggers) {
			if (trigger.IsTriggered())
				return true;
		}
		
		return false;
	}
}
