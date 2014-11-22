using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundAction : ActionRunner
{
	string soundName;
	SoundManager soundManager;
	bool loop;

	public SoundAction(string soundName, bool loop){
		this.soundName = soundName;
		this.loop = loop;
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
	}

	public override void Start(){
		soundManager.startSound(soundName, loop, done);
	}

	public override void Interrupt(){
		soundManager.interruptSound (soundName);
	}

}


