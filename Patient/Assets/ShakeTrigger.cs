using UnityEngine;
using System.Collections;

public class ShakeTrigger : Trigger
{
	private bool JustGotShake = false;
	
	public ShakeTrigger(GameObject gameObject) {
		NodDetect shakeDetect = gameObject.GetComponent<NodDetect> ();
		if (shakeDetect == null)
			Debug.LogError (gameObject.name + " must have NodDetect");
		
		shakeDetect.Shake += OnNod;
	}
	
	public override bool IsTriggered() {
		if (JustGotShake) {
			JustGotShake = false;
			return true;
		}
		return false;
	}
	
	public void OnNod() {
		JustGotShake = true;
	}
	
}
