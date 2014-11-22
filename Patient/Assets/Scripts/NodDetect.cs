using UnityEngine;
using System.Collections;

public class NodDetect : MonoBehaviour {
	
	public delegate void NodHandler();
	public delegate void ShakeHandler();
	
	public event NodHandler Nod;
	public event ShakeHandler Shake;
	
	const double Y_BOUNDARY = .3333;
	const double X_BOUNDARY = 1.4/3.0;
	bool checkHeadNod;
	bool checkHeadShake;
	
	Vector3 initialAngle;
	Vector3 checkAngle;
	
	void OnNod() {
		if (Nod != null)
			Nod ();
	}
	
	void OnShake() {
		if (Shake != null)
			Shake();
	}
	
	// Use this for initialization
	void Start () {
		initialAngle = Camera.main.transform.forward;
		checkHeadNod = false;
		checkHeadShake = false;
	}
	
	// every frame checks head nod / shake
	void Update () {
		//checks if currently doing a head nod
		if (checkHeadNod) {
			checkMove (checkAngle,true);
			checkHeadNod = false;
		} 
		//checks if currently doing head shake
		else if (checkHeadShake) {
			checkMove (checkAngle,false);
			checkHeadShake = false;
		} 
		//detect any new movements
		else {
			DetectMovement ();
		}
		
	}
	
	void DetectMovement() {
		// REQUIRES: !checkHeadNod && !checkHeadShake
		
		Vector3 currentAngle = Camera.main.transform.forward;
		
		//Check if the head moved to a different square
		float dy = Mathf.Abs (currentAngle.y - initialAngle.y);
		float dx = Mathf.Abs (currentAngle.x - initialAngle.x);
		
		if(dy >= Y_BOUNDARY && !(dx >= X_BOUNDARY)) {
			//Passed a block on the y and not on x side: head nod
			checkHeadNod = true;
			checkHeadShake = false;
			checkAngle = currentAngle;
			
			//Determined a head nod
		} else if(dx >= X_BOUNDARY && !(dy >= Y_BOUNDARY)) {
			//Passed a block on the x and not on y side: head shake
			checkHeadShake = true;
			checkHeadNod = false;
			checkAngle = currentAngle;
			
			//Determined a head shake
		} else {
			initialAngle = currentAngle;
		}
	}
	
	//takes in startangle and whether it's a nod or shake that's being checked
	void checkMove(Vector3 startAngle, bool nod) {
		Vector3 currAngle = Camera.main.transform.forward;
		//finds vertical and horizontal distance difference
		
		// TODO: why take the absolute value now, if we do so later also?
		// TODO: can we just use x and y? What if they're facing a different way?
		float startY = Mathf.Abs (startAngle.y);
		float currY = Mathf.Abs (currAngle.y);
		
		float startX = Mathf.Abs (startAngle.x);
		float currX = Mathf.Abs (currAngle.x);
		
		float yDiff = Mathf.Abs (startY - currY);
		float xDiff = Mathf.Abs (startX - currX);
		
		//random numbers for how far it can be.. fiddle around wth this for camera constraints
		
		if (!((yDiff < .5) && (xDiff < .3)))
			return;
		
		if (nod) {
			if (yDiff > .3 && xDiff < .2) {
				OnNod();
			}
		} else {
			//shake
			if (xDiff < .3 && xDiff > .2) {
				OnShake();
			}
		}
	}
}