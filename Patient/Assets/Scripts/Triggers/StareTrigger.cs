using UnityEngine;
using System.Collections;

public class StareTrigger : Trigger {
	string targetName;
	bool stareTriggered;

	public StareTrigger(string targetName) : this(GameObject.Find("patient"), targetName) {}

	public StareTrigger(GameObject gameObject, string targetName) {
		this.targetName = targetName;
		ObjectStare objectStare = gameObject.GetComponent<ObjectStare> ();
		if (objectStare == null) {
			// TODO: maybe make a PatientException??
			throw new UnityException("objectStare missing!!!");
		}
		stareTriggered = false;
		objectStare.OnStare += OnStare;
	}

	public override bool IsTriggered() {
		return stareTriggered;
	}

	private void OnStare(string objectName) {
		if (objectName == targetName) {
			stareTriggered = true;
		}
	}
}
