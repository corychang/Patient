using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene4State : GameStateManager {

	protected override IList<GameState> GetGameStatesList() {
	
		return new List<GameState>() {
			new GameState(
				"scene4dialoguePart1",
				new Dictionary<Trigger, string>() {
					{new NodTrigger(), "scene4yes"},
					{new ShakeTrigger(), "scene4no"}
				},
				new DialogAction("Sibling: But, when it comes to the good of humanity, you agree that it’s worth the risk right?")	
			),
			new GameState(
				"scene4yes",
				new Dictionary<Trigger, string>() {
					{new MainActionFinishedTrigger(), "scene4dialoguePart2"}
				},
				new SetVariableAction("scene4answer", true)
			),
			new GameState(
				"scene4no",
				new Dictionary<Trigger, string>() {
					{new MainActionFinishedTrigger(), "scene4dialoguePart2"}
				},
				new SetVariableAction("scene4answer", false)
			),
			new GameState(
				"scene4dialoguePart2",
				new Dictionary<Trigger, string>() {
				
				},
				new SequentialAction(
					// knock sound
					new DialogAction("Sibling: Yes?"),
					// The door opens and the mirror is perfectly angled to reflect you.
					new DialogAction("Doctor: Visiting hours are almost over. There are some forms you need to sign."),
					new DialogAction("Sibling: Sure?"),
					new DialogAction("Sibling: I’ll be going now, but think about this.")
					// display credits
					// if yes, loop game
					// if no, main menu
//					new RestartGameAction()
				)
			)
		};
	
		// But, when it comes to the good of humanity, you agree that it’s worth the risk right?
		// yes or no
		// Visiting hours are almost over. There are some forms you need to sign.
		// Sure
	
	}
}
