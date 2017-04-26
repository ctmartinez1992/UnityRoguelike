using UnityEngine;
using System.Collections;

public class DungeonTester : MonoBehaviour {

    public int seed = System.Environment.TickCount;             //Ensures randomness.
    public DUNGEON_LEVEL_SIZE levelSize;                        //A pre-selected size for the dungeon. If custom is selected, the code will look for its size in the nRows and nCols variables.
    public int nRows;                                           //The amount of tiles in the x coordinate.
    public int nCols;                                           //The amount of tiles in the y coordinate.
    public DUNGEON_LEVEL_LAYOUT levelLayout;                    //gives a unique layout to the dungeon.
    public int roomMin;                                         //The minimum size of a room for both x and y coordinates.
    public int roomMax;                                         //The maximum size of a room for both x and y coordinates.
    public DUNGEON_LEVEL_ROOM_LAYOUT roomLayout;                //The way the rooms are layid out throughout the dungeon.
    public DUNGEON_LEVEL_CORRIDOR_LAYOUT corridorLayout;        //The way the corridors are laid out throughout the dungeon.
    public DUNGEON_LEVEL_REMOVE_DEADENDS removeDeadends;        //Option to remove deadends entirely, some of them, or none at all.                  
    public bool addStairs;                                      //If it has stairs, it means the dungeon has multiple levels.
    public int level;                                           //Level this dungeon is in. If it's the first level, the dungeon will have an entrance, which will be the spawn point of the player.

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

    public Transform boardHolder;								//A variable to store a reference to the transform of our Board object.

    public DungeonLevel GenerateDungeonLevel() {
        return new DungeonLevel(seed, levelSize, nRows, nCols, levelLayout, roomMin, roomMax, roomLayout, corridorLayout, removeDeadends, addStairs, 0, false, false);
    }

    public void BuildDungeonGOs(DungeonLevel d) {
        boardHolder = new GameObject("Board").transform;

        for(int r = 0; r <= d.nRows; r++) {
            for(int c = 0; c <= d.nCols; c++) {
                GameObject toInstantiate = stoneWallTiles[Random.Range(0, stoneWallTiles.Length)];

                if((d.cell[r][c] & DungeonLevel.STAIR_UP) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneStairsUpTile;
                }
                else if((d.cell[r][c] & DungeonLevel.STAIR_DN) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneStairsDownTile;
                }
                else if((d.cell[r][c] & DungeonLevel.ARCH) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneArchTile;
                }
                else if((d.cell[r][c] & DungeonLevel.OPEN) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneDoorOpenTile;
                }
                else if((d.cell[r][c] & DungeonLevel.CLOSED) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneDoorClosedTile;
                }
                else if((d.cell[r][c] & DungeonLevel.LOCKED) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneDoorLockedTile;
                }
                else if((d.cell[r][c] & DungeonLevel.SECRET) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneDoorSecretTile;
                }
                else if((d.cell[r][c] & DungeonLevel.ROOM) != DungeonLevel.NOTHING) {
                    toInstantiate = stoneFloorTiles[Random.Range(0, stoneFloorTiles.Length)];
                }
                else if((d.cell[r][c] & DungeonLevel.CORRIDOR) != DungeonLevel.NOTHING) {
                    toInstantiate = corridorTiles[Random.Range(0, corridorTiles.Length)];
                }
                else {
                    toInstantiate = stoneWallTiles[Random.Range(0, stoneWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(r, c, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }
}
