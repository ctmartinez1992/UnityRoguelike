using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    private GameObject optionsPanel;
    private GameObject videoPanel;
    private GameObject audioPanel;
    private GameObject controlsPanel;
    private GameObject gameplayPanel;
    private GameObject confirmationExitPanel;

    private Selectable videoResolutionSelectable;
    private Selectable videoFullscreenSelectable;
    private Selectable videoVSyncSelectable;
    private Slider audioSoundSlider;
    private Slider audioMusicSlider;
    private ButtonMapper controlsUpMapper;
    private ButtonMapper controlsDownMapper;
    private ButtonMapper controlsLeftMapper;
    private ButtonMapper controlsRightMapper;
    private ButtonMapper controlsInventoryMapper;
    private Selectable gameplayLanguageSelectable;
    private Selectable gameplayLoseMoneyOnDeathSelectable;

    public string defaultVideoResolution;
    public string defaultVideoFullscreen;
    public string defaultVideoVSync;
    public int defaultAudioSound;
    public int defaultAudioMusic;
    public KeyCode defaultControlsUpKeyCode;
    public KeyCode defaultControlsDownKeyCode;
    public KeyCode defaultControlsLeftKeyCode;
    public KeyCode defaultControlsRightKeyCode;
    public KeyCode defaultControlsInventoryKeyCode;
    public string defaultGameplayLanguage;
    public string defaultGameplayLoseMoneyOnDeath;

    private void Start() {
        optionsPanel = GameObject.Find("OptionsPanel");
        videoPanel = GameObject.Find("OptionsVideoPanel");
        audioPanel = GameObject.Find("OptionsAudioPanel");
        controlsPanel = GameObject.Find("OptionsControlsPanel");
        gameplayPanel = GameObject.Find("OptionsGameplayPanel");
        confirmationExitPanel = GameObject.Find("ConfirmationExitPanel");

        videoResolutionSelectable = videoPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionResolution").GetComponent<Selectable>();
        videoFullscreenSelectable = videoPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionFullscreen").GetComponent<Selectable>();
        videoVSyncSelectable = videoPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionVSync").GetComponent<Selectable>();
        audioSoundSlider = audioPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionSound").GetComponent<Slider>();
        audioMusicSlider = audioPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionMusic").GetComponent<Slider>();
        controlsUpMapper = controlsPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionUp").GetComponent<ButtonMapper>();
        controlsDownMapper = controlsPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionDown").GetComponent<ButtonMapper>();
        controlsLeftMapper = controlsPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionLeft").GetComponent<ButtonMapper>();
        controlsRightMapper = controlsPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionRight").GetComponent<ButtonMapper>();
        controlsInventoryMapper = controlsPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionInventory").GetComponent<ButtonMapper>();
        gameplayLanguageSelectable = gameplayPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionLanguage").GetComponent<Selectable>();
        gameplayLoseMoneyOnDeathSelectable = gameplayPanel.transform.FindChild("Options").FindChild("Right").FindChild("OptionLoseMoneyOnDeath").GetComponent<Selectable>();

        optionsPanel.SetActive(false);
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        confirmationExitPanel.SetActive(false);
    }

    public void Activate() {
        optionsPanel.SetActive(true);
    }

    public void Deactivate() {
        optionsPanel.SetActive(false);
    }

    public void Toggle() {
        if(optionsPanel.activeSelf) {
            Deactivate();
        }
        else {
            Activate();
        }
    }

    public void ActivateVideo() {
        videoPanel.SetActive(true);
    }

    public void DeactivateVideo() {
        videoPanel.SetActive(false);
    }

    public void ToggleVideo() {
        if(videoPanel.activeSelf) {
            DeactivateVideo();
        }
        else {
            ActivateVideo();
        }
    }

    public void ActivateAudio() {
        audioPanel.SetActive(true);
    }

    public void DeactivateAudio() {
        audioPanel.SetActive(false);
    }

    public void ToggleAudio() {
        if(audioPanel.activeSelf) {
            DeactivateAudio();
        }
        else {
            ActivateAudio();
        }
    }

    public void ActivateControls() {
        controlsPanel.SetActive(true);
    }

    public void DeactivateControls() {
        controlsPanel.SetActive(false);
    }

    public void ToggleControls() {
        if(controlsPanel.activeSelf) {
            DeactivateControls();
        }
        else {
            ActivateControls();
        }
    }

    public void ActivateGameplay() {
        gameplayPanel.SetActive(true);
    }

    public void DeactivateGameplay() {
        gameplayPanel.SetActive(false);
    }

    public void ToggleGameplay() {
        if(gameplayPanel.activeSelf) {
            DeactivateGameplay();
        }
        else {
            ActivateGameplay();
        }
    }

    public void ActivateConfirmationExit() {
        confirmationExitPanel.SetActive(true);
    }

    public void DeactivateConfirmationExit() {
        confirmationExitPanel.SetActive(false);
    }

    public void ToggleConfirmationExit() {
        if(confirmationExitPanel.activeSelf) {
            DeactivateConfirmationExit();
        }
        else {
            ActivateConfirmationExit();
        }
    }

    public void ResetAllToDefault() {
        ResetVideoPanelToDefault();
        ResetAudioPanelToDefault();
        ResetControlsPanelToDefault();
        ResetGameplayPanelToDefault();
    }

    public void ResetVideoPanelToDefault() {
        videoResolutionSelectable.SetCurrentOptionByText(defaultVideoResolution);
        videoFullscreenSelectable.SetCurrentOptionByText(defaultVideoFullscreen);
        videoVSyncSelectable.SetCurrentOptionByText(defaultVideoVSync);
    }

    public void ResetAudioPanelToDefault() {
        audioSoundSlider.SetSliderHandleByValue(defaultAudioSound);
        audioMusicSlider.SetSliderHandleByValue(defaultAudioMusic);
    }

    public void ResetControlsPanelToDefault() {
        controlsUpMapper.SetKeyCodeByDefault(defaultControlsUpKeyCode);
        controlsDownMapper.SetKeyCodeByDefault(defaultControlsDownKeyCode);
        controlsLeftMapper.SetKeyCodeByDefault(defaultControlsLeftKeyCode);
        controlsRightMapper.SetKeyCodeByDefault(defaultControlsRightKeyCode);
        controlsInventoryMapper.SetKeyCodeByDefault(defaultControlsInventoryKeyCode);
    }

    public void ResetGameplayPanelToDefault() {
        gameplayLanguageSelectable.SetCurrentOptionByText(defaultGameplayLanguage);
        gameplayLoseMoneyOnDeathSelectable.SetCurrentOptionByText(defaultGameplayLoseMoneyOnDeath);
    }
}
