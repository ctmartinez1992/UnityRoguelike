using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Selectable : MonoBehaviour {
    
	public PlayerMainMenu player;

	public Button button;
	public Text textField;

	public string[] options;
	public int currentOption;

	public float timeElapsed;

	void Update() {
		if(EventSystem.current.currentSelectedGameObject && button.name == EventSystem.current.currentSelectedGameObject.name) {
			bool left = player.playerInput.GetAxisRaw("UI Horizontal") < 0f;
			bool right = player.playerInput.GetAxisRaw("UI Horizontal") > 0f;

			if(left && timeElapsed > .5f) {
				currentOption--;
				if(currentOption < 0) {
					currentOption = options.Length - 1;
				}

				SetTextFieldValue();

				timeElapsed = 0f;
			}
			if(right && timeElapsed > .5f) {
				currentOption++;
				if(currentOption >= options.Length) {
					currentOption = 0;
				}

				SetTextFieldValue();

				timeElapsed = 0f;
			}
		}

		timeElapsed += Time.deltaTime;
	}

	void SetTextFieldValue() {
		textField.text = options[currentOption];
	}

	public void SetCurrentOptionByText(string optionName) {
		for(int i = 0; i < options.Length; ++i) {
			if(options[i] == optionName) {
				currentOption = i;
				SetTextFieldValue();
				timeElapsed = 0f;
			}
		}
	}
}