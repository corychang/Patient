using UnityEngine;
using System.Collections;

public class AnimationAction : ActionRunner {

	GameObject obj;
	string animName;
	AnimationManager animManager;
	bool loop;

	public AnimationAction(string name, bool loop, GameObject obj){
		this.animName = name;
		this.loop = loop;
		this.obj = obj;
		this.animManager = GameObject.Find ("animationManager").GetComponent<AnimationManager> ();
	}

	// Use this for initialization
	public override void Start () {
		animManager.startAnimation(obj, animName, loop);
	}
	
	// Update is called once per frame
	public override void Interrupt () {
		animManager.stopAnimation (obj, animName);
	}
}
