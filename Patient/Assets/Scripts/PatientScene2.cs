using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatientScene2	 : GameStateManager {
	
	/**
	 * Creates the gameState for the PatientScene
	 */
	protected override IList<GameState> GetGameStatesList() {
		
		//Gets the Dialog object from the camera for the dialog action
		DialogManager dialog = Camera.mainCamera.GetComponent<DialogManager>();
		
		SoundManager soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		AnimationManager animationManager = GameObject.Find ("animationManager").GetComponent<AnimationManager> ();
		GameObject person = GameObject.Find ("person");
		AnimationClip animation = person.GetComponent<Animation> ().GetClip ("Take 001");
		soundManager.addSound (new Sound (person, "Assets\\surreal sounds 4.mp3", "surrealSound"));


		GameObject obj = GameObject.Find ("Main Camera");
		if (obj == null) {
			Debug.Log ("couldn't find Main Camera");
		}	

		List<ActionRunner> surrealEffects = new List<ActionRunner> ();
		surrealEffects.Add (new CameraInvertAction());
		surrealEffects.Add (new SoundAction("surrealSound", true));
		ParallelAction hallu = new ParallelAction (surrealEffects);

		List<ActionRunner> list = new List<ActionRunner> ();
		list.Add (new DialogAction ("How long have you been awake? Uh, actually, first, do you know where you are?", 4, dialog));
		SequentialAction visitor = new SequentialAction (list); 

		DialogAction no1 = new DialogAction ("Well, we’re in St. Paul’s Hospital. It works with Lydersen Labs to develop pharmaceuticals. " +
			"Actually, I work here, but in the research side with people from Lydersen.", 3, dialog);

		DialogAction yes1 = new DialogAction ("Oh, okay, that’s good.", 2, dialog);

		DialogAction no2 = new DialogAction ("That’s fine, I’m just glad you’re okay. You got " +
		                                     "hit by a car. I don’t know exactly what happened, but you’ve been in a coma for a while.", 3, dialog);
		
		DialogAction yes2 = new DialogAction ("Yeah, that was a pretty bad crash. We were worried you wouldn’t make it.", 2, dialog);

		DialogAction preDoctorQs = new DialogAction ("Okay, you should remember who you are, right? " +
						"The doctor gave me these questions to ask you, just making sure you’re all here.", 4, dialog);

		DialogAction no3 = new DialogAction ("Wait, really? But that — In that case, do you remember me? ", 2, dialog);

		DialogAction yes3 = new DialogAction ("See, I knew the doctor was worrying too much. In that case, you remember me, right?", 2, dialog);

		DialogAction no4 = new DialogAction ("I- um. I, well.", 2, dialog);

		DialogAction yes4 = new DialogAction ("But then, how do — no, nevermind.",2,  dialog);

		DialogAction WorldExpo = new DialogAction ("I’m your brother. Uh, I don’t know what to say… I work here and, \n" +
						"um, I do research in epigenetics. \n" +
						"This never happened before… I didn’t think that — the doctor did say you might…", 5, dialog);

		DialogAction question2 = new DialogAction ("Do you remember anything about how you got here?", 2, dialog);


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
					{new AllActionsFinishedTrigger(visitor), "QATime1"}
				},
				visitor, hallu
			),
					 
			new GameState(	
				"QATime1",
				new Dictionary<Trigger, string>() {
					{shakeTrigger, "no1"},
					{nodTrigger, "yes1"}
				},
				new DialogAction("", 0, dialog)
			),


			new GameState(
				"no1",
				new Dictionary<Trigger, string>() {
					{new AllActionsFinishedTrigger(no1), "question2"}
				},
				no1
			),
			new GameState(
				"yes1",
				new Dictionary<Trigger, string>() {
					{new AllActionsFinishedTrigger(yes1), "question2"}
				},
				yes1
			),

			new GameState(
				"question2",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(question2), "QATime2"}
			},
			question2
			),
			
			new GameState(
				"QATime2",
				new Dictionary<Trigger, string>() {
					{shakeTrigger, "no2"},
					{nodTrigger, "yes2"}
				},
				new DialogAction("", 0, dialog)
			),

			new GameState(
				"no2",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(no2), "question3"}
			},
			no2
			),

			new GameState(
				"yes2",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(yes2), "question3"}
			},
			yes2
			),


			new GameState(
				"question3",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(preDoctorQs), "preQATime3"}
			},
			preDoctorQs
			),
			
			new GameState(
				"preQATime3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "question4_no"},
				{nodTrigger, "question4_yes"}
			},
				new DialogAction("", 0, dialog)
			),

			new GameState(
				"question4_no",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(no3), "no3"}
			},
			no3
			),
			
			new GameState(
				"no3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "no4"},
				{nodTrigger, "Scene3"}
			},
			new DialogAction("", 0, dialog)
			),


			new GameState(
				"question4_yes",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(no3), "yes3"}
			},
			yes3
			),

			new GameState(
				"yes3",
				new Dictionary<Trigger, string>() {
				{shakeTrigger, "no4"},
				{nodTrigger, "yes4"}
			},
			new DialogAction("", 0, dialog)
			),

			new GameState(
				"no4",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(no4), "WorldExpo"},
			},
				no4
			),

			new GameState(
				"yes4",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(yes4), "WorldExpo"},
			},
			yes4
			),
			
			
			
			new GameState(
				"WorldExpo",
				new Dictionary<Trigger, string>() {
				{new AllActionsFinishedTrigger(WorldExpo), "Scene3"},
			},
			WorldExpo
			),

			new GameState(
				"Scene3",
				new Dictionary<Trigger, string>() {
			},
			new DialogAction("", 0, dialog)
			)
		};
	}
}

