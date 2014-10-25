using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour {

	private GameState currentGameState;
	private IDictionary<string, GameState> gameStatesByName;
	
	// TODO: implement for each level
	IList<GameState> GetGameStatesList() {
		return new List<GameState> {
			new GameState(
				"start",
				new Dictionary<Trigger, string>() {
					{new KeyDownTrigger(KeyCode.N), "2"}
				},
				new DebugLogAction("ALL YOUR BASE IS HAS CAtS")
			),
			new GameState(
				"2",
				new Dictionary<Trigger, string>() {
					{new KeyDownTrigger(KeyCode.N), "start"}
				},
				new DebugLogAction("LOLWUT")
			)
		};
	}

	// Use this for initialization
	void Start () {
		IList<GameState> gameStates = GetGameStatesList();
		currentGameState = gameStates[0];
		
		gameStatesByName = new Dictionary<string, GameState>();
		foreach (GameState gameState in gameStates) {
			Utilities.Assert(!gameStatesByName.ContainsKey(gameState.Name), "Names must be unique");
			gameStatesByName[gameState.Name] = gameState;
		}
		
		// Validate that each game state only goes to existing successor states.
		foreach (GameState gameState in gameStates) {
			foreach (string successor in gameState.GetSuccessorStates()) {
				Utilities.Assert(gameStatesByName.ContainsKey(successor), "State " + gameState.Name + "'s successor " + successor + " must exist");
			}
		}
		
		currentGameState.Start();
	}
	
	// Update is called once per frame
	void Update () {
		var next = currentGameState.GetNextState();
		if (next != currentGameState.Name) {
			currentGameState = gameStatesByName[next];
			currentGameState.Start();
		}
	}
}
