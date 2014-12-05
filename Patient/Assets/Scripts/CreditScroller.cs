using UnityEngine;
using System.Collections;

public class CreditScroller : MonoBehaviour {

	/* Public Variables    */
	public GameObject camera;
	public int speed;
	public string level;

	//Update is called once per frame
	void Update () {
		camera.transform.Translate (Vector3.down * Time.deltaTime * speed);

	}

	IEnumerator waitFor() {

		if (!level.Equals("null")) {
			yield return new WaitForSeconds (20);
			Application.LoadLevel (level);
		}
	}

}
