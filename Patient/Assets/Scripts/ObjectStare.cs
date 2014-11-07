using UnityEngine;
using System.Collections;

public class ObjectStare : MonoBehaviour {
	public int angle = 10;

	// Use this for initialization
	void Start () {
		// does nothing to initialize
	}
	
	// Update is called once per frame
	void Update () {
		//new position and direction of camera
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

		Debug.DrawRay(pos, dir*10, Color.red);
		Debug.DrawRay(pos, dirHigh*10, Color.red);
		Debug.DrawRay(pos, dirLow*10, Color.red);
		Debug.DrawRay(pos, dirRight*10, Color.red);
		Debug.DrawRay(pos, dirLeft*10, Color.red);
		Debug.DrawRay(pos, dirHighRight*10, Color.red);
		Debug.DrawRay(pos, dirHighLeft*10, Color.red);
		Debug.DrawRay(pos, dirLowRight*10, Color.red);
		Debug.DrawRay(pos, dirLowLeft*10, Color.red);

		//updating if something has been checked
		// For now, just keeping one hit object as we don't use it outside hte
		// if
		RaycastHit hit;


		if (Physics.Raycast(camVector,out hit,Mathf.Infinity)){
			print ("something has been hit Forward");
		}
		if (Physics.Raycast(camVectorHigh,out hit,Mathf.Infinity)){
			print ("something has been hit High");
		}
		if (Physics.Raycast(camVectorLow,out hit,Mathf.Infinity)){
			print ("something has been hit Low");
		}
		if (Physics.Raycast(camVectorRight,out hit,Mathf.Infinity)){
			print ("something has been hit Right");
		}
		if (Physics.Raycast(camVectorLeft,out hit,Mathf.Infinity)){
			print ("something has been hit Left");
		}
		if (Physics.Raycast(camVectorHighRight,out hit,Mathf.Infinity)){
			print ("something has been hit High Right");
		}
		if (Physics.Raycast(camVectorHighLeft,out hit,Mathf.Infinity)){
			print ("something has been hit High Left");
		}
		if (Physics.Raycast(camVectorLowRight,out hit,Mathf.Infinity)){
			print ("something has been hit Low Right");
		}
		if (Physics.Raycast(camVectorLowLeft,out hit,Mathf.Infinity)){
			print ("something has been hit Low Left");
		}
	}
}
