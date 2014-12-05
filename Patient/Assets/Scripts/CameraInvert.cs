using UnityEngine;
using System.Collections;

public class CameraInvert : MonoBehaviour 
{

	private bool inverted = false;
	public bool Inverted { 
		get { return inverted; }
		set 
		{
			if (inverted == value)
				return; // do nothing if they didn't actually update the value
			inverted = value;

			Camera[] cams = Camera.allCameras;
			
			foreach (Camera cam in cams){
				
				cam.ResetProjectionMatrix ();
			cam.ResetWorldToCameraMatrix ();
			cam.ResetProjectionMatrix ();
			if(inverted) 
			{
				cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3 (1, -1, 1));
			}
			}
		}
	}
	
	//initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void Restore()
	{
		Inverted = false;
	}
}
