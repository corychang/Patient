#pragma strict

var newSkin : GUISkin;



function pauseMenu()
{
	GUI.BeginGroup(Rect(Screen.width/2 - 150, 50,300,250));
	
	//menu box
	GUI.Box(Rect(0,0,300,250),"Patient");
	
	if (GUI.Button(Rect(55,100,180,40),"Resume"))
	{
		Time.timeScale = 1.0; //Unpause game
		
		//close pause box
		var pauseMenuScript = GetComponent("pauseMenu") as MonoBehaviour;
		pauseMenuScript.enabled = false;
		
	}
	
	if (GUI.Button(Rect(55,150,180,40), "Main Menu"))
	{
		Application.LoadLevel(0);
	}
	
	if (GUI.Button(Rect(55,200,180,40),"Quit"))
	{
	 	Application.Quit();
	}
	
	GUI.EndGroup();
}	

function OnGUI()
{
	GUI.skin = newSkin;
	pauseMenu();
}
