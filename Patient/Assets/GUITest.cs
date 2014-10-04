using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	
	private bool show = false;
	private string dialogue;
	private float startingTime;
	private float duration;
	public float Duration{get{return duration;}}
	private bool isDialogOn = false;
	public bool IsDialogOn{get{return isDialogOn;}}

	void updateText(string text, float dur){
		duration = dur;
		startingTime = Time.time;
		dialogue = text;
	}

	//test case
	void Start(){
		updateText("sdfddsds", 3F);
	}

	void OnGUI() {
		//GUIStyle style = new GUIStyle ();
		//style.richText = true;
		float currentTime = Time.time;
		if (currentTime-startingTime <= duration) {
						show = true;
				} else {
	     			show = false;
		        }

		if (show) {
			GUI.Label (new Rect (10, 140, 480, 200), dialogue);
			isDialogOn =true;
			}
	}
	
	
}