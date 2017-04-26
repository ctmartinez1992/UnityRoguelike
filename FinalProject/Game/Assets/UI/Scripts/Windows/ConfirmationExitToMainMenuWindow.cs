using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ConfirmationExitToMainMenuWindow : GenericWindow {

	public void OnYes() {
		SceneManager.LoadScene("MainMenu");
	}
}
