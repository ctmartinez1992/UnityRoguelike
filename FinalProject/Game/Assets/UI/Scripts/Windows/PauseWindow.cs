using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseWindow : GenericWindow {

	public ConfirmationExitToDesktopWindow exitToDesktopWindow;
	public ConfirmationExitToMainMenuWindow exitToMainMenuWindow;

	public GameObject gameGO;
	//public PauseGame pauseGameScript;

	public void OnReturn() {
		gameGO.SetActive(true);
		//pauseGameScript.paused = false;

		this.Close();
	}

	public void OnOptions() {
		manager.Open((int)Windows.OptionsWindow - 1);
	}

	public void OnExitToMainMenu() {
		manager.Open((int)Windows.ConfirmationExitToMainMenuWindow - 1);
	}

	public void OnExitToDesktop() {
		manager.Open((int)Windows.ConfirmationExitToDesktopWindow - 1);
	}
}