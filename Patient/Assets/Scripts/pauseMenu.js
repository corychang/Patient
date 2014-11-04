#pragma strict

var newSkin : GUISkin;



function pauseMenu()
{
	var groupY = 50;
	var groupWidth = 300;
	var groupHeight = 250;
	GUI.BeginGroup(Rect(Screen.width/2 - 150, groupY,groupWidth,groupHeight));
	
	var butx = 55;
	var buty = 100;
	var butWidth = 180;
	var butHeight = 40;
	
	var boxX = 0;
	var boxY = 0;
	var boxWidth = 300;
	var boxHeight = 250;
	//menu box
	GUI.Box(Rect(boxX, boxY, boxWidth, boxHeight),"Patient");
	
	if (GUI.Button(Rect(butx,buty,butWidth,butHeight),"Resume"))
	{
		Time.timeScale = 1.0; //Unpause game
		
		//close pause box
		var pauseMenuScript = GetComponent("pauseMenu") as MonoBehaviour;
		pauseMenuScript.enabled = false;
		
	}
	
	if (GUI.Button(Rect(butx,buty+50,butWidth,butHeight), "Main Menu"))
	{
		Application.LoadLevel(0);
	}
	
	if (GUI.Button(Rect(butx,buty+100,butWidth,butHeight),"Quit"))
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
