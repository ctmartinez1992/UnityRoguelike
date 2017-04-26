using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Rewired;

//Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
public class Player : MovingObject {

	private int playerInputId = 0;
	public Rewired.Player playerInput;
	
	public float restartLevelDelay = 1f;		//Delay time in seconds to restart level.

	public int wallDamage = 1;					//How much damage a player does to a wall when chopping it.

    public int hp;
	public int maxHP;
	public int mp;
	public int maxMP;
	public int money;

    public int stairsUsable;                    //Can the stairs be used. A small time frame of 2 moves where the player can't use them.
    public bool isPlayerTurn;                   //Can the player do actions.

    public AudioClip moveSound1;				//1 of 2 Audio clips to play when player moves.
	public AudioClip moveSound2;				//2 of 2 Audio clips to play when player moves.
	public AudioClip drinkSound1;				//1 of 2 Audio clips to play when player collects a soda object.
	public AudioClip drinkSound2;				//2 of 2 Audio clips to play when player collects a soda object.
	public AudioClip gameOverSound;				//Audio clip to play when player dies.
	
	[HideInInspector] public Animator animator;	//Used to store a reference to the Player's animator component.
	private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.

    private bool paused = false;                //Is the game paused?

    protected override void Start() {
		playerInput = ReInput.players.GetPlayer(playerInputId);
		animator = GetComponent<Animator>();

		base.Start();

        RoomData room = DungeonGameManager.instance.currentDungeonLevel.rooms[Random.Range(0, DungeonGameManager.instance.currentDungeonLevel.rooms.Count)];
        int randomPlayerX = room.row + Random.Range(0, room.width) + 1;
        int randomPlayerY = room.column + Random.Range(0, room.height) + 1;
        transform.position = new Vector3(randomPlayerX, randomPlayerY, transform.position.z);
	}

	private void Update() {
        if(paused) {
            return;
        }
		if(!isPlayerTurn) {
			return;
		}
		
		int horizontal = 0;  	//Used to store the horizontal move direction.
		int vertical = 0;		//Used to store the vertical move direction.
		
		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
			float h = playerInput.GetAxis("Move Horizontal");
			float v = playerInput.GetAxis("Move Vertical");

			if(h > 0f) {
				horizontal = 1;
			}
			else if(h < 0f) {
				horizontal = -1;
			}

			if(v > 0f) {
				vertical = 1;
			}
			else if(v < 0f) {
				vertical = -1;
			}

			if(horizontal != 0) {
				vertical = 0;
			}
        #endif

        if(horizontal != 0 || vertical != 0) {
			//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it).
			//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
			AttemptMove<InteractibleComponent>(horizontal, vertical);
		}
	}

    public void Pause() {
        paused = true;
        Time.timeScale = 0;
    }

    public void Resume() {
        paused = false;
        Time.timeScale = 1;
    }

	//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
	protected override void AttemptMove<T>(int xDir, int yDir) {
		base.AttemptMove<T>(xDir, yDir);

		RaycastHit2D hit;
		
		//If Move returns true, meaning Player was able to move into an empty space.
		if(Move(xDir, yDir, out hit, true)) {
			SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);

            if(stairsUsable > 0) {
                stairsUsable--;
            }
		}

		CheckIfGameOver();

        isPlayerTurn = false;
	}

	//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
	protected override void OnCantMove<T>(T component) {
        System.Type componentType = component.GetType();

        if(componentType == typeof(Door)) {
            Door door = component as Door;
            door.WalkInto();
        }
        else if(componentType == typeof(Wall)) {
            Wall wall = component as Wall;
            wall.WalkInto();
        }

        /*
        Wall hitWall = component as Wall;
		hitWall.DamageWall(wallDamage);
		animator.SetTrigger("playerChop");
        */
	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D(Collider2D other) {
        if(stairsUsable == 0) {
            if(other.tag == "StairsDown") {
                DungeonGameManager.instance.GoThroughStairs(STAIRS_DIRECTION.DOWN);
                StopCoroutine(movementCoroutine);
            }
            else if(other.tag == "StairsUp") {
                DungeonGameManager.instance.GoThroughStairs(STAIRS_DIRECTION.UP);
                StopCoroutine(movementCoroutine);
            }
        }

        if(other.tag == "Item") {
        }
	}

	//CheckIfGameOver checks if the player is out of food points and if so, ends the game.
	private void CheckIfGameOver() {
		if(hp <= 0) {
			SoundManager.instance.PlaySingle(gameOverSound);
			SoundManager.instance.musicSource.Stop();
			DungeonGameManager.instance.GameOver();
		}
	}

	//If a certain class needs to access the player's input and can't be sure if the playerInput variable is assigned, it should first check if the variable is not null, if it is, it should call this function.
	public void GetPlayer() {
		playerInput = ReInput.players.GetPlayer(playerInputId);
	}
}