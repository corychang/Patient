using UnityEngine;
using System.Collections;

public class NodDetect : MonoBehaviour {

	public static NodDetect Instance;

	void Awake() {
		Instance = this;
	}


	public delegate void NodHandler();
	public delegate void ShakeHandler();
	
	public event NodHandler Nod;
	public event ShakeHandler Shake;
	
	private enum State {WaitForMovement, CheckNod, CheckShake}
	private State currentState;
	
	// angles per second threshold to trigger nod
	const double Y_BOUNDARY = 200;
	const double X_BOUNDARY = 50;
	
	Vector3 previousAngle;
	Vector3 checkAngle;
	Vector3 checkDirection;
	float checkStartTime;
	
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
		previousAngle = GetCurrentRotation();
		currentState = State.WaitForMovement;
	}
	
	// every frame checks head nod / shake
	void Update () {
		if (Time.time < 0.5f)
			return; // don't detect nod or shake at first since player moves camera into window then
	
		//checks if currently doing a head nod
		if (currentState == State.CheckNod) {
			checkMove (checkAngle,true);
		} 
		//checks if currently doing head shake
		else if (currentState == State.CheckShake) {
			checkMove (checkAngle,false);
		} 
		//detect any new movements
		else {
			DetectMovement ();
		}
		
		
		if ((currentState == State.CheckNod || currentState == State.CheckShake)
		    && (Time.time - checkStartTime) > 0.5f) {
			currentState = State.WaitForMovement;
			previousAngle = GetCurrentRotation();
		}
		    
	}
	
	static float SmallestDifferenceBetweenAngles(float from, float to) {
		// http://stackoverflow.com/questions/1878907/the-smallest-difference-between-2-angles
		// REQUIRES: 0 <= from < 360, 0 <= to < 360
		// ENSURES: -180 <= result <= 180
		
		float diff = to - from;
		return Mathf.Repeat(diff + 180f, 360f) - 180f;
	}
	
	static Vector3 SmallestDifferenceBetweenAngles(Vector3 from, Vector3 to) {
		return new Vector3(
			SmallestDifferenceBetweenAngles(from.x, to.x),
			SmallestDifferenceBetweenAngles(from.y, to.y),
			SmallestDifferenceBetweenAngles(from.z, to.z)
		);
	}
	
	private Vector3 GetCurrentRotation() {
		// returns current rotation in Euler format, degrees
		// (x,y,z) like in the inspector
		return Camera.main.transform.rotation.eulerAngles;
	}
	
	void DetectMovement() {
		// REQUIRES: !checkHeadNod && !checkHeadShake
		
		Vector3 currentAngle = GetCurrentRotation(); // degrees
		checkDirection = SmallestDifferenceBetweenAngles(previousAngle, currentAngle);
		
		//Check if the head moved to a different square
		float dy = Mathf.Abs(checkDirection.y) / Time.deltaTime; // shake
		float dx = Mathf.Abs(checkDirection.x) / Time.deltaTime; // nod
		previousAngle = currentAngle;
		
		if(dy >= Y_BOUNDARY && !(dx >= X_BOUNDARY)) {
			currentState = State.CheckShake;
			checkAngle = currentAngle;
			checkStartTime = Time.time;
			
		} else if(dx >= X_BOUNDARY && !(dy >= Y_BOUNDARY)) {
			currentState = State.CheckNod;
			checkAngle = currentAngle;
			checkStartTime = Time.time;
		}
	}
	
	//takes in startangle and whether it's a nod or shake that's being checked
	void checkMove(Vector3 startAngle, bool nod) {
		Vector3 currentAngle = GetCurrentRotation();
		//finds vertical and horizontal distance difference

		Vector3 diff = SmallestDifferenceBetweenAngles(checkAngle, currentAngle);
		Vector3 absDiff = new Vector3(Mathf.Abs(diff.x),Mathf.Abs(diff.y),Mathf.Abs(diff.z));		
		
		if (!nod) {
			if (absDiff.y > 30 && Mathf.Sign(checkDirection.y) != Mathf.Sign (diff.y)) {
				currentState = State.WaitForMovement;
				OnShake();
				previousAngle = GetCurrentRotation();
			}
		} else {
			if (absDiff.x > 10 && Mathf.Sign(checkDirection.x) != Mathf.Sign (diff.x)) {
				currentState = State.WaitForMovement;
				previousAngle = GetCurrentRotation();
				OnNod();
			}
		}
		
		
//		// TODO: why take the absolute value now, if we do so later also?
//		// TODO: can we just use x and y? What if they're facing a different way?
//		float startY = Mathf.Abs (startAngle.y);
//		float currY = Mathf.Abs (currentAngle.y);
//		
//		float startX = Mathf.Abs (startAngle.x);
//		float currX = Mathf.Abs (currentAngle.x);
//		
//		float yDiff = Mathf.Abs (startY - currY);
//		float xDiff = Mathf.Abs (startX - currX);
//		
//		//random numbers for how far it can be.. fiddle around wth this for camera constraints
//		
//		if (!((yDiff < .5) && (xDiff < .3)))
//			return;
//		
//		if (nod) {
//			if (yDiff > .3 && xDiff < .2) {
//				OnNod();
//			}
//		} else {
//			//shake
//			if (xDiff < .3 && xDiff > .2) {
//				OnShake();
//			}
//		}
	}
}