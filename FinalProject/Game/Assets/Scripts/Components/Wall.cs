using UnityEngine;
using System.Collections;

public class Wall : InteractibleComponent {
	
	public AudioClip hitting1;

	public int hp;
    public int maxHP;

    public override void Use() {
        Debug.Log("It's a wall, nothing you can do it.");
    }

    public override void Interact() {
        Debug.Log("It's a wall, a good place to put the check for traps, and find special buttons check.");
    }

    public override void WalkInto() {
        DamageWall(1);
    }

    //DamageWall is called when the player attacks a wall. Allow it for now, but walls will have a ton of health, only realistically destroyable by explosives.
    public void DamageWall(int dmg) {
        SoundManager.instance.PlaySingle(hitting1);
        
		hp -= dmg;

		if(hp <= 0) {
			gameObject.SetActive(false);
        }

        DungeonGameManager.instance.player.animator.SetTrigger("playerChop");
    }
}