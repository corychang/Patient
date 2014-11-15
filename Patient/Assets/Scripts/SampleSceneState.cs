using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleSceneState : GameStateManager {

	/**
	 * Creates the gameState for the scene
	 */
	protected override IList<GameState> GetGameStatesList() {

		//Gets the Dialog object from the camera for the dialog action
		GUITest dialog = Camera.mainCamera.GetComponent<GUITest>();

		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject obj = GameObject.Find ("Main Camera");
		if (obj == null) {
			Debug.Log ("couldn't find Main Camera");
		}
		soundManager.addSound (new Sound (obj, "Assets/backgroundMusic.mp3", "background"));
		soundManager.addSound (new Sound (obj, "Assets/crowd-talking-1.mp3", "chatter"));


		//Creates the Parallel Action list for the Yes State transition
		IList<Action> yesStateActionList = new List<Action> ();
		yesStateActionList.Add (new DialogAction ("Currently at the yes state", 3F, dialog));
		yesStateActionList.Add (new SoundAction ("background", false));
		ParallelAction yesActions = new ParallelAction (yesStateActionList);

		//Creates the Parallel Action list for the No State transition
		IList<Action> noStateActionList = new List<Action> ();
		noStateActionList.Add (new DialogAction ("Currently at the no state", 3F, dialog));
		noStateActionList.Add (new SoundAction ("chatter", false));
		noStateActionList.Add (new CameraInvertAction ());
		ParallelAction noActions = new ParallelAction (noStateActionList);

		if (dialog == null) {
			Debug.Log("null pointer with dialog");			
		}

		return new List<GameState> {
			new GameState(
				"start",
				new Dictionary<Trigger, string>() {
				{new NodTrigger(GameObject.Find("patient")), "yes"}, 
				{new ShakeTrigger(GameObject.Find("patient")), "no"}

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
			)
		};
	}
}
