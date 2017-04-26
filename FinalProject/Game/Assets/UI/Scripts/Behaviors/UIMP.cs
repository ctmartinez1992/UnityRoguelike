using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIMP : MonoBehaviour {

	public Player player;

	public Texture2D potionFull;
	public Texture2D potionHalf;
	public Texture2D potionEmpty;

	public Image potionToClone;

	public List<Image> potions;

	public GameObject parent;

	public int positionOfFirstPotion = 15;
	public int distanceBetweenPotions = 7;

	public void Start() {
		potions = new List<Image>();

		int maxPotions = player.maxHP;
		for(int i = 0; i < maxPotions; i += 2) {
			AddPotion();
		}

		UpdatePotions();
	}

	public void UpdatePotions() {
		int maxPotions = player.maxHP / 2;
		int currentPotions = Mathf.FloorToInt(player.hp / 2);
		int leftoverPotions = player.hp % 2;

		foreach(Image heart in potions) {
			if(currentPotions > 0) {
				heart.sprite = Sprite.Create(potionFull, new Rect(0, 0, potionFull.width, potionFull.height), new Vector2(0.5f, 0.5f));
				currentPotions--;
				maxPotions--;
			}
			else if(currentPotions <= 0 && leftoverPotions == 1) {
				heart.sprite = Sprite.Create(potionHalf, new Rect(0, 0, potionHalf.width, potionHalf.height), new Vector2(0.5f, 0.5f));
				leftoverPotions = 0;
				maxPotions--;
			}
			else if(maxPotions > 0) {
				heart.sprite = Sprite.Create(potionEmpty, new Rect(0, 0, potionEmpty.width, potionEmpty.height), new Vector2(0.5f, 0.5f));
				maxPotions--;
			}
		}
	}

	public void AddPotion() {
		Image potion = Instantiate(potionToClone);

		float newX = positionOfFirstPotion + (potions.Count * distanceBetweenPotions);
		potion.transform.position = new Vector3(newX, potion.transform.position.y, potion.transform.position.z);
		potion.transform.SetParent(parent.transform, false);

		potions.Add(potion);

		UpdatePotions();
	}
}