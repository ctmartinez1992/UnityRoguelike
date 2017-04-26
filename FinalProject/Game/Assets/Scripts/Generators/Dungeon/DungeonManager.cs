using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager instance = null;			//Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject stoneEntranceTile;
    public GameObject stoneDoorOpenTile;
    public GameObject stoneDoorClosedTile;
    public GameObject stoneDoorLockedTile;
    public GameObject stoneDoorSecretTile;
    public GameObject stoneArchTile;
    public GameObject stoneStairsUpTile;
    public GameObject stoneStairsDownTile;
    public GameObject[] stoneFloorTiles;
    public GameObject[] stoneWallTiles;
    public GameObject[] corridorTiles;

    public List<Dungeon> dungeons;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public Dungeon GenerateRandomDungeon() {
        Dungeon dungeon = new Dungeon(DUNGEON_DIFFICULTY.VETERAN);
        dungeon.GenerateDungeon();
        dungeons.Add(dungeon);

        return(dungeon);
    }
}
