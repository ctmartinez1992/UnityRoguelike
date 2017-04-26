using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct KeyCodeImage {
	public KeyCode code;
	public Texture2D image;
}

public class ControlsKeyCode : MonoBehaviour {
	public KeyCodeImage[] codes;
}