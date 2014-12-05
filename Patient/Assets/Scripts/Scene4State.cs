using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene4State : GameStateManager {

	protected override IList<GameState> GetGameStatesList() {
	
		return new List<GameState>() {
			new GameState(
				"scene4dialoguePart1",
				new Dictionary<Trigger, string>() {
					{new MainActionFinishedTrigger(), "scene4question"}
				},
				new DialogAction("Sibling: But, when it comes to the good of humanity, you agree that it’s worth the risk right?")	
			),
			new GameState(
				"scene4question",
				new Dictionary<Trigger, string>() {
					{new NodTrigger(), "scene4yes"},
					{new ShakeTrigger(), "scene4no"}
				},
				new NoAction()
			),
			new GameState(
				"scene4dialoguePart2",
				new Dictionary<Trigger, string>() {
				
				},
				new SequentialAction(new List<ActionRunner>() {
				
				})
			)
		};
	
		// But, when it comes to the good of humanity, you agree that it’s worth the risk right?
		// yes or no
		// Visiting hours are almost over. There are some forms you need to sign.
		// Sure
	
	}
}
