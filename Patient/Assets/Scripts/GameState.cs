using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {

	public readonly string Name;
	
	private IDictionary<Trigger,string> transitionTable;
	private ActionRunner mainAction;

	public GameState(string name, IDictionary<Trigger,string> transitionTable, ActionRunner mainAction, 
	                 ActionRunner actionToInterrupt = null) {
		// TODO: add ongoing action support
		
		this.Name = name;
		this.transitionTable = transitionTable;
		this.mainAction = mainAction;
		if (actionToInterrupt != null) {
			actionToInterrupt.Interrupt();
		}
	}
	
	public void Start() {
		mainAction.Start();
	}
	
	// called every update
	public string GetNextState() {
	
		foreach (KeyValuePair<Trigger, string> entry in transitionTable) {
			var trigger = entry.Key;
			var targetState = entry.Value;
			if (trigger.IsTriggered()) {
				return targetState;
			}
		}
	
		return Name;
	}
	
	// To be used only for verification purposes in GameStateManager
	public ICollection<string> GetSuccessorStates() {
		return transitionTable.Values;
	}
}
