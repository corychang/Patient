using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour {

	private GameState currentGameState;
	private IDictionary<string, GameState> gameStatesByName;
	private IDictionary<string, object> variables;
	public static GameStateManager Instance ;
	
	public string CurrentGameStateName; // show in the inspector to help debug, read only
	
	public void SetVariable(string name, object value) {
		variables[name] = value;
	}
	
	public object GetVariable(string name) {
		return variables[name];
	}
	
	// TODO: implement for each level
	protected virtual IList<GameState> GetGameStatesList() {
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
		Instance = this;
		variables = new Dictionary<string, object>();
		IList<GameState> gameStates = GetGameStatesList();
		Utilities.Assert (gameStates.Count > 0, "Must have at least 1 game state");
		
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
	}
	
	bool started;
	
	// Update is called once per frame
	void Update () {
		if (!started) {
			started = true;
			currentGameState.Start();
		}
	
		CurrentGameStateName = currentGameState.Name;
		var next = currentGameState.GetNextState();
		if (next != currentGameState.Name) {
			currentGameState = gameStatesByName[next];
			currentGameState.Start();
		}
	}
	
	public bool GetMainActionFinished() {
		if (currentGameState == null) {
			Debug.LogWarning("Trying to GetMainActionFinished(), but no current game state!");
			return false;
		}
		
		return currentGameState.MainAction.IsFinished();
	}
}
