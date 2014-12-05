using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreySceneState : GameStateManager {

	/**
	 * Creates the gameState for the scene
	 */
	protected override IList<GameState> GetGameStatesList() {

		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.main.GetComponent<DialogManager>();
		GameObject patient = GameObject.Find ("patient");

		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject obj = GameObject.FindGameObjectWithTag ("MainCamera");
		if (obj == null) {
			Debug.LogError ("couldn't find Main Camera");
		}
		soundManager.addSound (new Sound (obj, "Assets/Sounds/backgroundMusic.mp3", "background"));
		soundManager.addSound (new Sound (obj, "Assets/Sounds/crowd-talking-1.mp3", "chatter"));
		soundManager.addSound (new Sound (obj, "Assets/Sounds/sadMusic.mp3", "sadMusic"));


		ActionRunner scene3HallucinateAction = new ParallelAction(
			new List<ActionRunner>() {
				new CameraInvertAction(),
				new LightPulseAction() //TODO: Corey also sound affect
			}
		);
		GameState scene3Hallucinate = new GameState (
			"scene3Hallucinate",
			new Dictionary<Trigger, string> {
			{new ShakeTrigger(patient), "scene3Rose"}
			},
			scene3HallucinateAction
		);
		GameState scene3Rose = new GameState (
			"scene3Rose",
			new Dictionary<Trigger, string>(),
			new DialogAction("rose", 2, dialog),
			scene3HallucinateAction
			);
	
		return new List<GameState> {
			scene3Hallucinate,
			scene3Rose
		};
	}
}
