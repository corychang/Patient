﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class DialogManager : MonoBehaviour{

	private OnFinished onFinished;
	private bool show = false;
	private string dialogue;
	public float startingTime;
	private float duration;
	public float Duration{get{return duration;}}
	private bool isDialogueOn = false;
	public bool IsDialogueOn{get{return isDialogueOn;}}

	private float leftPercentage = .05f;
	private float topPercentage = 0.8f;
	private float rightPercentage = 0.85f;
	private float botttomPercentage = 0.95f;
	
	public static DialogManager Instance;
	
	void Awake() {
		Instance = this;
//		Debug.Log (string.Join(",", FindObjectsOfType<DialogManager>().Select(x => x.name).ToArray()));

		if (FindObjectsOfType(this.GetType()).Length > 1)
			Debug.LogError("More than 1 " + this.GetType().Name + "in scene");
	}
	
	void Start() {
		SoundManager.Instance.addSound(new Sound("Assets/Sounds/gibberish.mp3", "gibberish"));
	}

	public void updateText(string text, float dur, OnFinished onFinished = null) {
		duration = dur;
		startingTime = Time.time;
		dialogue = text;
		this.onFinished = onFinished;
		SoundManager.Instance.startSound ("gibberish", true, null);
	}
	void OnGUI() {
		//GUIStyle style = new GUIStyle ();
		//style.richText = true;
		float currentTime = Time.time;
		if (currentTime-startingTime <= duration) {
			show = true;
		} else if (show) {
			SoundManager.Instance.interruptSound("gibberish");
			if (onFinished != null) {
				onFinished();
			}
	     	show = false;
	    }

		if (show) {
			GUIStyle style = new GUIStyle();
			style.fontSize = 36;
			style.wordWrap = true;

			ShadowAndOutline.DrawOutline(
				new Rect (leftPercentage * Screen.width,
			          	  topPercentage * Screen.height,
			          	  rightPercentage * Screen.width,
			          	  botttomPercentage * Screen.height
			          ),
				dialogue,
				style,
				Color.black,
				Color.white,
				4
			);

			isDialogueOn = true;
		}
	}
	
	
}