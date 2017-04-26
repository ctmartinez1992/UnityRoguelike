using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rewired;

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class PlayerMainMenu : MonoBehaviour {

	private int playerInputId = 0;
	public Rewired.Player playerInput;

	private void Start() {
		if(playerInput == null) {
			playerInput = ReInput.players.GetPlayer(playerInputId);
		}
	}

	private void Update() {
	}

	//If a certain class needs to access the player's input and can't be sure if the playerInput variable is assigned, it should first check if the variable is not null, if it is, it should call this function.
	public void GetPlayer() {
		playerInput = ReInput.players.GetPlayer(playerInputId);
	}
}