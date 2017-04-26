using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow {

	public Text leftStatsLabel;
	public Text leftStatsValues;
	public Text rightStatsLabel;
	public Text rightStatsValues;
	public Text scoreValue;

	public int totalStats = 6;
	public int statsPerColumn = 3;
	private int currentStat = 0;

	public float statsDelay = 1f;
	private float delay = 0;

	private void UpdateStatsText(Text label, Text value) {
		label.text += "Stat " + currentStat + "\n";
		value.text += Random.Range(0, 1000).ToString("D4") + "\n";
	}

	private void ShowNextStat() {
		if(currentStat > totalStats - 1) {
			scoreValue.text = Random.Range(0, 1000000000).ToString("D10");
			currentStat = -1;
			return;
		}

		if(currentStat < statsPerColumn) {
			UpdateStatsText(leftStatsLabel, leftStatsValues);
		}
		else {

			UpdateStatsText(rightStatsLabel, rightStatsValues);
		}

		currentStat++;
	}

	public void ClearText() {
		leftStatsLabel.text = "";
		leftStatsValues.text = "";
		rightStatsLabel.text = "";
		rightStatsValues.text = "";
		scoreValue.text = "";
	}

	public void OnNext() {
		OnNextWindow();
	}

	public override void Open() {
		ClearText();
		base.Open();
	}

	public override void Close() {
		base.Close();
		currentStat = 0;
	}

	void Update() {
		delay += Time.deltaTime;

		if(delay > statsDelay && currentStat != -1) {
			ShowNextStat();
			delay = 0;
		}
	}
}
