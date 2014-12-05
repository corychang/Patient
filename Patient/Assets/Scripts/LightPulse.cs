using UnityEngine;
using System.Collections;

public class LightPulse : MonoBehaviour {
	private bool shouldPulse;
	public bool ShouldPulse {
		get { return shouldPulse; }
		set { 
			shouldPulse = value;
			if (!shouldPulse) {
				surrealLight.spotAngle = 1;
			}
		}
	}

	private Light surrealLight;
	private int deltaAngle;

	// Use this for initialization
	void Start () {
		deltaAngle = 20;
		surrealLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldPulse) {
			if (surrealLight.spotAngle + deltaAngle < 1 || 
			    surrealLight.spotAngle + deltaAngle > 179) {
				deltaAngle *= -1;
			}
			surrealLight.spotAngle += deltaAngle;
		}
	}
}
