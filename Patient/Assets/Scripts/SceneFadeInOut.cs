using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 0.5f;
	private bool sceneStarting = true;

	public void FadeToClear() {

		//guiTexture.color = Color.Lerp (guiTexture.color, Camera.main.backgroundColor, fadeSpeed * Time.deltaTime);

		if (guiTexture.color.a <= 0.05f) {
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			sceneStarting = false;
		}
		guiTexture.enabled = false;
	}

	public void FadeToBlack() {
		Debug.Log("Fading...");
		guiTexture.enabled = true;
		guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
		//guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

}
