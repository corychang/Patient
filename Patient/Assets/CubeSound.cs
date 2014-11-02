using UnityEngine;
using System.Collections;

public class CubeSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		/*GameObject gObject = GameObject.Find ("SoundManager");
		if (gObject == null) {
						Debug.Log ("couldn't find sound manager");
				}
		SoundManager soundManager = gObject.GetComponent<SoundManager> ();
		if (soundManager == null){
			Debug.Log("didn't find sound manager");
	 	}
		soundManager.startSound ("chatter", false, onfinisheds);*/
	}

	public void onfinished(){
		Debug.Log ("finished sound");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
		