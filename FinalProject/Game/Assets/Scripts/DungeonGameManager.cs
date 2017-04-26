using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum STAIRS_DIRECTION {
    UP = -1,
    DOWN = 1
};

public class DungeonGameManager : MonoBehaviour {

	public static DungeonGameManager instance = null;       //Static instance of DungeonGameManager which allows it to be accessed by any other script.

    public float turnDelay = 0.1f;                          //Delay between each Player turn.

    public DungeonManager dungeonManager;
    public Dungeon currentDungeon;
    public DungeonLevel currentDungeonLevel;
    public int currentDungeonLevelNumber;

    private GameObject dungeonNameImage;                    //Image to block out level as levels are being set up, background for levelText.
    private Text dungeonNameImageText;                      //Text to display current level number.
    private float dungeonStartDelay = 2f;                   //How long it takes to show the name of the dungeon and starting the dungeon.
    private float dungeonLevelStartDelay = 2f;              //How long it takes to start the next level.

    [HideInInspector] public Player player;                 //Reference to the player.
    private List<Enemy> enemies = new List<Enemy>();		//List of all Enemy units, used to issue them move commands.

	private bool enemiesMoving;								//Boolean to check if enemies are moving.
	private bool doingSetup = true;							//Boolean to check if we're setting up board, prevent Player from moving during setup.
    
    void Awake() {
		if(instance == null) {
			instance = this;
		}
	}

	void OnEnable() {
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		InitGame();
	}

	void InitGame() {
        //Only make this variable false, after showing the name, achieved with the invoke a few lines below.
		doingSetup = true;

		dungeonNameImage = GameObject.Find("DungeonNameImage");
		dungeonNameImageText = GameObject.Find("DungeonNameImageText").GetComponent<Text>();
        dungeonNameImageText.text = "Dungeon of Doom";
        dungeonNameImage.SetActive(true);

		Invoke("HideDungeonNameImage", dungeonStartDelay);

        if(player == null) {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

		enemies.Clear();

		if(dungeonManager == null) {
			dungeonManager = GetComponent<DungeonManager>();
		}

        currentDungeon = dungeonManager.GenerateRandomDungeon();
        currentDungeonLevel = currentDungeon.levels[0];
	}

	//Hides black image used between levels.
	void HideDungeonNameImage() {
		dungeonNameImage.SetActive(false);
		doingSetup = false;
	}

	void Update() {
		if(player.isPlayerTurn || enemiesMoving || doingSetup) {			
			return;
		}

		StartCoroutine(MoveEnemies());
	}
	
	//Call this to add the passed in Enemy to the List of Enemy objects.
	public void AddEnemyToList(Enemy script) {
		enemies.Add(script);
	}
    
	public void GameOver() {
		//levelText.text = "After " + level + " days, you starved.";
		//levelImage.SetActive(true);
		enabled = false;
	}
	
	//Coroutine to move enemies in sequence.
	IEnumerator MoveEnemies() {
		enemiesMoving = true;

		yield return new WaitForSeconds(turnDelay);

		if(enemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}

		for(int i = 0; i < enemies.Count; ++i) {
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		player.isPlayerTurn = true;
		enemiesMoving = false;
    }
    
    public void GoThroughStairs(STAIRS_DIRECTION dir) {
        currentDungeon.boards[currentDungeonLevelNumber].gameObject.SetActive(false);
        currentDungeonLevelNumber += (int)dir;
        currentDungeonLevel = currentDungeon.levels[currentDungeonLevelNumber];
        currentDungeon.boards[currentDungeonLevelNumber].gameObject.SetActive(true);

        Transform playerTransform = GameObject.Find("Player").transform;
        if(currentDungeonLevel.isFirstLevel || currentDungeonLevel.isLastLevel) {
            playerTransform.position = new Vector3(currentDungeonLevel.stairs[0].row, currentDungeonLevel.stairs[0].col, playerTransform.position.z);
        }
        else {
            if(dir == STAIRS_DIRECTION.DOWN) {
                playerTransform.position = new Vector3(currentDungeonLevel.stairs[1].row, currentDungeonLevel.stairs[1].col, playerTransform.position.z);
            }
            else {
                playerTransform.position = new Vector3(currentDungeonLevel.stairs[0].row, currentDungeonLevel.stairs[0].col, playerTransform.position.z);
            }
        }

        //Reset the variable everytime you go through stairs.
        player.stairsUsable = 2;
    }
}