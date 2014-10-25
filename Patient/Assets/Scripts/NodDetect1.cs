using UnityEngine;
using System.Collections;

public class NodDetect1 : MonoBehaviour {

	double Y_BOUNDARY = .3333;
	double X_BOUNDARY = 1.4/3.0;
	bool checkHeadNod;
	bool checkHeadShake;
	Vector3 initialAngle;

	Vector3 checkAngle;

	double t = 0.0;
	bool up = false;
	bool nod = false;
	bool pressed = false;
	Vector3 startAngle;
	bool completedNod = false;

	
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
			t +=Time.deltaTime;

			if (t<5){
				checkMove (checkAngle,true);
				checkHeadNod = false;
			}

		} 
		//checks if currently doing head shake
		else if (checkHeadShake) {
			t +=Time.deltaTime;

			if(t<5){
				checkMove (checkAngle,false);
				checkHeadShake = false;
			}
		} 
		//detect any new movements
		else {
			DetectMovement ();
		}

	}
		
	void DetectMovement() {
		Vector3 startAngle = Camera.main.transform.forward;
		
		//Check if the head moved to a different square
		float startY = Mathf.Abs (startAngle.y - initialAngle.y);
		float startX = Mathf.Abs (startAngle.x - initialAngle.x);
		
		//Passed a block on the y and not on x side: head nod
		if(startY >= Y_BOUNDARY) {
			if(!(startX >= X_BOUNDARY)) {
				checkHeadNod = true;
				checkHeadShake = false;
				checkAngle = startAngle;

				return; //Determined a head nod
			}
			else {
				checkHeadNod = false;
			}
		}
		
		//Passed a block on the x and not on y side: head shake
		if(startX >= X_BOUNDARY) {
			if(!(startY >= Y_BOUNDARY)) {
				checkHeadShake = true;
				checkHeadNod = false;
				checkAngle = startAngle;

				return; //Determined a head shake
			}
			else {
				checkHeadShake = false;
			}
		}
		
		//Passed a block on the y and on x side: diagonal
		if(startY >= Y_BOUNDARY) {
			if((startX >= X_BOUNDARY)) {
				checkHeadNod = false;
				checkHeadShake = false;
			}
		}
		
		if(startX >= X_BOUNDARY) {
			if((startY >= Y_BOUNDARY)) {
				checkHeadNod = false;
				checkHeadShake = false;
			}
		}
		
		initialAngle = Camera.main.transform.forward;
		return;
	}
		

		//takes in startangle and whether it's a nod or shake that's being checked
		void checkMove(Vector3 startAngle, bool nod){
		
		Vector3 currAngle = Camera.main.transform.forward;

			//finds vertical and horizontal distance difference
			float startY = Mathf.Abs (startAngle.y);
			float currY = Mathf.Abs (currAngle.y);
	
			float startX = Mathf.Abs (startAngle.x);
			float currX = Mathf.Abs (currAngle.x);
	
			float yDiff = Mathf.Abs (startY - currY);
			float xDiff = Mathf.Abs (startX - currX);
	
			//random numbers for how far it can be.. fiddle around wth this for camera constraints
			if (nod) {

				if (yDiff > .3 && xDiff < .2) {
					up = true;
					//print ("it's true!");
				} 

			}

			//shake
			if (!nod) {
				if (xDiff < .3 && xDiff > .2) {
					up = true;
				}
			}

			if (up == true) {

				Vector3 endAngle = currAngle;
			
				float startY1 = Mathf.Abs (startAngle.y);
				float startX1 = Mathf.Abs (startAngle.x);
	
				float endY = Mathf.Abs (endAngle.y);
				float endX = Mathf.Abs (endAngle.x);
	
				float yDiff1 = Mathf.Abs (startY1 - endY);
				float xDiff1 = Mathf.Abs (startX1 - endX);


				//fiddle with these numbers
				if ((Mathf.Abs (yDiff1) < .5) && (xDiff1 < .3)) {

					if (nod){
						print ("NOD!");
					}
					else{
						print ("SHAKE!");
					}
		
					completedNod = true;
					t = 0;
					up = false;
				}



	}		
	
	}
}