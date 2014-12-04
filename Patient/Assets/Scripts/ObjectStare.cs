using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectStare : MonoBehaviour {
	public delegate void StareHandler(string objectName);
	const float TIME_FOR_STARE = 2.0f; // time required to stare at an object, in seconds



	public event StareHandler OnStare;

	private IDictionary<string, float> currentObjects;

	public int angle = 10;
	// Use this for initialization
	void Start () {
		currentObjects = new Dictionary<string, float> ();
	}

	void Update() {
		float currentTime = Time.time;
		IEnumerable<string> objectsHit = CastRays ();
		// First add objects from objectsHit that aren't yet being tracked
		foreach (string name in objectsHit) {
			if (! currentObjects.ContainsKey(name)) {
				currentObjects[name] = Time.time;
			}
		}

		foreach (object obj in currentObjects.Keys.ToList()) {
			string name = (string) obj;
			if (objectsHit.Contains(name)) {
				if (currentTime - currentObjects[name] > TIME_FOR_STARE) {
					if (OnStare != null) {
						OnStare(name);
					}
					// Require we stare at the object for TIME_FOR_STARE seconds before
					// calling the trigger again. Just to stop us from spamming the fn call.
					currentObjects.Remove(name);
				}
			} 
			else {
				currentObjects.Remove(name);
			}
		}
	}
	
	// Update is called once per frame
	IEnumerable<string> CastRays () {
		ArrayList objectsHit = new ArrayList ();
		//new position and direction of camera
		// TODO: Change from mainCamera to main
		Vector3 pos = Camera.mainCamera.gameObject.transform.position;
		Vector3 dir = Camera.mainCamera.gameObject.transform.forward;
		Ray camVector = new Ray(pos, dir);

		//looking in a cone of 30 degrees from forward direction
		//new directions based on position and direction of camera
		Vector3 dirLeft = Quaternion.Euler(0,-angle,0)*dir;
		Vector3 dirRight = Quaternion.Euler(0,angle,0)*dir;
		Vector3 dirLow = Quaternion.Euler(angle,0,0)*dir;
		Vector3 dirHigh = Quaternion.Euler(-angle,0,0)*dir;
		Vector3 dirHighRight = Quaternion.Euler(-angle,angle,0)*dir;
		Vector3 dirHighLeft = Quaternion.Euler(-angle,-angle,0)*dir;
		Vector3 dirLowRight = Quaternion.Euler(angle,angle,0)*dir;
		Vector3 dirLowLeft = Quaternion.Euler(angle,-angle,0)*dir;

		//making a ray for each direction
		Ray camVectorHigh = new Ray(pos,dirHigh);
		Ray camVectorLow = new Ray(pos,dirLow);
		Ray camVectorRight = new Ray(pos,dirRight);
		Ray camVectorLeft = new Ray(pos,dirLeft);
		Ray camVectorHighRight = new Ray(pos,dirHighRight);
		Ray camVectorHighLeft = new Ray(pos,dirHighLeft);
		Ray camVectorLowRight = new Ray(pos,dirLowRight);
		Ray camVectorLowLeft = new Ray(pos,dirLowLeft);

		Debug.DrawRay(pos, dir*100, Color.red);
		Debug.DrawRay(pos, dirHigh*100, Color.red);
		Debug.DrawRay(pos, dirLow*100, Color.red);
		Debug.DrawRay(pos, dirRight*100, Color.red);
		Debug.DrawRay(pos, dirLeft*100, Color.red);
		Debug.DrawRay(pos, dirHighRight*100, Color.red);
		Debug.DrawRay(pos, dirHighLeft*100, Color.red);
		Debug.DrawRay(pos, dirLowRight*100, Color.red);
		Debug.DrawRay(pos, dirLowLeft*100, Color.red);

		//updating if something has been checked
		// For now, just keeping one hit object as we don't use it outside hte
		// if
		RaycastHit hit;

		// I'm keeping the explicit variables camVectorX in case we later do specific behavior for each ray.
		// However, currently we want to loop over all the rays so I'm making an inline Ray[]
		foreach (Ray camRay in new Ray[] {
			camVector, camVectorHigh, camVectorLow, camVectorRight, 
			camVectorLeft, camVectorHighRight, camVectorHighLeft, camVectorLowRight, camVectorLowLeft}) {
			if (Physics.Raycast(camRay, out hit, Mathf.Infinity)) {
				objectsHit.Add(hit.transform.gameObject.name);
			}
		}
		return objectsHit.ToArray ().Distinct ().Cast<string> ();
	}
}
