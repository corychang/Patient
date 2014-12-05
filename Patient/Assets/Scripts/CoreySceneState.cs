using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreySceneState : GameStateManager {

	/**
	 * Creates the gameState for the scene
	 */
	protected override IList<GameState> GetGameStatesList() {

		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.main.GetComponent<DialogManager>();
		GameObject patient = GameObject.Find ("patient");

		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject obj = GameObject.FindGameObjectWithTag ("MainCamera");
		if (obj == null) {
			Debug.LogError ("couldn't find Main Camera");
		}
		soundManager.addSound (new Sound (obj, "Assets/Sounds/surreal_sound1.mp3", "surreal1"));
		soundManager.addSound (new Sound (GameObject.FindGameObjectWithTag ("MainCamera"),
		                                           "Assets/Sounds/gibberish.mp3", "gibberish"));


		ActionRunner scene3HallucinateAction = new ParallelAction(
			new List<ActionRunner>() {
				new CameraInvertAction(),
				new LightPulseAction(), //TODO: Corey also sound affect
				new SoundAction("surreal1", true)
			}
		);
		GameState scene3Hallucinate = new GameState (
			"scene3Hallucinate",
			new Dictionary<Trigger, string> {
			{new ShakeTrigger(patient), "scene3RoseDialog"}
			},
			scene3HallucinateAction
		);

		ActionRunner scene3RoseDialogAction = new SequentialAction (
			new List<ActionRunner>(){
				new DialogAction("Whoa, what are you doing?"),
				new DialogAction("You okay?"),				
				new DialogAction("I hope the flower Mom brought actually works"),
				new DialogAction("I mean, it does mean \"get-well-soon\""),
			}
		);
		GameState scene3RoseDialog = new GameState (
			"scene3RoseDialog",
			new Dictionary<Trigger, string> () {
			{new MainActionFinishedTrigger(), "scene3Rose"}
			},	
			scene3RoseDialogAction,
			scene3HallucinateAction
		);

		GameState scene3Rose = new GameState(
			"scene3Rose",
			new Dictionary<Trigger, string> () {
			{new StareTrigger(patient, "rose"), "scene3Story"}
			},
			new NoAction()
		);


		ActionRunner scene3StoryAction = new SequentialAction (new List<ActionRunner> (){
			new DialogAction("They weren't originally blue; they were red."),
			new DialogAction("Some people wanted to clone roses to see if they could breed them " +
			                 "true and get a reliable variety they could stock and sell."),
			new DialogAction("For some reason, " +
			                 "they got brown flowers instead of the red ones they wanted so they were about to stop " +
			                 "the funding, but they finally tried cloning the brown roses."),
			new DialogAction("The next clones were blue " +
			                 "and bred true enough for commercial purposes."),
			new DialogAction("The thing is, everyone wasn't too bothered " +
			"about roses being cloned, but when they moved the project to animals from roses, " +
			"that was when it went to hell."),
			new DialogAction("See, after the whole thing with the roses, there were ideas " +
			                 "to fix the underpopulation problem with cloning, but there was a crowd of people against " +
			                 "it."),
			new DialogAction("It was risky and unreliable, actually, the whole roses debacle was really a lucky bit."),
			new DialogAction("Mom is against it, as a matter of fact —it's why you two got into fights.")
		});

		GameState scene3Story = new GameState (
			"scene3Story",
			new Dictionary<Trigger, string>(),
			scene3StoryAction
		);
	
		return new List<GameState> {
			scene3Hallucinate,
			scene3RoseDialog,
			scene3Rose,
			scene3Story
		};
	}
}
