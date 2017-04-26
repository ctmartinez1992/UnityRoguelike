using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Slider : MonoBehaviour {
    
	public PlayerMainMenu player;

	public Button button;

	public RectTransform sliderHandle;

	public int min;
	public int max;
	public int increment;
	public int currentValue;

	public float timeElapsed;

	void Update() {
		if(EventSystem.current.currentSelectedGameObject && button.name == EventSystem.current.currentSelectedGameObject.name) {
			bool left = player.playerInput.GetAxisRaw("UI Horizontal") < 0f;
			bool right = player.playerInput.GetAxisRaw("UI Horizontal") > 0f;

			if(left && timeElapsed > .1f) {
				currentValue -= increment;
				if(currentValue < min) {
					currentValue = min;
				}

				SetSliderHandle();
				timeElapsed = 0f;
			}
			if(right && timeElapsed > .1f) {
				currentValue += increment;
				if(currentValue > max) {
					currentValue = max;
				}

				SetSliderHandle();
				timeElapsed = 0f;
			}
		}

		timeElapsed += Time.deltaTime;
	}

	void SetSliderHandle() {
		sliderHandle.localPosition = new Vector3(currentValue - (max - min) / 2, sliderHandle.localPosition.y, sliderHandle.localPosition.z);
	}

	public void SetSliderHandleByValue(int value) {
		if(currentValue >= min && currentValue <= max) {
			currentValue = value;
			SetSliderHandle();
		}
	}
}