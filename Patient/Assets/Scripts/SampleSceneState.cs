using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleSceneState : GameStateManager {

	/**
	 * Creates the gameState for the scene
	 */
	protected override IList<GameState> GetGameStatesList() {
		return new List<GameState> {
			new GameState(
				"start",
				new Dictionary<Trigger, string>() {
				{new NodTrigger(GameObject.Find("patient")), "yes"}, 
				{new ShakeTrigger(GameObject.Find("patient")), "no"}

				},
				new DebugLogAction("Currently at the start state")
			),
			new GameState(
				"no",
				new Dictionary<Trigger, string>(),
				new DebugLogAction("Currently at the no state")
			),
			new GameState(
				"yes",
				new Dictionary<Trigger, string>(),
				new DebugLogAction("Currently at the yes state")
				)
		};
	}
}
