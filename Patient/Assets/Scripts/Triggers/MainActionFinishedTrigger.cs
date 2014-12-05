using UnityEngine;
using System.Collections;

public class MainActionFinishedTrigger : Trigger {
	
	public override bool IsTriggered() {
		Debug.Log (GameStateManager.Instance.GetMainActionFinished());
		return GameStateManager.Instance.GetMainActionFinished();
	}
	
}
