using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {
	
	public int playerDamage; 				//The amount of food points to subtract from the player when attacking.

	public AudioClip attackSound1;			//First of two audio clips to play when attacking the player.
	public AudioClip attackSound2;			//Second of two audio clips to play when attacking the player.

	private Animator animator;				//Variable of type Animator to store a reference to the enemy's Animator component.
	private Transform target;				//Transform to attempt to move toward each turn.

	private bool skipMove;					//Boolean to determine whether or not enemy should skip a turn or move this turn.

	//Start overrides the virtual Start function of the base class.
	protected override void Start() {
		//Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. This allows the GameManager to issue movement commands.
		DungeonGameManager.instance.AddEnemyToList(this);

		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").transform;

		base.Start();
	}

	//Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns. See comments in MovingObject for more on how base AttemptMove function works.
	protected override void AttemptMove<T>(int xDir, int yDir) {
		if(skipMove) {
			skipMove = false;
			return;
		}

		base.AttemptMove<T>(xDir, yDir);
		skipMove = true;
	}

	//MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
	public void MoveEnemy() {
		//Declare variables for X and Y axis move directions, these range from -1 to 1. These values allow us to choose between the cardinal directions: up, down, left and right.
		int xDir = 0;
		int yDir = 0;

		if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon) {
			yDir = target.position.y > transform.position.y ? 1 : -1;
		}
		else {
			xDir = target.position.x > transform.position.x ? 1 : -1;
		}

		AttemptMove<Player>(xDir, yDir);
	}
	
	
	//Called if Enemy attempts to move into a space occupied by a Player, T is the component we expect to encounter, in this case Player.
	protected override void OnCantMove<T>(T component) {
		Player hitPlayer = component as Player;
		animator.SetTrigger("enemyAttack");
		SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
	}
}