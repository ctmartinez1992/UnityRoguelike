using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour {

	[HideInInspector] public GenericWindow[] windows;

	public int currentWindowID;
	public int defaultWindowID;

	public bool startClosed;

	public GenericWindow GetWindow(int value) {
		return windows[value];
	}

	private void ToggleVisibility(int value) {
		int total = windows.Length;

		for(int i = 0; i < total; ++i) {
			GenericWindow window = windows[i];

			if(i == value) {
				window.Open();
			}
			else if(window.gameObject.activeSelf) {
				window.Close();
			}
		}
	}

	public GenericWindow Open(int value) {
		if(value < 0 || value > windows.Length) {
			return null;
		}

		currentWindowID = value;
		ToggleVisibility(currentWindowID);

		return GetWindow(currentWindowID);
	}

	void Start() {
		GenericWindow.manager = this;

		if(!startClosed) {
			Open(defaultWindowID);
		}
	}
}