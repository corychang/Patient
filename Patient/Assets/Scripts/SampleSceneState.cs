using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleSceneState : GameStateManager {

	/**
	 * Creates the gameState for the scene
	 */
	protected override IList<GameState> GetGameStatesList() {

		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.main.GetComponent<DialogManager>();

		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject obj = GameObject.FindGameObjectWithTag ("MainCamera");
		if (obj == null) {
			Debug.LogError ("couldn't find Main Camera");
		}
		soundManager.addSound (new Sound (obj, "Assets/Sounds/backgroundMusic.mp3", "background"));
		soundManager.addSound (new Sound (obj, "Assets/Sounds/crowd-talking-1.mp3", "chatter"));
		soundManager.addSound (new Sound (obj, "Assets/Sounds/sadMusic.mp3", "sadMusic"));


		//Creates the Parallel Action list for the Yes State transition
		IList<ActionRunner> yesStateActionList = new List<ActionRunner> ();
		yesStateActionList.Add (new DialogAction ("Currently at the yes state", 3F, dialog));
		yesStateActionList.Add (new SoundAction ("background", false));
		ParallelAction yesActions = new ParallelAction (yesStateActionList);

		//Creates the Parallel Action list for the No State transition
		IList<ActionRunner> noStateActionList = new List<ActionRunner> ();
		noStateActionList.Add (new DialogAction ("Currently at the no state", 3F, dialog));
		noStateActionList.Add (new SoundAction ("chatter", false));
		noStateActionList.Add (new CameraInvertAction ());
		ParallelAction noActions = new ParallelAction (noStateActionList);

		//Creates the Parallel Action list for the Stare State transition
		IList<ActionRunner> stareStateActionList = new List<ActionRunner> ();
		stareStateActionList.Add (new DialogAction ("Currently at the Stare State", 3F, dialog));
		stareStateActionList.Add (new SoundAction ("sadMusic", false));
		ParallelAction stareActions = new ParallelAction (stareStateActionList);

		if (dialog == null) {
			Debug.Log("null pointer with dialog");			
		}

		return new List<GameState> {
			new GameState(
				"start",
				new Dictionary<Trigger, string>() {
				{new NodTrigger(GameObject.Find("patient")), "yes"}, 
				{new ShakeTrigger(GameObject.Find("patient")), "no"}, 
				{new StareTrigger(GameObject.Find("patient"), "StareTarget"), "stare"}
				},
				new DialogAction("Currently at the start state", 3F, dialog)
			),
			new GameState(
				"no",
				new Dictionary<Trigger, string>(),
				noActions
			),
			new GameState(
				"yes",
				new Dictionary<Trigger, string>(),
				yesActions
			),
			new GameState(
				"stare",
				new Dictionary<Trigger, string>(),
				stareActions
			)
		};
	}
}
