#pragma strict

var newSkin : GUISkin;



function pauseMenu()
{
	GUI.BeginGroup(Rect(Screen.width/2 - 150, 50,300,250));
	
	
	
	EditorGUI.HelpBox(Rect(50,50,200,150),"To move around, press the control pad!",MessageType.Info);
	
	GUI.EndGroup();
}	

function OnGUI()
{
	GUI.skin = newSkin;
	pauseMenu();
}
