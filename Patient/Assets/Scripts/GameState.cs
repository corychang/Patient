using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState {

	private string name;
	private IDictionary<Trigger,string> transitionTable;
	private Action mainAction;

	public GameState(string name, IDictionary<Trigger,string> transitionTable, Action mainAction) {
		// TODO: add ongoing action support
		
		this.name = name;
		this.transitionTable = transitionTable;
		this.mainAction = mainAction;
	}
}
