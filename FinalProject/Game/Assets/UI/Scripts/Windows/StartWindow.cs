using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow : GenericWindow {

	public Button continueButton;

	public override void Open() {
		bool canContinue = true;

		continueButton.gameObject.SetActive(canContinue);

		if(continueButton.gameObject.activeSelf) {
			firstSelected = continueButton.gameObject;
		}

		base.Open();
	}

	public void NewGame() {
		SceneManager.LoadScene("Level1");
	}

	public void Continue() {
		Debug.Log("Continue Pressed");
	}

	public void Options() {
		manager.Open((int)Windows.OptionsWindow - 1);
	}

	public void ExitGame() {
		manager.Open((int)Windows.ConfirmationExitToDesktopWindow - 1);
	}
}