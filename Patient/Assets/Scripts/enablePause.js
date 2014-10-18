#pragma strict

function enablePause()
{
	if (Input.GetKey ("escape"))
	{
		Time.timeScale = 0; //pause game
		
		//show pause menu
		var pauseMenu = GetComponent("pauseMenu") as MonoBehaviour;
		pauseMenu.enabled = true; 
	}
}