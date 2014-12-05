using UnityEngine;
using System.Collections;

public class ShakeTrigger : Trigger
{
	private bool JustGotShake = false;
	
	public ShakeTrigger() : this(GameObject.Find ("patient")) {
	
	}
	
	public ShakeTrigger(GameObject gameObject) {
		NodDetect shakeDetect = gameObject.GetComponent<NodDetect> ();
		if (shakeDetect == null)
			Debug.LogError (gameObject.name + " must have NodDetect");
	}

	public override void Init() {
		NodDetect.Instance.Shake += OnShake;
	}
	
	public override bool IsTriggered() {
		if (JustGotShake) {
			JustGotShake = false;
			return true;
		}
		return false;
	}
	
	public void OnShake() {
		Debug.Log ("OnShake called");
		JustGotShake = true;
		NodDetect.Instance.Shake -= OnShake;
	}
	
}
