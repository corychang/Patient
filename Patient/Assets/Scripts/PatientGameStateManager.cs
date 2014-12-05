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
			new SequentialAction(
			new DialogAction("Mother: Honey, I just wanted to call to see if you’re all right."),
			new DialogAction("Mother: The doctor says you’ll make a full recovery from your accident, but he won’t tell me what happened..."),
			new DialogAction("Mother: He said the same last time, too."),
			new DialogAction("Mother: Do you know how I feel when you go missing?"),
			new DialogAction("Mother: Dad is- I- I know we don’t agree, but you can’t keep running away!"),
			new DialogAction("Every time!"),
			new DialogAction("Mother: If we always disagree (about cloning), why do you bring it up!"),
			new DialogAction("Mother: ... "),
			new DialogAction("Mother: No, that’s not why I called… I just need to know if you’re okay."),
			new DialogAction("Mother: The doctor says you’ll be awake the next time Dad and I’ll visit, so get plenty of rest."),
			new DialogAction("Mother: We'll see you soon")
			),
			phoneRing
		);

		return new List<GameState> () {
			scene1Start,
			scene1Phone,
			scene1Monologue
		};
	}
}
