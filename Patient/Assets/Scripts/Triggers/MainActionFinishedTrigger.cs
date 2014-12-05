using UnityEngine;
using System.Collections;

public class MainActionFinishedTrigger : Trigger {
	
	public override bool IsTriggered() {
		return GameStateManager.Instance.GetMainActionFinished();
	}
	
}
