using UnityEngine;
using System.Collections;

public class AnimationTester : MonoBehaviour {

	GameObject obj;
	// Use this for initialization
	void Start () {
		AnimationManager animManager = GameObject.Find ("animationManager").GetComponent<AnimationManager> ();
		obj = GameObject.Find ("Player");
		ActionRunner anim1 = new AnimationAction ("Take 001", true, obj);
		ActionRunner anim2 = new AnimationAction ("Take 001", false, obj);
		//	anim1.Start ();
		anim2.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
