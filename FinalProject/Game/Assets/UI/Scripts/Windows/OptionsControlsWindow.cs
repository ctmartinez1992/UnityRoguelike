using UnityEngine;
using System.Collections;

public class OptionsControlsWindow : GenericWindow {

	public KeyCode upKeyCode;
	public KeyCode downKeyCode;
	public KeyCode leftKeyCode;
	public KeyCode rightKeyCode;
	public KeyCode jumpKeyCode;
	public KeyCode attackKeyCode;
	public KeyCode artifactKeyCode;
	public KeyCode artifactNextKeyCode;
	public KeyCode artifactPrevKeyCode;
	public KeyCode pauseKeyCode;

	public ButtonMapper upMapper;
	public ButtonMapper downMapper;
	public ButtonMapper leftMapper;
	public ButtonMapper rightMapper;
	public ButtonMapper jumpMapper;
	public ButtonMapper attackMapper;
	public ButtonMapper artifactMapper;
	public ButtonMapper artifactNextMapper;
	public ButtonMapper artifactPrevMapper;
	public ButtonMapper pauseMapper;

	public void OnDefault() {
		upMapper.SetKeyCodeByDefault(upKeyCode);
		downMapper.SetKeyCodeByDefault(downKeyCode);
		leftMapper.SetKeyCodeByDefault(leftKeyCode);
		rightMapper.SetKeyCodeByDefault(rightKeyCode);
		jumpMapper.SetKeyCodeByDefault(jumpKeyCode);
		attackMapper.SetKeyCodeByDefault(attackKeyCode);
		artifactMapper.SetKeyCodeByDefault(artifactKeyCode);
		artifactNextMapper.SetKeyCodeByDefault(artifactNextKeyCode);
		artifactPrevMapper.SetKeyCodeByDefault(artifactPrevKeyCode);
		pauseMapper.SetKeyCodeByDefault(pauseKeyCode);
	}
}
