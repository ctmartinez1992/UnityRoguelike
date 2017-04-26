using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public Image splashImage;
	public string loadLevel;

	IEnumerator Start() {
		splashImage.canvasRenderer.SetAlpha(0f);

		splashImage.CrossFadeAlpha(1f, 1.5f, false);
		yield return new WaitForSeconds(2.5f);
		splashImage.CrossFadeAlpha(0f, 2.5f, false);
		yield return new WaitForSeconds(2.5f);

		SceneManager.LoadScene(loadLevel);
	}
}