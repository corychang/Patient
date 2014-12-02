using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	

	void addAnimation (GameObject obj, AnimationClip clip, string name, int firstframe, int lastframe, bool addLoopFrame){
		obj.animation.AddClip(clip, name, firstframe, lastframe, addLoopFrame);
	}

	void rmAnimation (GameObject obj, string name) {
		obj.animation.RemoveClip(name);
	}

	string[] getAnimations (GameObject obj){
		int size = obj.animation.GetClipCount ();
		string[] ani_names = new string[size];
		int counter = 0;
		foreach (AnimationState states in obj.animation) {
			ani_names [counter++] = states.name;
		}
		return ani_names;
	}

	public void startAnimation (GameObject obj, string name, bool loop) {
		if (loop) {obj.animation[name].wrapMode = WrapMode.Loop;}
		else {obj.animation[name].wrapMode = WrapMode.Once;}
		obj.animation.Play(name);
	}

	public void stopAnimation (GameObject obj, string name){
		obj.animation.Stop (name);
	}




}
