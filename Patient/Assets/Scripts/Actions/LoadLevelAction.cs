using UnityEngine;
using System.Collections;

public class LoadLevelAction : ActionRunner {

	private int levelNumber;
	public LoadLevelAction(int levelNumber) {
		this.levelNumber = levelNumber;
	}

	private string levelName;
	public LoadLevelAction(string levelName) {
		this.levelName = levelName;
	}
	
	public override void Start () {
		if (levelName == null)
			Application.LoadLevel(levelNumber);
		else
			Application.LoadLevel(levelName);
	}
	
	// This is done as soon as it starts, so there's no way to interrupt it.
	public override void Interrupt () {}
}

