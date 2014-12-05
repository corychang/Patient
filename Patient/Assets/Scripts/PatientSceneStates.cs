using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatientSceneStates : GameStateManager {
	
	/**
	 * Creates the gameState for the PatientScene
	 */
	protected override IList<GameState> GetGameStatesList() {
		
		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.main.GetComponent<DialogManager>();
		
		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject obj = GameObject.Find ("Main Camera");
		if (obj == null) {
			Debug.Log ("couldn't find Main Camera");
		}
		soundManager.addSound (new Sound (obj, "Assets/backgroundMusic.mp3", "background"));

		//Inital parallel actions: the black screen and looping cell phone ringing
		IList<ActionRunner> startActionList = new List<ActionRunner> ();
		startActionList.Add (new FadeAction (true)); //Fade to black
		startActionList.Add (new SoundAction ("background", true));
		ParallelAction startActions = new ParallelAction (startActionList);
		
		//Creates the Parallel Action list for the Shake State transition
		IList<ActionRunner> yesStateActionList = new List<ActionRunner> ();
		yesStateActionList.Add (new DialogAction ("Waking up", 3F, dialog));
		ParallelAction shakeActions = new ParallelAction (yesStateActionList);
		
		if (dialog == null) {
			Debug.Log("null pointer with dialog");			
		}
		
		return new List<GameState> {
			new GameState(
				"start",
				new Dictionary<Trigger, string>() {
				{new NodTrigger(GameObject.Find("patient")), "shake"}, 
				{new ShakeTrigger(GameObject.Find("patient")), "shake"}
			},
				startActions
			),
				
			new GameState(
				"shake",
				new Dictionary<Trigger, string>() {
				},
				shakeActions,
				new FadeAction(true) //stop Fading to black
			)
		};
	}
}

