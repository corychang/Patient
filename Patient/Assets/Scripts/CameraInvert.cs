using UnityEngine;
using System.Collections;

public class CameraInvert : MonoBehaviour 
{

	public bool Inverted { get; set; }
	//initialization
	void Start () 
	{
		Inverted = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Inverted) // invert when toggleInvert is true
		{
			transform.RotateAround(transform.position, Vector3.forward, 180.0f);
			Camera.main.transform.Translate(0f,2f,0f, Space.World);
		}
	}
}
