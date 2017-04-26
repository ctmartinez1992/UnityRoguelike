using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsVideoWindow : GenericWindow {

	public string defaultResolution;
	public string defaultFullscreen;
	public string defaultVSync;

	public Selectable resolutionSelectable;
	public Selectable fullscreenSelectable;
	public Selectable vsyncSelectable;

	public void OnDefault() {
		resolutionSelectable.SetCurrentOptionByText(defaultResolution);
		fullscreenSelectable.SetCurrentOptionByText(defaultFullscreen);
		vsyncSelectable.SetCurrentOptionByText(defaultVSync);
	}
}