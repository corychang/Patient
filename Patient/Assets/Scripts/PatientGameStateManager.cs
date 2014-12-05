using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// THIS IS THE FINAL VERSION OF THE GameStateManager.
public class PatientGameStateManager : GameStateManager {

	protected override IList<GameState> GetGameStatesList() {
		// Initialization
		GameObject patient = GameObject.Find ("patient");
		SoundManager.Instance.addSound (new Sound ("Assets/Sounds/phoneRinging.mp3", "phoneRinging"));

		/////////////////// Scene 1   ///////////////////////
		GameState scene1Start = new GameState (
			"scene1Start",
			new Dictionary<Trigger, string>() {
			{new ShakeTrigger(), "scene1Phone"}
			},
			new FadeAction(true)
		);

		SoundAction phoneRing = new SoundAction ("phoneRinging", true);
		GameState scene1Phone = new GameState (
			"scene1Phone",
			new Dictionary<Trigger, string>() {
			{new StareTrigger("phone"), "scene1Monologue"}
			},
			new ParallelAction(new FadeAction(false), phoneRing)
		);

		GameState scene1Monologue = new GameState (
			"scene1Monologue",
			new Dictionary<Trigger, string>(),
			new DialogAction("ASDFASDF")
		);

		return new List<GameState> () {
			scene1Start,
			scene1Phone,
			scene1Monologue
		};
	}
}
