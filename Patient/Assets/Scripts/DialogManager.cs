using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour{

	private OnFinished onFinished;
	private bool show = false;
	private string dialogue;
	public float startingTime;
	private float duration;
	public float Duration{get{return duration;}}
	private bool isDialogueOn = false;
	public bool IsDialogueOn{get{return isDialogueOn;}}

	private int left = 10;
	private int top = 140;
	private int right = 480;
	private int bottom = 200;

	public void updateText(string text, float dur, OnFinished onFinished = null) {
		duration = dur;
		startingTime = Time.time;
		dialogue = text;
		this.onFinished = onFinished;
	}

	//test case
	/*void Start(){
		updateText("sdfddsds", 3F);
	}
*/
	void OnGUI() {
		//GUIStyle style = new GUIStyle ();
		//style.richText = true;
		float currentTime = Time.time;
		if (currentTime-startingTime <= duration) {
			show = true;
		} else {
			if (onFinished != null && show) {
				onFinished();
			}
	     	show = false;
	    }

		if (show) {
			GUI.Label (new Rect (left, top, right, bottom), dialogue);
			isDialogueOn =true;
		}
	}
	
	
}