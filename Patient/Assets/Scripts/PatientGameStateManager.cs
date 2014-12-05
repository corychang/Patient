using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// THIS IS THE FINAL VERSION OF THE GameStateManager.
public class PatientGameStateManager : GameStateManager {

	IList<GameState> GetScene1List() {
		// Initialization
		GameObject patient = GameObject.Find ("patient");
		SoundManager.Instance.addSound (new Sound ("Assets/Sounds/phoneRinging.mp3", "phoneRinging"));

		SoundAction phoneRing = new SoundAction ("phoneRinging", true);
		GameState scene1Start = new GameState (
			"scene1Start",
			new Dictionary<Trigger, string>() {
			{new ShakeTrigger(), "scene1Phone"}
			},
			new ParallelAction(new FadeAction(true), phoneRing, new ChildSelectorAction("Sibling", "sleep"))
		);
		GameState scene1Phone = new GameState (
			"scene1Phone",
			new Dictionary<Trigger, string>() {
			{new StareTrigger("phone"), "scene1Monologue"}
			},
			new FadeAction(false)
		);

		GameState scene1Monologue = new GameState (
			"scene1Monologue",
			new Dictionary<Trigger, string>() {
			{new MainActionFinishedTrigger(), "hallucination"}
			},
			new SequentialAction(
			new DialogAction("Mother: Honey, I just wanted to call to see if you’re all right."),
			new DialogAction("Mother: The doctor says you’ll make a full recovery from your accident, but he won’t tell me what happened..."),
			new DialogAction("Mother: He said the same last time, too."),
			new DialogAction("Mother: Do you know how I feel when you go missing?"),
			new DialogAction("Mother: Dad is- I- I know we don’t agree, but you can’t keep running away!"),
			new DialogAction("Every time!"),
			new DialogAction("Mother: ... "),
			new DialogAction("Mother: No, that’s not why I called… I just need to know if you’re okay."),
			new DialogAction("Mother: The doctor says you’ll be awake the next time Dad and I’ll visit, so get plenty of rest."),
			new DialogAction("Mother: We'll see you soon")
			),
			phoneRing
		);

		return new List<GameState> () {
			scene1Start,
			scene1Phone,
			scene1Monologue
		};
	}

	IList<GameState> GetScene2List() {
		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.mainCamera.GetComponent<DialogManager>();
		
		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		GameObject person = GameObject.Find ("person");
		soundManager.addSound (new Sound ("Assets/Sounds/surreal_sound4.mp3", "surrealSound"));
		
		
		GameObject obj = GameObject.Find ("Main Camera");
		if (obj == null) {
			Debug.Log ("couldn't find Main Camera");
		}	
		ParallelAction hallu = new ParallelAction (new CameraInvertAction(), new SoundAction("surrealSound", true));	
		SequentialAction visitor = new SequentialAction (
			new ChildSelectorAction("Sibling", "normalpose"),
			new DialogAction("Sibling: I’m glad to see you’re awake."),
			new DialogAction("Sibling: We’re in St. Paul’s Hospital which works with Lydersen Labs to develop pharmaceuticals."),
			new DialogAction("Sibling: Actually, I work here, but in the research side with people from Lydersen.")
		); 
		
		DialogAction WorldExpo = new DialogAction ("I’m your brother. Uh, I don’t know what to say… I work here and, \n" +
		                                           "um, I do research in epigenetics. \n" +
		                                           "This never happened before… I didn’t think that — the doctor did say you might…");
		
		if (dialog == null) {
			Debug.Log("null pointer with dialog");			
		}
		
		return new List<GameState> {
			new GameState(
				"hallucination",
				new Dictionary<Trigger, string>() {
				{new NodTrigger(), "Visitor"}
			},hallu
			),
			new GameState(
				"Visitor",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "question1"}
			},
			visitor, hallu
			),
			
			new GameState(
				"question1Prompt",
				new Dictionary<Trigger, string>(){{new MainActionFinishedTrigger(), "question1"}},
				new SequentialAction(new DialogAction("Sibling: We were really worried we were going to lose you, and there’s barely enough people around as it is…"),
					new DialogAction("Sibling: Do you remember your name?"))
			),

			new GameState(
				"question1",
				new Dictionary<Trigger, string>(){
				{new NodTrigger(), "question2prompt"},
				{new ShakeTrigger(), "question3prompt"}
				},
				new NoAction()
			),

			new GameState(
				"question2prompt",
				new Dictionary<Trigger, string>(){{new MainActionFinishedTrigger(), "question2"}},
				new DialogAction("Sibling: See, I knew the doctor was worrying too much. In that case, you remember me, right?")
			),

			new GameState(
				"question2",
				new Dictionary<Trigger, string>(){
				{new NodTrigger(), "scene3Hallucinate"},
				{new ShakeTrigger(), "question2No"}
			},
			new NoAction()
			),

			new GameState(
				"question2No",
				new Dictionary<Trigger, string>(){{new MainActionFinishedTrigger(), "WorldExpo"}},
			new DialogAction("I- um. I, well")
			),

			new GameState(
				"question3prompt",
				new Dictionary<Trigger, string>(){{new MainActionFinishedTrigger(), "question3"}},
				new DialogAction("Sibling: Wait, really? But that — In that case, do you remember me?")
			),

			new GameState(
				"question3",
				new Dictionary<Trigger, string>(){
				{new NodTrigger(), "question3Yes"},
				{new ShakeTrigger(), "question2No"}
			},
			new NoAction()
			),

			new GameState(
				"question3Yes",
				new Dictionary<Trigger, string>(){{new MainActionFinishedTrigger(), "WorldExpo"}},
				new DialogAction("Sibling: But then, how do — no, nevermind.")
			),
			
			new GameState(
				"WorldExpo",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "scene3Hallucinate"},
			},
			WorldExpo
			)
		};
	}
	
	IList<GameState> GetScene3List() {
		
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
			new SoundAction("surreal1", true),
			new ChildSelectorAction("Sibling", "lookflower")
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
			new DialogAction("Some people wanted to clone roses to see " +
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
	
	private IList<GameState> GetScene4List() {
		
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
				new Dictionary<Trigger, string>() {},
				new SequentialAction(
					// TODO: knock sound
					new DialogAction("Sibling: Yes?"),
					new SetAnimVarAction("DoorHinge", "Open", true),
					new DialogAction("Doctor: Visiting hours are almost over. There are some forms you need to sign."),
					new DialogAction("Sibling: Sure?"),
					new DialogAction("Sibling: I’ll be going now, but think about this."),
					// TODO: display credits
					// if yes, loop game
					// if no, main menu
					new IfVariableAction("scene4answer",
						new LoadLevelAction(0),
						new LoadLevelAction("Menu")
					)
				)
			)
		};	
	}

	protected override IList<GameState> GetGameStatesList() {
		return 
			GetScene1List()
			.Concat(GetScene2List())
			.Concat(GetScene3List())
			.Concat(GetScene4List())
			.ToList ();
	}
}
