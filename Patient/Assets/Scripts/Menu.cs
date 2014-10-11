using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private string  MyText = "Default Text";
	private string MyTextField = "Default";

	void OnGUI()
	{

		GUILayout.BeginArea(new Rect (50, 50, 400, Screen.width / 2));

		GUILayout.BeginHorizontal();

		GUILayout.Label ("Menu");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Button ("Home", GUILayout.Width (50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Button ("Pause", GUILayout.Width (50));
		GUILayout.EndHorizontal();
				
		GUILayout.BeginHorizontal();
		GUILayout.Button ("Resume", GUILayout.Width (60));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Button ("Save", GUILayout.Width (50));
		GUILayout.EndHorizontal();


		GUILayout.EndArea();
	}
}
