using UnityEngine;
using System.Collections;

public class ObjectStare : MonoBehaviour {

	Vector3 pos;
	Vector3 dir;
	int angle;

	Ray camVector;

	//checks each direction to see if something was hit
	RaycastHit hitForward;
	RaycastHit hitHigh;
	RaycastHit hitLow;
	RaycastHit hitRight;
	RaycastHit hitLeft;
	RaycastHit hitHighRight;
	RaycastHit hitHighLeft;
	RaycastHit hitLowRight;
	RaycastHit hitLowLeft;

	//looking in a cone of 30 degrees from forward direction
	Vector3 dirHigh;
	Vector3 dirLow;
	Vector3 dirRight;
	Vector3 dirLeft;
	Vector3 dirHighRight;
	Vector3 dirHighLeft;
	Vector3 dirLowRight;
	Vector3 dirLowLeft;

	//making a ray for each direction
	Ray camVectorHigh;
	Ray camVectorLow;
	Ray camVectorRight;
	Ray camVectorLeft;
	Ray camVectorHighRight;
	Ray camVectorHighLeft;
	Ray camVectorLowRight;
	Ray camVectorLowLeft;

	// Use this for initialization
	void Start () {
		pos = Camera.mainCamera.gameObject.transform.position;
		dir = Camera.mainCamera.gameObject.transform.forward;

		angle = 10;

		camVector = new Ray(pos, dir);

		dirLeft = Quaternion.Euler(0,-angle,0)*dir;
		dirRight = Quaternion.Euler(0,angle,0)*dir;
		dirLow = Quaternion.Euler(angle,0,0)*dir;
		dirHigh = Quaternion.Euler(-angle,0,0)*dir;
		dirHighRight = Quaternion.Euler(-angle,angle,0)*dir;
		dirHighLeft = Quaternion.Euler(-angle,-angle,0)*dir;
		dirLowRight = Quaternion.Euler(angle,angle,0)*dir;
		dirLowLeft = Quaternion.Euler(angle,-angle,0)*dir;


		camVectorHigh = new Ray(pos,dirHigh);
		camVectorLow = new Ray(pos,dirLow);
		camVectorRight = new Ray(pos,dirRight);
		camVectorLeft = new Ray(pos,dirLeft);
		camVectorHighRight = new Ray(pos,dirHighRight);
		camVectorHighLeft = new Ray(pos,dirHighLeft);
		camVectorLowRight = new Ray(pos,dirLowRight);
		camVectorLowLeft = new Ray(pos,dirLowLeft);

		Debug.DrawRay(pos, dir*10, Color.red);
		Debug.DrawRay(pos, dirHigh*10, Color.red);
		Debug.DrawRay(pos, dirLow*10, Color.red);
		Debug.DrawRay(pos, dirRight*10, Color.red);
		Debug.DrawRay(pos, dirLeft*10, Color.red);
		Debug.DrawRay(pos, dirHighRight*10, Color.red);
		Debug.DrawRay(pos, dirHighLeft*10, Color.red);
		Debug.DrawRay(pos, dirLowRight*10, Color.red);
		Debug.DrawRay(pos, dirLowLeft*10, Color.red);


		//check to see if something has been hit
		if (Physics.Raycast(camVector,out hitForward,Mathf.Infinity)){
			print ("something has been hit Forward");
		}
		if (Physics.Raycast(camVectorHigh,out hitHigh,Mathf.Infinity)){
			print ("something has been hit High");
		}
		if (Physics.Raycast(camVectorLow,out hitLow,Mathf.Infinity)){
			print ("something has been hit Low");
		}
		if (Physics.Raycast(camVectorRight,out hitRight,Mathf.Infinity)){
			print ("something has been hit Right");
		}
		if (Physics.Raycast(camVectorLeft,out hitLeft,Mathf.Infinity)){
			print ("something has been hit Left");
		}
		if (Physics.Raycast(camVectorHighRight,out hitHighRight,Mathf.Infinity)){
			print ("something has been hit High Right");
		}
		if (Physics.Raycast(camVectorHighLeft,out hitHighLeft,Mathf.Infinity)){
			print ("something has been hit High Left");
		}
		if (Physics.Raycast(camVectorLowRight,out hitLowRight,Mathf.Infinity)){
			print ("something has been hit Low Right");
		}
		if (Physics.Raycast(camVectorLowLeft,out hitLowLeft,Mathf.Infinity)){
			print ("something has been hit Low Left");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//new position and direction of camera
		pos = Camera.mainCamera.gameObject.transform.position;
		dir = Camera.mainCamera.gameObject.transform.forward;

		//new directions based on position and direction of camera
		dirLeft = Quaternion.Euler(0,-angle,0)*dir;
		dirRight = Quaternion.Euler(0,angle,0)*dir;
		dirLow = Quaternion.Euler(angle,0,0)*dir;
		dirHigh = Quaternion.Euler(-angle,0,0)*dir;
		dirHighRight = Quaternion.Euler(-angle,angle,0)*dir;
		dirHighLeft = Quaternion.Euler(-angle,-angle,0)*dir;
		dirLowRight = Quaternion.Euler(angle,angle,0)*dir;
		dirLowLeft = Quaternion.Euler(angle,-angle,0)*dir;

		//adjusting camVectors
		camVector.origin = pos;
		camVector.direction = dir;

		camVectorHigh.direction = dirHigh;
		camVectorLow.direction = dirLow;
		camVectorRight.direction = dirRight;
		camVectorLeft.direction = dirLeft;
		camVectorHighRight.direction = dirHighRight;
		camVectorHighLeft.direction = dirHighLeft;
		camVectorLowRight.direction = dirLowRight;
		camVectorLowLeft.direction = dirLowLeft;

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

		if (Physics.Raycast(camVector,out hitForward,Mathf.Infinity)){
			print ("something has been hit Forward");
		}
		if (Physics.Raycast(camVectorHigh,out hitHigh,Mathf.Infinity)){
			print ("something has been hit High");
		}
		if (Physics.Raycast(camVectorLow,out hitLow,Mathf.Infinity)){
			print ("something has been hit Low");
		}
		if (Physics.Raycast(camVectorRight,out hitRight,Mathf.Infinity)){
			print ("something has been hit Right");
		}
		if (Physics.Raycast(camVectorLeft,out hitLeft,Mathf.Infinity)){
			print ("something has been hit Left");
		}
		if (Physics.Raycast(camVectorHighRight,out hitHighRight,Mathf.Infinity)){
			print ("something has been hit High Right");
		}
		if (Physics.Raycast(camVectorHighLeft,out hitHighLeft,Mathf.Infinity)){
			print ("something has been hit High Left");
		}
		if (Physics.Raycast(camVectorLowRight,out hitLowRight,Mathf.Infinity)){
			print ("something has been hit Low Right");
		}
		if (Physics.Raycast(camVectorLowLeft,out hitLowLeft,Mathf.Infinity)){
			print ("something has been hit Low Left");
		}
	}
}
