using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsAudioWindow : GenericWindow {

	public int defaultSound;
	public int defaultMusic;

	public Slider soundSlider;
	public Slider musicSlider;

	public void OnDefault() {
		soundSlider.SetSliderHandleByValue(defaultSound);
		musicSlider.SetSliderHandleByValue(defaultMusic);
	}
}