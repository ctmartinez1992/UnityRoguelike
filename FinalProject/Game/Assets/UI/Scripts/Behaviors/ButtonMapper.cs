using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Rewired;

public class ButtonMapper : MonoBehaviour {

	public PlayerMainMenu player;
	public Rewired.InputManager inputManager;
	public ControlsKeyCode controlsImageMapper;

	public Text textPress;
	public Image imageButton;

	public Button button;

	public string associatedAction;
	public KeyCode associatedKey;
	public bool isAxis;
	public Rewired.Pole axisPole;

	public bool inEditMode;

	public float timeElapsed;

	void Awake() {
		if(player.playerInput == null) {
			player.GetPlayer();
		}

		if(isAxis) {
			foreach (ActionElementMap map in player.playerInput.controllers.maps.ElementMapsWithAction(associatedAction, true)) {
				if(map.axisContribution == axisPole) {
					associatedKey = map.keyCode;
					SetImage();
				}
			}
		}
		else {
			ActionElementMap map = player.playerInput.controllers.maps.GetFirstElementMapWithAction(associatedAction, true);
			if(map == null) {
				Debug.Log(associatedAction);
			}
			associatedKey = map.keyCode;
			SetImage();
		}
	}

	void Update() {
		textPress.enabled = inEditMode;
		imageButton.enabled = !inEditMode;

		if(EventSystem.current.currentSelectedGameObject && button.name == EventSystem.current.currentSelectedGameObject.name) {
			if(inEditMode) {
				Rewired.ControllerPollingInfo pollingInfo = player.playerInput.controllers.Keyboard.PollForFirstKeyDown();

				if(pollingInfo.keyboardKey != KeyCode.None) {
					associatedKey = pollingInfo.keyboardKey;
					SetImage();

					inEditMode = false;

					/*
					Rewired.ControllerMap map = guiInput.player.controllers.maps.GetMap<Rewired.ControllerMap>(0, 0);
					Rewired.ActionElementMap[] allElements = map.GetElementMaps();

					for (int i = 0; i < allElements.Length; ++i) {
						Debug.Log (allElements[i].keyCode);
						if(allElements[i].keyCode == pollingInfo.keyboardKey) {
							Rewired.ActionElementMap element = allElements[i];

							map.ReplaceElementMap(
								new Rewired.ElementAssignment(
									map.controllerType,
									element.elementType,
									element.elementIdentifierId,
									element.axisRange,
									pollingInfo.keyboardKey,
									element.modifierKeyFlags,
									element.actionId,
									element.axisContribution,
									element.invert,
									element.id));

							break;
						}
					}

					/*for(int i = 0; i < inputManager.userData.GetKeyboardMap (0, 0).actionElementMaps.Count; ++i) {
						if (inputManager.userData.GetKeyboardMap (0, 0).GetActionElementMap (i).keyboardKeyCode == Rewired.KeyboardKeyCode.UpArrow) {
							Rewired.ActionElementMap tmp = inputManager.userData.GetKeyboardMap (0, 0).GetActionElementMap (i).

							inputManager.userData.GetKeyboardMap (0, 0).DeleteActionElementMap(i)
						}
					}*/

					timeElapsed = 0f;
				}
			}
			else {
				bool enter = player.playerInput.GetButton("UI Submit");

				if(enter && timeElapsed > .5f) {
					inEditMode = true;
					timeElapsed = 0f;
				}
			}
		}

		timeElapsed += Time.deltaTime;
	}

	public void SetImage() {
		for(int i = 0; i < controlsImageMapper.codes.Length; ++i) {
			if(controlsImageMapper.codes[i].code == associatedKey) {
				Texture2D tex = controlsImageMapper.codes[i].image;
				imageButton.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
			}
		}
	}

	public void SetKeyCodeByDefault(KeyCode keyCode) {
		associatedKey = keyCode;
		SetImage();
	}
}