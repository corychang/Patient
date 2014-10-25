using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour {

	private GameState currentGameState;
	private IDictionary<string, GameState> gameStatesByName;

	// Use this for initialization
	void Start () {
		IList<GameState> gameStates = new List<GameState>();
		// TODO: initialize IList<GameState> gameStates
		
		// TODO: set currentGameState
		
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
		
	}
	
	// Update is called once per frame
	void Update () {
		currentGameState = gameStatesByName[currentGameState.GetNextState()];
	
	}
}
