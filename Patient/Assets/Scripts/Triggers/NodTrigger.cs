using UnityEngine;
using System.Collections;

public class NodTrigger : Trigger
{
	private bool JustGotNod = false;
	
	public NodTrigger() : this(GameObject.Find("patient")) {
	}

	public NodTrigger(GameObject gameObject) {
		NodDetect nodDetect = gameObject.GetComponent<NodDetect> ();
		if (nodDetect == null)
			Debug.LogError (gameObject.name + " must have NodDetect");

		nodDetect.Nod += OnNod;
	}

	public override bool IsTriggered() {
		if (JustGotNod) {
			JustGotNod = false;
			return true;
		}
		return false;
	}

	public void OnNod() {
		JustGotNod = true;
	}

}
