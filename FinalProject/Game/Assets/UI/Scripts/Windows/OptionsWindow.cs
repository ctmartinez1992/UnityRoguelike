using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsWindow : GenericWindow {

	public OptionsVideoWindow videoWindow;
	public OptionsAudioWindow audioWindow;
	public OptionsControlsWindow controlsWindow;
	public OptionsGameplayWindow gameplayWindow;

	public void OnVideo() {
		manager.Open((int)Windows.OptionsVideoWindow - 1);
	}

	public void OnAudio() {
		manager.Open((int)Windows.OptionsAudioWindow - 1);
	}

	public void OnControls() {
		manager.Open((int)Windows.OptionsControlsWindow - 1);
	}

	public void OnGameplay() {
		manager.Open((int)Windows.OptionsGameplayWindow - 1);
	}

	public void OnResetAllToDefault() {
		videoWindow.OnDefault();
		audioWindow.OnDefault();
		controlsWindow.OnDefault();
		gameplayWindow.OnDefault();
	}
}