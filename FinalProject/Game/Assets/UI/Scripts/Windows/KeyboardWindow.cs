using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow {

	public Text inputField;
	public int maxCharacters = 7;

	private float delay = 0;
	private float cursorDelay = .5f;
	private bool blink;
	private string text = "";

	public void OnKeyPress(string key) {
		if(text.Length < maxCharacters) {
			text += key;
		}
	}

	public void OnDelete() {
		if(text.Length > 0) {
			text = text.Remove(text.Length - 1);
		}
	}

	public void OnAccept() {
		OnNextWindow();
	}

	public void OnCancel() {
		OnPreviousWindow();
	}

	void Update() {
		string updateText = text;

		if(text.Length < maxCharacters) {
			updateText += "_";

			if(blink) {
				updateText = updateText.Remove(updateText.Length - 1);
			}
		}

		inputField.text = updateText;

		delay += Time.deltaTime;
		if(delay > cursorDelay) {
			delay = 0;
			blink = !blink;
		}
	}
}