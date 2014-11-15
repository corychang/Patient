using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundTester : MonoBehaviour {
	bool started = false;
	// Use this for initialization
	void Start () {
		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();

		GameObject obj = GameObject.Find ("Cube");
		if (obj == null) {
			Debug.Log ("couldn't find Cube");
		}
		soundManager.addSound(new Sound(obj, "Assets/crowd-talking-1.mp3", "chatter"));
		
		
		obj = GameObject.Find ("Terrain");
		if (obj == null) {
			Debug.Log ("couldn't find Terrain");
		}
		soundManager.addSound(new Sound(obj, "Assets/backgroundMusic.mp3", "happy"));

	}
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			Debug.Log ("creating new sound action");
			Action soundAction = new SoundAction ("chatter", false);
			Action soundAction2 = new SoundAction("happy", false);
			IList<Action> list = new List<Action> ();
			list.Add (soundAction);
			list.Add (soundAction2);
			Action parAction = new ParallelAction(list);
			list = new List<Action> ();
			list.Add (parAction);
			list.Add (parAction);
			Action seqAction = new SequentialAction(list);
			seqAction.Start ();
			started = true;
		}
	
	}
}
