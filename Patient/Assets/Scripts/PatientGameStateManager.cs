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
			new ParallelAction(new FadeAction(true), phoneRing)
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
			//new DialogAction("Mother: The doctor says you’ll make a full recovery from your accident, but he won’t tell me what happened..."),
			//new DialogAction("Mother: He said the same last time, too."),
			//new DialogAction("Mother: Do you know how I feel when you go missing?"),
			//new DialogAction("Mother: Dad is- I- I know we don’t agree, but you can’t keep running away!"),
			//new DialogAction("Every time!"),
			//new DialogAction("Mother: If we always disagree (about cloning), why do you bring it up!"),
			//new DialogAction("Mother: ... "),
			//new DialogAction("Mother: No, that’s not why I called… I just need to know if you’re okay."),
			//new DialogAction("Mother: The doctor says you’ll be awake the next time Dad and I’ll visit, so get plenty of rest."),
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
		
		List<ActionRunner> list = new List<ActionRunner> ();
		list.Add (new DialogAction ("How long have you been awake? Uh, actually, first, do you know where you are?"));
		SequentialAction visitor = new SequentialAction (list); 
		
		DialogAction no1 = new DialogAction ("Well, we’re in St. Paul’s Hospital. It works with Lydersen Labs to develop pharmaceuticals. " +
		                                     "Actually, I work here, but in the research side with people from Lydersen.");
		
		DialogAction yes1 = new DialogAction ("Oh, okay, that’s good.");
		
		DialogAction no2 = new DialogAction ("That’s fine, I’m just glad you’re okay. You got " +
		                                     "hit by a car. I don’t know exactly what happened, but you’ve been in a coma for a while.");
		
		DialogAction yes2 = new DialogAction ("Yeah, that was a pretty bad crash. We were worried you wouldn’t make it.");
		
		DialogAction preDoctorQs = new DialogAction ("Okay, you should remember who you are, right? " +
		                                             "The doctor gave me these questions to ask you, just making sure you’re all here.");
		
		DialogAction no3 = new DialogAction ("Wait, really? But that — In that case, do you remember me? ");
		
		DialogAction yes3 = new DialogAction ("See, I knew the doctor was worrying too much. In that case, you remember me, right?");
		
		DialogAction no4 = new DialogAction ("I- um. I, well.");
		
		DialogAction yes4 = new DialogAction ("But then, how do — no, nevermind.");
		
		DialogAction WorldExpo = new DialogAction ("I’m your brother. Uh, I don’t know what to say… I work here and, \n" +
		                                           "um, I do research in epigenetics. \n" +
		                                           "This never happened before… I didn’t think that — the doctor did say you might…");
		
		DialogAction question2 = new DialogAction ("Do you remember anything about how you got here?");
		
		
		NodTrigger nodTrigger = new NodTrigger (obj);
		ShakeTrigger shakeTrigger = new ShakeTrigger (obj);
		
		if (dialog == null) {
			Debug.Log("null pointer with dialog");			
		}
		
		return new List<GameState> {
			new GameState(
				"hallucination",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "Visitor"}
			},hallu
			),
			new GameState(
				"Visitor",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "QATime1"}
			},
			visitor, hallu
			),
			
			new GameState(	
			              "QATime1",
			              new Dictionary<Trigger, string>() {
				{shakeTrigger, "no1"},
				{nodTrigger, "yes1"}
			},
			new NoAction()
			),
			
			
			new GameState(
				"no1",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "question2"}
			},
			no1
			),
			new GameState(
				"yes1",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "question2"}
			},
			yes1
			),
			
			new GameState(
				"question2",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "QATime2"}
			},
			question2
			),
			
			new GameState(
				"QATime2",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "no2"},
				{nodTrigger, "yes2"}
			},
			new NoAction()
			),
			
			new GameState(
				"no2",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "question3"}
			},
			no2
			),
			
			new GameState(
				"yes2",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "question3"}
			},
			yes2
			),
			
			
			new GameState(
				"question3",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "preQATime3"}
			},
			preDoctorQs
			),
			
			new GameState(
				"preQATime3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "question4_no"},
				{nodTrigger, "question4_yes"}
			},
			new NoAction()
			),
			
			new GameState(
				"question4_no",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "no3"}
			},
			no3
			),
			
			new GameState(
				"no3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "no4"},
				{nodTrigger, "scene3Hallucinate"}
			},
			new NoAction()
			),
			
			
			new GameState(
				"question4_yes",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "yes3"}
			},
			yes3
			),
			
			new GameState(
				"yes3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "no4"},
				{nodTrigger, "yes4"}
			},
			new NoAction()
			),
			
			new GameState(
				"no4",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "WorldExpo"},
			},
			no4
			),
			
			new GameState(
				"yes4",
				new Dictionary<Trigger, string>() {
				{new MainActionFinishedTrigger(), "WorldExpo"},
			},
			yes4
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
