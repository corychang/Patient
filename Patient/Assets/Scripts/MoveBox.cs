using UnityEngine;
using System.Collections;

public class MoveBox : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		print ("Entered trigger!");
		//GameObject obj = other.gameObject;
		//obj.transform.position = obj.transform.position + Vector3.right * 5 + Vector3.up*2;
		other.rigidbody.velocity = new Vector3 (5, 20, 5);
	}

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}



