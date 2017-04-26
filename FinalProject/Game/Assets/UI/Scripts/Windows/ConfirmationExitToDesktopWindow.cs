using UnityEngine;
using System.Collections;

public class ConfirmationExitToDesktopWindow : GenericWindow {

	public void OnYes() {
		Application.Quit();
	}
}