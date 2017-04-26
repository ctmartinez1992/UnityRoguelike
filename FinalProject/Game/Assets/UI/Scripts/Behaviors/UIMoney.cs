using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIMoney : MonoBehaviour {

	public Player player;

	public Text textAmount;

	public int currentAmount;

	void Start() {
		UpdateTextAmount();
	}

	void Update() {
		UpdateTextAmount();
	}

	public void UpdateTextAmount() {
		if(currentAmount != player.money) {
			currentAmount += (player.money > currentAmount) ? 1 : -1;
		}

		textAmount.text = currentAmount.ToString();
	}
}