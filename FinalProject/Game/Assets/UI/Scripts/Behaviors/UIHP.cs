using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIHP : MonoBehaviour {
	
	public Player player;

	public Texture2D heartFull;
	public Texture2D heartHalf;
	public Texture2D heartEmpty;

	public Image heartToClone;

	public List<Image> hearts;

	public GameObject parent;

	public int positionOfFirstHeart = 15;
	public int distanceBetweenHearts = 7;

	public void Start() {
		hearts = new List<Image>();

		int maxHearts = player.maxHP;
		for(int i = 0; i < maxHearts; i += 2) {
			AddHeart();
		}

		UpdateHearts();
	}

	public void UpdateHearts() {
		int maxHearts = player.maxHP / 2;
		int currentHearts = Mathf.FloorToInt(player.hp / 2);
		int leftoverHearts = player.hp % 2;

		foreach(Image heart in hearts) {
			if(currentHearts > 0) {
				heart.sprite = Sprite.Create(heartFull, new Rect(0, 0, heartFull.width, heartFull.height), new Vector2(0.5f, 0.5f));
				currentHearts--;
				maxHearts--;
			}
			else if(currentHearts <= 0 && leftoverHearts == 1) {
				heart.sprite = Sprite.Create(heartHalf, new Rect(0, 0, heartHalf.width, heartHalf.height), new Vector2(0.5f, 0.5f));
				leftoverHearts = 0;
				maxHearts--;
			}
			else if(maxHearts > 0) {
				heart.sprite = Sprite.Create(heartEmpty, new Rect(0, 0, heartEmpty.width, heartEmpty.height), new Vector2(0.5f, 0.5f));
				maxHearts--;
			}
		}
	}

	public void AddHeart() {
		Image heart = Instantiate(heartToClone);

		float newX = positionOfFirstHeart + (hearts.Count * distanceBetweenHearts);
		heart.transform.position = new Vector3(newX, heart.transform.position.y, heart.transform.position.z);
		heart.transform.SetParent(parent.transform, false);

		hearts.Add(heart);

		UpdateHearts();
	}
}