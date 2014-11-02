using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sound
{	
	private string name;
	private string filePath;
	private GameObject gameObject;
	public OnFinished onFinished;
	private bool isActivated = false;
	private AudioSource source;
	
	public Sound(GameObject gameObject, string filePath, string name)
	{
		this.gameObject = gameObject;
		this.filePath = filePath;
		this.name = name;

		source = gameObject.AddComponent<AudioSource>();
		AudioClip clip = Resources.LoadAssetAtPath<AudioClip> (filePath);
		if (clip == null) Debug.Log ("can't find audio file");
		source.clip = clip;
	}



	public string getName(){
		return name;
	}

	//play soundclip on gameobject
	public void play(bool loop, OnFinished onfinished){
		onFinished = onfinished;
		isActivated = true;
		source.Play();	
		source.volume = 1;
		source.loop = loop;
	}

	//check if ifinished and if so, call onFinished
	public void ifFinishedFinish(){
		if (isActivated && !source.isPlaying) {
			isActivated = false;
			if(onFinished != null){
				onFinished();
				Debug.Log ("Audio finished");
			}
		}
	}

	//interrupt soundclip and call onFinished
	public void interrupt(){
		if (onFinished != null) {
			onFinished ();
			isActivated = false;
		}
		source.mute = true;
	}

}		